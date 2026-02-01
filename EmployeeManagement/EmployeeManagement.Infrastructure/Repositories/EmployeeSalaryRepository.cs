using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class EmployeeSalaryRepository : IEmployeeSalaryRepository
    {
        private readonly AppDbContext _context;

        public EmployeeSalaryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeSalary>> GetCurrentYearAsync(int employeeId)
        {
            var year = DateTime.Now.Year;
            return await _context.EmployeeSalaries
                .Where(s => s.EmployeeId == employeeId && s.SalaryDate.Year == year)
                .OrderByDescending(s => s.SalaryDate)
                .ToListAsync();
        }

        public async Task AddAsync(EmployeeSalary salary)
        {
            _context.EmployeeSalaries.Add(salary);
            await _context.SaveChangesAsync();
        }
    }
}
