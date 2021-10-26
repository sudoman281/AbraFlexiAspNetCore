using AbraFlexiAspNetCore;
using Microsoft.Extensions.Configuration;

namespace AbraFlexiAspNetCoreTests
{
    public class BaseTest
    {
        protected readonly IAbraFlexiClient Client;
        public BaseTest()
        {
            var builder = new ConfigurationBuilder().AddUserSecrets<BaseTest>();
            IConfiguration configuration = builder.Build();
            
            Client = new AbraFlexiClient(configuration["Abra:Server"], configuration["Abra:Company"],
                configuration["Abra:User"], configuration["Abra:Password"]);
        }
    }
}