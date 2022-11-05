using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web_Autopark.Controllers;

public class OrderItemController : Controller
{
    public IRepository<OrderItem> orderItemsRepository;
    public IRepository<Vehicle> vehiclesrepository;
    public IRepository<Component> componentsRepository;
    public IRepository<Order> orderRepository;

    public OrderItemController(IRepository<OrderItem> rep, IRepository<Vehicle> vehs, IRepository<Component> comps, IRepository<Order> ords)
    {
        orderItemsRepository = rep;
        vehiclesrepository = vehs;
        componentsRepository = comps;
        orderRepository = ords;
    }

    public async Task<IActionResult> Index()
    {
        var orderItems = await orderItemsRepository.GetAll();
        return View(orderItems);
    }
    [HttpGet]
    public async Task<IActionResult> Create(int id)
    {
        var components = await componentsRepository.GetAll();
        var order = await orderRepository.GetItem(id);
        var vehicle = await vehiclesrepository.GetItem(order.VehicleId);
        ViewBag.Vehicle = $"{vehicle.Model} {vehicle.RegistrationNumber}";
        ViewBag.PartsList = components.Select(component => new SelectListItem(component.Name, component.ComponentId.ToString()));
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(OrderItem orderItem)
    {
        await orderItemsRepository.Create(orderItem);
        return RedirectToAction("Index", "Order");
    }
}