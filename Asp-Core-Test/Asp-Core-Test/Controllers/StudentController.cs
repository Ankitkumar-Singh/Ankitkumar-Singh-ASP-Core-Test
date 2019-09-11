using Asp_Core_Test.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Asp_Core_Test.Controllers
{
    #region "Student controller"
    [Route("Student")]
    public class StudentController : Controller
    {
        //Variable Declaration
        private readonly IStudentRepository _studentRepository;

        #region "StudentController Constructor"       
        //StudentController Constructor
        public StudentController(IStudentRepository studentRepository) => _studentRepository = studentRepository;
        #endregion

        #region "List student deatls."
        //List all student and show search result
        [Route("/")]
        [Route("Index")]
        public IActionResult Index(string search = null)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var found = _studentRepository.SearchStudent(search);
                return View(found);
            }
            return View(_studentRepository.GetStudentList());
        }
        #endregion

        #region "To save student details."
        //Get specific student detail, if availabe else return empty.
        [Route("Save/{id?}")]
        [HttpGet]
        public IActionResult Save(Guid? id)
        {
            var newStudent = _studentRepository.GetStudent(id);

            ViewBag.PhdSubjectId = _studentRepository.GetSubjectList();

            if (newStudent == null)
                newStudent = new Student();

            return View(newStudent);
        }

        //Post student details.
        [Route("Save/{id?}")]
        [HttpPost]
        public IActionResult Save(Student student)
        {
            ViewBag.PhdSubjectId = _studentRepository.GetSubjectList();

            if (ModelState.IsValid)
                return RedirectToAction("index", _studentRepository.SaveStudent(student));

            return View(student);
        }
        #endregion

        #region "Show student details"
        //Accept id of student and show details.
        [Route("Details/{id?}")]
        [HttpGet]
        public IActionResult Details(Guid id) => View(_studentRepository.GetStudent(id));
        #endregion

        #region "Delete student details"
        //Accept id of student and show details.
        [Route("Delete/{id?}")]
        [HttpGet]
        public IActionResult Delete(Guid? id) => View(_studentRepository.GetStudent(id));

        //Delete student detail.
        [Route("Delete/{id?}")]
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _studentRepository.DeleteStudent(id);
            return RedirectToAction("index");
        }
        #endregion
    }
    #endregion
}