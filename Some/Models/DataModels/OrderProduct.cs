using CourseProject.Attributes;

namespace CourseProject.Models.DataModels
{
    [RelatedTableName("Product")]
    [RelatedEntityType("Product")]
    [MasterEntityName("Order")]
    [TransitionTableName("Order_products")]
    public class OrderProduct: Product
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public OrderProduct(int quantity, decimal price)
        {
            Quantity = quantity;
            Price = price;
        }

        public OrderProduct()
        {
        }
    }


}