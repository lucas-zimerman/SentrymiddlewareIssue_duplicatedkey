using LightHouse.IntegrationTests;
using Xunit;

namespace TestProject1
{
    [Collection("AuthWebAppFactory")]
    public class UnitTest1
    {
        private readonly HttpClient _client;
        private const string VersionApi = "2";

        public UnitTest1(AuthWebApplicationFactory factory) 
        {
            _client = factory.GetAuthClient();
        }
        [Fact]
        public async Task TestMethod1()
        {
            var httpResponse = await _client.GetAsync("weatherforecast");
            httpResponse.EnsureSuccessStatusCode();
            var test=  await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(test);
        }
    }
}