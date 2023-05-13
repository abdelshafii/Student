using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Models
{
    public class StudentModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string frstNameArabic { get; set; }

        [Required]
        [MaxLength(15)]
        public string scndNameArabic { get; set; }

        [Required]
        [MaxLength(15)]
        public string thrdNameArabic { get; set; }
        [Required]
        [MaxLength(15)]
        public string fristNameEnglish { get; set; }
        [Required]
        [MaxLength(15)]
        public string scndNameEnglish { get; set; }
        [Required]
        [MaxLength(15)]
        public string thrdNameEnglish { get; set; }
        [MaxLength(7)]
        public string NationalCode { get; set; }

        public int nationalityId { get; set; }
        public Nationality nationality { get; set; }

        public int GovernorateId { get; set; }
        public Governorate Governorate { get; set; }

        public int cityId { get; set; }

        public City city { get; set; }


        [MaxLength(50)]
        [MinLength(15)]
        public string address { get; set; }

        public int qualificationId { get; set; }
        public Qualification qualification { get; set; }

        public int DegreeId { get; set; }
        public Degree qualificationDegree { get; set; }

        [MaxLength(6)]
        public string percentage { get; set; }

        
        public byte[] imagurl { get; set; }
        public string studentCode { get; set; }


        public int InstituteId { get; set; }
        public Institute Institute { get; set; }

        public int departmentId { get; set; }
        public Department department { get; set; }
        //[NotMapped]
        //public IFormFile file { get; set; }
    }
}
