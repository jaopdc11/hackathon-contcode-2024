using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hackacont2024.Models;

namespace Hackacont2024.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Support()
    {
        return View();
    }
    public IActionResult Register()
    {
        return View();
    }
    public IActionResult Entrada()
    {
        return View();
    }
    public IActionResult LostPassword()
    {
        return View();
    }
    public IActionResult DashBoard()
    {
        return View();
    }
    public IActionResult Veiculos()
    {
        return View();
    }
    public IActionResult Containers()
    {
        return View();
    }
    public IActionResult Loggin()
    {
        return View();
    }
    public IActionResult Painel()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
