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

    public async Task<IActionResult> Delete(int id)
    {
        await repo.Delete(id);
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Edit(Component component)
    {
        
        return RedirectToAction("Index");
    }
}