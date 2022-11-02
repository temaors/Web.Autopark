using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web_Autopark.Controllers;

public class VehicleController : Controller
{
    public IRepository<Vehicle> repo;
    public IRepository<VehicleType> types;

    public VehicleController(IRepository<Vehicle> rep, IRepository<VehicleType> type)
    {
        repo = rep;
        types = type;
    }

    public async Task<IActionResult> Index()
    {
        var vehicles = await repo.GetAll();
        foreach (var item in vehicles)
        {
            item.VehicleType = await types.GetItem(item.VehicleTypeId);
        }
        return View(vehicles);
    }

    public async Task<IActionResult> Create()
    {
        var vehicleTypes = await types.GetAll();
        ViewBag.TypeList = vehicleTypes.Select(type => new SelectListItem(type.Name,type.VehicleTypeId.ToString()));
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Vehicle vehicle)
    {
        await repo.Create(vehicle);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<ActionResult> GetInfo(int id)
    {
        var vehicle = await repo.GetItem(id);
        vehicle.VehicleType = await types.GetItem(vehicle.VehicleTypeId);
        return View(vehicle);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var vehicle = await repo.GetItem(id);
        var vehicleTypes = await types.GetAll();
        ViewBag.TypeList = vehicleTypes.Select(type => new SelectListItem(type.Name,type.VehicleTypeId.ToString()));
        return View(vehicle);
    }
    [HttpPost]
    public async Task<IActionResult> EditConfirm(Vehicle vehicle)
    {
        await repo.Update(vehicle);
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(int id)
    {
        await repo.Delete(id);
        return RedirectToAction("Index");
    }
}