using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CourseProject.Attributes;

namespace CourseProject.Models.DataModels

{
    [TableName("User")]
    public class User:Entity
    {
        [Required]
        [DisplayName("Логін")]
        public string UserName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Name  { get; set; }
        public string PasswordHash { get; set; }

        [ForeignKey("role_Id")] 
        public Role Role { get; set; }
        [Required]
        [DisplayName("номер телефону")]
        public string PhoneNumber { get; set; }

        public User()
        {
            Role = new Role();
        }
    }
}