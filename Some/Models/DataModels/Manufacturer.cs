using CourseProject.Attributes;

namespace CourseProject.Models.DataModels
{
    [TableName("Manufacturer")]
    public class Manufacturer : Entity
    {
        public string Name { get; set; }

        public string Country { get; set; }
    }
}
