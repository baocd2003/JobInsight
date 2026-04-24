"""
Web scraping service using Jina Reader (replaces Firecrawl).
No API key required. Just a GET request to r.jina.ai/{url}.
"""
import requests

_JINA_BASE = "https://r.jina.ai"
_HEADERS = {
    "Accept": "text/markdown",
    "X-Return-Format": "markdown",
}
_TIMEOUT = 30


def scrape_url(url: str) -> str:
    """Scrape a single URL and return clean markdown."""
    resp = requests.get(f"{_JINA_BASE}/{url}", headers=_HEADERS, timeout=_TIMEOUT)
    resp.raise_for_status()
    return resp.text or ""


def crawl_site(url: str, limit: int = 20, path_pattern: str = "") -> list[dict]:  # noqa: ARG001
    """
    Scrape the root URL only (Jina is single-page).
    Returns list of { url, markdown } dicts.
    """
    md = scrape_url(url)
    return [{"url": url, "markdown": md}] if md else []
