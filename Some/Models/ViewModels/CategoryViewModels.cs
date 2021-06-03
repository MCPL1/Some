using System;
using System.Collections.Generic;
using System.Linq;
using CourseProject.Models.DataModels;

namespace CourseProject.Models.ViewModels
{

    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int BaseCategoryId { get; set; }

        public List<CategoryViewModel> SubCategories { get; set; }
    }

    public class CategoryTreeViewModel
    {
        public CategoryViewModel Category { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }

    public class CategoryCreateViewModel
    {
        public CategoryViewModel Category { get; set; }
        public List<CategoryViewModel> ParentCategories { get; set; }

        public CategoryCreateViewModel()
        {
        }

        public CategoryCreateViewModel(IEnumerable<Category> flatCategories)
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

            ParentCategories = categories.ToList();
        }
    }
}