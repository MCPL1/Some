using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CourseProject.Identity.Models;
using CourseProject.Models.DataModels;
using CourseProject.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using CourseProject.Data.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace CourseProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Delivery> _deliveryRepository;
        private readonly IRepository<DeliveryType> _deliveryTypeRepository;
        private readonly IRepository<DeliveryProvider> _deliveryProviderRepository;

        public OrderController(UserManager<User> userManager, IRepository<Order> orderRepository,
            IRepository<Delivery> deliveryRepository, IRepository<DeliveryType> deliveryTypeRepository, IRepository<DeliveryProvider> deliveryProviderRepository)
        {
            this._userManager = userManager;
            _orderRepository = orderRepository;
            _deliveryRepository = deliveryRepository;
            _deliveryTypeRepository = deliveryTypeRepository;
            _deliveryProviderRepository = deliveryProviderRepository;
        }

        public async Task<IActionResult> Index(int id)
        {
            var orders = await _orderRepository.GetMany(order => order.User.Id, id);
            return View(orders);
        }

        [Authorize(Roles = "admin, anon, user")]
        public async Task<IActionResult> Create()
        {
            var types = await _deliveryTypeRepository.GetAll();
            var providers = await _deliveryProviderRepository.GetAll();
            return View(new OrderCreateViewModel() { DeliveryProviders = providers.ToList(),
                DeliveryTypes = types.ToList() });
        }

        [Authorize(Roles = "admin, anon, user")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Delivery model)
        {
            var order = new Order
            {
                Status = {Id = 1},
                User = {Id = int.Parse(_userManager.GetUserId(User))},
                Date = DateTime.Now
            };
            var savedData = HttpContext.Session.GetString("cart");
            CartViewModel data;
            if ((savedData != null) && (savedData.Any()))
                data = JsonSerializer.Deserialize<CartViewModel>(savedData);
            else
                data = new CartViewModel();
            if (data.Items.Count == 0) return RedirectToAction("Index", "Cart");
            foreach (var item in data.Items)
            {
                order.Products.Add(new OrderProduct(item.Quantity, item.Quantity * item.Product.Price)
                    {Id = item.Product.Id});
            }

            var delivery = new Delivery
            {
                Date = DateTime.Now,
                DeliveryProvider = {Id = 1},
                DeliveryType = {Id = 1},
                Address = model.Address,
                Parcel_number = 34
            };

            var orderId = await _orderRepository.Create(order);
            delivery.Order = new Order() {Id = orderId};
            await _deliveryRepository.Create(delivery);
            HttpContext.Session.SetString("cart", JsonSerializer.Serialize(new CartViewModel()));
            return RedirectToAction("Index","User");
        }

        [Authorize(Roles = "admin, anon, user")]
        public async Task<IActionResult> ConfirmIndex()
        {
            return View("Confirm", (await _orderRepository
                .GetMany(order => order.Status.Id, 1)).ToList());
        }

        [Authorize(Roles = "admin, anon, user")]
        public async Task<IActionResult> Confirm(int id)
        {
            var order = await _orderRepository.GetById(id);
            order.Status.Id = 2;
            await _orderRepository.Update(order, o => o.Id, id);
            return RedirectToAction("ConfirmIndex");
        }
    }
}