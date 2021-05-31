using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using CourseProject.Attributes;

namespace CourseProject.Models.DataModels
{
    [TableName("Categories")]
    public class Category : Entity
    {
        [DisplayName("aaaaaaa")]
        public string Name { get; set; }

        [ForeignKey("BaseId")]
        public Category BaseCategory { get; set; }
    }
}