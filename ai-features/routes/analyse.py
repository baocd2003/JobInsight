from fastapi import APIRouter, HTTPException
from models.schemas import AnalyseRequest, AnalyseResponse
from services.claude_service import analyse_cv

router = APIRouter()


@router.post("/analyse", response_model=AnalyseResponse)
async def analyse(req: AnalyseRequest) -> AnalyseResponse:
    if not req.cv_text or len(req.cv_text.strip()) < 50:
        raise HTTPException(status_code=400, detail="CV text is too short to analyse.")

    if not req.target_job_title:
        raise HTTPException(status_code=400, detail="target_job_title is required.")

    try:
        return analyse_cv(req)
    except ValueError as e:
        raise HTTPException(status_code=422, detail=str(e))
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"AI analysis failed: {str(e)}")
