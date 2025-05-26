using System.ComponentModel.DataAnnotations;

namespace Company.hesham.PL.Models
{
    public class SignInDto
    {
        [DataType(DataType.EmailAddress, ErrorMessage = "Email Address Is Required")]
        public required string Email { get; set; }


        [DataType(DataType.Password, ErrorMessage = "Password Address Is Required")]
        public required string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
