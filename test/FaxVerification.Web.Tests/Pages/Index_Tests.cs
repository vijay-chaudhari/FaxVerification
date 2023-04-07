using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace FaxVerification.Pages;

public class Index_Tests : FaxVerificationWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
