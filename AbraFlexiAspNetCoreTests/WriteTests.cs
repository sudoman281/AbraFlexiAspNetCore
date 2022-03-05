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
            var response = await Client.CreateInvoice(DateTime.Now.AddDays(14), DateTime.Now, "CZK",
                "Testovaci Vaclav", "KARTA", invoiceItems);
            Assert.NotNull(response.Body);
            Assert.True(response.Body.Success);
            Assert.NotNull(response.Body.Stats);
            Assert.Equal(1, response.Body.Stats.Created);
        }

        [Fact]
        public async Task TestCreateBankReceivedPayment()
        {
            var response = await Client.CreateBankReceivedPayment(560, "FIO", "CZK", DateTime.Now);
            Assert.NotNull(response.Body);
            Assert.True(response.Body.Success);
            Assert.NotNull(response.Body.Stats);
            Assert.Equal(1, response.Body.Stats.Created);
        }
        
        [Fact]
        public async Task TestCreateBankReceivedPaymentForeignCurrency()
        {
            var response = await Client.CreateBankReceivedPayment(28, "FIO", "EUR", DateTime.Now);
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
            var responses = await Client.CreateAndPayInvoice(DateTime.Now.AddDays(14), DateTime.Now, DateTime.Now,
                "CZK", "Testovaci Vaclav", "KARTA", invoiceItems, "FIO");
            Assert.NotNull(responses["Pair"].Body);
            Assert.True(responses["Pair"].Body.Success);
        }

        [Fact]
        public async Task TestCreateAndPayInvoice1()
        {
            var invoiceItems = new List<CreateInvoiceInvoiceItem>
            {
                new(2, "KP1R"),
                new(1, "KP1M"),
            };
            var responses = await Client.CreateAndPayInvoice(new DateTime(2021, 6, 12).AddDays(14),
                new DateTime(2021, 6, 12), new DateTime(2021, 6, 12).AddDays(1),
                "CZK", "Kobrasoft s.r.o.", "KARTA", invoiceItems, "FIO", "09840451", null, "Bratrušov",
                "Bratrušov 95", "787 01", "CZ");
            Assert.NotNull(responses["Pair"].Body);
            Assert.True(responses["Pair"].Body.Success);
        }
    }
}