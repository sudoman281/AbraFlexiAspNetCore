using Microsoft.Extensions.DependencyInjection;

namespace AbraFlexiAspNetCore
{
    /// <summary>
    /// Root object of the API
    /// </summary>
    public static class AbraFlexi
    {
        /// <summary>
        /// Adds AbraFlexi to the dependency injection as a singleton.
        /// </summary>
        /// <param name="serviceCollection">Service collection</param>
        /// <param name="serverUrl">For example myCompany.flexibee.eu (without the last slash, without a protocol)</param>
        /// <param name="companyIdentifier">Company identifier (found in the URL after logging in to the web interface)</param>
        /// <param name="user">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public static IServiceCollection AddAbraFlexi(this IServiceCollection serviceCollection, string serverUrl,
            string companyIdentifier, string user, string password)
        {
            serviceCollection.AddSingleton<IAbraFlexiClient, AbraFlexiClient>(_ => new AbraFlexiClient(serverUrl,
                companyIdentifier, user, password));
            return serviceCollection;
        }
    }
}