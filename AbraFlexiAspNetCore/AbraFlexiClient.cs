using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AbraFlexiAspNetCore.Models;

namespace AbraFlexiAspNetCore
{
    public interface IAbraFlexiClient
    {
        public Task<AbraResponse<IList<PriceItem>>> GetPriceList();
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

        private void InitHttpClient()
        {
            var baseUri = new Uri($"https://{ServerUrl}");
            _httpClient.BaseAddress = baseUri;
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.ConnectionClose = true;
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string url)
        {
            var message = new HttpRequestMessage(method, $"/c/{CompanyIdentifier}/{url}");
            message.Headers.Authorization = _auth;
            return message;
        }

        public async Task<AbraResponse<IList<PriceItem>>> GetPriceList()
        {
            var request = CreateRequest(HttpMethod.Get, "cenik.json");
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var list = json.ToObject<List<PriceItem>>();
                return new AbraResponse<IList<PriceItem>>(true, list);
            }

            return new AbraResponse<IList<PriceItem>>(false);
        }
    }
}