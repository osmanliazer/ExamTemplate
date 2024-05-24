using System.ComponentModel.DataAnnotations;

namespace IndigoExam.ViewModels
{
    public class LoginVM
    {
        public bool IsRemember { get; set; }
        public string UserNameorEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
