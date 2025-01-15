using BigWing.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigWing.Core.Entities;
public class Department : BaseEntity 
{
    public string Name { get; set; }    
    public ICollection<User> Users{ get; set; } 
}
