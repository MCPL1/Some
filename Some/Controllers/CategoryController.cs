using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Data.Repositories;
using CourseProject.Models.DataModels;
using CourseProject.Models.ViewModels;

namespace CourseProject.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IRepository<Models.DataModels.ItemType> _categoryRepository;

        public CategoryController(IRepository<Models.DataModels.ItemType> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Create()
        {
            var cats = await _categoryRepository.GetAll();
            return View(new CategoryCreateViewModel(cats){ Category = new CategoryViewModel()});
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateViewModel model)
        {
            var category = new Models.DataModels.ItemType() { Name = model.Category.Name};
            await _categoryRepository.Create(category);
            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cat = await _categoryRepository.GetById(id);
            return View(cat);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Models.DataModels.ItemType category)
        {
            await _categoryRepository.Update(category, cat => cat.Id, category.Id);
            return RedirectToAction("Index", "Product");
        }
    }
}
