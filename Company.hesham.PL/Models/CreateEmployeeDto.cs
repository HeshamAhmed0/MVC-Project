using System.ComponentModel.DataAnnotations;

namespace Company.hesham.PL.Models
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage ="Name Is Not Valid")]
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public int Age { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}")]  // this is pattern for Addres
        public string Address { get; set; }
        public decimal salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HiringDate { get; set; }
        public int? DepartmentId { get; set; }
    }
}
