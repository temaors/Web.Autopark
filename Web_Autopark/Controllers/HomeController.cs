using System.Diagnostics;
using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web_Autopark.Models;

namespace Web_Autopark.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public IRepository<Vehicle> repo;
    public HomeController(ILogger<HomeController> logger, IRepository<Vehicle> rep)
    {
        _logger = logger;
        repo = rep;
    }

    public IActionResult Index()
    {
        return View(repo.GetAll());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}