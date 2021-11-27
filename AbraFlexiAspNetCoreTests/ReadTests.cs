using System.Linq;
using System.Threading.Tasks;
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
        
        [Fact]
        public async Task TestGetInvoiceTypes()
        {
            var response = await Client.GetInvoiceTypes();
            Assert.NotNull(response.Data);
            Assert.NotNull(response.Data.FirstOrDefault(d => d.Code == "FAKTURA"));
        }
        
        [Fact]
        public async Task GetCurrencies()
        {
            var response = await Client.GetCurrencies();
            Assert.NotNull(response.Data);
            Assert.NotNull(response.Data.FirstOrDefault(d => d.Code == "CZK"));
        }
        
        [Fact]
        public async Task GetPaymentTypes()
        {
            var response = await Client.GetPaymentTypes();
            Assert.NotNull(response.Data);
            Assert.NotNull(response.Data.FirstOrDefault(d => d.Code == "KARTA"));
        }
        
        [Fact]
        public async Task GetBankAccounts()
        {
            var response = await Client.GetBankAccounts();
            Assert.NotNull(response.Data);
            Assert.NotNull(response.Data.FirstOrDefault(d => d.Code == "FIO"));
        }
    }
}