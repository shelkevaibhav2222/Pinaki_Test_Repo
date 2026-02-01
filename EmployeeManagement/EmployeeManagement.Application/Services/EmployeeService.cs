using AutoMapper;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repo;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            var employees = await _repo.GetAllAsync();
            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        public async Task AddAsync(EmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            employee.CreatedDate = DateTime.Now;
            await _repo.AddAsync(employee);
        }
    }
}
