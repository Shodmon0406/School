using AutoMapper;
using Domain.Dtos.Class;
using Domain.Dtos.Classroom;
using Domain.Dtos.StipendDto;
using Domain.Dtos.Student;
using Domain.Dtos.Teacher;
using Domain.Entities;
using Domain.Filters.ClassFilter;

namespace Infrastructure.AutoMapper;

public class ServiceProfile:Profile
{
    public ServiceProfile()
    {
        CreateMap<Class, GetClassFilter>().ReverseMap();
        CreateMap<Class, AddClassDto>().ReverseMap();
        CreateMap<Classroom, GetClassroomDto>().ReverseMap();
        CreateMap<Classroom, AddClassroomDto>().ReverseMap();
        CreateMap<Student, GetStudentDto>().ReverseMap();
        CreateMap<Student, AddStudentDto>().ReverseMap();
        CreateMap<Subject, GetSubjectDto>().ReverseMap();
        CreateMap<Subject, AddSubjectDto>().ReverseMap();
        CreateMap<Teacher, GetTeacherDto>().ReverseMap();
        CreateMap<Teacher, AddTeacherDto>().ReverseMap();

        CreateMap<Stipend, GetStipendDto>();
        CreateMap<AddStipendDto, Stipend>();
    }
    
    
    
}