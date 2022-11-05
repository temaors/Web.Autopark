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

    public async Task<IActionResult> Index(string sortOrder)
    {
        ViewData["ModelSortParm"] = sortOrder == "model" ? "model_desc" : "model";
        ViewData["TypeSortParm"] = sortOrder == "type" ? "type_desc" : "type";
        ViewData["MileageSortParm"] = sortOrder == "mileage" ? "mileage_desc" : "mileage";
        
        var vehicles = await repo.GetAll();
        foreach (var item in vehicles)
        {
            item.VehicleType = await types.GetItem(item.VehicleTypeId);
        }

        switch (sortOrder)
        {
            case "model":
                vehicles = vehicles.OrderBy(vehicles => vehicles.Model);
                break;
            case "type":
                vehicles = vehicles.OrderBy(vehicles => vehicles.VehicleType.Name);
                break;
            case "type_desc":
                vehicles = vehicles.OrderByDescending(vehicles => vehicles.VehicleType.Name);
                break;
            case "mileage":
                vehicles = vehicles.OrderBy(vehicles => vehicles.Mileage);
                break;
            case "mileage_desc":
                vehicles = vehicles.OrderByDescending(vehicles => vehicles.Mileage);
                break;
            case "model_desc":
                vehicles = vehicles.OrderByDescending(vehicles => vehicles.Model);
                break;
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
        if (ModelState.IsValid)
        {
            await repo.Create(vehicle);
            return RedirectToAction("Index");
        }
        var vehicleTypes = await types.GetAll();
        ViewBag.TypeList = vehicleTypes.Select(type => new SelectListItem(type.Name,type.VehicleTypeId.ToString()));

        return View(vehicle);
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