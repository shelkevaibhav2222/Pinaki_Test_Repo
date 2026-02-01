using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagement.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly EmployeeService _service;

        public IndexModel(EmployeeService service)
        {
            _service = service;
        }

        public List<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();

        public async Task OnGetAsync()
        {
            Employees = await _service.GetAllAsync();
        }
    }
}
