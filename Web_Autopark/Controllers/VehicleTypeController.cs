using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web_Autopark.Controllers;

public class VehicleTypeController : Controller
{
    private readonly ILogger<VehicleTypeController> _logger;

    public IRepository<VehicleType> repo;
    
    public VehicleTypeController(ILogger<VehicleTypeController> logger, IRepository<VehicleType> rep)
    {
        _logger = logger;
        repo = rep;
    }

    public IActionResult Index()
    {
        return View(repo.GetAll());
    }
    
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(VehicleType vehicleType)
    {
        repo.Create(vehicleType);
        return RedirectToAction("Index");
    }
    [HttpDelete]
    public IActionResult DeleteConfirm(int id)
    {
        repo.Delete(id);
        return RedirectToAction("Index");
    }

    // public IActionResult Delete(int id)
    // {
    //     repo.GetItem(id);
    //     
    // }
}