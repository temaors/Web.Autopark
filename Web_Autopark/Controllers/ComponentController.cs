using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web_Autopark.Controllers;

public class ComponentController : Controller
{
    public IRepository<Component> repo;

    public ComponentController(IRepository<Component> rep)
    {
        repo = rep;
    }
    public async Task<IActionResult> Index()
    {
        var components = await repo.GetAll();
        return View(components);
    }
}