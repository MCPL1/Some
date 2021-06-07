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
        public List<ItemType> Types { get; set; }

        public ItemCreateViewModel()
        {
            Item = new Item();
            Types = new List<ItemType>();
        }
    }

    public class ItemUpdateViewModel 
    {
        public Item Item { get; set; }
        public IFormFile Image { get; set; }
        public List<ItemType> Types { get; set; }

        public ItemUpdateViewModel()
        {
            Item = new Item();
            Types = new List<ItemType>();
        }
    }


    public class ItemIndexViewModel
    {
        public List<Item> Items { get; set; }
        public List<CategoryViewModel> Categories { get; }

        public ItemIndexViewModel(IEnumerable<ItemType> flatCategories)
        {
            var categories = (from fc in flatCategories
                select new CategoryViewModel()
                {
                    Id = fc.Id,
                    Name = fc.Name
                }).ToList();
            
            Categories = categories.ToList();
        }
    }
}