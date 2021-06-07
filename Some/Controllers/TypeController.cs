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
    public class TypeController : Controller
    {

        private readonly IRepository<Models.DataModels.Type> _categoryRepository;

        public TypeController(IRepository<Models.DataModels.Type> categoryRepository)
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
            var category = new Models.DataModels.Type() { Name = model.Category.Name};
            await _categoryRepository.Create(category);
            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cat = await _categoryRepository.GetById(id);
            return View(cat);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Models.DataModels.Type type)
        {
            await _categoryRepository.Update(type, cat => cat.Id, type.Id);
            return RedirectToAction("Index", "Item");
        }
    }
}
