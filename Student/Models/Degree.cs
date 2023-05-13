using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Models
{
    public class Degree
    {
        [Key]
        public int DegreeId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
