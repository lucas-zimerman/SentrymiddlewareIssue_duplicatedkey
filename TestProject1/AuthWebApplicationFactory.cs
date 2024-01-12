using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using Xunit;

namespace LightHouse.IntegrationTests
{
    public class AuthWebApplicationFactory : WebApplicationFactory<Program>
    {
        private HttpClient _client;
        private static readonly object LockClient = new();
        private static readonly Dictionary<string, string> Tokens = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.UseEnvironment("Development");
            builder.UseSentry();
            builder.UseSentry();

            builder.ConfigureServices(services =>
            {
            });
        }

        public HttpClient GetAuthClient()
        {
            lock (LockClient)
            {
                if (_client == null)
                {
                    _client = CreateClient();
                }
                _client.DefaultRequestHeaders.Clear();


                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "3");
                _client.DefaultRequestHeaders.Add("EmpresaId", "1");
                _client.DefaultRequestHeaders.Add("UnidadeId", "2");
                _client.DefaultRequestHeaders.Add("X-Transaction-Id", "UnitTestTransaction");
            }
            return _client;
        }
    }

    [CollectionDefinition("AuthWebAppFactory")]
    public class AuthWebApplicationFactoryCollection : ICollectionFixture<AuthWebApplicationFactory>
    {
    }

}
