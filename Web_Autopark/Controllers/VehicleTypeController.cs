using System.Collections;
using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using AutoparkDAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Web_Autopark.Controllers;

public class VehicleTypeController : Controller
{
    private readonly IRepository<VehicleType> repo;

    public VehicleTypeController(IRepository<VehicleType> rep)
    {
        repo = rep;
    }

    public async Task<IActionResult> Index()
    {
        var vehicleTypes = await repo.GetAll();
        return View(vehicleTypes);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VehicleType vehicleType)
    {
        if (ModelState.IsValid)
        {
            await repo.Create(vehicleType);
            return RedirectToAction("Index");
        }
        return View(vehicleType);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var vehicleType = await repo.GetItem(id);
        return View(vehicleType);
    }

    public async Task<IActionResult> EditConfirm(VehicleType vehicleType)
    {
        await repo.Update(vehicleType);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        await repo.Delete(id);
        return RedirectToAction("Index");
    }
}