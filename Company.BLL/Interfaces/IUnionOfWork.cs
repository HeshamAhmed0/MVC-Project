﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IUnionOfWork
    {
        public IDepatmenReposatory depatmenReposatory { get; }
        public IEmployeeReposatorycs employeeReposatory { get; }
        public Task<int> Complete();
    }
}
