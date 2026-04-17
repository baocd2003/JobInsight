using System.Threading.Tasks;
using JobInsight.CV;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace JobInsight.Controllers.CV
{
    [RemoteService]
    [Area("app")]
    [Route("api/app/cv")]
    public class CvController : AbpControllerBase
    {
        private readonly ICvAppService _cvAppService;

        public CvController(ICvAppService cvAppService)
        {
            _cvAppService = cvAppService;
        }

        /// <summary>
        /// Upload a CV file and get an AI-powered analysis against current job market.
        /// File is NOT stored — only the analysis result is persisted.
        /// </summary>
        [HttpPost("analyse")]
        [RequestSizeLimit(5 * 1024 * 1024)] // 5MB
        public async Task<CvAnalysisResultDto> AnalyseAsync(
            IFormFile file,
            [FromForm] string targetJobTitle)
        {
            return await _cvAppService.UploadAndAnalyseAsync(file, new AnalyseCvInput
            {
                TargetJobTitle = targetJobTitle
            });
        }
    }
}
