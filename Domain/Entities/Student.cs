using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Student : IdentityUser
    {
        public string StudentCode { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Gender Gender { get; set; }
        public DateTime DOB { get; set; }
        public int Stage { get; set; }
        public bool Active { get; set; } = true;
        public DateTime JoinDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public List<ClassStudent> ClassesStudents { get; set; }
        public List<StudentParrent> StudentParrents { get; set; }
        public List<StudentOlympiad> StudentOlympiad { get; set; }
    }


}
