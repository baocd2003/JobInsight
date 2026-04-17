"""
Crawler entry point.

Run once:
    python -m crawler.main

Run on schedule (every N hours):
    python -m crawler.main --schedule
"""
import argparse
import time

from crawler.config import CRAWL_INTERVAL_HOURS, MAX_JOBS_PER_RUN, STORAGE_MODE
from crawler.sites import itviec
from crawler.storage.api_storage import ApiStorage
from crawler.storage.db_storage import DbStorage


def get_storage():
    if STORAGE_MODE == "api":
        print("[crawler] Storage mode: C# API")
        return ApiStorage()
    else:
        print("[crawler] Storage mode: Direct DB (PostgreSQL)")
        return DbStorage()


def run_once():
    storage = get_storage()
    total_inserted = total_skipped = total_errors = 0

    # ── ITviec ──────────────────────────────────────────────────────────────────
    print("\n[crawler] ── ITviec ─────────────────────────────────────────")
    try:
        jobs = itviec.crawl(max_jobs=MAX_JOBS_PER_RUN)
        if jobs:
            result = storage.save_jobs(jobs, itviec.SOURCE)
            total_inserted += result["inserted"]
            total_skipped += result["skipped"]
            total_errors += result["errors"]
            print(f"[crawler] ITviec → inserted:{result['inserted']} skipped:{result['skipped']} errors:{result['errors']}")
    except Exception as e:
        print(f"[crawler] ITviec failed: {e}")
        total_errors += 1

    # ── Add more sites here ──────────────────────────────────────────────────
    # from crawler.sites import topcv
    # jobs = topcv.crawl(max_jobs=MAX_JOBS_PER_RUN)
    # ...

    print(f"\n[crawler] Done — inserted:{total_inserted} skipped:{total_skipped} errors:{total_errors}")
    return total_inserted, total_skipped, total_errors


def run_scheduled():
    print(f"[crawler] Scheduled mode — running every {CRAWL_INTERVAL_HOURS}h")
    while True:
        print(f"\n[crawler] ── Starting crawl run ──────────────────────────────")
        try:
            run_once()
        except Exception as e:
            print(f"[crawler] Unexpected error in run: {e}")

        wait_seconds = CRAWL_INTERVAL_HOURS * 3600
        print(f"[crawler] Next run in {CRAWL_INTERVAL_HOURS}h — sleeping...")
        time.sleep(wait_seconds)


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("--schedule", action="store_true", help="Run on a recurring schedule")
    args = parser.parse_args()

    if args.schedule:
        run_scheduled()
    else:
        run_once()
