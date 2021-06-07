using CourseProject.Attributes;

namespace CourseProject.Models.DataModels
{
    [RelatedTableName("Product")]
    [RelatedEntityType("Product")]
    [MasterEntityName("Order")]
    [TransitionTableName("Order_products")]
    public class OrderProduct: Product
    {
        public int Quantity_ { get; set; }
        public decimal CurrPrice { get; set; }

        public OrderProduct(int quantity, decimal price)
        {
            Quantity_ = quantity;
            CurrPrice = price;
        }

        public OrderProduct()
        {
        }
    }


}