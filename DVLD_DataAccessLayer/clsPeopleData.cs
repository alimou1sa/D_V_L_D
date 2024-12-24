using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccessLayer
{
    public class clsPeopleData
    {

        public static bool GetPersonInfoByID(int PersonID, ref string FirstName, ref string LastName, ref string SecondName, ref string ThirdName,
            ref string Email, ref string Phone, ref string Address, ref DateTime DateOfBirth,
           ref int NationalityCountryID, ref string ImagePath, ref byte Gendor, ref string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * from People
                 WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    SecondName = (string)reader["SecondName"];
                    Gendor = (byte)reader["Gendor"];
                    NationalNo = (string)reader["NationalNo"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    PersonID = (int)reader["PersonID"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    //ImagePath: allows null in database so we should handle null
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }

                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }

                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }
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

        public static int AddNewPerson(string FirstName, string LastName, string SecondName,
            string ThirdName, string Email, string Phone, string Address, DateTime DateOfBirth,
          int NationalityCountryID, string ImagePath, byte Gendor, string NationalNo)
        {

            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[People]
           ([NationalNo]
           ,[FirstName]
           ,[SecondName]
           ,[ThirdName]
           ,[LastName]
           ,[DateOfBirth]
           ,[Gendor]
           ,[Address]
           ,[Phone]
           ,[Email]
           ,[NationalityCountryID]
           ,[ImagePath])
     VALUES
           (@NationalNo
           ,@FirstName
           ,@SecondName
           ,@ThirdName
           ,@LastName
           ,@DateOfBirth
           ,@Gendor
           ,@Address
           ,@Phone
           ,@Email
           ,@NationalityCountryID
           ,@ImagePath);
             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            if (ThirdName != "" && ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PersonID = insertedID;
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


            return PersonID;
        }

        public static bool UpdatePerson(int PersonID, string FirstName, string LastName, string SecondName,
            string ThirdName, string Email, string Phone, string Address, DateTime DateOfBirth,
          int NationalityCountryID, string ImagePath, byte Gendor, string NationalNo)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[People]
       SET [NationalNo] = @NationalNo,
      [FirstName] = @FirstName,
      [SecondName] = @SecondName,
      [ThirdName] = @ThirdName,
      [LastName] = @LastName,
      [DateOfBirth] = @DateOfBirth,
      [Gendor] = @Gendor,
      [Address] = @Address,
      [Phone] = @Phone,
      [Email] = @Email,
      [NationalityCountryID] = @NationalityCountryID,
      [ImagePath] = @ImagePath
        WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);


            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);


            if (ThirdName != "" && ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);


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

        public static DataTable GetAllPeople()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
SELECT People.PersonID, People.NationalNo, People.FirstName,
                People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,  
				CASE WHEN People.Gendor = 0 THEN 'Male' WHEN People.Gendor =1
			 THEN 'Female' END AS Gendor,
                 Countries.CountryName,People.Address, People.Phone, People.Email 
                FROM   People INNER JOIN
                    Countries ON People.NationalityCountryID = Countries.CountryID   
     ";

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

        public static bool DeletePeople(int PersonID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete People 
                                where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static bool IsPeopleExistbyPersonID(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE PersonID = @PersonID";

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

        public static DataTable GetAllPeopleByFirstName(char[] StartWith)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName,
                People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,
               		CASE WHEN People.Gendor = 0 THEN 'Male' WHEN People.Gendor =1
			 THEN 'Female' END AS Gendor, Countries.CountryName,People.Address, People.Phone, People.Email 
                FROM   People INNER JOIN
                    Countries ON People.NationalityCountryID = Countries.CountryID   where FirstName like ''+@StartWith+'%'";

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

        public static DataTable GetAllPeopleByLastName( char[] StartWith)
        {
            
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName,
                People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,
               		CASE WHEN People.Gendor = 0 THEN 'Male' WHEN People.Gendor =1
			 THEN 'Female' END AS Gendor, Countries.CountryName,People.Address, People.Phone, People.Email 
                FROM   People INNER JOIN
                    Countries ON People.NationalityCountryID = Countries.CountryID    where LastName like ''+@StartWith+'%'";

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

        public static DataTable GetAllPeopleBySecondName(char[] StartWith)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName,
                People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,
                 		CASE WHEN People.Gendor = 0 THEN 'Male' WHEN People.Gendor =1
			 THEN 'Female' END AS Gendor, Countries.CountryName,People.Address, People.Phone, People.Email 
                FROM   People INNER JOIN
                    Countries ON People.NationalityCountryID = Countries.CountryID    where SecondName like ''+@StartWith+'%'";

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

        public static DataTable GetAllPeopleByThirdName(char[] StartWith)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName,
                People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,
               		CASE WHEN People.Gendor = 0 THEN 'Male' WHEN People.Gendor =1
			 THEN 'Female' END AS Gendor, Countries.CountryName,People.Address, People.Phone, People.Email 
                FROM   People INNER JOIN
                    Countries ON People.NationalityCountryID = Countries.CountryID   
where ThirdName like ''+@StartWith+'%'";

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

        public static DataTable GetAllPeopleByAddress(char[] StartWith)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName,
                People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,
          		CASE WHEN People.Gendor = 0 THEN 'Male' WHEN People.Gendor =1
			 THEN 'Female' END AS Gendor, Countries.CountryName,People.Address, People.Phone, People.Email 
                FROM   People INNER JOIN
                    Countries ON People.NationalityCountryID = Countries.CountryID   
where Address like ''+@StartWith+'%'";

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

        public static DataTable GetAllPeopleByGendor(byte Gendor)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName,
                People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,
               		CASE WHEN People.Gendor = 0 THEN 'Male' WHEN People.Gendor =1
			 THEN 'Female' END AS Gendor,Countries.CountryName,People.Address, People.Phone, People.Email 
                FROM   People INNER JOIN
                    Countries ON People.NationalityCountryID = Countries.CountryID   
              where People.Gendor  = @Gendor";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Gendor", Gendor);

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

        public static DataTable GetAllPeopleByNationalNo(char[] StartWith)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName,
                People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,
               		CASE WHEN People.Gendor = 0 THEN 'Male' WHEN People.Gendor =1
			 THEN 'Female' END AS Gendor, Countries.CountryName,People.Address, People.Phone, People.Email 
                FROM   People INNER JOIN
                    Countries ON People.NationalityCountryID = Countries.CountryID   where NationalNo like ''+@StartWith+'%'";

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

        public static bool IsPeopleExistbyNationalNo(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

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

        public static DataTable GetAllPeopleByPersonID(char[] StartWith)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName,
                People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,
           		CASE WHEN People.Gendor = 0 THEN 'Male' WHEN People.Gendor =1
			 THEN 'Female' END AS Gendor, Countries.CountryName,People.Address, People.Phone, People.Email 
                FROM   People INNER JOIN
                    Countries ON People.NationalityCountryID = Countries.CountryID
                 WHERE PersonID like ''+@StartWith+'%' ";

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

        public static DataTable GetAllPeopleByEmail(char[] StartWith)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName,
                People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,
              		CASE WHEN People.Gendor = 0 THEN 'Male' WHEN People.Gendor =1
			 THEN 'Female' END AS Gendor, Countries.CountryName,People.Address, People.Phone, People.Email 
                FROM   People INNER JOIN
                    Countries ON People.NationalityCountryID = Countries.CountryID 
where Email like ''+@StartWith+'%'";

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

        public static DataTable GetAllPeopleByPhone(char[] StartWith)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName,
                People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,
              		CASE WHEN People.Gendor = 0 THEN 'Male' WHEN People.Gendor =1
			 THEN 'Female' END AS Gendor, Countries.CountryName,People.Address, People.Phone, People.Email 
                FROM   People INNER JOIN
                    Countries ON People.NationalityCountryID = Countries.CountryID   
where Phone like ''+@StartWith+'%'";

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

        public static DataTable GetAllPeopleByNationalty(char[] StartWith)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName,
                People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,
           		CASE WHEN People.Gendor = 0 THEN 'Male' WHEN People.Gendor =1
			 THEN 'Female' END AS Gendor, Countries.CountryName,People.Address, People.Phone, People.Email 
                FROM   People INNER JOIN
                    Countries ON People.NationalityCountryID = Countries.CountryID   
where CountryName like ''+@StartWith+'%'";

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

        public static bool GetFirstPersonInfo(ref int PersonID, ref string FirstName, ref string LastName, ref string SecondName, ref string ThirdName,
         ref string Email, ref string Phone, ref string Address, ref DateTime DateOfBirth,
         ref int NationalityCountryID, ref string ImagePath, ref byte Gendor, ref string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * from People
                 WHERE PersonID =(Select top(1) PersonID from People )";

            SqlCommand command = new SqlCommand(query, connection);

           // command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    PersonID=(int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    SecondName = (string)reader["SecondName"];
                    Gendor = (byte)reader["Gendor"];
                    NationalNo = (string)reader["NationalNo"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    PersonID = (int)reader["PersonID"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    //ImagePath: allows null in database so we should handle null
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }

                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }

                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }
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


        public static bool GetPersonInfoByNationalNo( string NationalNo,ref int PersonID, ref string FirstName, ref string LastName, ref string SecondName, ref string ThirdName,
            ref string Email, ref string Phone, ref string Address, ref DateTime DateOfBirth,
           ref int NationalityCountryID, ref string ImagePath, ref byte Gendor)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * from People
                 WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    SecondName = (string)reader["SecondName"];
                    Gendor = (byte)reader["Gendor"];
                    NationalNo = (string)reader["NationalNo"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    PersonID = (int)reader["PersonID"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    //ImagePath: allows null in database so we should handle null
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }

                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }

                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }
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


    }
}
