using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace StudentManagement.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Name = "两同名",
                    ClassName = ClassNameEnum.FirstGrade,
                    Email = "ghost@qq.com",
                },

                new Student
                {
                    Id = 2,
                    Name = "凉风",
                    ClassName = ClassNameEnum.FirstGrade,
                    Email = "ghhh@qq.com",
                }

                );
        }
    }
}
