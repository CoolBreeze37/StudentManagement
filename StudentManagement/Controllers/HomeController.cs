using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    public class HomeController:Controller
    {
        private readonly IStudentRepository _studentRepository;

        public HomeController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        
        public ViewResult Index()
        {
            IEnumerable<Student> students = _studentRepository.GetAllStudents();
            return View(students);
        }

        public ViewResult Detail(int? id)
        {
            HomeDetialsViewsModel homeDetialsViewsModel = new HomeDetialsViewsModel()
            { 
                student = _studentRepository.GetStudent(id??1),
                PageTitle = "学生详情"
            };

            return View(homeDetialsViewsModel);//绝对路径三种方式
                                                                               // View("~/MyViews/Test.cshtml")//推荐
                                                                               // View("/MyViews/Test.cshtml")
                                                                               //相对路径
                                                                               //View("../../MyViews/Test")

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student student)
        {
            if(ModelState.IsValid)
            {
                Student newStudent = _studentRepository.Add(student);
                return RedirectToAction("Detail", new { id = newStudent.Id });
            }

            return View();
            
        }
    }
}
