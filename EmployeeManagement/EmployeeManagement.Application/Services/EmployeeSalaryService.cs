using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;

namespace EmployeeManagement.Application.Services
{
    public class EmployeeSalaryService
    {
        private readonly IEmployeeSalaryRepository _repo;
        private readonly IMapper _mapper;

        public EmployeeSalaryService(IEmployeeSalaryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<EmployeeSalaryDto>> GetCurrentYearSalariesAsync(int employeeId)
        {
            var salaries = await _repo.GetCurrentYearAsync(employeeId);
            return _mapper.Map<List<EmployeeSalaryDto>>(salaries);
        }

        public async Task AddSalaryAsync(EmployeeSalaryDto dto)
        {
            var salary = _mapper.Map<EmployeeSalary>(dto);
            salary.CreatedDate = DateTime.Now;
            await _repo.AddAsync(salary);
        }
    }
}
