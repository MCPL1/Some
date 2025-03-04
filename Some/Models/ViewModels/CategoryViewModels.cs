﻿using System;
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

        public CategoryCreateViewModel()
        {
        }

        public CategoryCreateViewModel(IEnumerable<DataModels.Type> flatCategories)
        {
           
        }
    }
}