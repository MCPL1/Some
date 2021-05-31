using System.ComponentModel;
using CourseProject.Attributes;

namespace CourseProject.Models.DataModels
{
    [TableName("Categories")]
    public class Category : Entity
    {
        [DisplayName("Category")]
        public string Name { get; set; }
    }
}