using System.ComponentModel.DataAnnotations;

namespace Company.hesham.PL.Models
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage = "Code is Nessesary")]
        public int Code { get; set; }
        [Required(ErrorMessage = "Name is Nessesary")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Date is Nessesary")]
        public DateTime CreatenIn { get; set; }

       
    }
}
