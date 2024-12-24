using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class clsUserData
    {

        public static bool GetUsersInfoByPasswordandUserName(string Password,
          string UserName, ref int UserID, ref bool IsActeive, ref int PersonID)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from Users
             where Users.UserName=@UserName and Users.Password=@Password and IsActive=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    IsActeive = (bool)reader["IsActive"];
                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        public static bool GetUsersInfoByUserID( int UserID,ref string Password,
        ref string UserName, ref bool IsActeive, ref int PersonID)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from Users
             where UserID=@UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    UserName = (string)reader["UserName"];
                    IsActeive = (bool)reader["IsActive"];
                    Password = (string)reader["Password"];
                    PersonID = (int)reader["PersonID"];
                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static DataTable GetAllUsers()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
SELECT Users.UserID, Users.PersonID, People.FirstName+' '+ People.SecondName+' '+People.ThirdName+
	' '+	People.LastName as FullName,Users.UserName, Users.IsActive
FROM   Users INNER JOIN
             People ON Users.PersonID = People.PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }


        public static int AddNewUser(string UserName, string Password, bool IsActeiv, int PersonID)
        {
            //this function will return the new contact id if succeeded and -1 if not.
            int UserID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[Users]
           ([UserName]
           ,[Password]
           ,[IsActeiv]
           ,[PersonID])
     VALUES
           (@UserName
           ,@Password
           ,@IsActeiv
           ,@PersonID);
           SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActeiv", IsActeiv);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    UserID = insertedID;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return UserID;
        }

        public static bool IsPersonConnectWithUser(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Found=1 FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        public static bool UpdateUser(int UserID, string UserName, string Password, bool IsActive, int PersonID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[Users]
                              SET
                              [PersonID] = @PersonID
                             ,[UserName] = @UserName
                             ,[Password] = @Password
                             ,[IsActive] = @IsActive
                              WHERE UserID=@UserID;";



            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@UserID", UserID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllUsersByUserID(char[] StartWith)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query =  @"
SELECT Users.UserID, Users.PersonID, People.FirstName+' '+ People.SecondName+' '+People.ThirdName+
	' '+	People.LastName as FullName,Users.UserName, Users.IsActive
        FROM   Users INNER JOIN
             People ON Users.PersonID = People.PersonID
WHERE UserID like ''+@StartWith+'%' ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@StartWith", StartWith);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable GetAllUsersByUserName(char[] StartWith)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
SELECT Users.UserID, Users.PersonID, People.FirstName+' '+ People.SecondName+' '+People.ThirdName+
	' '+	People.LastName as FullName,Users.UserName, Users.IsActive
        FROM   Users INNER JOIN
             People ON Users.PersonID = People.PersonID
WHERE UserName like ''+@StartWith+'%' ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@StartWith", StartWith);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable GetAllUsersByFullName(char[] StartWith)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
SELECT Users.UserID, Users.PersonID, People.FirstName+' '+ People.SecondName+' '+People.ThirdName+
	' '+	People.LastName as FullName,Users.UserName, Users.IsActive
        FROM   Users INNER JOIN
             People ON Users.PersonID = People.PersonID
WHERE People.FirstName+' '+ People.SecondName+' '+People.ThirdName+
	' '+	People.LastName like ''+@StartWith+'%' ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@StartWith", StartWith);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable GetAllUsersByIsActive_True_()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
SELECT Users.UserID, Users.PersonID, People.FirstName+' '+ People.SecondName+' '+People.ThirdName+
	' '+	People.LastName as FullName,Users.UserName, Users.IsActive
        FROM   Users INNER JOIN
             People ON Users.PersonID = People.PersonID
WHERE IsActive =1";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable GetAllUsersByPersonID(char[] StartWith)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
SELECT Users.UserID, Users.PersonID, People.FirstName+' '+ People.SecondName+' '+People.ThirdName+
	' '+	People.LastName as FullName,Users.UserName, Users.IsActive
        FROM   Users INNER JOIN
             People ON Users.PersonID = People.PersonID
WHERE People.PersonID like ''+@StartWith+'%' ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@StartWith", StartWith);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable GetAllUsersByIsActive_False_()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
SELECT Users.UserID, Users.PersonID, People.FirstName+' '+ People.SecondName+' '+People.ThirdName+
	' '+	People.LastName as FullName,Users.UserName, Users.IsActive
        FROM   Users INNER JOIN
             People ON Users.PersonID = People.PersonID
WHERE IsActive =0";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool DeleteUser(int UserID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Users
                                where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }


    }
}
