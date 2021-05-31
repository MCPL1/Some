using System.ComponentModel.DataAnnotations.Schema;
using CourseProject.Attributes;
using CourseProject.Identity.Models;

namespace CourseProject.Models.DataModels

{
    [TableName("Users")]
    public class User:Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NormalizedName { get; set; }
        public string NormalizedSurname { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }

        [ForeignKey("RoleId")] 
        public Role Role { get; set; }

        public User()
        {
            Role = new Role();
        }
    }
}