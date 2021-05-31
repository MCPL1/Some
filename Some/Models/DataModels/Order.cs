using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CourseProject.Attributes;
using CourseProject.Identity.Models;

namespace CourseProject.Models.DataModels
{
    [TableName("Orders")]
    public class Order : Entity
    {
        public string Address_ { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public string Status { get; set; }

        [ForeignKeyToMany("Product")] 
        public List<OrderProduct> Products { get; set; }

        public Order()
        {
            User = new User();
            Products = new List<OrderProduct>();
        }
    }
}