using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CourseProject.Attributes;
using Microsoft.AspNetCore.Cors;

namespace CourseProject.Models.DataModels
{
    [TableName("Products")]
    public class Product : Entity
    {
        [StringLength(8, ErrorMessage = "Name length can't be more than 8.")]
        [DisplayName("Title")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        [ForeignKey("CategoryId")] public Category Category { get; set; }

        [ForeignKey("ManufacturerId")] public Manufacturer Manufacturer { get; set; }
    }
}