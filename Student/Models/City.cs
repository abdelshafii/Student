using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Models
{
    public class City
    {
        [Key]
        public int cityId { get; set; }
        [Required]
        public string Name { get; set; }


        //public int GovernorateId { get; set; }

        public Governorate governorate { get; set; }
    }
}
