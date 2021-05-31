using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using CourseProject.Models.DataModels;

namespace CourseProject.Models.ViewModels
{
    public class ProductCreateViewModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }

        public ProductCreateViewModel()
        {
            Product = new Product();
            Categories = new List<Category>();
        }
    }

    public class ProductUpdateViewModel : ProductCreateViewModel
    {
        public ProductUpdateViewModel():base()
        {
        }
    }
}