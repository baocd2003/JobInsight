import os
from dotenv import load_dotenv

load_dotenv(dotenv_path=os.path.join(os.path.dirname(__file__), '..', '.env'), override=True)

# Groq (used by CV analysis service)
GROQ_API_KEY = os.environ["GROQ_API_KEY"]
GROQ_MODEL = os.getenv("GROQ_MODEL", "llama-3.3-70b-versatile")

# Gemini (used by crawler extractor — 1M tokens/day free)
GEMINI_API_KEY = os.environ["GEMINI_API_KEY"]
GEMINI_MODEL = os.getenv("GEMINI_MODEL", "gemini-2.0-flash")

# Storage mode: "api" or "db"
STORAGE_MODE = os.getenv("CRAWLER_STORAGE_MODE", "db")

# C# API (used when STORAGE_MODE=api)
CSHARP_API_URL = os.getenv("CSHARP_API_URL", "https://localhost:44312")
CSHARP_API_TOKEN = os.getenv("CSHARP_API_TOKEN", "")

# PostgreSQL (used when STORAGE_MODE=db)
DB_HOST = os.getenv("DB_HOST", "localhost")
DB_PORT = int(os.getenv("DB_PORT", "5432"))
DB_NAME = os.getenv("DB_NAME", "job_insight")
DB_USER = os.getenv("DB_USER", "postgres")
DB_PASSWORD = os.getenv("DB_PASSWORD", "12345")

# Crawl settings
CRAWL_INTERVAL_HOURS = int(os.getenv("CRAWL_INTERVAL_HOURS", "6"))
MAX_JOBS_PER_RUN = int(os.getenv("MAX_JOBS_PER_RUN", "50"))
