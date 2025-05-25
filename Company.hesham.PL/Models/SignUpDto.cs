using System.ComponentModel.DataAnnotations;

namespace Company.hesham.PL.Models
{
    public class SignUpDto
    {
        [Required(ErrorMessage ="UserName Is Required")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "FirstName Is Required")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "LastName Is Required")]
        public string LastName { get; set; }


        [DataType(DataType.EmailAddress,ErrorMessage ="Email Address Is Required")]
        public string Email { get; set; }


        [DataType(DataType.Password, ErrorMessage = "Password Address Is Required")]
        public string Password { get; set; }


        [DataType(DataType.Password, ErrorMessage = "ConfirmPassword Address Is Required")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
        
    }
}
