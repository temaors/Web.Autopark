using System.Collections;
using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using AutoparkDAL.Repositories;
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
        await repo.Create(vehicleType);
        return RedirectToAction("Index");
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
        // string message = "Your request is being processed.";
        // System.Text.StringBuilder sb = new System.Text.StringBuilder();
        // sb.Append("alert('");
        // sb.Append(message);
        // sb.Append("');");
        // ClientScript.RegisterOnSubmitStatement(this.GetType(), "alert", sb.ToString());
        await repo.Delete(id);
        return RedirectToAction("Index");
    }
}