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
        [Required]
        [DisplayName("Назва")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [DisplayName("Опис")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayName("Ціна")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Тип")]
        [ForeignKey("type_id")] 
        public Type Type { get; set; }

        [Required]
        [DisplayName("Акційна послуга")]
        public bool IsPromotion { get; set; }

        [Required]
        [DisplayName("Максимальна кількість")]
        public int MaxQuantity { get; set; }

        [Required]
        [DisplayName("Збільшення ціни")]
        public decimal PriceModifier { get; set; }
        public string ImageLink { get; set; }

        public Item()
        {
            Type = new Type();
        }

    }
}