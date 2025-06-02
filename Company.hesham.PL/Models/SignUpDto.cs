using System.ComponentModel.DataAnnotations;

namespace Company.hesham.PL.Models
{
    public class SignUpDto
    {
        [Required(ErrorMessage ="UserName Is Required")]
        public required string UserName { get; set; }


        [Required(ErrorMessage = "FirstName Is Required")]
        public required string FirstName { get; set; }


        [Required(ErrorMessage = "LastName Is Required")]
        public required string LastName { get; set; }


        [DataType(DataType.EmailAddress,ErrorMessage ="Email Address Is Required")]
        public required string Email { get; set; }


        [DataType(DataType.Password, ErrorMessage = "Password Address Is Required")]
        public required string Password { get; set; }


        [DataType(DataType.Password, ErrorMessage = "ConfirmPassword Address Is Required")]
        [Compare(nameof(Password))]
        public required string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
        
    }
}
