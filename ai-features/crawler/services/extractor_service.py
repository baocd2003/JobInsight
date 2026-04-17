"""
Uses Groq (Llama) to extract structured job data from raw markdown/text
returned by Firecrawl.
"""
import json
import re
from datetime import datetime
from typing import Optional

from groq import Groq

from crawler.config import GROQ_API_KEY, GROQ_MODEL
from crawler.models import RawJob

_client = Groq(api_key=GROQ_API_KEY)


def extract_jobs_from_markdown(markdown: str, source_url: str, source_website: str) -> list[RawJob]:
    """
    Send crawled markdown to Groq and get back a list of structured job objects.
    Works for both list pages (multiple jobs) and detail pages (single job).
    """
    prompt = f"""You are a job data extraction AI. Extract all job postings from the content below.

Source: {source_website} ({source_url})

## Content
{markdown[:8000]}

## Instructions
Extract every job posting you can find. For each job return a JSON object.
If a field is not found, use null.
Skills should be an array of skill/technology names mentioned.
work_mode: "Remote", "Hybrid", or "Onsite"
experience_level: "Intern", "Junior", "Mid", "Senior", "Lead", or "Manager"
currency: "USD" or "VND"

## Response Format
Respond ONLY with a JSON array, no markdown:
[
  {{
    "title": "Backend Developer",
    "company_name": "ABC Corp",
    "external_id": "job-123",
    "description": "...",
    "requirements": "...",
    "skills": ["C#", ".NET", "Docker"],
    "location": "Ho Chi Minh City",
    "work_mode": "Hybrid",
    "job_type": "Full-time",
    "experience_level": "Mid",
    "years_exp_min": 2,
    "years_exp_max": 4,
    "salary_min": 1500,
    "salary_max": 2500,
    "currency": "USD",
    "salary_display": "$1500 - $2500",
    "posted_date": "2026-04-09",
    "company_logo": "https://...",
    "company_website": "https://...",
    "company_size": "51-200",
    "company_industry": "Technology"
  }}
]"""

    response = _client.chat.completions.create(
        model=GROQ_MODEL,
        messages=[{"role": "user", "content": prompt}],
        max_tokens=4096,
        temperature=0.1,
    )

    raw = response.choices[0].message.content

    try:
        data = json.loads(raw)
    except json.JSONDecodeError:
        match = re.search(r'\[.*\]', raw, re.DOTALL)
        if not match:
            print(f"[extractor] Could not parse JSON from Groq response: {raw[:200]}")
            return []
        data = json.loads(match.group())

    jobs = []
    for item in data:
        try:
            jobs.append(RawJob(
                title=item.get("title", ""),
                company_name=item.get("company_name", ""),
                source_url=source_url,
                source_website=source_website,
                external_id=str(item.get("external_id") or ""),
                description=item.get("description") or "",
                requirements=item.get("requirements") or "",
                skills=item.get("skills") or [],
                location=item.get("location") or "",
                work_mode=item.get("work_mode") or "",
                job_type=item.get("job_type") or "Full-time",
                experience_level=item.get("experience_level") or "",
                years_exp_min=_to_int(item.get("years_exp_min")),
                years_exp_max=_to_int(item.get("years_exp_max")),
                salary_min=_to_float(item.get("salary_min")),
                salary_max=_to_float(item.get("salary_max")),
                currency=item.get("currency") or "USD",
                salary_display=item.get("salary_display") or "",
                posted_date=_to_date(item.get("posted_date")),
                expiry_date=_to_date(item.get("expiry_date")),
                company_logo=item.get("company_logo") or "",
                company_website=item.get("company_website") or "",
                company_size=item.get("company_size") or "",
                company_industry=item.get("company_industry") or "",
            ))
        except Exception as e:
            print(f"[extractor] Skipping malformed job item: {e}")

    return jobs


def _to_int(val) -> Optional[int]:
    try:
        return int(val) if val is not None else None
    except (ValueError, TypeError):
        return None


def _to_float(val) -> Optional[float]:
    try:
        return float(val) if val is not None else None
    except (ValueError, TypeError):
        return None


def _to_date(val) -> Optional[datetime]:
    if not val:
        return None
    try:
        return datetime.strptime(str(val), "%Y-%m-%d")
    except ValueError:
        return None
