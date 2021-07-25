using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EY.Core.Domain;

namespace EY.Core.Repository
{
    public interface IDepartmentRepository
    {
        Task CreateAsync(Department item);
        Task UpdateAsync(Department item);
        Task<IReadOnlyCollection<Department>> FetchAsync();
        Task<Department> GetDepartmentByIdAsync(int? id);
        Task AddOrUpdateEmployeeAsync(int departmentId, Employee emp);
        Task RemoveEmployeeAsync(int departmentId, int employeeId);
    }
}
