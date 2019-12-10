using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using Departments_and_Employees.Models;

namespace Departments_and_Employees.Data
{
    public class EmployeeRepository
    {
        /// <summary>
        ///  Represents a connection to the database.
        ///   This is a "tunnel" to connect the application to the database.
        ///   All communication between the application and database passes through this connection.
        /// </summary>
        public SqlConnection Connection
        {
            get
            {
                // This is "address" of the database
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=DepartmentsEmployees;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }

        /// <summary>
        ///  Returns a list of all departments in the database
        /// </summary>
        public List<Employee> GetAllEmployees()
        {
            //  We must "use" the database connection.
            //  Because a database is a shared resource (other applications may be using it too) we must
            //  be careful about how we interact with it. Specifically, we Open() connections when we need to
            //  interact with the database and we Close() them when we're finished.
            //  In C#, a "using" block ensures we correctly disconnect from a resource even if there is an error.
            //  For database connections, this means the connection will be properly closed.
            using (SqlConnection conn = Connection)
            {
                // Note, we must Open() the connection, the "using" block   doesn't do that for us.
                conn.Open();

                // We must "use" commands too.
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // Here we setup the command with the SQL we want to execute before we execute it.
                    cmd.CommandText = "SELECT Id, FirstName, LastName, DepartmentId FROM Employee";

                    // Execute the SQL in the database and get a "reader" that will give us access to the data.
                    SqlDataReader reader = cmd.ExecuteReader();

                    // A list to hold the departments we retrieve from the database.
                    List<Employee> employees = new List<Employee>();

                    // Read() will return true if there's more data to read
                    while (reader.Read())
                    {
                        // The "ordinal" is the numeric position of the column in the query results.
                        //  For our query, "Id" has an ordinal value of 0 and "DeptName" is 1.
                        int idColumnPosition = reader.GetOrdinal("Id");

                        // We user the reader's GetXXX methods to get the value for a particular ordinal.
                        int idValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);

                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);

                        int departmentIdColumnPosition = reader.GetOrdinal("DepartmentId");
                        int departmentIdValue = reader.GetInt32(departmentIdColumnPosition);


                        // Now let's create a new department object using the data from the database.
                        Employee employee = new Employee
                        {
                            Id = idValue,
                            FirstName = firstNameValue,
                            LastName = lastNameValue,
                            DepartmentId = departmentIdValue
                        };

                        // ...and add that department object to our list.
                        employees.Add(employee);
                    }

                    // We should Close() the reader. Unfortunately, a "using" block won't work here.
                    reader.Close();

                    // Return the list of departments who whomever called this method.
                    return employees;
                }
            }
        }

        public List<Employee> GetAllEmployeesWithDepartments()
        {
            //  We must "use" the database connection.
            //  Because a database is a shared resource (other applications may be using it too) we must
            //  be careful about how we interact with it. Specifically, we Open() connections when we need to
            //  interact with the database and we Close() them when we're finished.
            //  In C#, a "using" block ensures we correctly disconnect from a resource even if there is an error.
            //  For database connections, this means the connection will be properly closed.
            using (SqlConnection conn = Connection)
            {
                // Note, we must Open() the connection, the "using" block   doesn't do that for us.
                conn.Open();

                // We must "use" commands too.
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // Here we setup the command with the SQL we want to execute before we execute it.
                    cmd.CommandText = "SELECT Id, FirstName, LastName, DepartmentId FROM Employee";

                    // Execute the SQL in the database and get a "reader" that will give us access to the data.
                    SqlDataReader reader = cmd.ExecuteReader();

                    // A list to hold the departments we retrieve from the database.
                    List<Employee> employees = new List<Employee>();

                    // Read() will return true if there's more data to read
                    while (reader.Read())
                    {
                        // The "ordinal" is the numeric position of the column in the query results.
                        //  For our query, "Id" has an ordinal value of 0 and "DeptName" is 1.
                        int idColumnPosition = reader.GetOrdinal("Id");

                        // We user the reader's GetXXX methods to get the value for a particular ordinal.
                        int idValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);

                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);

                        int departmentIdColumnPosition = reader.GetOrdinal("DepartmentId");
                        int departmentIdValue = reader.GetInt32(departmentIdColumnPosition);

                        var employeeDepartmentRepo = new DepartmentRepository();
                        var employeeDepartment = employeeDepartmentRepo.GetDepartmentById(departmentIdValue);

                        // Now let's create a new department object using the data from the database.
                        Employee employee = new Employee
                        {
                            Id = idValue,
                            FirstName = firstNameValue,
                            LastName = lastNameValue,
                            DepartmentId = departmentIdValue,
                            Department = employeeDepartment
                        };

                        // ...and add that department object to our list.
                        employees.Add(employee);
                    }

                    // We should Close() the reader. Unfortunately, a "using" block won't work here.
                    reader.Close();

                    // Return the list of departments who whomever called this method.
                    return employees;
                }
            }
        }
        /// <summary>
        ///  Returns a single department with the given id.
        /// </summary>
        public Employee GetEmployeeById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, FirstName, LastName, DepartmentId FROM Employee WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Employee employee = null;

                    // If we only expect a single row back from the database, we don't need a while loop.
                    if (reader.Read())
                    {
                    var employeeDepartmentRepo = new DepartmentRepository();
                        employee = new Employee
                        {
                            Id = id,
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                            Department = employeeDepartmentRepo.GetDepartmentById(reader.GetInt32(reader.GetOrdinal("DepartmentId")))
                        };
                    };

                    reader.Close();

                    return employee;
                }
            }
        }

        public void AddEmployee(Employee employee)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // These SQL parameters are annoying. Why can't we use string interpolation?
                    // ... sql injection attacks!!!
                    cmd.CommandText = "INSERT INTO Employee (FirstName, LastName, DepartmentId) OUTPUT INSERTED.Id Values (@FirstName, @LastName, @DepartmentId)";
                    cmd.Parameters.Add(new SqlParameter("@FirstName", employee.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", employee.LastName));
                    cmd.Parameters.Add(new SqlParameter("@DepartmentId", employee.DepartmentId));
                    int id = (int)cmd.ExecuteScalar();

                    employee.Id = id;
                }
            }

            // when this method is finished we can look in the database and see the new department.
        }
        public void UpdateEmployee()
        {
            //string id, string employeeFirstName, string employeeLastName, string employeeDepartmentId


            Console.WriteLine("--------------");


            var employeeRepo = new EmployeeRepository();
            var allEmployees = employeeRepo.GetAllEmployees();

            foreach (var employee in allEmployees)
            {
                Console.WriteLine($"{employee.Id}. {employee.FirstName} {employee.LastName}");
            }
            Console.WriteLine("--------------");
            Console.WriteLine();
            Console.WriteLine("What employee would you like to edit?");
            Console.WriteLine();
            var id = Console.ReadLine();

            while (id != "")
            {
                Console.Clear();
                Console.WriteLine("What would you like to update?");
                Console.WriteLine();
                Console.WriteLine("1. First Name");
                Console.WriteLine("2. Last Name");
                Console.WriteLine("3. Department");
                Console.WriteLine();

                var option = Console.ReadLine();

                var parsedId = Int32.Parse(id);
                if(option == "1")
                {
                    Console.Clear();
                    Console.WriteLine("Edit First Name");

                    var employeeUpdatedFirstName = Console.ReadLine();
                    if (employeeUpdatedFirstName != "")
                    {

                        var updatingEmployee = new Employee() { FirstName = employeeUpdatedFirstName };

                        using (SqlConnection conn = Connection)
                        {
                            conn.Open();
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = @"UPDATE Employee
                                             SET FirstName = @firstName
                                             WHERE Id = @id";
                                cmd.Parameters.Add(new SqlParameter("@firstName", updatingEmployee.FirstName));
                                cmd.Parameters.Add(new SqlParameter("@id", parsedId));

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                else if(option == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Edit Last Name");

                    var employeeUpdatedLastName = Console.ReadLine();

                    if (employeeUpdatedLastName != "")
                    {

                        var updatingEmployee = new Employee() { LastName = employeeUpdatedLastName };

                        using (SqlConnection conn = Connection)
                        {
                            conn.Open();
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = @"UPDATE Employee
                                             SET FirstName = @lastName
                                             WHERE Id = @id";
                                cmd.Parameters.Add(new SqlParameter("@lastName", updatingEmployee.LastName));
                                cmd.Parameters.Add(new SqlParameter("@id", parsedId));

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                else if (option == "3")
                {
                    Console.Clear();
                    var departmentRepo = new DepartmentRepository();

                        var allDepartments = departmentRepo.GetAllDepartments();

                        foreach (var depo in allDepartments)
                        {
                            Console.WriteLine($"{depo.Id}. {depo.DeptName}");
                        }
                        Console.WriteLine();
                        Console.WriteLine("What department do you want to move them to?");

                        var newId = Console.ReadLine();
                    
                    if (newId != "")
                    {
                        var employeeUpdatedDepartmentId = Int32.Parse(newId);
                        var updatingEmployee = new Employee() { DepartmentId = employeeUpdatedDepartmentId };

                        using (SqlConnection conn = Connection)
                        {
                            conn.Open();
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = @"UPDATE Employee
                                         SET DepartmentId = @departmentId
                                         WHERE Id = @id";
                                cmd.Parameters.Add(new SqlParameter("@departmentId", updatingEmployee.DepartmentId));
                                cmd.Parameters.Add(new SqlParameter("@id", parsedId));

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                else
                {
                    break;
                }
            };
        }
        public void UpdateDepartmentId(string id)
        {
            Console.Clear();
            Console.WriteLine("Edit Department ID");

            if(id != "")
            {
                var parsedId = Int32.Parse(id);

                var updatingEmployee = new Employee() { DepartmentId = parsedId };

                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE Employee
                                             SET DepartmentId = @departmentId
                                             WHERE Id = @id";
                        cmd.Parameters.Add(new SqlParameter("@departmentId", updatingEmployee.DepartmentId));
                        cmd.Parameters.Add(new SqlParameter("@id", parsedId));

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public void DeleteEmployee(string id)
        {
            if (id != "")
            {
                var parsedEmployeeId = Int32.Parse(id);
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM Employee WHERE Id = @id";
                        cmd.Parameters.Add(new SqlParameter("@id", parsedEmployeeId));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
