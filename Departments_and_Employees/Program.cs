using System;
using Departments_and_Employees.Data;
using Departments_and_Employees.Models;

namespace Departments_and_Employees
{
    class Program
    {
        static void Main(string[] args)
        {
            var departmentRepo = new DepartmentRepository();
            var allDepartments = departmentRepo.GetAllDepartments();

            var employeeRepo = new EmployeeRepository();
            var allEmployees = employeeRepo.GetAllEmployees();

            var hardCodedId = 3;
            //var departmentWithId3 = departmentRepo.GetDepartmentById(hardCodedId);

            Console.WriteLine("All Departments");
            Console.WriteLine("---------------");
            foreach (var depo in allDepartments)
            {
                Console.WriteLine($"{depo.Id} { depo.DeptName}");
            }

            Console.WriteLine();
            Console.WriteLine("-----------------");
            //Console.WriteLine(departmentWithId3.DeptName);

            Console.WriteLine();
            Console.WriteLine("All Employees");
            Console.WriteLine("---------------");

            foreach (var employee in allEmployees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName}: {employee.Department.DeptName}");
            }

            var employeeWithId3 = employeeRepo.GetEmployeeById(hardCodedId);

            Console.WriteLine("---------------");

            Console.WriteLine();
            Console.WriteLine(); 
            Console.WriteLine(); 
            Console.WriteLine();

            Console.WriteLine("What department would you like to add?");

            var legal = Console.ReadLine();

            departmentRepo.AddDepartment(legal);

            Console.WriteLine("What Department (ID) would you like to update?");
            var departmentUpdate = Console.ReadLine();
            if(departmentUpdate != "")
            {
            Console.WriteLine("What should the new department name be?");
            var newDepartmentName = Console.ReadLine();

            departmentRepo.UpdateDepartment(departmentUpdate, newDepartmentName);
            };

            Console.WriteLine();
            Console.WriteLine("What Department (ID) would you like to delete?");
            var departmentID = Console.ReadLine();

            departmentRepo.DeleteDepartment(departmentID);

            Console.WriteLine();
            Console.WriteLine("Would you like to add an employee? Type 'YES' if yes, otherwise hit 'ENTER'.");
            var addNewEmployee = Console.ReadLine();
            Console.WriteLine();

            if(addNewEmployee == "YES" || addNewEmployee == "Yes" || addNewEmployee == "yes")
            {
                Console.WriteLine("What is the first name of the new employee?");
                var employeeFirstName = Console.ReadLine();
                Console.WriteLine();

                Console.WriteLine("What is the last name of the new employee?");
                var employeeLastName = Console.ReadLine();
                Console.WriteLine();

                Console.WriteLine("What is the department (ID) they will be in?");
                var newEmployeeDepartmentId = Console.ReadLine();

                var newEmployee = new Employee()
                {
                    FirstName = employeeFirstName,
                    LastName = employeeLastName,
                    DepartmentId = Int32.Parse(newEmployeeDepartmentId)
                };

                employeeRepo.AddEmployee(newEmployee);

            } 
            else
            {
                Console.WriteLine("Invalid Command");
            }


        }
    }
}
