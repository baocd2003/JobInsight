"""
ITviec crawler — scrapes job listing pages and detail pages.
"""
from crawler.config import MAX_JOBS_PER_RUN
from crawler.models import RawJob
from crawler.services.firecrawl_service import scrape_url, crawl_site
from crawler.services.extractor_service import extract_jobs_from_markdown

SOURCE = "ITviec"
BASE_URL = "https://itviec.com"

# Entry pages to crawl (job listing pages by category)
LISTING_URLS = [
    f"{BASE_URL}/it-jobs",
    f"{BASE_URL}/it-jobs/backend",
    f"{BASE_URL}/it-jobs/frontend",
    f"{BASE_URL}/it-jobs/fullstack",
    f"{BASE_URL}/it-jobs/devops",
    f"{BASE_URL}/it-jobs/data",
    f"{BASE_URL}/it-jobs/mobile",
]


def crawl(max_jobs: int = MAX_JOBS_PER_RUN) -> list[RawJob]:
    """
    Crawl ITviec listing pages, extract jobs via Groq.
    Returns list of RawJob (deduplicated by source_url).
    """
    all_jobs: list[RawJob] = []
    seen_urls: set[str] = set()

    for listing_url in LISTING_URLS:
        if len(all_jobs) >= max_jobs:
            break

        print(f"[itviec] Scraping: {listing_url}")
        try:
            markdown = scrape_url(listing_url)
            if not markdown:
                continue

            jobs = extract_jobs_from_markdown(markdown, listing_url, SOURCE)
            print(f"[itviec] Extracted {len(jobs)} jobs from {listing_url}")

            for job in jobs:
                if job.source_url not in seen_urls and job.title and job.company_name:
                    seen_urls.add(job.source_url)
                    all_jobs.append(job)

        except Exception as e:
            print(f"[itviec] Error scraping {listing_url}: {e}")

    print(f"[itviec] Total jobs collected: {len(all_jobs)}")
    return all_jobs[:max_jobs]
