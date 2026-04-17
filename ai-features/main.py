from fastapi import FastAPI
from routes.analyse import router as analyse_router

app = FastAPI(title="JobInsight AI Service", version="1.0.0")

app.include_router(analyse_router)


@app.get("/health")
def health():
    return {"status": "ok"}
