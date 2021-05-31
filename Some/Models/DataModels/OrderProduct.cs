using CourseProject.Attributes;

namespace CourseProject.Models.DataModels
{
    [RelatedTableName("Products")]
    [RelatedEntityType("Product")]
    [MasterEntityName("Order")]
    [TransitionTableName("OrderProducts")]
    public class OrderProduct: Product
    {
        public int Quantity { get; set; }
    }


}