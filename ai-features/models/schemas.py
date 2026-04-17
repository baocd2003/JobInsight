from pydantic import BaseModel, ConfigDict
from typing import List


class MarketSkill(BaseModel):
    skill_name: str
    mention_count: int
    frequency_percent: float
    is_required: bool


class AnalyseRequest(BaseModel):
    cv_text: str
    target_job_title: str
    market_skills: List[MarketSkill]


class ExtractedSkill(BaseModel):
    skill_name: str
    proficiency_level: str  # Basic / Intermediate / Advanced / Expert


class MissingSkill(BaseModel):
    skill_name: str
    priority: str  # High / Medium / Low


class AnalyseResponse(BaseModel):
    model_config = ConfigDict(protected_namespaces=())

    strengths: List[str]
    weaknesses: List[str]
    extracted_skills: List[ExtractedSkill]
    missing_skills: List[MissingSkill]
    market_match_score: float
    raw_response: str
    model_used: str
