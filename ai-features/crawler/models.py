from dataclasses import dataclass, field
from typing import Optional
from datetime import datetime


@dataclass
class RawJob:
    """Raw job data extracted by Groq from crawled content."""
    title: str
    company_name: str
    source_url: str
    source_website: str
    external_id: str = ""
    description: str = ""
    requirements: str = ""
    skills: list[str] = field(default_factory=list)       # raw skill names
    location: str = ""
    work_mode: str = ""                                    # Remote / Hybrid / Onsite
    job_type: str = "Full-time"
    experience_level: str = ""                             # Junior / Mid / Senior
    years_exp_min: Optional[int] = None
    years_exp_max: Optional[int] = None
    salary_min: Optional[float] = None
    salary_max: Optional[float] = None
    currency: str = "USD"
    salary_display: str = ""
    posted_date: Optional[datetime] = None
    expiry_date: Optional[datetime] = None
    # Company extras
    company_logo: str = ""
    company_website: str = ""
    company_size: str = ""
    company_industry: str = ""
