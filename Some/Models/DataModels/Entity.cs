using CourseProject.Attributes;

namespace CourseProject.Models.DataModels
{
    public abstract class Entity
    {
        [ReadOnlyProperty] 
        public int Id { get; set; }

        protected Entity()
        {
        }
    }
}