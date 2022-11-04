using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web_Autopark.Controllers;

public class OrderController : Controller
{
    public IRepository<Order> repo;

    public OrderController(IRepository<Order> rep)
    {
        repo = rep;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await repo.GetAll();
        return View(orders);
    }
    
    public async Task<IActionResult> Create(Order order)
    {
        await repo.Create(order);
        return RedirectToAction("Index");
    }
}