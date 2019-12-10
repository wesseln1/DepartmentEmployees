using System;
using System.Collections.Generic;
using System.Text;
using Departments_and_Employees.Models;

namespace Departments_and_Employees.Models
{
    // C# representation of the Department table
    public class Department
    {
        public int Id { get; set; }
        public string DeptName { get; set; }
        public List<Employee> EmployeesInDept { get; set; }
    }
}
