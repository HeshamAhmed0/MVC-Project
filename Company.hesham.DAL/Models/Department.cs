using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.hesham.DAL.Models
{
    public class Department:BaseEntity
    {
        [Required(ErrorMessage ="InVaild Code")]
        public int Code { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
 

    }
}
