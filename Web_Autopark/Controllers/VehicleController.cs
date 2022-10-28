using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web_Autopark.Controllers;

public class VehicleController : Controller
{
    public IRepository<Vehicle> repo;
    
    public VehicleController(IRepository<Vehicle> rep)
    {
        repo = rep;
    }

    public async Task<IActionResult> Index()
    {
        var vehicles = await repo.GetAll();
        return View(vehicles);
    }
}