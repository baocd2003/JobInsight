using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace JobInsight.Pages;

public class Index_Tests : JobInsightWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
