using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CourseProject.Models.DataModels;
using CourseProject.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using CourseProject.Data;
using CourseProject.Data.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace CourseProject.Controllers
{
    [Authorize(Roles = Const.All)]
    public class OrderController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Status> _statusRepository;
        private readonly IRepository<Item> _itemRepository;


        public OrderController(UserManager<User> userManager,
            IRepository<Order> orderRepository,
            IRepository<Status> statusRepository, IRepository<Item> itemRepository)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
            _statusRepository = statusRepository;
            _itemRepository = itemRepository;
        }

        public async Task<IActionResult> Index(int id)
        {
            var orders = await _orderRepository.GetMany(order => order.User.Id, id);
            return View(orders);
        }


        public async Task<IActionResult> Create()
        {
            if (GetCart().Items.Count == 0)
                return RedirectToAction("Index", "Cart");
            var model = new OrderCreateViewModel()
            {
                Order = new Order() { Date = DateTime.Now }
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    Status = {Id = 1}, //забыл, да, харе уже!
                    User = {Id = int.Parse(_userManager.GetUserId(User))},
                    CheckoutDate = DateTime.Now,
                    Date = model.Order.Date,
                    DeliveryProvider = model.Order.DeliveryProvider,
                    DeliveryType = model.Order.DeliveryType,
                    Address = model.Order.Address
                };
                var data = GetCart();
                if (data.Items.Count == 0) return RedirectToAction("Index", "Cart");
                foreach (var item in data.Items)
                {
                    order.Items.Add(new OrderItem(item.Quantity, item.Quantity * item.Item.Price)
                        {Id = item.Item.Id});
                }

                var parcelNum = new Random().Next(10000000, 99999999);
                


                var orderId = await _orderRepository.Create(order);
                RemoveCart();
                return RedirectToAction("Index", "User");
            }

            return RedirectToAction("Create");
        }

        [Authorize(Roles = Const.Admin)]
        public async Task<IActionResult> ConfirmIndex(int id = 1)
        {
            var model = new OrderConfirmViewModel()
            {
                Orders = (await _orderRepository.GetMany(order => order.Status.Id, id)).ToList(),
                StatusList = (await _statusRepository.GetAll()).ToList()
            };
            return View("Confirm", model);
        }

        [Authorize(Roles = Const.Admin)]
        public async Task<IActionResult> Confirm(int id)
        {
            var order = await _orderRepository.GetById(id);
            order.Status.Id = 2; //ага, попавсь!
            await _orderRepository.Update(order, o => o.Id, id);
            return RedirectToAction("ConfirmIndex");
        }

        [Authorize(Roles = Const.Admin)]
        public async Task<IActionResult> Reject(int id)
        {
            var order = await _orderRepository.GetById(id);
            order.Status.Id = 3; //ага, попавсь!
            await _orderRepository.Update(order, o => o.Id, id);
            return RedirectToAction("ConfirmIndex");
        }

        [Authorize(Roles = Const.Admin)]
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderRepository.GetById(id);
            for (var i = 0; i < order.Items.Count; i++)
            {
                var orderItem = await _itemRepository.GetById(order.Items[i].Id);
                order.Items[i] = new OrderItem()
                {
                    Id = orderItem.Id,
                    Name = orderItem.Name,
                    Category = orderItem.Category,
                    Description = orderItem.Description,
                    ImageLink = orderItem.ImageLink,
                    Quantity = order.Items[i].Quantity,
                    Price = order.Items[i].Price
                };
            }

            return RedirectToAction("ConfirmIndex");
        }

        public CartViewModel GetCart()
        {
            var savedData = Request.Cookies["cart"];
            CartViewModel data;
            if (savedData != null && savedData.Any())
                data = JsonSerializer.Deserialize<CartViewModel>(savedData);
            else
                data = new CartViewModel();
            return data;
        }

        public void RemoveCart()
        {
            Response.Cookies.Delete("cart");
        }
    }
}