using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using CourseProject.Attributes;

namespace CourseProject.Models.DataModels
{
    [TableName("Type")]
    public class ItemType : Entity
    {
        [DisplayName("Тип")] 
        public string Name { get; set; }
    }
}