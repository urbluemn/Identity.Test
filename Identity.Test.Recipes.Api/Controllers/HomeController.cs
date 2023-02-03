
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Test.Recipes.Api.Controllers;

[Route("[controller]")]
public class HomeController : Controller 
{
    public IActionResult Index()
    {
        return View();
    }

    [Route("[action]")]
    [Authorize]
    public string GetString()
    {
        return "String from Recipes.Api";
    }
}