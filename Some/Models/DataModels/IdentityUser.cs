using System.ComponentModel.DataAnnotations.Schema;
using CourseProject.Attributes;
using CourseProject.Identity.Models;

namespace CourseProject.Models.DataModels

{
    [TableName("Users")]
    public class User:Entity
    {
        
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string NormalizedEmail { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }

        [ForeignKey("RoleId")] 
        public Role Role { get; set; }

        public User()
        {
            Role = new Role();
        }
    }
}