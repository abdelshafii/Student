using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Models
{
    public class Institute
    {
        [Key]
        public int InstituteId { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Department> Departments { get; set; }
    }
}
