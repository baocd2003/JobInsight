"""
ITviec crawler — extracts job URLs from listing pages, then scrapes each detail page.
"""
import re
import time

from crawler.config import MAX_JOBS_PER_RUN, CRAWL_DELAY_SECONDS
from crawler.models import RawJob
from crawler.services.firecrawl_service import scrape_url
from crawler.services.extractor_service import extract_jobs_from_markdown

SOURCE = "ITviec"
BASE_URL = "https://itviec.com"

LISTING_URLS = [
    f"{BASE_URL}/it-jobs",
    f"{BASE_URL}/it-jobs/backend-developer",
    f"{BASE_URL}/it-jobs/frontend-developer",
    f"{BASE_URL}/it-jobs/fullstack-developer",
    f"{BASE_URL}/it-jobs/devops-engineer",
    f"{BASE_URL}/it-jobs/data-engineer",
    f"{BASE_URL}/it-jobs/mobile-application-developer",
]


def _extract_job_urls(markdown: str) -> list[str]:
    """
    ITviec listing pages embed job slugs in sign-in redirect URLs.
    Pattern: /sign_in?job=<slug>&job_index=N
    Actual detail URL: /it-jobs/<slug>
    """
    slugs = re.findall(r'sign_in\?job=([^&\s"\']+)', markdown)
    seen: set[str] = set()
    urls: list[str] = []
    for slug in slugs:
        if slug not in seen:
            seen.add(slug)
            urls.append(f"{BASE_URL}/it-jobs/{slug}")
    return urls


def crawl(max_jobs: int = MAX_JOBS_PER_RUN) -> list[RawJob]:
    all_jobs: list[RawJob] = []
    seen_keys: set[str] = set()

    # Phase 1: collect all unique job detail URLs from listing pages
    job_urls: list[str] = []
    seen_job_urls: set[str] = set()

    for listing_url in LISTING_URLS:
        print(f"[itviec] Scanning listing: {listing_url}")
        try:
            markdown = scrape_url(listing_url)
            if not markdown:
                continue
            found = _extract_job_urls(markdown)
            for url in found:
                if url not in seen_job_urls:
                    seen_job_urls.add(url)
                    job_urls.append(url)
            print(f"[itviec] Found {len(found)} job URLs (total unique: {len(job_urls)})")
        except Exception as e:
            print(f"[itviec] Error scanning {listing_url}: {e}")

    print(f"[itviec] Total unique job URLs to scrape: {len(job_urls)}")

    # Phase 2: scrape each detail page and extract structured job data
    for job_url in job_urls:
        if len(all_jobs) >= max_jobs:
            break

        print(f"[itviec] Scraping detail: {job_url}")
        try:
            markdown = scrape_url(job_url)
            if not markdown:
                continue

            jobs = extract_jobs_from_markdown(markdown, job_url, SOURCE, max_chars=8000)
            for job in jobs:
                dedup_key = job.external_id or f"{job.title}|{job.company_name}"
                if dedup_key not in seen_keys and job.title and job.company_name:
                    seen_keys.add(dedup_key)
                    all_jobs.append(job)

        except Exception as e:
            print(f"[itviec] Error scraping {job_url}: {e}")

        time.sleep(CRAWL_DELAY_SECONDS)

    print(f"[itviec] Total jobs collected: {len(all_jobs)}")
    return all_jobs[:max_jobs]
