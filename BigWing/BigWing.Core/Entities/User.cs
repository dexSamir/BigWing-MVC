using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigWing.Core.Entities; 
public class User : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; } 
    public string Comment { get; set; }  
    public string ProfileImageUrl { get; set; }  
    public int? DepartmentId { get; set; }   
    public Department? Department { get; set; }
}
