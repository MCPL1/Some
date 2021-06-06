using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CourseProject.Attributes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;

namespace CourseProject.Models.DataModels
{
    [TableName("Item")]
    public class Item : Entity
    {
        [DisplayName("Назва")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Опис")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [DisplayName("Ціна")]
        public decimal Price { get; set; }

        [ForeignKey("type_id")] 
        public ItemType Category { get; set; }
        public bool IsPromotion { get; set; }
        public int MaxQuantity { get; set; }
        public decimal PriceModifier { get; set; }
        public string ImageLink { get; set; }

        public Item()
        {
            Category = new ItemType();
        }

    }
}