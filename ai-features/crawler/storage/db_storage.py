"""
Storage adapter: kết nối thẳng PostgreSQL để insert jobs.
Ưu điểm: nhanh, bulk insert, không cần auth token.
Nhược điểm: bypass business logic C#, không trigger events/audit.
"""
import uuid
from datetime import datetime, timezone
from typing import Optional

import psycopg2
import psycopg2.extras

# Register UUID adapter so Python uuid.UUID objects are handled by psycopg2
psycopg2.extras.register_uuid()

from crawler.config import DB_HOST, DB_PORT, DB_NAME, DB_USER, DB_PASSWORD
from crawler.models import RawJob
from crawler.storage.base import BaseStorage

# ABP table prefix
_PREFIX = "App"


def _conn():
    return psycopg2.connect(
        host=DB_HOST, port=DB_PORT, dbname=DB_NAME,
        user=DB_USER, password=DB_PASSWORD,
    )


class DbStorage(BaseStorage):

    def save_jobs(self, jobs: list[RawJob], source_website: str) -> dict:
        inserted = skipped = errors = 0

        with _conn() as conn:
            # Pre-load existing skills from DB for matching
            skill_map = self._load_skill_map(conn)
            crawl_id = self._start_crawl_history(conn, source_website)

            for job in jobs:
                try:
                    result = self._upsert_job(conn, job, skill_map, crawl_id)
                    if result == "inserted":
                        inserted += 1
                    else:
                        skipped += 1
                except Exception as e:
                    print(f"[db_storage] Error saving '{job.title}': {e}")
                    conn.rollback()
                    errors += 1

            self._finish_crawl_history(conn, crawl_id, inserted, skipped, errors)
            conn.commit()

        return {"inserted": inserted, "skipped": skipped, "errors": errors}

    # ─── Job upsert ─────────────────────────────────────────────────────────────

    def _upsert_job(self, conn, job: RawJob, skill_map: dict, crawl_id) -> str:
        with conn.cursor() as cur:
            # Skip if same external_id + source already exists
            if job.external_id:
                cur.execute(
                    f'SELECT "Id" FROM "{_PREFIX}Jobs" WHERE "ExternalId"=%s AND "SourceWebsite"=%s AND "IsDeleted"=false',
                    (job.external_id, job.source_website),
                )
                if cur.fetchone():
                    return "skipped"

            now = datetime.now(timezone.utc)
            company_id = self._upsert_company(conn, job, now)
            location_id = self._upsert_location(conn, job.location, now) if job.location else None
            job_id = uuid.uuid4()

            cur.execute(f"""
                INSERT INTO "{_PREFIX}Jobs" (
                    "Id", "Title", "CompanyId", "ExternalId", "SourceWebsite", "SourceUrl",
                    "LocationId", "WorkMode", "SalaryMin", "SalaryMax", "Currency", "SalaryDisplay",
                    "IsNegotiable", "ExperienceLevel", "YearsOfExperienceMin", "YearsOfExperienceMax",
                    "JobType", "Description", "Requirements", "Benefits", "Tags",
                    "PostedDate", "ExpiryDate", "LastCrawledAt", "IsActive", "IsExpired", "ViewCount",
                    "ExtraProperties", "ConcurrencyStamp",
                    "CreationTime", "IsDeleted"
                ) VALUES (
                    %s,%s,%s,%s,%s,%s,
                    %s,%s,%s,%s,%s,%s,
                    %s,%s,%s,%s,
                    %s,%s,%s,%s,%s,
                    %s,%s,%s,%s,%s,%s,
                    %s,%s,
                    %s,%s
                )
            """, (
                job_id, job.title, company_id, job.external_id or str(job_id),
                job.source_website, job.source_url,
                location_id, job.work_mode, job.salary_min, job.salary_max,
                job.currency, job.salary_display,
                False, job.experience_level, job.years_exp_min, job.years_exp_max,
                job.job_type, job.description, job.requirements, '', '[]',
                job.posted_date or now, job.expiry_date, now, True, False, 0,
                '{}', str(uuid.uuid4()),
                now, False,
            ))

            # Insert JobSkills
            self._insert_job_skills(conn, job_id, job.skills, skill_map, now)

            conn.commit()
            return "inserted"

    # ─── Company upsert ─────────────────────────────────────────────────────────

    def _upsert_company(self, conn, job: RawJob, now: datetime):
        with conn.cursor() as cur:
            slug = _slugify(job.company_name)
            cur.execute(
                f'SELECT "Id" FROM "{_PREFIX}Companies" WHERE "NameSlug"=%s AND "IsDeleted"=false',
                (slug,),
            )
            row = cur.fetchone()
            if row:
                return row[0]

            company_id = uuid.uuid4()
            cur.execute(f"""
                INSERT INTO "{_PREFIX}Companies" (
                    "Id", "Name", "NameSlug", "ExternalId", "SourceWebsite",
                    "Industry", "CompanySize", "Website", "LogoUrl",
                    "Address", "LinkedInUrl", "FacebookUrl",
                    "TotalJobsPosted", "ActiveJobsCount",
                    "Description", "WhyJoinUs",
                    "ExtraProperties", "ConcurrencyStamp", "CreationTime",
                    "IsDeleted"
                ) VALUES (
                    %s,%s,%s,%s,%s,
                    %s,%s,%s,%s,
                    %s,%s,%s,
                    %s,%s,
                    %s,%s,
                    %s,%s,%s,
                    %s
                )
            """, (
                company_id, job.company_name, slug, '', job.source_website,
                job.company_industry or '', job.company_size or '', job.company_website or '', job.company_logo or '',
                '', '', '',
                0, 0,
                '', '',
                '{}', str(uuid.uuid4()), now,
                False,
            ))
            return company_id

    # ─── Location upsert ────────────────────────────────────────────────────────

    def _upsert_location(self, conn, location_name: str, now: datetime) -> Optional[uuid.UUID]:
        if not location_name:
            return None
        with conn.cursor() as cur:
            slug = _slugify(location_name)
            cur.execute(
                f'SELECT "Id" FROM "{_PREFIX}Locations" WHERE "Slug"=%s',
                (slug,),
            )
            row = cur.fetchone()
            if row:
                return row[0]

            loc_id = uuid.uuid4()
            cur.execute(f"""
                INSERT INTO "{_PREFIX}Locations" (
                    "Id", "City", "District", "Country", "DisplayName", "Slug",
                    "TotalJobs", "IsRemote", "IsDeleted",
                    "ExtraProperties", "ConcurrencyStamp", "CreationTime"
                ) VALUES (%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s)
            """, (
                loc_id, location_name, '', 'Vietnam', location_name, slug,
                0, False, False,
                '{}', str(uuid.uuid4()), now,
            ))
            return loc_id

    # ─── Skills ─────────────────────────────────────────────────────────────────

    def _load_skill_map(self, conn) -> dict:
        """Returns { normalized_name: skill_id } for all existing skills."""
        with conn.cursor() as cur:
            cur.execute(f'SELECT "Id","NormalizedName" FROM "{_PREFIX}Skills" WHERE "IsDeleted"=false')
            return {row[1]: row[0] for row in cur.fetchall()}

    def _insert_job_skills(self, conn, job_id, skill_names: list[str], skill_map: dict, now: datetime):
        with conn.cursor() as cur:
            for skill_name in skill_names:
                if not skill_name:
                    continue
                normalized = _normalize_skill(skill_name)
                skill_id = skill_map.get(normalized)

                if not skill_id:
                    # Auto-create unknown skill
                    skill_id = uuid.uuid4()
                    cur.execute(f"""
                        INSERT INTO "{_PREFIX}Skills" (
                            "Id", "Name", "NormalizedName", "Category", "SubCategory",
                            "Aliases", "RelatedSkills", "IconUrl", "Color",
                            "TotalJobMentions", "TrendingScore", "IsDeleted",
                            "ExtraProperties", "ConcurrencyStamp", "CreationTime"
                        ) VALUES (%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s)
                        ON CONFLICT DO NOTHING
                    """, (
                        skill_id, skill_name, normalized, 'Uncategorized', '',
                        '[]', '[]', '', '',
                        0, 0, False,
                        '{}', str(uuid.uuid4()), now,
                    ))
                    skill_map[normalized] = skill_id

                # Insert JobSkill (ignore duplicate)
                cur.execute(f"""
                    INSERT INTO "{_PREFIX}JobSkills" (
                        "Id","JobId","SkillId","IsRequired","IsPrimarySkill",
                        "ProficiencyLevel","MentionCount","ExtraProperties","CreationTime"
                    ) VALUES (%s,%s,%s,%s,%s,%s,%s,%s,%s)
                    ON CONFLICT DO NOTHING
                """, (
                    uuid.uuid4(), job_id, skill_id,
                    True, False, 'Intermediate', 1, '{}', now,
                ))

    # ─── CrawlHistory ───────────────────────────────────────────────────────────

    def _start_crawl_history(self, conn, source_website: str):
        with conn.cursor() as cur:
            crawl_id = uuid.uuid4()
            now = datetime.now(timezone.utc)
            cur.execute(f"""
                INSERT INTO "{_PREFIX}CrawlHistories" (
                    "Id", "SourceWebsite", "StartTime", "Status",
                    "JobsFound", "JobsCreated", "JobsUpdated", "JobsSkipped",
                    "ErrorCount", "ErrorDetails", "PagesProcessed",
                    "ExtraProperties", "ConcurrencyStamp", "CreationTime"
                ) VALUES (%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s)
            """, (
                crawl_id, source_website, now, 'Running',
                0, 0, 0, 0,
                0, '', 0,
                '{}', str(uuid.uuid4()), now,
            ))
            conn.commit()
            return crawl_id

    def _finish_crawl_history(self, conn, crawl_id, inserted: int, skipped: int, errors: int):
        with conn.cursor() as cur:
            now = datetime.now(timezone.utc)
            cur.execute(f"""
                UPDATE "{_PREFIX}CrawlHistories"
                SET "Status"=%s, "EndTime"=%s, "JobsFound"=%s,
                    "JobsCreated"=%s, "JobsSkipped"=%s, "ErrorCount"=%s
                WHERE "Id"=%s
            """, (
                'Completed' if errors == 0 else 'PartialSuccess',
                now, inserted + skipped + errors,
                inserted, skipped, errors, crawl_id,
            ))


# ─── Utilities ──────────────────────────────────────────────────────────────────

def _slugify(text: str) -> str:
    import re
    text = text.lower().strip()
    text = re.sub(r'[^\w\s-]', '', text)
    text = re.sub(r'[\s_-]+', '-', text)
    return text[:100]


def _normalize_skill(name: str) -> str:
    return name.upper().replace(' ', '').replace('.', '').replace('#', 'SHARP').replace('+', 'PLUS')
