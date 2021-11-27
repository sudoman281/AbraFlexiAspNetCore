using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbraFlexiAspNetCore.Models.Create;
using Xunit;

namespace AbraFlexiAspNetCoreTests
{
    public class WriteTests : BaseTest
    {
        [Fact]
        public async Task TestCreateInvoice()
        {
            var invoiceItems = new List<CreateInvoiceInvoiceItem>
            {
                new(1, "KP1R")
            };
            var response = await Client.CreateInvoice(DateTime.Now.AddDays(14), "CZK",
                "Testovaci Vaclav", "KARTA", invoiceItems);
            Assert.NotNull(response.Body);
            Assert.True(response.Body.Success);
            Assert.NotNull(response.Body.Stats);
            Assert.Equal(1, response.Body.Stats.Created);
        }
        
        [Fact]
        public async Task TestCreateBankReceivedPayment()
        {
            var response = await Client.CreateBankReceivedPayment(560, "FIO", "CZK");
            Assert.NotNull(response.Body);
            Assert.True(response.Body.Success);
            Assert.NotNull(response.Body.Stats);
            Assert.Equal(1, response.Body.Stats.Created);
        }

        [Fact]
        public async Task TestCreateAndPayInvoice()
        {
            var invoiceItems = new List<CreateInvoiceInvoiceItem>
            {
                new(2, "KP1R"),
                new(3, "KP1M"),
            };
            var responses = await Client.CreateAndPayInvoice(DateTime.Now.AddDays(14),
                "CZK", "Testovaci Vaclav", "KARTA", invoiceItems, "FIO");
            Assert.NotNull(responses["Pair"].Body);
            Assert.True(responses["Pair"].Body.Success);
        }
    }
}