﻿using Domain.Enums;

namespace Domain.Dtos.Teacher;

public class TeacherDto
{
    public int TeacherId { get; set; }
    public string Password { get; set; } = null!;
    public int TeacherCode { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Gender Gender { get; set; }
    public DateTime DOB { get; set; }
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime JoinDate { get; set; }
    public int WorkingDays { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
}