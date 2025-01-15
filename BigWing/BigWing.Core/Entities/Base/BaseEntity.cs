﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigWing.Core.Entities.Base;
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; }   
    public DateTime? UpdatedTime { get; set;} 
    public bool IsDeleted { get; set; }
}