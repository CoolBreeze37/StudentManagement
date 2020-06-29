using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using StudentManagement.Models;
using StudentManagement.ViewsModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    public class HomeController:Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly HostingEnvironment hostingEnviroment;

        public HomeController(IStudentRepository studentRepository,HostingEnvironment hostingEnvironment)
        {
            _studentRepository = studentRepository;
            this.hostingEnviroment = hostingEnvironment;
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
        public ActionResult Create(StudentCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                string uniqueFileName = null;
                ////多文件上传
                ////if (model.Photos != null&&model.Photos.Count>0)
                ////{

                ////foreach(var photo in model.Photos)
                ////{
                ////    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "images");
                ////    uniqueFileName = Guid.NewGuid().ToString() + "_" +photo.FileName;
                ////    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                ////    photo.CopyTo(new FileStream(filePath, FileMode.Create));

                ////}

                //// }
                //if (model.Photo != null)
                //{
                //    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "images");
                //    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                //    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                //}
                uniqueFileName =UpLodes(model);

                    Student newStudent = new Student()
                {
                    Name = model.Name,
                    Email = model.Email,
                    ClassName = model.ClassName,
                    PhotoPath = uniqueFileName
                };
                _studentRepository.Add(newStudent);
                Console.WriteLine("############################################");
                Console.WriteLine(newStudent.Id);
                return RedirectToAction("Detail", new { id = newStudent.Id });
            }

            return View();
            
        }


        [HttpGet]
        public ViewResult Edit(int id)
        {
            Student student = _studentRepository.GetStudent(id);
            
            StudentEditViewModel studentEditViewModel = new StudentEditViewModel
            {
                Id = student.Id,
                Name = student.Name,
                ClassName = student.ClassName,
                ExistingPhotoPath = student.PhotoPath,
                Email = student.Email
            };
            return View(studentEditViewModel);
        }
        [HttpPost]
        public IActionResult Edit(StudentEditViewModel studentEditViewModel)
        {
            if(ModelState.IsValid)
            {
                Student student = _studentRepository.GetStudent(studentEditViewModel.Id);
                student.Name = studentEditViewModel.Name;
                student.Email = studentEditViewModel.Email;
                student.ClassName = studentEditViewModel.ClassName;
                string uniqueFileName = studentEditViewModel.ExistingPhotoPath;
                if (studentEditViewModel.Photo!=null)
                {
                    if(studentEditViewModel.ExistingPhotoPath!=null)
                    {
                        string filePath = Path.Combine(hostingEnviroment.WebRootPath,"images",studentEditViewModel.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    uniqueFileName = UpLodes(studentEditViewModel);
                }
                student.PhotoPath = uniqueFileName;
                _studentRepository.Update(student);

                return RedirectToAction("Index");
            }
            return View(studentEditViewModel);
        }

        private string UpLodes(StudentCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
                    
            }
            return uniqueFileName;
        }
    }
}
