using CourseProject.Attributes;

namespace CourseProject.Models.DataModels
{
    [RelatedTableName("Product")]
    [RelatedEntityType("Product")]
    [MasterEntityName("Order")]
    [TransitionTableName("Order_products")]
    public class OrderProduct: Product
    {
        public int _Quantity { get; set; }
        public decimal _Price { get; set; }

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