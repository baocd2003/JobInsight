"""
Storage adapter: gọi C# API để lưu jobs.
Ưu điểm: đi qua business logic của C# (validation, events, audit log).
Nhược điểm: cần token auth, chậm hơn (1 request / job).
"""
import requests
import urllib3
from crawler.config import CSHARP_API_URL, CSHARP_API_TOKEN
from crawler.models import RawJob
from crawler.storage.base import BaseStorage

# Tắt SSL warning khi dev (localhost cert tự ký)
urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)


class ApiStorage(BaseStorage):
    def __init__(self):
        self._base = CSHARP_API_URL.rstrip("/")
        self._headers = {
            "Content-Type": "application/json",
            "Authorization": f"Bearer {CSHARP_API_TOKEN}",
        }

    def save_jobs(self, jobs: list[RawJob], source_website: str) -> dict:
        inserted = skipped = errors = 0

        for job in jobs:
            try:
                result = self._upsert_job(job)
                if result == "inserted":
                    inserted += 1
                else:
                    skipped += 1
            except Exception as e:
                print(f"[api_storage] Error saving '{job.title}': {e}")
                errors += 1

        return {"inserted": inserted, "skipped": skipped, "errors": errors}

    def _upsert_job(self, job: RawJob) -> str:
        payload = {
            "title": job.title,
            "sourceWebsite": job.source_website,
            "sourceUrl": job.source_url,
            "externalId": job.external_id,
            "description": job.description,
            "requirements": job.requirements,
            "workMode": job.work_mode,
            "jobType": job.job_type,
            "experienceLevel": job.experience_level,
            "yearsOfExperienceMin": job.years_exp_min,
            "yearsOfExperienceMax": job.years_exp_max,
            "salaryMin": job.salary_min,
            "salaryMax": job.salary_max,
            "currency": job.currency,
            "salaryDisplay": job.salary_display,
            "postedDate": job.posted_date.isoformat() if job.posted_date else None,
            "expiryDate": job.expiry_date.isoformat() if job.expiry_date else None,
            "skillNames": job.skills,
            "company": {
                "name": job.company_name,
                "logoUrl": job.company_logo,
                "website": job.company_website,
                "companySize": job.company_size,
                "industry": job.company_industry,
                "sourceWebsite": job.source_website,
            },
            "locationName": job.location,
        }

        resp = requests.post(
            f"{self._base}/api/app/jobs/crawl",
            json=payload,
            headers=self._headers,
            verify=False,   # localhost self-signed cert
            timeout=15,
        )

        if resp.status_code == 409:  # Conflict = duplicate
            return "skipped"

        resp.raise_for_status()
        return "inserted"
