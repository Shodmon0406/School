using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Account;

public class Register
{
    public string UserName { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Compare("Password")]
    public string ComfirmPassword { get; set; }

    public string Email { get; set; }
    public string Phone { get; set; }
}