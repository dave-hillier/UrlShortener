using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace UrlShortener.Tests
{
  public class UrlShortenerIntegrationTests
  {
    private readonly TestServer _server;
    private readonly HttpClient _client;

    public UrlShortenerIntegrationTests()
    {
      _server = new TestServer(new WebHostBuilder()
        .UseEnvironment("Test")
        .UseStartup<Startup>());
      _client = _server.CreateClient();
    }

    [Fact]
    public async Task ReturnHelloWorld()
    {
      var response = await _client.GetAsync("/");
      response.EnsureSuccessStatusCode();

      var responseString = await response.Content.ReadAsStringAsync();

      Assert.Equal("Hello World!", responseString);
    }

    [Fact]
    public async Task PostLongUrl()
    {
      var response = await _client.GetAsync("/");
      response.EnsureSuccessStatusCode();

      var responseString = await response.Content.ReadAsStringAsync();

      Assert.Equal("Hello World!", responseString);
    }
  }
}
