using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigWing.BL.VM.User;
public class UserCreateVM
{
    [Required, MaxLength(24)]
    public string Name { get; set; } = null!;
    [Required, MaxLength(48)]
    public string Surname { get; set; } = null!;

    [Required, MaxLength(64)]
    public string Username { get; set; } = null!;

    [Required, MaxLength(256)]
    public string Comment { get; set; } = null!;
    [Required, MaxLength(128), EmailAddress]
    public string Email { get; set; } = null!;

    [Required, MaxLength(32), DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required, MaxLength(32), DataType(DataType.Password), Compare(nameof(Password))]
    public string RePassword { get; set; } = null!; 
    public IFormFile? ProfileImage {  get; set; } 
    public int? DepartmentId { get; set; } 
}
