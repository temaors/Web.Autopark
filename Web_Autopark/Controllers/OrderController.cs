using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web_Autopark.Controllers;

public class OrderController : Controller
{
    public IRepository<Order> OrdersRepository; // make it private readonly
    public IRepository<Vehicle> VehiclesRepository; // make it private readonly

    public OrderController(IRepository<Order> ordersRepository, IRepository<Vehicle> vehiclesRepository)
    {
        OrdersRepository = ordersRepository;
        VehiclesRepository = vehiclesRepository;
    }


    public async Task<ActionResult> Index()
    {
        var orders = await OrdersRepository.GetAll();
        foreach (var order in orders)
        {
            order.Vehicle = await VehiclesRepository.GetItem(order.VehicleId); //it's better to get all vehicles and then map them for orders from collection
        }
        return View(orders);
    }

    public async Task<ActionResult> Create() //rename
    {
        var vehicles = await VehiclesRepository.GetAll();
        ViewBag.VehiclesList = vehicles.Select(vehicle =>
            new SelectListItem($"{vehicle.Model} {vehicle.RegistrationNumber}", vehicle.VehicleId.ToString()));
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Order order)
    {
        await OrdersRepository.Create(order);
        return RedirectToAction("Index");
    }
}