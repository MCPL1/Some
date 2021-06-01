using CourseProject.Models.DataModels;
using System.Collections.Generic;

namespace CourseProject.Models.ViewModels
{
    public class OrderCreateViewModel
    {
        public Delivery Delivery { get; set; }

        public List<DeliveryType> DeliveryTypes { get; set; }

        public List<DeliveryProvider> DeliveryProviders { get; set; }
    }

}