using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CourseProject.Attributes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;

namespace CourseProject.Models.DataModels
{
    [TableName("Product")]
    public class Product : Entity
    {
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [DisplayName("Price")]
        [Required]
        public decimal Price { get; set; }

        [ForeignKey("category_id")]
        [Required]

        public Category Category { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string Image { get; set; }

        [ForeignKey("manufacturer_id")]
        [Required]

        public Manufacturer Manufacturer { get; set; }

        public Product()
        {
            Manufacturer = new Manufacturer();
            Category = new Category();
        }

    }
}