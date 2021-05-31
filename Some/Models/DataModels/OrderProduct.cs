using CourseProject.Attributes;

namespace CourseProject.Models.DataModels
{
    [RelatedTableName("Product")]
    [RelatedEntityType("Product")]
    [MasterEntityName("Order_")]
    [TransitionTableName("Order_product")]
    public class OrderProduct: Product
    {
        public int Quantity { get; set; }
        public decimal CurrentPrice { get; set; }
    }


}