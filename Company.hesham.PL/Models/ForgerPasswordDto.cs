using System.ComponentModel.DataAnnotations;

namespace Company.hesham.PL.Models
{
    public class ForgerPasswordDto
    {
        [DataType(DataType.EmailAddress, ErrorMessage = "Email Address Is Required")]
        public required string Email { get; set; }

    }
}
