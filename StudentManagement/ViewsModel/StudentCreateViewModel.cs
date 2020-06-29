using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using StudentManagement.Models;

namespace StudentManagement.ViewsModel
{
    public class StudentCreateViewModel
    {


        [Required(ErrorMessage = "请输入姓名"), MaxLength(50, ErrorMessage = "名字的长度不能超过50个字符")]
        [Display(Name = "姓名")]
        public string Name { get; set; }


        [Display(Name = "班级信息")]
        [Required(ErrorMessage = "班级信息不可为空")]
        public ClassNameEnum? ClassName { get; set; } //int类型默认必填的 所以需要加一个？取消必填

        [Required(ErrorMessage = "邮箱地址不可为空")]
        [Display(Name = "邮箱地址")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }
        [Display(Name="图片")]
        public IFormFile Photo { get; set; }
        //public List<IFormFile> Photos { get; set; }
    }
}
