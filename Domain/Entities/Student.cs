using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class Student : IdentityUser
    {
        public string Address { get; set; } = null!;
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
        public List<Stipend> Stipends { get; set; }
    }


}
