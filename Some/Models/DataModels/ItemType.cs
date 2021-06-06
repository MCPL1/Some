using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using CourseProject.Attributes;

namespace CourseProject.Models.DataModels
{
    [TableName("Type")]
    public class ItemType : Entity
    {
        [DisplayName("Категорія")] 
        public string Name { get; set; }
    }
}