using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AbraFlexiAspNetCore.Models;
using AbraFlexiAspNetCore.Models.Create;
using Newtonsoft.Json;

namespace AbraFlexiAspNetCore
{
    public interface IAbraFlexiClient
    {
        public Task<AbraResponse<IList<PriceItem>>> GetPriceList();
        public Task<AbraResponse<IList<InvoiceType>>> GetInvoiceTypes();
        public Task<AbraResponse<IList<Currency>>> GetCurrencies();
        public Task<AbraResponse<IList<PaymentType>>> GetPaymentTypes();
        public Task<AbraResponse<IList<BankAccount>>> GetBankAccounts();

        public Task<AbraPostResponse> CreateInvoice(DateTime due, string currencyCode, string companyName,
            string paymentTypeCode, List<CreateInvoiceInvoiceItem> invoiceItems, string? @in = null, string? tin = null,
            string? city = null, string? street = null, string? zipCode = null, string? country = null);

        public Task<AbraPostResponse> CreateBankReceivedPayment(float price, string bankCode, string currencyCode);
        public Task<AbraPostResponse> PairReceivedPaymentToInvoice(int paymentId, int invoiceId);

        public Task<Dictionary<string, AbraPostResponse>> CreateAndPayInvoice(DateTime due, string currencyCode,
            string companyName, string paymentTypeCode, List<CreateInvoiceInvoiceItem> invoiceItems, string bankCode,
            string? @in = null, string? tin = null, string? city = null, string? street = null, string? zipCode = null,
            string? country = null);
    }

    public class AbraFlexiClient : IAbraFlexiClient
    {
        private readonly AuthenticationHeaderValue _auth;
        private readonly HttpClient _httpClient = new();

