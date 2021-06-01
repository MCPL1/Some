using CourseProject.Models.DataModels;

namespace CourseProject.Models.ViewModels
{
    public class OrderCreateViewModel
    {
        public Order Order { get; set; }

        public Delivery Delivery { get; set; }
    }
}