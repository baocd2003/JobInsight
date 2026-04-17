from abc import ABC, abstractmethod
from crawler.models import RawJob


class BaseStorage(ABC):
    @abstractmethod
    def save_jobs(self, jobs: list[RawJob], source_website: str) -> dict:
        """
        Persist a list of jobs.
        Returns { "inserted": int, "skipped": int, "errors": int }
        """
        pass
