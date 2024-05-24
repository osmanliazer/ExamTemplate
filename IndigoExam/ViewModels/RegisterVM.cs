using System.ComponentModel.DataAnnotations;

namespace IndigoExam.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
