﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CourseProject.Attributes;
using CourseProject.Identity.Models;
using System;

namespace CourseProject.Models.DataModels
{
    [TableName("Order_")]
    public class Order : Entity
    {
        [ForeignKey("user_Id")]
        public User User { get; set; }

        public string Status { get; set; }

        public DateTime Date  { get; set; }

        [ForeignKeyToMany("Product")] 
        public List<OrderProduct> Products { get; set; }

        [ForeignKey("delivery_id")]
        public Delivery Delivery { get; set; }

        public Order()
        {
            User = new User();
            Products = new List<OrderProduct>();
            Delivery = new Delivery();
        }
    }
}