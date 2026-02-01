using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<EmployeeSalary> Salaries { get; set; } = new List<EmployeeSalary>();
    }
}
