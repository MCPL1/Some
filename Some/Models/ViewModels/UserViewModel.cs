using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required] public string UserName { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string SurName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNum { get; set; }
    }

    public class UserLoginViewModel
    {
        [Required] public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}