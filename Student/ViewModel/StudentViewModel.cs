
using Student.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Student.ViewModel
{
    public class StudentViewModel
    {
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

        [MaxLength(50)]
        public string address { get; set; }

        [MaxLength(6)]
        public string percentage { get; set; }

        
        [Display(Name = "Select Image...")]
        public byte[] imagurl { get; set; }
        public string studentCode { get; set; }

        [Display(Name = "Institute")]
        public int InstituteId { get; set; }
        public IEnumerable<Institute> Institutes { get; set; }

        [Display(Name = "Governorate")]
        public int GovernorateId { get; set; }
        public IEnumerable<Governorate> Governorates { get; set; }

        [Display(Name = "natonality")]

        public int nationalityId { get; set; }
        public IEnumerable<Nationality> nationalities { get; set; }

        [Display(Name = "Department")]
        public int departmentId { get; set; }
        public IEnumerable<Department> departments { get; set; }

        [Display(Name = "City")]
        public int cityId { get; set; }
        public IEnumerable<City> Cities { get; set; }

        [Display(Name = "Degree")]
        public int DegreeId { get; set; }
        public IEnumerable<Degree> Degrees { get; set; }

        [Display(Name = "Qualification")]
        public int qualificationId { get; set; }
        public IEnumerable<Qualification> Qualifications { get; set; }
    }
}
