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
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Component component)
    {
        await repo.Create(component);
        return RedirectToAction("Index");
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
    
    public async Task<IActionResult> Edit(int id)
    {
        var component = await repo.GetItem(id);
        return View(component);
    }
    
    public async Task<IActionResult> EditConfirm(Component component)
    {
        await repo.Update(component);
        return RedirectToAction("Index");
    }
}