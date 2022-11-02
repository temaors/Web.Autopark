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
        var vehicle = await _vehiclesRepository.Get(id);
        vehicle.VehicleType = await _vehiclesTypesRepository.Get(vehicle.Vehicle_Type_Id);
        return View(vehicle);
    }
    
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Upsert(int? vehicleId)
    // {
    //     Vehicle vehicle = new Vehicle();
    //     if (vehicleId == null)
    //     {
    //         return View(vehicle);
    //     }
    //     else
    //     {
    //         await repo.GetItem(vehicleId);
    //     }
    //     return RedirectToAction("Index");
    // }
    
    public async Task<IActionResult> Edit()
    {
        return View();
    }
    public async Task<IActionResult> EditConfirm()
    {
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete()
    {
        return RedirectToAction("Index");
    }
}