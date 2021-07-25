using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EY.BL.Interfaces;
using EY.Core.Domain;
using EY.Core.Repository;

namespace EY.BL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _depRepository;
        public DepartmentService(IDepartmentRepository depRepository)
        {
            this._depRepository = depRepository;
        }

        public async Task CreateAsync(Department item)
        {
            await _depRepository.CreateAsync(item);
        }
        public async Task UpdateAsync(Department item)
        {
            await _depRepository.UpdateAsync(item);
        }

        public async Task<IReadOnlyCollection<Department>> FetchAsync()
        {
            return await _depRepository.FetchAsync();
        }
        
        public async Task RemoveEmployeeAsync(int departmentId, int employeeId)
        {
            await _depRepository.RemoveEmployeeAsync(departmentId, employeeId);
        }

        public async Task<Department> GetDepartmentByIdAsync(int? id)
        {
            return await _depRepository.GetDepartmentByIdAsync(id);
        }

        public async Task AddOrUpdateEmployeeAsync(int departmentId, Employee emp)
        {
            await _depRepository.AddOrUpdateEmployeeAsync(departmentId, emp);
        }
    }
}
