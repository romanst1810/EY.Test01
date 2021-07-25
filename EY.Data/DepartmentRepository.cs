using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EY.Core;
using EY.Core.Domain;
using EY.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace EY.Data
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public DepartmentRepository()
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

        public async Task CreateAsync(Department item)
        {
            item = item ?? throw new ArgumentNullException(nameof(item));

            using (var context = CreateContext())
            {
                context.Departments.Add(item);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Department item)
        {
            item = item ?? throw new ArgumentNullException(nameof(item));

            using (var context = CreateContext())
            {
                Department d = await context.Departments.FirstOrDefaultAsync(x => x.Id == item.Id);
                d.Description = item.Description;
                d.Name = item.Name;
                d.WorkStatus = item.WorkStatus;
                d.DateUpdate = DateTime.Now;
                await context.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyCollection<Department>> FetchAsync()
        {
            using (var context = CreateContext())
            {
                return await context.Departments.Include(e => e.Employees).ToArrayAsync();
            }
        }

        public async Task<Department> GetDepartmentByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            using (var context = CreateContext())
            {
                return await context.Departments.Include(e => e.Employees).FirstOrDefaultAsync(d => d.Id == id);
            }
        }

        public async Task AddOrUpdateEmployeeAsync(int departmentId, Employee emp)
        {
            if (emp == null || departmentId <= 0)
                throw new ArgumentNullException(nameof(emp), nameof(departmentId));

            using (var context = CreateContext())
            {
                Department dep = await context.Departments.Include(e => e.Employees)
                    .FirstOrDefaultAsync(x => x.Id == departmentId);
                if (dep != null)
                {
                    Employee employeeExists = dep.Employees.FirstOrDefault(e => e.Id == emp.Id);
                    if (employeeExists != null)
                    {
                        employeeExists.StartDate = emp.StartDate;
                        employeeExists.EndDate = emp.EndDate;
                        employeeExists.FirstName = emp.FirstName;
                        employeeExists.LastName = emp.LastName;
                        employeeExists.SocialId = emp.SocialId;
                        employeeExists.WorkDescription = emp.WorkDescription;
                    }
                    else
                    {
                        emp.CurrentDepartment = dep;
                        dep.Employees.Add(emp);
                    }
                    await context.SaveChangesAsync();
                }
                else throw new Exception("Department does not exists");
            }
        }

        public async Task RemoveEmployeeAsync(int departmentId, int employeeId)
        {
            if (departmentId > 0 && employeeId > 0)
                throw new ArgumentNullException(nameof(departmentId) + nameof(employeeId));
            using (var context = CreateContext())
            {
                Department dep = await context.Departments.Include(e => e.Employees)
                    .FirstOrDefaultAsync(x => x.Id == departmentId);
                if (dep != null)
                {
                    Employee emp = dep.Employees.FirstOrDefault(x => x.Id == employeeId);
                    if (emp != null)
                    {
                        dep.Employees.Remove(emp);
                        await context.SaveChangesAsync();
                    }
                }
                else throw new Exception("Department or Employee does not exists");
            }
        }

        private static DepartmentContext CreateContext()
        {
            return new DepartmentContext();
        }
    }
}
