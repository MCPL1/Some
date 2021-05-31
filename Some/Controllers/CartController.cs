using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CourseProject.Data.Repositories;
using CourseProject.Models.DataModels;
using CourseProject.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CourseProject.Controllers
{
    public class CartController : Controller
    {
        private readonly IRepository<Product> _repository;

        public CartController(IRepository<Product> repository)
        {
            _repository = repository;
        }

        
        public ActionResult Index()
        {
            var cart = GetViewModel();
            return View(cart);
        }

        public async Task<IActionResult> AddItem(int id)
        {
            var data = GetViewModel();
            if (data != null && data.Items.Any(x => x.Product.Id == id))
                data.Items.Where(x => x.Product.Id == id).ToList().ForEach(x => x.Quantity++);
            else
                data?.AddItem(new CartItemViewModel(await _repository.GetById(id)));
            SetViewModel(data);
            return RedirectToAction("Index", "Product");
        }

        public ActionResult Delete(int position)
        {
            var data = GetViewModel();
            data?.Items.RemoveAt(position);
            SetViewModel(data);
            return RedirectToAction("Index");
        }

        private CartViewModel GetViewModel()
        {
            var savedData = HttpContext.Session.GetString("cart");
            CartViewModel data;
            if ((savedData != null) && (savedData.Any()))
                data = JsonSerializer.Deserialize<CartViewModel>(savedData);
            else
                data = new CartViewModel();
            return data;
        }

        private void SetViewModel(CartViewModel model)
        {
            var cartSerialize = JsonSerializer.Serialize(model, new JsonSerializerOptions() { WriteIndented = true });
            HttpContext.Session.SetString("cart", cartSerialize);
        }
    }
}