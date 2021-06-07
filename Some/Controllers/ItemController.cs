using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Data;
using CourseProject.Data.Repositories;
using CourseProject.Models;
using CourseProject.Models.DataModels;
using CourseProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace CourseProject.Controllers
{
    [AllowAnonymous]
    public class ItemController : Controller
    {
        private readonly IRepository<Item> _itemRepository;
        private readonly IRepository<Models.DataModels.ItemType> _typeRepository;


        IWebHostEnvironment _appEnvironment;

        public ItemController(IRepository<Item> itemRepository, IRepository<Models.DataModels.ItemType> typeRepository, IWebHostEnvironment appEnvironment)
        {
            _itemRepository = itemRepository;
            _typeRepository = typeRepository;
            _appEnvironment = appEnvironment;
        }


        public async Task<IActionResult> Index(string sortOrder = "price_normal")
        {
            var items = await _itemRepository.GetAll();
            var categories = await _typeRepository.GetAll();
            items = sortOrder switch
            {
                "price_desc" => items.OrderByDescending(s => s.Price),
                "price_asc" => items.OrderBy(s => s.Price),
                "price_normal" => items,
                _ => items
            };

            var model = new ItemIndexViewModel(categories)
            {
                Items = items.ToList()
            };
            return View(model);
        }

        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var items = await _itemRepository.GetMany(p => p.Type.Id, categoryId);
            var categories = await _typeRepository.GetAll();
            var model = new ItemIndexViewModel(categories)
            {
                Items = items.ToList(),
            };
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> GetByPrice([FromBody] (int minPrice, int maxPrice) tuple)
        {
            var items = await _itemRepository.GetAll();
            var categories = await _typeRepository.GetAll();
            var model = new ItemIndexViewModel(categories)
            {
                Items = items.Where(p => p.Price > 50 /*&& p.Price < maxPrice*/).ToList(),
            };
            return View("Index", model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _itemRepository.GetById(id);
            return View(item);
        }

        [Authorize(Roles = Const.Admin)]
        public async Task<IActionResult> Create()
        {
            var model =
                new ItemCreateViewModel()
                {
                    Types = (await _typeRepository.GetAll()).ToList(),
                };
            return View(model);
        }

        [Authorize(Roles = Const.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create(ItemCreateViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Create");
            var item = model.Item;
            if (model.Image != null)
            {
                var path = "/Images/" + model.Image.FileName;
                await using var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create);
                await model.Image.CopyToAsync(fileStream);
                fileStream.Close();
                item.ImageLink = path;
            }


            await _itemRepository.Create(item);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = Const.Admin)]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _itemRepository.GetById(id);
            var categories = await _typeRepository.GetAll();
            var model = new ItemUpdateViewModel()
            {
                Item = item,
                Types = categories.ToList()
            };
            return View(model);
        }

        [Authorize(Roles = Const.Admin)]
        [HttpPost]
        public async Task<IActionResult> Edit1(ItemUpdateViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Create");
            if (model.Image != null)
            {
                var path = "/Images/" + model.Image.FileName;
                await using var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create);
                await model.Image.CopyToAsync(fileStream);
                fileStream.Close();
                model.Item.ImageLink = path;
            }

            await _itemRepository.Update(model.Item, x => x.Id, model.Item.Id);
            return RedirectToAction("Details", model.Item);
        }
    }
}