        public AbraFlexiClient(string serverUrl, string companyIdentifier, string user, string password)
        {
            CompanyIdentifier = companyIdentifier;
            ServerUrl = serverUrl;
            User = user;
            Password = password;

            _auth = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"{User}:{Password}")));
            InitHttpClient();
        }

        private string ServerUrl { get; }
        private string CompanyIdentifier { get; }
        private string User { get; }
        private string Password { get; }

        #region Helper functions

        private void InitHttpClient()
        {
            var baseUri = new Uri($"https://{ServerUrl}");
            _httpClient.BaseAddress = baseUri;
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.ConnectionClose = true;
            _httpClient.DefaultRequestHeaders.Authorization = _auth;
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string url)
        {
            var message = new HttpRequestMessage(method, $"/c/{CompanyIdentifier}/{url}");
            return message;
        }

        private async Task<AbraPostResponse> PostRequest(string url, object data)
        {
            string jsonContent = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"/c/{CompanyIdentifier}/{url}", httpContent);
            if (!response.IsSuccessStatusCode)
                return new AbraPostResponse
                {
                    Error = "Something went wrong."
                };

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AbraPostResponse>(json);
            return result;
        }

        #endregion

        #region Get

        private async Task<AbraResponse<IList<T>>> GetList<T>(string url)
        {
            var request = CreateRequest(HttpMethod.Get, url);
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) return new AbraResponse<IList<T>>(false);

            var json = await response.Content.ReadAsStringAsync();
            var list = json.ToObject<List<T>>();
            return new AbraResponse<IList<T>>(true, list);
        }

        public async Task<AbraResponse<IList<PriceItem>>> GetPriceList()
        {
            return await GetList<PriceItem>("cenik.json");
        }

        public async Task<AbraResponse<IList<InvoiceType>>> GetInvoiceTypes()
        {
            return await GetList<InvoiceType>("typ-faktury-vydane.json");
        }

        public async Task<AbraResponse<IList<Currency>>> GetCurrencies()
        {
            return await GetList<Currency>("mena.json");
        }

        public async Task<AbraResponse<IList<PaymentType>>> GetPaymentTypes()
        {
            return await GetList<PaymentType>("forma-uhrady.json");
        }

        public async Task<AbraResponse<IList<BankAccount>>> GetBankAccounts()
        {
            return await GetList<BankAccount>("bankovni-ucet.json");
        }

        #endregion

        #region Post

        public async Task<AbraPostResponse> CreateInvoice(DateTime due, string currencyCode, string companyName,
            string paymentTypeCode, List<CreateInvoiceInvoiceItem> invoiceItems, string? @in = null, string? tin = null,
            string? city = null, string? street = null, string? zipCode = null, string? country = null)
        {
            var invoiceTypes = await GetInvoiceTypes();
            var currencies = await GetCurrencies();
            var paymentTypes = await GetPaymentTypes();
            var priceList = await GetPriceList();

            var invoiceTypeId = invoiceTypes.Data!.SingleOrDefault(t => t.Code == "FAKTURA")?.Id;
            if (invoiceTypeId == null)
            {
                return new AbraPostResponse
                {
                    Error = "Invoice type FAKTURA not found."
                };
            }

            var currencyId = currencies.Data!.SingleOrDefault(c => c.Code == currencyCode)?.Id;
            if (currencyId == null)
            {
                return new AbraPostResponse
                {
                    Error = $"Currency {currencyCode} not found."
                };
            }

            var paymentTypeId = paymentTypes.Data!.SingleOrDefault(c => c.Code == paymentTypeCode)?.Id;
            if (paymentTypeId == null)
            {
                return new AbraPostResponse
                {
                    Error = $"Payment type {paymentTypeCode} not found."
                };
            }

            foreach (var invoiceItem in invoiceItems)
            {
                if (priceList.Data!.All(p => p.Code != invoiceItem.Code))
                {
                    return new AbraPostResponse
                    {
                        Error = $"Price list item {invoiceItem.Code} not found."
                    };
                }

                invoiceItem.PriceListId = priceList.Data!.Single(p => p.Code == invoiceItem.Code).Id;
            }

            var invoices = new List<CreateInvoiceInvoice>
            {
                new(due, invoiceTypeId.Value, currencyId.Value, DateTime.Now,
                    companyName, @in, tin, city, street, zipCode, country, invoiceItems, false,
                    paymentTypeId.Value)
            };
            return await PostRequest("faktura-vydana.json",
                new AbraRequest<CreateInvoice>(new CreateInvoice(invoices)));
        }

        public async Task<AbraPostResponse> CreateBankReceivedPayment(float price, string bankCode, string currencyCode)
        {
            var banks = await GetBankAccounts();
            var currencies = await GetCurrencies();
            var invoiceTypes = await GetInvoiceTypes();

            var bankId = banks.Data!.SingleOrDefault(t => t.Code == bankCode)?.Id;
            if (bankId == null)
            {
                return new AbraPostResponse
                {
                    Error = $"Bank {bankCode} not found."
                };
            }

            var currencyId = currencies.Data!.SingleOrDefault(t => t.Code == currencyCode)?.Id;
            if (currencyId == null)
            {
                return new AbraPostResponse
                {
                    Error = $"Currency {currencyCode} not found."
                };
            }

            var invoiceTypeId = invoiceTypes.Data!.SingleOrDefault(t => t.Code == "FAKTURA")?.Id;
            if (invoiceTypeId == null)
            {
                return new AbraPostResponse
                {
                    Error = "Invoice type FAKTURA not found."
                };
            }

            var payments = new List<CreateBankReceivedPaymentPayment>
            {
                new(price, bankId.Value, invoiceTypeId.Value, currencyId.Value,
                    "typPohybu.prijem", DateTime.Now, true)
            };
            return await PostRequest("faktura-vydana.json",
                new AbraRequest<CreateBankReceivedPayment>(new CreateBankReceivedPayment(payments)));
        }

        public async Task<AbraPostResponse> PairReceivedPaymentToInvoice(int paymentId, int invoiceId)
        {
            return await PostRequest("banka.json",
                new AbraRequest<CreateInvoiceReceivedPaymentPair>(
                    new CreateInvoiceReceivedPaymentPair(paymentId, invoiceId)));
        }

        public async Task<Dictionary<string, AbraPostResponse>> CreateAndPayInvoice(DateTime due, string currencyCode,
            string companyName, string paymentTypeCode, List<CreateInvoiceInvoiceItem> invoiceItems, string bankCode,
            string? @in = null, string? tin = null, string? city = null, string? street = null, string? zipCode = null,
            string? country = null)
        {
            var priceList = await GetPriceList();
            float price = 0;
            foreach (var invoiceItem in invoiceItems)
            {
                var priceListItem = priceList.Data!.SingleOrDefault(d => d.Code == invoiceItem.Code);
                if (priceListItem == null)
                {
                    return new Dictionary<string, AbraPostResponse>
                    {
                        {
                            "General", new()
                            {
                                Error = $"Item {invoiceItem.Code} not found."
                            }
                        }
                    };
                }

                price += invoiceItem.Quantity * float.Parse(priceListItem.PriceBaseWithVat);
            }

            var responses = new Dictionary<string, AbraPostResponse>();

            var invoiceResponse = await CreateInvoice(due, currencyCode, companyName, paymentTypeCode,
                invoiceItems, @in, tin, city, street, zipCode, country);
            responses.Add("Invoice", invoiceResponse);
            var paymentResponse = await CreateBankReceivedPayment(price, bankCode, currencyCode);
            responses.Add("Payment", paymentResponse);
            if (!(invoiceResponse.Body?.Success ?? false) || !(paymentResponse.Body?.Success ?? false))
                return responses;
            
            var pairResponse = await PairReceivedPaymentToInvoice(paymentResponse.Body!.Results![0].Id,
                invoiceResponse.Body!.Results![0].Id);
            responses.Add("Pair", pairResponse);

            return responses;
        }

        #endregion
    }
}