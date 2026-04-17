using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace JobInsight.Web.Pages;

public class IndexModel : JobInsightPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
