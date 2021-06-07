using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required] [DisplayName("Логін")] public string UserName { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string SurName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("номер телефону")]
        public string PhoneNum { get; set; }
    }

    public class UserLoginViewModel
    {
        [Required] [DisplayName("Логін")] public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("пароль")]
        public string Password { get; set; }
    }

    public class UserBestViewModel
    {
        public int Id { get; set; }
        [Required] [DisplayName("Логін")] public string UserName { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string SurName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("номер телефону")]
        public string PhoneNumber { get; set; }

        [Required]
        public int OrderQuantity { get; set; }
    }
}