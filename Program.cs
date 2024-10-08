using System;
using Microsoft.Data.Sqlite;

namespace EmployeeCRUDApp;

class Program
{

    static void Main(string[] args)
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear(); // Clear the console before showing the menu
            Console.WriteLine("\n--- Employee Management ---");
            Console.WriteLine("1. Create Employee");
            Console.WriteLine("2. List Employees");
            Console.WriteLine("3. Update Employee");
            Console.WriteLine("4. Delete Employee");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");
            string option = Console.ReadLine();
            using (var connection = OpenConnection())
            {
                switch (option)
                {
                    case "1":
                        Console.Clear(); // Clear the screen before creating an employee
                        CreateEmployee(connection);
                        break;
                    case "2":
                        Console.Clear(); // Clear the screen before listing employees
                        ListEmployees(connection);
                        break;
                    case "3":
                        Console.Clear(); // Clear the screen before updating an employee
                        UpdateEmployee(connection);
                        break;
                    case "4":
                        Console.Clear(); // Clear the screen before deleting an employee
                        DeleteEmployee(connection);
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
                if (!exit)
                {
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine(); // Pause for user to see the result before clearing
                }
            }
        }
    }

    // Open the SQLite connection
    static SqliteConnection OpenConnection()
    {
        var connection = new SqliteConnection("Data Source=employee.db;");
        connection.Open();
        string createTableQuery = @"CREATE TABLE IF NOT EXISTS Employees (
                EmployeeID INTEGER PRIMARY KEY AUTOINCREMENT,
                FirstName TEXT NOT NULL,
                LastName TEXT NOT NULL,
                DateOfBirth TEXT NOT NULL)";
        using (var command = new SqliteCommand(createTableQuery, connection))
        {
            command.ExecuteNonQuery();
        }
        return connection;
    }
    // Create a new employee
    static void CreateEmployee(SqliteConnection connection)
    {
        Console.Write("Enter First Name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter Last Name: ");
        string lastName = Console.ReadLine();
        Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
        string dob = Console.ReadLine();

        bool result = EmployeeRepository.CreateEmployee(connection, firstName, lastName, dob);

        if (result == true)
        {
            Console.WriteLine("Employee created successfully.");
        }
        else
        {
            Console.WriteLine("Failed to Create Employee.");
        }
    }
    // List all employees
    public static void ListEmployees(SqliteConnection connection)
    {
        string selectQuery = "SELECT * FROM Employees";
        using (var command = new SqliteCommand(selectQuery, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                Console.WriteLine("\n--- Employee List ---");
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["EmployeeID"]}, Name: {reader["FirstName"]}{reader["LastName"]}, DOB: {reader["DateOfBirth"]} ");
                }
            }
        }
    }
    // Update an employee
    static void UpdateEmployee(SqliteConnection connection)
    {
        Console.Write("Enter Employee ID to update: ");
        int employeeID = int.Parse(Console.ReadLine());

        // to-do
        // SELECT * FROM Employees where EmployeeId=@empId
        // executeNonQuery

        Console.Write("Enter new First Name: ");
        string newFirstName = Console.ReadLine();
        Console.Write("Enter new Last Name: ");
        string newLastName = Console.ReadLine();

        bool result = EmployeeRepository.UpdateEmployee(connection, newFirstName, newLastName, employeeID);

        if (result == true)
        {
            Console.WriteLine("Employee updated successfully.");
        }
        else
        {
            Console.WriteLine("Failed to Update Employee.");
        }
    }
    // Delete an employee
    static void DeleteEmployee(SqliteConnection connection)
    {
        Console.Write("Enter Employee ID to delete: ");
        int employeeID = int.Parse(Console.ReadLine());


        // to-do
        // SELECT * FROM Employees where EmployeeId=@empId
        // executeNonQuery

        bool result = EmployeeRepository.DeleteEmployee(connection, employeeID);
        if (result == true)
        {
            Console.WriteLine("Employee Deleted successfully.");
        }
        else
        {
            Console.WriteLine("Failed to Delete Employee.");
        }
    }
}

