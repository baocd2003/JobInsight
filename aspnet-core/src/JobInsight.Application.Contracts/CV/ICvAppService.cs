using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Volo.Abp.Application.Services;

namespace JobInsight.CV
{
    public interface ICvAppService : IApplicationService
    {
        /// <summary>
        /// Upload CV file, extract text, analyse against job market, return result immediately.
        /// File is NOT stored — only analysis result and extracted skills are persisted.
        /// </summary>
        Task<CvAnalysisResultDto> UploadAndAnalyseAsync(IFormFile file, AnalyseCvInput input);
    }
}
