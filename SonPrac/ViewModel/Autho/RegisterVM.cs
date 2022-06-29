using System.ComponentModel.DataAnnotations;

namespace SonPrac.ViewModel.Autho
{
    public class RegisterVM
    {
        [Required,MaxLength(100)]
        public string FirsName { get; set; }
        [Required,MaxLength(100)]
        public string LastName { get; set; }
        [Required, MaxLength(100)]
        public string Username { get; set; }
        [Required,DataType(DataType.EmailAddress)]
    
        public string Email { get; set; }
        [Required,DataType(DataType.Password)]

        public string Password { get; set; }
        [Required,DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
