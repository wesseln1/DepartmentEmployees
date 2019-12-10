using System;
using Departments_and_Employees.Data;
using Departments_and_Employees.Models;

namespace Departments_and_Employees
{
    class Program
    {
        static void Main(string[] args)
        {
            static void options() 
            { 
            Console.WriteLine("1. View All Departments");
            Console.WriteLine("2. Add Department");
            Console.WriteLine("3. Update Department");
            Console.WriteLine("4. Delete Department");
            Console.WriteLine("5. View ALL Employees");
            Console.WriteLine("6. View ALL Employees and their Departments");
            Console.WriteLine("7. Add Employee");
            Console.WriteLine("8. Update Employee");
            Console.WriteLine("9. Delete Employee");
            Console.WriteLine("Press 'ENTER' to exit");
            };

            var optionSelected = true;
            while (optionSelected)
            {
                options();
                Console.WriteLine();

                Console.WriteLine("Choose an option");
                Console.Write("> ");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    var departmentRepo = new DepartmentRepository();
                    var allDepartments = departmentRepo.GetAllDepartments();

                    Console.Clear();
                    Console.WriteLine("All Departments");
                    Console.WriteLine("---------------");
                    foreach (var depo in allDepartments)
                    {
                        Console.WriteLine($"{ depo.DeptName}");
                        Console.WriteLine($"ID: {depo.Id}");
                        Console.WriteLine();
                    }
                    Console.WriteLine("---------------");
                    Console.WriteLine();
                    Console.ReadLine();
                    Console.Clear();


                }
                else if (option == "2")
                {
                    var departmentRepo = new DepartmentRepository();
                    var allDepartments = departmentRepo.GetAllDepartments();

                    Console.Clear();
                    Console.WriteLine("Current Departments");
                    Console.WriteLine("---------------");
                    foreach (var depo in allDepartments)
                    {
                        Console.WriteLine($"{depo.Id} { depo.DeptName}");
                    }
                    Console.WriteLine("---------------");
                    Console.WriteLine();
                    Console.WriteLine("What department would you like to add?");

                    var legal = Console.ReadLine();
                    departmentRepo.AddDepartment(legal);
                    if(legal != "")
                    {
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine($"Great! {legal} Department has now been added!");
                        Console.WriteLine();
                        Console.WriteLine("Would you like to add an employee to the new department?");
                        Console.WriteLine();
                        Console.WriteLine("1. Yes");
                        Console.WriteLine("2. No");
                        Console.WriteLine();
                        var employeeOption = Console.ReadLine();

                        if(employeeOption == "1")
                        {
                            Console.Clear();
                            var employeeRepo = new EmployeeRepository();
                            var allEmployees = employeeRepo.GetAllEmployees();

                            foreach (var employee in allEmployees)
                            {
                                Console.WriteLine($"{employee.Id}. {employee.FirstName} {employee.LastName}");
                            }

                            Console.WriteLine("Which employee would you like to add to this department?");
                            var moveEmployee = Console.ReadLine();
                            employeeRepo.UpdateDepartmentId(moveEmployee);
                            Console.Clear();
                        }
                        else if(option == "")
                        {
                            Console.Clear();
                        }
                    }
                }
                else if (option == "3")
                {
                    var departmentRepo = new DepartmentRepository();
                    var allDepartments = departmentRepo.GetAllDepartments();

                    Console.Clear();
                    Console.WriteLine("All Departments");
                    Console.WriteLine("---------------");
                    foreach (var depo in allDepartments)
                    {
                        Console.WriteLine($"{depo.Id} { depo.DeptName}");
                    }
                    Console.WriteLine("---------------");

                    Console.WriteLine();
                    Console.WriteLine("What Department (ID) would you like to update?");
                    var departmentUpdate = Console.ReadLine();
                    if (departmentUpdate != "")
                    {
                        Console.WriteLine("What should the new department name be?");
                        var newDepartmentName = Console.ReadLine();

                        departmentRepo.UpdateDepartment(departmentUpdate, newDepartmentName);
                    };
                    Console.Clear();
                }
                else if (option == "4")
                {
                    var departmentRepo = new DepartmentRepository();
                    var allDepartments = departmentRepo.GetAllDepartments();

                    Console.Clear();
                    Console.WriteLine("All Departments");
                    Console.WriteLine("---------------");
                    foreach (var depo in allDepartments)
                    {
                        Console.WriteLine($"{ depo.DeptName}");
                        Console.WriteLine($"ID: {depo.Id}");
                        Console.WriteLine();
                    }
                    Console.WriteLine("---------------");

                    Console.WriteLine();
                    Console.WriteLine("What Department (ID) would you like to delete?");
                    var departmentID = Console.ReadLine();

                    departmentRepo.DeleteDepartment(departmentID);
                    Console.Clear();

                }
                else if (option == "5")
                {
                    var employeeRepo = new EmployeeRepository();
                    var allEmployees = employeeRepo.GetAllEmployees();

                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("All Employees");
                    Console.WriteLine("-----------------");

                    foreach (var employee in allEmployees)
                    {
                        Console.WriteLine($"{employee.FirstName} {employee.LastName}");
                    }
                    Console.WriteLine("---------------");
                    Console.ReadLine();
                    Console.Clear();
                }
                else if (option == "6")
                {
                    var employeeRepo = new EmployeeRepository();
                    var allEmployees = employeeRepo.GetAllEmployeesWithDepartments();

                    Console.Clear();
                    Console.WriteLine("All Employees");
                    Console.WriteLine("-----------------");

                    foreach (var employee in allEmployees)
                    {
                        Console.WriteLine($"Employee ID: {employee.Id}");
                        Console.WriteLine($"First Name: {employee.FirstName}");
                        Console.WriteLine($"Last Name: {employee.LastName}");
                        Console.WriteLine($"Department: {employee.Department.DeptName}");
                        Console.WriteLine();
                    }
                    Console.WriteLine("---------------");
                    Console.ReadLine();
                    Console.Clear();
                }
                else if (option == "7")
                {
                    var departmentRepo = new DepartmentRepository();
                    var allDepartments = departmentRepo.GetAllDepartments();

                    var employeeRepo = new EmployeeRepository();

                    Console.Clear();
                    Console.WriteLine("What is the first name of the new employee?");
                    var employeeFirstName = Console.ReadLine();
                    Console.WriteLine();

                    Console.Clear();
                    Console.WriteLine("What is the last name of the new employee?");
                    var employeeLastName = Console.ReadLine();
                    Console.WriteLine();

                    Console.Clear();
                    Console.WriteLine("All Departments");
                    Console.WriteLine("---------------");
                    foreach (var depo in allDepartments)
                    {
                        Console.WriteLine($"{ depo.DeptName}");
                        Console.WriteLine($"ID: {depo.Id}");
                        Console.WriteLine();
                    }
                    Console.WriteLine("---------------");
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
                    Console.Clear();
                    Console.WriteLine($"{newEmployee.FirstName} {newEmployee.LastName} has been added!");
                    Console.ReadLine();
                    Console.Clear();
                }
                else if (option == "8")
                {
                    var employeeRepo = new EmployeeRepository();
                    Console.Clear();
                    employeeRepo.UpdateEmployee();
                    Console.Clear();
                }
                else if (option == "9")
                {
                    var employeeRepo = new EmployeeRepository();
                    var allEmployees = employeeRepo.GetAllEmployeesWithDepartments();

                    Console.Clear();
                    Console.WriteLine("All Employees");
                    Console.WriteLine("-----------------");

                    foreach (var employee in allEmployees)
                    {
                        Console.WriteLine($"Employee ID: {employee.Id}");
                        Console.WriteLine($"First Name: {employee.FirstName}");
                        Console.WriteLine($"Last Name: {employee.LastName}");
                        Console.WriteLine($"Department: {employee.Department.DeptName}");
                        Console.WriteLine();
                    }
                    Console.WriteLine("---------------");
                    Console.WriteLine();
                    Console.WriteLine("Which employee would you like to delete?");
                    var deleteEmployee = Console.ReadLine();
                    if(deleteEmployee != "")
                    {
                        Console.Clear();
                        Console.WriteLine("Are you sure you want to delete this employee?");
                        Console.WriteLine("1. Yes");
                        Console.WriteLine("2. No");
                        Console.WriteLine();
                        var verify = Console.ReadLine();

                        var deletedEmployee = employeeRepo.GetEmployeeById(Int32.Parse(deleteEmployee));
                        if(verify == "1")
                        {
                            Console.Clear();
                            employeeRepo.DeleteEmployee(deleteEmployee);
                            Console.WriteLine($"You have successfully deleted {deletedEmployee.FirstName} {deletedEmployee.LastName}!");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"{deletedEmployee.FirstName} {deletedEmployee.LastName} was not deleted!");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.Clear();
                    }
                }
                else 
                {
                    optionSelected = false;
                }
            }
        }
    }
}