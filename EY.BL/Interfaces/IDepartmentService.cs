﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EY.Core.Domain;

namespace EY.BL.Interfaces
{
    public interface IDepartmentService
    {
        Task CreateAsync(Department item);
        Task UpdateAsync(Department item);
        Task<IReadOnlyCollection<Department>> FetchAsync();
        Task AddEmployeeAsync(int departmentId, int employeeId);
        Task RemoveEmployeeAsync(int departmentId, int employeeId);
    }
}