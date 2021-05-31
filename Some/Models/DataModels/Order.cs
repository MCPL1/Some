using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CourseProject.Attributes;
using CourseProject.Identity.Models;
using System;

namespace CourseProject.Models.DataModels
{
    [TableName("Orders")]
    public class Order : Entity
    {
        [ForeignKey("UserId")]
        public User User { get; set; }

        public string Status { get; set; }

        public DateTime Date  { get; set; }

        [ForeignKeyToMany("Product")] 
        public List<OrderProduct> Products { get; set; }

        public Delivery Delivery { get; set; }

        public Order()
        {
            User = new User();
            Products = new List<OrderProduct>();
            Delivery = new Delivery();
        }
    }
}