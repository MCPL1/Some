using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CourseProject.Models.DataModels;
using Microsoft.AspNetCore.Http;

namespace CourseProject.Models.ViewModels
{
    public class ItemCreateViewModel
    {
        public Item Item { get; set; }
        public IFormFile Image { get; set; }
        public List<Category> Categories { get; set; }

        public ItemCreateViewModel()
        {
            Item = new Item();
            Categories = new List<Category>();
        }
    }

    public class ItemUpdateViewModel : ItemCreateViewModel
    {
        public ItemUpdateViewModel() : base()
        {
        }
    }


    public class ItemIndexViewModel
    {
        public List<Item> Items { get; set; }
        public List<CategoryViewModel> Categories { get; }

        public ItemIndexViewModel(IEnumerable<Category> flatCategories)
        {
            var categories = (from fc in flatCategories
                select new CategoryViewModel()
                {
                    Id = fc.Id,
                    Name = fc.Name,
                    BaseCategoryId = fc.BaseCategory?.Id ?? 0
                }).ToList();

            var lookup = categories.Where(c => c.BaseCategoryId != 0).ToLookup(c => c.BaseCategoryId);


            foreach (var c in categories)
            {
                if (lookup.Contains(c.Id))
                    c.SubCategories = lookup[c.Id].ToList();
            }

            Categories = categories.Where(c => c.BaseCategoryId == 0).ToList();
        }
    }
}