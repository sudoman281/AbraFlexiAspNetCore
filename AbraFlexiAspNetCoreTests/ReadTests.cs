using System.Threading.Tasks;
using AbraFlexiAspNetCore;
using Xunit;

namespace AbraFlexiAspNetCoreTests
{
    public class ReadTests : BaseTest
    {
        [Fact]
        public async Task TestGetPriceList()
        {
            var response = await Client.GetPriceList();
            Assert.True(response.Successful);
            Assert.NotNull(response.Data);
        }
    }
}