using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Student.Data;
using Student.Models;
using Student.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Controllers
{
    public class Students : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;
        private new List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 2097152;
        private readonly IWebHostEnvironment hosting;

        public Students(ApplicationDbContext context, IWebHostEnvironment hosting,
            IToastNotification toastNotification)
        {
            _context = context;
            this.hosting = hosting;
            _toastNotification = toastNotification;
        }


        //Get
        public async Task<IActionResult> Index(string SearchName = null)
        {
            var students = await _context.Students
                .Include(n => n.nationality)
                .Include(n => n.Institute)
                .Include(n => n.qualification)
                .Include(n => n.Governorate)
                .Include(n => n.department)
                .Include(n => n.qualificationDegree)
                .Include(n => n.city).
                OrderByDescending(m => m.percentage).ToListAsync();

            if (SearchName != null)
            {
                students = await _context.Students.Include(n => n.city).Where(a => a.studentCode.ToLower().Contains(SearchName)
                      || a.frstNameArabic.ToLower().Contains(SearchName)
                      || a.fristNameEnglish.ToLower().Contains(SearchName)
                      || a.department.Name.ToLower().Contains(SearchName)
                      || a.Institute.Name.ToLower().Contains(SearchName)
                      || a.percentage.ToLower().Contains(SearchName)
                      || a.address.ToLower().Contains(SearchName)
                      || a.Governorate.Name.ToLower().Contains(SearchName)
                      || a.qualificationDegree.Name.ToLower().Contains(SearchName)
                      || a.nationality.Name.ToLower().Contains(SearchName)
                    )
                     .ToListAsync();
            }
            return View(students);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(n => n.nationality)
                .Include(n => n.Institute)
                .Include(n => n.qualification)
                .Include(n => n.Governorate)
                .Include(n => n.department)
                .Include(n => n.qualificationDegree)
                .Include(n => n.city)
                .FirstOrDefaultAsync(m => m.Id == id);

            

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }


        //GET
        public async Task<IActionResult> Create()
        {
            var viewModel = new StudentViewModel
            {
                Cities = await _context.Cities.OrderBy(m => m.Name).ToListAsync(),
                Degrees = await _context.Degrees.OrderBy(m => m.Name).ToListAsync(),
                departments = await _context.Departments.OrderBy(m => m.Name).ToListAsync(),
                Governorates = await _context.governorates.OrderBy(m => m.Name).ToListAsync(),
                Institutes = await _context.institutes.OrderBy(m => m.Name).ToListAsync(),
                nationalities = await _context.nationalities.OrderBy(m => m.Name).ToListAsync(),
                Qualifications = await _context.qualifications.OrderBy(m => m.Name).ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel studentVm, int? id)
        {
            var files = Request.Form.Files;
            var poster = files.FirstOrDefault();
            using var dataStream = new MemoryStream();
            await poster.CopyToAsync(dataStream);
            Random random = new Random();





            if (!ModelState.IsValid)
            {
                var v = _context.governorates.Find(id);
                studentVm.Cities = await _context.Cities.OrderBy(m => m.Name).ToListAsync();
                studentVm.Degrees = await _context.Degrees.OrderBy(m => m.Name).ToListAsync();
                studentVm.departments = await _context.Departments.OrderBy(m => m.Name).ToListAsync();
                studentVm.Governorates = await _context.governorates.OrderBy(m => m.Name).ToListAsync();
                studentVm.Institutes = await _context.institutes.OrderBy(m => m.Name).ToListAsync();
                studentVm.nationalities = await _context.nationalities.OrderBy(m => m.Name).ToListAsync();
                studentVm.Qualifications = await _context.qualifications.OrderBy(m => m.Name).ToListAsync();
                var student = new StudentModel
                {
                    frstNameArabic = studentVm.frstNameArabic,
                    scndNameArabic = studentVm.scndNameArabic,
                    thrdNameArabic = studentVm.thrdNameArabic,
                    fristNameEnglish = studentVm.fristNameEnglish,
                    scndNameEnglish = studentVm.scndNameEnglish,
                    thrdNameEnglish = studentVm.thrdNameEnglish,
                    studentCode = random.Next(41717000, 41717999).ToString(),
                    NationalCode = studentVm.NationalCode,
                    address = studentVm.address,
                    percentage = studentVm.percentage,

                    imagurl = dataStream.ToArray(),

                    cityId = studentVm.cityId,
                    DegreeId = studentVm.DegreeId,
                    departmentId = studentVm.departmentId,
                    GovernorateId = studentVm.GovernorateId,
                    InstituteId = studentVm.InstituteId,
                    nationalityId = studentVm.nationalityId,
                    qualificationId = studentVm.qualificationId
                };


                await _context.AddAsync(student);

                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Added Successfully");
                return View(studentVm);
            }

            if (!files.Any())
            {
                studentVm.Cities = await _context.Cities.OrderBy(m => m.Name).ToListAsync();
                studentVm.Degrees = await _context.Degrees.OrderBy(m => m.Name).ToListAsync();
                studentVm.departments = await _context.Departments.OrderBy(m => m.Name).ToListAsync();
                studentVm.Governorates = await _context.governorates.OrderBy(m => m.Name).ToListAsync();
                studentVm.Institutes = await _context.institutes.OrderBy(m => m.Name).ToListAsync();
                studentVm.nationalities = await _context.nationalities.OrderBy(m => m.Name).ToListAsync();
                studentVm.Qualifications = await _context.qualifications.OrderBy(m => m.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Please Select An Image..!");
                return View("Create", studentVm);
            }

            if (!_allowedExtenstions.Contains(Path.GetExtension(poster.FileName).ToLower()))
            {
                studentVm.Cities = await _context.Cities.OrderBy(m => m.Name).ToListAsync();
                studentVm.Degrees = await _context.Degrees.OrderBy(m => m.Name).ToListAsync();
                studentVm.departments = await _context.Departments.OrderBy(m => m.Name).ToListAsync();
                studentVm.Governorates = await _context.governorates.OrderBy(m => m.Name).ToListAsync();
                studentVm.Institutes = await _context.institutes.OrderBy(m => m.Name).ToListAsync();
                studentVm.nationalities = await _context.nationalities.OrderBy(m => m.Name).ToListAsync();
                studentVm.Qualifications = await _context.qualifications.OrderBy(m => m.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Only .jpg and .png are Allowed..!");
                return View("Create", studentVm);
            }
            if (poster.Length > 2097152)
            {
                studentVm.Cities = await _context.Cities.OrderBy(m => m.Name).ToListAsync();
                studentVm.Degrees = await _context.Degrees.OrderBy(m => m.Name).ToListAsync();
                studentVm.departments = await _context.Departments.OrderBy(m => m.Name).ToListAsync();
                studentVm.Governorates = await _context.governorates.OrderBy(m => m.Name).ToListAsync();
                studentVm.Institutes = await _context.institutes.OrderBy(m => m.Name).ToListAsync();
                studentVm.nationalities = await _context.nationalities.OrderBy(m => m.Name).ToListAsync();
                studentVm.Qualifications = await _context.qualifications.OrderBy(m => m.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Poster CanNot be More Than 2 MB!");
                return View("Create", studentVm);
            }


            studentVm.Cities = await _context.Cities.OrderBy(m => m.Name).ToListAsync();
            studentVm.Degrees = await _context.Degrees.OrderBy(m => m.Name).ToListAsync();
            studentVm.departments = await _context.Departments.OrderBy(m => m.Name).ToListAsync();
            studentVm.Governorates = await _context.governorates.OrderBy(m => m.Name).ToListAsync();
            studentVm.Institutes = await _context.institutes.OrderBy(m => m.Name).ToListAsync();
            studentVm.nationalities = await _context.nationalities.OrderBy(m => m.Name).ToListAsync();
            studentVm.Qualifications = await _context.qualifications.OrderBy(m => m.Name).ToListAsync();


            var students = new StudentModel
            {
                frstNameArabic = studentVm.frstNameArabic,
                scndNameArabic = studentVm.scndNameArabic,
                thrdNameArabic = studentVm.thrdNameArabic,
                fristNameEnglish = studentVm.fristNameEnglish,
                scndNameEnglish = studentVm.scndNameEnglish,
                thrdNameEnglish = studentVm.thrdNameEnglish,
                NationalCode = studentVm.NationalCode,
                nationalityId = studentVm.nationalityId,
                GovernorateId = studentVm.GovernorateId,
                cityId = studentVm.cityId,
                address = studentVm.address,
                qualificationId = studentVm.qualificationId,
                DegreeId = studentVm.DegreeId,
                percentage = studentVm.percentage,
                studentCode = random.Next(41717000, 41717999).ToString(),
                InstituteId = studentVm.InstituteId,
                departmentId = studentVm.departmentId,
                imagurl = dataStream.ToArray()
            };

            _context.Students.Add(students);
            _context.SaveChanges();

            _toastNotification.AddSuccessToastMessage("Student Created Successfully");
            return RedirectToAction(nameof(Index));
        }


        //GET
        public async Task<IActionResult> Edit(int? id)
        {



            if (id == null)
                return NotFound();








            var student = await _context.Students.Include(n => n.nationality)
                .Include(n => n.Institute)
                .Include(n => n.qualification)
                .Include(n => n.Governorate)
                .Include(n => n.department)
                .Include(n => n.qualificationDegree)
                .Include(n => n.city)
                .FirstOrDefaultAsync(m => m.Id == id);


            if (student == null)
                return NotFound();

            var viewModel = new StudentViewModel
            {
                frstNameArabic = student.frstNameArabic,
                scndNameArabic = student.scndNameArabic,
                thrdNameArabic = student.thrdNameArabic,
                fristNameEnglish = student.fristNameEnglish,
                scndNameEnglish = student.scndNameEnglish,
                thrdNameEnglish = student.thrdNameEnglish,
                NationalCode = student.NationalCode,
                Cities = await _context.Cities.OrderBy(m => m.Name).ToListAsync(),
                Degrees = await _context.Degrees.OrderBy(m => m.Name).ToListAsync(),
                departments = await _context.Departments.OrderBy(m => m.Name).ToListAsync(),
                Governorates = await _context.governorates.OrderBy(m => m.Name).ToListAsync(),
                Institutes = await _context.institutes.OrderBy(m => m.Name).ToListAsync(),
                nationalities = await _context.nationalities.OrderBy(m => m.Name).ToListAsync(),
                Qualifications = await _context.qualifications.OrderBy(m => m.Name).ToListAsync(),

                address = student.address,
                percentage = student.percentage,
                studentCode = student.studentCode,
                imagurl = student.imagurl
            };

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentViewModel studentVm)
        {
            if (!ModelState.IsValid)
            {
                return View(studentVm);
            }

            var students = await _context.Students.Include(n => n.nationality)
                .Include(n => n.Institute)
                .Include(n => n.qualification)
                .Include(n => n.Governorate)
                .Include(n => n.department)
                .Include(n => n.qualificationDegree)
                .Include(n => n.city)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (students == null)
                return NotFound();

            var files = Request.Form.Files;
            if (files.Any())
            {
                var poster = files.FirstOrDefault();

                using var dataStream = new MemoryStream();

                await poster.CopyToAsync(dataStream);

                studentVm.imagurl = dataStream.ToArray();

                if (!_allowedExtenstions.Contains(Path.GetExtension(poster.FileName).ToLower()))
                {
                    ModelState.AddModelError("Poster", "Only .PNG, .JPG images are allowed!");
                    return View(studentVm);
                }

                if (poster.Length > _maxAllowedPosterSize)
                {
                    ModelState.AddModelError("Poster", "Poster cannot be more than 2 MB!");
                    return View(studentVm);
                }
                
                students.imagurl = studentVm.imagurl;
            }
            students.frstNameArabic      = studentVm.frstNameArabic;
            students.scndNameArabic      = studentVm.scndNameArabic;
            students.thrdNameArabic      = studentVm.thrdNameArabic;
            students.fristNameEnglish    = studentVm.fristNameEnglish;
            students.scndNameEnglish     = studentVm.scndNameEnglish;
            students.thrdNameEnglish     = studentVm.thrdNameEnglish;
            students.NationalCode        = studentVm.NationalCode;
            students.nationalityId       = studentVm.nationalityId;
            students.GovernorateId       = studentVm.GovernorateId;
            students.cityId              = studentVm.cityId;
            students.address             = studentVm.address;
            students.qualificationId     = studentVm.qualificationId;
            students.DegreeId            = studentVm.DegreeId;
            students.studentCode         = studentVm.studentCode;
            students.InstituteId         = studentVm.InstituteId;
            students.departmentId        = studentVm.departmentId;
            students.percentage          = studentVm.percentage;

            _context.SaveChanges();

            _toastNotification.AddSuccessToastMessage("Student Edition Success☺☺");
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();

            return Ok();
        }


        #region Seed Methods
        [HttpGet]
        public JsonResult GetDepartment(int instituteId)
        {
            var instituteid = this.FindInstit(instituteId);
            var department = this.ListByFilter(c => c.institute == instituteid);
            return new JsonResult(new SelectList(department, "departmentId", "Name"));
        }

        [HttpGet]
        public JsonResult GetCity(int governorateId)
        {
            var governorateid = this.FindGover(governorateId);
            var City = this.ListByFilter(z => z.governorate == governorateid);
            return new JsonResult(new SelectList(City, "cityId", "Name"));
        }

        #endregion Seed Methods

        public List<Department> ListByFilter(Func<Department, bool> lamda)
        {
            var x = _context.Departments.Where(lamda).ToList();
            return x;
        }
        public Institute FindInstit(int Id)
        {
            var c = _context.institutes.Find(Id);
            return c;
        }
        public List<City> ListByFilter(Func<City, bool> lamda)
        {
            var x = _context.Cities.Where(lamda).ToList();
            return x;
        }
        public Governorate FindGover(int Id)
        {
            var c = _context.governorates.Find(Id);
            return c;
        }

    }
}
