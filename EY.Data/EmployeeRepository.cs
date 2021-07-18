using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EY.Core;
using EY.Core.Domain;
using EY.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace EY.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public EmployeeRepository()
        {
            try
            {
                var context = new DepartmentContext();
                context.Database.Migrate(); // apply all migrations
            }
            catch (Exception ex)
            {
            }
        }

        public async Task CreateAsync(Employee item)
        {
            item = item ?? throw new ArgumentNullException(nameof(item));

            using (var context = CreateContext())
            {
                context.Employees.Add(item);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyCollection<Employee>> FetchAsync()
        {
            using (var context = CreateContext())
            {
                return await context.Employees.ToArrayAsync();
            }
        }
        public async Task<IReadOnlyCollection<Employee>> FetchByDepartmentIdAsync(int departmentId)
        {
            using (var context = CreateContext())
            {
                return await context.Employees.Where(x=>x.CurrentDepartment.Id == departmentId).ToArrayAsync();
            }
        }

        private static DepartmentContext CreateContext()
        {
            return new DepartmentContext();
        }
    }
}
