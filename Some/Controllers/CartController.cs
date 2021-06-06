using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CourseProject.Data.Repositories;
using CourseProject.Models.DataModels;
using CourseProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CourseProject.Controllers
{
    [AllowAnonymous]
    public class CartController : Controller
    {
        private readonly IRepository<Item> _repository;

        public CartController(IRepository<Item> repository)
        {
            _repository = repository;
        }


        public ActionResult Index()
        {
            var cart = GetModel();
            return View(cart);
        }

        public async Task<IActionResult> AddItem(int id)
        {
            var data = GetModel();
            if (data.Items.Any(x => x.Item.Id == id))
            {
                data.Items.First(x => x.Item.Id == id).Quantity++;
                data.Amount += data.Items.First(x => x.Item.Id == id).Item.Price;
            }
            else
                data?.AddItem(new CartItemViewModel(await _repository.GetById(id)));

            SetModel(data, 30);
            return RedirectToAction("Index", "Item");
        }

        public ActionResult Delete(int position)
        {
            var data = GetModel();
            data.Amount -= data.Items[position].Item.Price;
            if (data.Items[position].Quantity > 1)
                data.Items[position].Quantity--;
            else
                data.Items.RemoveAt(position);
            
            SetModel(data, 30);
            return RedirectToAction("Index");
        }

        public ActionResult Add(int position)
        {
            var data = GetModel();
            data.Amount += data.Items[position].Item.Price;
            data.Items[position].Quantity++;
            SetModel(data, 30);
            return RedirectToAction("Index");
        }


        public CartViewModel GetModel()
        {
            var savedData = Request.Cookies["cart"];
            CartViewModel data;
            if (savedData != null && savedData.Any())
                data = JsonSerializer.Deserialize<CartViewModel>(savedData);
            else
                data = new CartViewModel();
            return data;
        }

        public void SetModel(CartViewModel value, int? expireTime)
        {
            var data = JsonSerializer.Serialize(value);
            var option = new CookieOptions
            {
                Expires = expireTime.HasValue
                    ? DateTime.Now.AddMinutes(expireTime.Value)
                    : DateTime.Now.AddMilliseconds(10)
            };
            Response.Cookies.Append("cart", data, option);
        }
    }
}