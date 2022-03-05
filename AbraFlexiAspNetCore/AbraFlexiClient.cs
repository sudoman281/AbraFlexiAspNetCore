using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// <summary>
    /// Abra request client
    /// </summary>
    public interface IAbraFlexiClient
    {
        /// <summary>
        /// Gets the price list items
        /// </summary>
        /// <returns>List of <see cref="PriceItem"/></returns>
        public Task<AbraResponse<IList<PriceItem>>> GetPriceList();

        /// <summary>
        /// Gets the invoice types
        /// </summary>
        /// <returns>List of <see cref="InvoiceType"/></returns>
        public Task<AbraResponse<IList<InvoiceType>>> GetInvoiceTypes();

        /// <summary>
        /// Gets the currencies
        /// </summary>
        /// <returns>List of <see cref="Currency"/></returns>
        public Task<AbraResponse<IList<Currency>>> GetCurrencies();

        /// <summary>
        /// Gets the countries
        /// </summary>
        /// <returns>List of <see cref="Country"/></returns>
        public Task<AbraResponse<IList<Country>>> GetCountries();

        /// <summary>
        /// Gets the payment types
        /// </summary>
        /// <returns>List of <see cref="PaymentType"/></returns>
        public Task<AbraResponse<IList<PaymentType>>> GetPaymentTypes();

        /// <summary>
        /// Gets the bank accounts
        /// </summary>
        /// <returns>List of <see cref="BankAccount"/></returns>
        public Task<AbraResponse<IList<BankAccount>>> GetBankAccounts();
        /// <summary>
        /// Gets the invoice pdf
        /// </summary>
        /// <param name="invoiceId">Id of the invoice</param>
        /// <returns></returns>
        public Task<byte[]?> GetInvoicePdf(int invoiceId);

        /// <summary>
        /// Creates an invoice
        /// </summary>
        /// <param name="due">Due date</param>
        /// <param name="dateIssued">Date issued</param>
        /// <param name="currencyCode">Abra currency code <seealso cref="GetCurrencies"/></param>
        /// <param name="companyName">Company name</param>
        /// <param name="paymentTypeCode">Abra payment type code <seealso cref="GetPaymentTypes"/></param>
        /// <param name="invoiceItems">Price list items of the invoice</param>
        /// <param name="in">IN (IČ)</param>
        /// <param name="tin">TIN (DIČ)</param>
        /// <param name="city">Company city</param>
        /// <param name="street">Company street</param>
        /// <param name="zipCode">Company ZIP code</param>
        /// <param name="countryCode">Company country code</param>
        /// <returns></returns>
        public Task<AbraPostResponse> CreateInvoice(DateTime due, DateTime dateIssued, string currencyCode, string companyName,
            string paymentTypeCode, List<CreateInvoiceInvoiceItem> invoiceItems, string? @in = null, string? tin = null,
            string? city = null, string? street = null, string? zipCode = null, string? countryCode = null);

        /// <summary>
        /// Creates a bank income
        /// </summary>
        /// <param name="price">Amount of money received</param>
        /// <param name="dateReceived">Date the payment was received</param>
        /// <param name="bankCode">Abra bank account code <seealso cref="GetBankAccounts"/></param>
        /// <param name="currencyCode">Abra currency code <seealso cref="GetCurrencies"/></param>
        /// <returns></returns>
        public Task<AbraPostResponse> CreateBankReceivedPayment(float price, string bankCode, string currencyCode, DateTime dateReceived);

        /// <summary>
        /// Pairs an income with an invoice 
        /// </summary>
        /// <param name="paymentId">Payment id</param>
        /// <param name="invoiceId">Invoice id</param>
        /// <returns></returns>
        public Task<AbraPostResponse> PairReceivedPaymentToInvoice(int paymentId, int invoiceId);

        /// <summary>
        /// Creates an invoice, payment and pairs them
        /// </summary>
        /// <param name="due">Due date</param>
        /// <param name="dateIssued">Date issued</param>
        /// <param name="datePaid">Date paid</param>
        /// <param name="currencyCode">Abra currency code <seealso cref="GetCurrencies"/></param>
        /// <param name="companyName">Company name</param>
        /// <param name="paymentTypeCode">Abra payment type code <seealso cref="GetPaymentTypes"/></param>
        /// <param name="invoiceItems">Price list items of the invoice</param>
        /// <param name="bankCode">Abra bank account code of the bank which received the payment <seealso cref="GetBankAccounts"/></param>
        /// <param name="in">IN (IČ)</param>
        /// <param name="tin">TIN (DIČ)</param>
        /// <param name="city">Company city</param>
        /// <param name="street">Company street</param>
        /// <param name="zipCode">Company ZIP code</param>
        /// <param name="country">Company country</param>
        /// <returns></returns>
        public Task<Dictionary<string, AbraPostResponse>> CreateAndPayInvoice(DateTime due, DateTime dateIssued, DateTime datePaid, string currencyCode,
            string companyName, string paymentTypeCode, List<CreateInvoiceInvoiceItem> invoiceItems, string bankCode,
            string? @in = null, string? tin = null, string? city = null, string? street = null, string? zipCode = null,
            string? country = null);
    }


    /// <inheritdoc />
    public class AbraFlexiClient : IAbraFlexiClient
    {
        private readonly AuthenticationHeaderValue _auth;
        private readonly HttpClient _httpClient = new();

        /// <summary>
        /// Abra request client
        /// </summary>
        /// <param name="serverUrl">For example myCompany.flexibee.eu (without the last slash, without a protocol)</param>
        /// <param name="companyIdentifier">Company identifier (found in the URL after logging in to the web interface)</param>
        /// <param name="user">Username</param>
        /// <param name="password">Password</param>
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

        /// <inheritdoc />
        public async Task<AbraResponse<IList<PriceItem>>> GetPriceList()
        {
            return await GetList<PriceItem>("cenik.json");
        }

        /// <inheritdoc />
        public async Task<AbraResponse<IList<InvoiceType>>> GetInvoiceTypes()
        {
            return await GetList<InvoiceType>("typ-faktury-vydane.json");
        }

        /// <inheritdoc />
        public async Task<AbraResponse<IList<Currency>>> GetCurrencies()
        {
            return await GetList<Currency>("mena.json?limit=100");
        }
        
        /// <inheritdoc />
        public async Task<AbraResponse<IList<Country>>> GetCountries()
        {
            return await GetList<Country>("stat.json?limit=100");
        }

        /// <inheritdoc />
        public async Task<AbraResponse<IList<PaymentType>>> GetPaymentTypes()
        {
            return await GetList<PaymentType>("forma-uhrady.json");
        }

        /// <inheritdoc />
        public async Task<AbraResponse<IList<BankAccount>>> GetBankAccounts()
        {
            return await GetList<BankAccount>("bankovni-ucet.json");
        }
        
        /// <inheritdoc />
        public async Task<byte[]?> GetInvoicePdf(int invoiceId)
        {
            var request = CreateRequest(HttpMethod.Get, $"faktura-vydana/{invoiceId}.pdf?report-name=fakturaKB$$SUM");
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadAsByteArrayAsync();
        }

        #endregion

        #region Post

        /// <inheritdoc />
        public async Task<AbraPostResponse> CreateInvoice(DateTime due, DateTime dateIssued, string currencyCode, string companyName,
            string paymentTypeCode, List<CreateInvoiceInvoiceItem> invoiceItems, string? @in = null, string? tin = null,
            string? city = null, string? street = null, string? zipCode = null, string? countryCode = null)
        {
            var invoiceTypes = await GetInvoiceTypes();
            var currencies = await GetCurrencies();
            var paymentTypes = await GetPaymentTypes();
            var priceList = await GetPriceList();
            var countryList = await GetCountries();

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

            int? countryId = null;
            if (countryCode != null)
            {
                countryId = countryList.Data!.SingleOrDefault(c => c.Code == countryCode)?.Id;
                if (countryId == null)
                {
                    return new AbraPostResponse
                    {
                        Error = $"Country code {countryCode} not found."
                    };
                }
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
                new(due, invoiceTypeId.Value, currencyId.Value, dateIssued,
                    companyName, @in, tin, city, street, zipCode, countryId, invoiceItems, false,
                    paymentTypeId.Value)
            };
            return await PostRequest("faktura-vydana.json",
                new AbraRequest<CreateInvoice>(new CreateInvoice(invoices)));
        }

        /// <inheritdoc />
        public async Task<AbraPostResponse> CreateBankReceivedPayment(float price, string bankCode, string currencyCode, DateTime dateReceived)
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
                    "typPohybu.prijem", dateReceived, true, currencyCode)
            };
            return await PostRequest("faktura-vydana.json",
                new AbraRequest<CreateBankReceivedPayment>(new CreateBankReceivedPayment(payments)));
        }

        /// <inheritdoc />
        public async Task<AbraPostResponse> PairReceivedPaymentToInvoice(int paymentId, int invoiceId)
        {
            return await PostRequest("banka.json",
                new AbraRequest<CreateInvoiceReceivedPaymentPair>(
                    new CreateInvoiceReceivedPaymentPair(paymentId, invoiceId)));
        }

        /// <inheritdoc />
        public async Task<Dictionary<string, AbraPostResponse>> CreateAndPayInvoice(DateTime due, DateTime dateIssued, DateTime datePaid, string currencyCode,
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

                price += invoiceItem.Quantity * float.Parse(priceListItem.PriceBaseWithVat, CultureInfo.InvariantCulture);
            }

            var responses = new Dictionary<string, AbraPostResponse>();

            var invoiceResponse = await CreateInvoice(due, dateIssued, currencyCode, companyName, paymentTypeCode,
                invoiceItems, @in, tin, city, street, zipCode, country);
            responses.Add("Invoice", invoiceResponse);
            var paymentResponse = await CreateBankReceivedPayment(price, bankCode, currencyCode, datePaid);
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