using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Models
{
    public class Department
    {
        [Key]
        public int departmentId { get; set; }
        [Required]
        public string Name { get; set; }

        //[ForeignKey("Institute")]
        //public int InstituteId { get; set; }

        public Institute institute { get; set; }
    }
}
