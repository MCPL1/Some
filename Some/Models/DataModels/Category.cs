using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using CourseProject.Attributes;

namespace CourseProject.Models.DataModels
{
    [TableName("Category")]
    public class Category : Entity
    {
        [DisplayName("Caregory")] 
        public string Name { get; set; }

        [ForeignKey("base_Id")]
        public Category BaseCategory { get; set; }

    }
}