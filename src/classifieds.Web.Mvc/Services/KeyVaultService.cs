using Abp.Dependency;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace classifieds.Web.Services
{

    public class KeyVaultService: IKeyVaultService,ITransientDependency
    {
        private SecretClient _client;
        public KeyVaultService()
        {
            var options = new SecretClientOptions()
            {
                Retry =
                    {
                        Delay= TimeSpan.FromSeconds(2),
                        MaxDelay = TimeSpan.FromSeconds(16),
                        MaxRetries = 5,
                        Mode = RetryMode.Exponential
                     }
            };
            _client = new SecretClient(new Uri("https://card-decks.vault.azure.net/"), new DefaultAzureCredential(), options);
        }
        public async Task<KeyVaultSecret> GetSecretAsync(string secretKey)
        {
            var result = await _client.GetSecretAsync(secretKey);
            return result.Value;
        }
    }
    public interface IKeyVaultService
    {
       Task<KeyVaultSecret> GetSecretAsync(string secretKey);
    }
}
