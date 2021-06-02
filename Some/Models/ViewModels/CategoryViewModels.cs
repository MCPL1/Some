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
}