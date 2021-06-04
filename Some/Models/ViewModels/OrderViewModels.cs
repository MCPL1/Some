using System.Collections.Generic;
using CourseProject.Models.DataModels;

namespace CourseProject.Models.ViewModels
{
    public class OrderCreateViewModel
    {
        public Order Order { get; set; }
        public List<DeliveryProvider> DeliveryProviders { get; set; }
        public List<DeliveryType> DeliveryTypes { get; set; }
    }

    public class OrderConfirmViewModel
    {
        public List<Order> Orders { get; set; }
        public List<Status> StatusList { get; set; }
    }
}