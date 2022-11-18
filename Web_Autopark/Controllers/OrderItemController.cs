using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web_Autopark.Controllers;

public class OrderItemController : Controller
{
    private readonly IRepository<OrderItem> orderItemsRepository;
    private readonly IRepository<Vehicle> vehiclesrepository;
    private readonly IRepository<Component> componentsRepository;
    private readonly IRepository<Order> orderRepository;

    public OrderItemController(IRepository<OrderItem> rep, IRepository<Vehicle> vehs, IRepository<Component> comps, IRepository<Order> ords)
    {
        orderItemsRepository = rep;
        vehiclesrepository = vehs;
        componentsRepository = comps;
        orderRepository = ords;
    }

    public async Task<IActionResult> Index(int id)
    {
        var order = await orderRepository.GetItem(id);
        var vehicle = await vehiclesrepository.GetItem(order.VehicleId);
        
        ViewBag.Vehicle = $"{vehicle.Model} {vehicle.RegistrationNumber}";
        
        var ordersItems = await orderItemsRepository.GetAll();
        var items = new List<OrderItem>();
        
        foreach (var orderItem in ordersItems)
        {
            if (orderItem.OrderId == id)
            {
                orderItem.Component = await componentsRepository.GetItem(orderItem.ComponentId);
                items.Add(orderItem);
            }
        }
            
        return View(items);
    }
    [HttpGet]
    public async Task<IActionResult> Create(int id)
    {
        var parts = await componentsRepository.GetAll();
        var order = await orderRepository.GetItem(id);
        var vehicle = await vehiclesrepository.GetItem(order.VehicleId);
        
        ViewBag.Vehicle = $"{vehicle.Model} {vehicle.RegistrationNumber}";
        ViewBag.PartsList = parts.Select(part => new SelectListItem(part.Name, part.ComponentId.ToString()));
        
        return View(new OrderItem {OrderId = id});
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrderItem orderItem)
    {
        await orderItemsRepository.Create(orderItem);
        return RedirectToAction("Index", "Order");
    }
}