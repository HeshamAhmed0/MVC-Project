﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.hesham.DAL.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateIn { get; set; }
    }
}
