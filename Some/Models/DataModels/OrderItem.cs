using CourseProject.Attributes;

namespace CourseProject.Models.DataModels
{
    [RelatedTableName("Item")]
    [RelatedEntityType("Item")]
    [MasterEntityName("Order")]
    [TransitionTableName("Order_products")]
    public class OrderItem: Item
    {
        public int Quantity { get; set; }
        public decimal OrderPrice { get; set; }

        public OrderItem(int quantity, decimal price)
        {
            Quantity = quantity;
            OrderPrice = price;
        }

        public OrderItem()
        {
        }
    }


}