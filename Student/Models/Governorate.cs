using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Models
{
    public class Governorate
    {
        [Key]
        public int GovernorateId { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
