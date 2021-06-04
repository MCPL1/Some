using System;
using System.Collections.Generic;
using CourseProject.Models.DataModels;
using Kursach.Models;

namespace CourseProject.Models.ViewModels
{
    [Serializable]
    public class CartViewModel
    {
        public decimal Amount { get;  set; }
        public List<CartItemViewModel> Items { get; set; }
        
        public void AddItem(CartItemViewModel item)
        {
            Items.Add(item);
            Amount += item.Quantity * item.Item.Price;
        }

        public CartViewModel(List<CartItemViewModel> items)
        {
            Items = items;
        }

        public CartViewModel()
        {
            Items = new List<CartItemViewModel>();
        }
    }

    [Serializable]
    public class CartItemViewModel
    {
        public Item Item { get; set; }

        public int Quantity { get; set; }

        public CartItemViewModel(Item item)
        {
            Item = item;
            Quantity = 1;
        }
    }
}