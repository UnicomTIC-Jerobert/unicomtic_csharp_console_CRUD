using Microsoft.Data.Sqlite;

namespace EmployeeCRUDApp
{
    class EmployeeRepository
    {

        public static bool CreateEmployee(SqliteConnection connection, string firstName, string lastName, string dob)
        {

            string insertQuery = "INSERT INTO Employees (FirstName, LastName, DateOfBirth) VALUES(@FirstName, @LastName, @DateOfBirth)";
            using (var command = new SqliteCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@DateOfBirth", dob);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        // public static void ListEmployees(SqliteConnection connection)
        // {
        //     string selectQuery = "SELECT * FROM Employees";
        //     using (var command = new SqliteCommand(selectQuery, connection))
        //     {
        //         using (var reader = command.ExecuteReader())
        //         {

        //         }
        //     }
        // }



        public static bool UpdateEmployee(SqliteConnection connection, string newFirstName, string newLastName, int employeeID)
        {

            string updateQuery = @"UPDATE Employees 
                                    SET FirstName = @FirstName, LastName = @LastName 
                                    WHERE EmployeeID = @EmployeeID";

            using (var command = new SqliteCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@FirstName", newFirstName);
                command.Parameters.AddWithValue("@LastName", newLastName);
                command.Parameters.AddWithValue("@EmployeeID", employeeID);
                int rowsAffected = command.ExecuteNonQuery();
                //Console.Write("rows affected : " + rowsAffected);

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool DeleteEmployee(SqliteConnection connection, int employeeID)
        {


            string deleteQuery = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";

            using (var command = new SqliteCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@EmployeeID", employeeID);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}