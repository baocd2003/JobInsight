import json
import os
import re

from dotenv import load_dotenv
from groq import Groq

from models.schemas import AnalyseRequest, AnalyseResponse, ExtractedSkill, MissingSkill

load_dotenv()

_client = Groq(api_key=os.environ["GROQ_API_KEY"])
_model = os.getenv("GROQ_MODEL", "llama-3.3-70b-versatile")


def build_prompt(req: AnalyseRequest) -> str:
    market_summary = "\n".join(
        f"- {s.skill_name}: {s.frequency_percent}% of job postings "
        f"({'required' if s.is_required else 'preferred'}), {s.mention_count} mentions"
        for s in req.market_skills[:30]
    )

    return f"""You are a career advisor AI. Analyse the following CV against real job market data.

## CV Content
{req.cv_text[:6000]}

## Target Role
{req.target_job_title}

## Current Market Data (last 90 days, real job postings)
{market_summary}

## Instructions
1. Extract ALL skills mentioned in the CV with proficiency level (Basic/Intermediate/Advanced/Expert).
2. Compare CV skills against the market data above.
3. Identify strengths (skills the candidate has that are in high demand).
4. Identify weaknesses (skill gaps or low-proficiency areas).
5. List missing skills (skills in market data that the candidate lacks), prioritised as High/Medium/Low.
6. Calculate a market_match_score from 0-100 based on how well the CV matches market demand.

## Response Format
Respond ONLY with valid JSON, no markdown, no explanation:
{{
  "extracted_skills": [
    {{"skill_name": "C#", "proficiency_level": "Advanced"}},
    ...
  ],
  "strengths": [
    "Strong C# and .NET Core experience — appears in 90% of backend job postings",
    ...
  ],
  "weaknesses": [
    "No cloud platform experience (AWS/Azure/GCP required in 60% of postings)",
    ...
  ],
  "missing_skills": [
    {{"skill_name": "Docker", "priority": "High"}},
    {{"skill_name": "Redis", "priority": "Medium"}},
    ...
  ],
  "market_match_score": 72.5
}}"""


def analyse_cv(req: AnalyseRequest) -> AnalyseResponse:
    prompt = build_prompt(req)

    response = _client.chat.completions.create(
        model=_model,
        messages=[{"role": "user", "content": prompt}],
        max_tokens=2048,
        temperature=0.3,
    )

    raw = response.choices[0].message.content

    try:
        data = json.loads(raw)
    except json.JSONDecodeError:
        match = re.search(r'\{.*\}', raw, re.DOTALL)
        if not match:
            raise ValueError(f"AI returned non-JSON response: {raw[:200]}")
        data = json.loads(match.group())

    return AnalyseResponse(
        strengths=data.get("strengths", []),
        weaknesses=data.get("weaknesses", []),
        extracted_skills=[
            ExtractedSkill(**s) for s in data.get("extracted_skills", [])
        ],
        missing_skills=[
            MissingSkill(**s) for s in data.get("missing_skills", [])
        ],
        market_match_score=float(data.get("market_match_score", 0)),
        raw_response=raw,
        model_used=_model,
    )
