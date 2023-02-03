using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace IdentityDemoApp.Controllers;

[Route("[controller]")]
public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    [Route("[action]")]
    public IActionResult Index()
    {
        return View();
    }
    [Route("[action]")]
    public async Task<IActionResult> GetRecipes()
    {
        var authClient = _httpClientFactory.CreateClient();
        var discoveryDocument = await authClient.GetDiscoveryDocumentAsync("https://localhost:10001");

        var tokenResponse = await authClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = discoveryDocument.TokenEndpoint,

            ClientId = "client_id",
            ClientSecret = "client_secret",
            
            Scope = "RecipesAPI"
        });

        var recipesClient = _httpClientFactory.CreateClient();

        recipesClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await recipesClient.GetAsync($"https://localhost:6001/Home/GetString");

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Message = response.StatusCode.ToString();
            return View();
        }

        var message = await response.Content.ReadAsStringAsync();

        ViewBag.Message = message;

        return View();
    }
}