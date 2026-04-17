"""
Wraps Firecrawl SDK v4 to scrape job pages and return clean markdown.
"""
from firecrawl import FirecrawlApp
from crawler.config import FIRECRAWL_API_KEY

_app = FirecrawlApp(api_key=FIRECRAWL_API_KEY)


def scrape_url(url: str) -> str:
    """Scrape a single URL and return markdown content."""
    result = _app.scrape(url, formats=["markdown"])
    return result.markdown or ""


def crawl_site(url: str, limit: int = 20, path_pattern: str = "") -> list[dict]:
    """
    Crawl multiple pages from a site.
    Returns list of { url, markdown } dicts.
    """
    params = {"limit": limit, "scrapeOptions": {"formats": ["markdown"]}}
    if path_pattern:
        params["includePaths"] = [path_pattern]

    result = _app.crawl_url(url, **params)

    pages = []
    for page in (result.data or []):
        md = getattr(page, "markdown", None) or ""
        src = getattr(page, "metadata", {})
        src_url = src.get("sourceURL", url) if isinstance(src, dict) else url
        if md:
            pages.append({"url": src_url, "markdown": md})

    return pages
