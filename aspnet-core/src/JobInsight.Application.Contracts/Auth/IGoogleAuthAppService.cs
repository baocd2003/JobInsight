using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace JobInsight.Auth;

public interface IGoogleAuthAppService : IApplicationService
{
    Task<GoogleLoginResultDto> LoginAsync(GoogleLoginInput input);
}
