

using System.ComponentModel.DataAnnotations;

namespace Company.hesham.PL.Models
{
    public class ResetPasswordDto
    {

        [DataType(DataType.Password, ErrorMessage = "Password Address Is Required")]
        public required string NewPassword { get; set; }


        [DataType(DataType.Password, ErrorMessage = "ConfirmPassword Address Is Required")]
        [Compare(nameof(NewPassword))]
        public required string ConfirmPassword { get; set; }
    }
}
