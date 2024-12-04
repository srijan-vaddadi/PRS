using System;
using System.Collections.Generic;
using System.Data;
using CsvHelper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Globalization;

namespace PRS.Repository
{
    public class UserRepository
    {
      

        public List<User> FetchAllUsers()
        {
            var filePath = @"C:\Users\srija\Srijan\Project\PRS\Data\users.csv";


            var users = new List<User>();


            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);


                for (int i = 0; i < lines.Length; i++)
                {
                    var values = lines[i].Split(',');

                    var user = new User
                    {
                        Username = values[0],
                        Password = values[1],
                        UserType =values[2],
                        Active = Convert.ToBoolean(values[3])
                       

                    };

                    users.Add(user);
                }



            }
            else
            {
                Console.WriteLine("CSV file not found.");
            }
            return users;
        }
        
        public void SaveUser(User user)
        {
            var filePath = @"C:\Users\srija\Srijan\Project\PRS\Data\users.csv";
            var users = FetchAllUsers();

            users.Add(user);

            // Writing to CSV using CsvHelper
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                WriteUsersToCsv(filePath, users);
            }
          }
            public void UpdateUser(User user, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //   string query = "INSERT INTO users (UserName, Password,Active) VALUES (@UserName, @Password,@Active)";
                string spname = "Sp_UpdateUser";
                using (SqlCommand command = new SqlCommand(spname, connection))
                {
                    command.Parameters.AddWithValue("@UserName", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                  //  command.Parameters.AddWithValue("@Active", 1);
                 //   command.Parameters.AddWithValue("@UserType", );
//
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spname;


                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine($"{rowsAffected} row(s) inserted.");
                }
            }
        }

        public void WriteUsersToCsv(string filePath, List<User> users) 
        { using (StreamWriter writer = new StreamWriter(filePath)) 
            { foreach (var user in users) 
                { writer.WriteLine($"{user.Username},{user.Password},{user.UserType},{user.Active}");}
            } 
        }
        public void ChangeUserPassword(User user, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //   string query = "INSERT INTO users (UserName, Password,Active) VALUES (@UserName, @Password,@Active)";
                string spname = "Sp_ChangeUserPassword";
                using (SqlCommand command = new SqlCommand(spname, connection))
                {
                    command.Parameters.AddWithValue("@UserName", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spname;


                   command.ExecuteNonQuery();

                   // Console.WriteLine($"{rowsAffected} row(s) Updated.");
                }
            }
        }
        public void EnableuserAccount(string username, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //   string query = "INSERT INTO users (UserName, Password,Active) VALUES (@UserName, @Password,@Active)";
                string spname = "Sp_EnableuserAccount";
                using (SqlCommand command = new SqlCommand(spname, connection))
                {
                    command.Parameters.AddWithValue("@UserName",username);
                 //   command.Parameters.AddWithValue("@Active", 1);

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spname;


                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine($"{rowsAffected} row(s) inserted.");
                }
            }
        }

        public void DisableuserAccount(string username, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //   string query = "INSERT INTO users (UserName, Password,Active) VALUES (@UserName, @Password,@Active)";
                string spname = "Sp_DisableuserAccount";
                using (SqlCommand command = new SqlCommand(spname, connection))
                {
                    command.Parameters.AddWithValue("@UserName", username);
                    //   command.Parameters.AddWithValue("@Active", 1);

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spname;


                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine($"{rowsAffected} row(s) inserted.");
                }
            }
        }

        public void UpdateUserType(User usr, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //   string query = "INSERT INTO users (UserName, Password,Active) VALUES (@UserName, @Password,@Active)";
                string spname = "Sp_UpdateUserType";
                using (SqlCommand command = new SqlCommand(spname, connection))
                {
                    command.Parameters.AddWithValue("@UserName", usr.Username);
                   // command.Parameters.AddWithValue("@UserType", usr.UserTypeId);
//
                    //   command.Parameters.AddWithValue("@Active", 1);

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spname;


                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine($"{rowsAffected} row(s) inserted.");
                }
            }
        }
        public List<Feature> FetchUserFeatures(string connectionString, string UserTypeId)
        {
            List<Feature> features = new List<Feature>();

            Feature feature;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string spname = "Sp_FetchUserFeatures";
                    using (SqlCommand command = new SqlCommand(spname, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = spname;
                        command.Parameters.Add("@UserTypeId", SqlDbType.Int).Value = 1;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                feature = new Feature();
                                feature.FeatureId = (int)reader["FeatureId"];
                                feature.FeatureName = (string)reader["FeatureName"];
                                features.Add(feature);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return features;
        }
    }
}
