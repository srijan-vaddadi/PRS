using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PRS.Repository
{
    public class UserRepository
    {
        public List<User> FetchAllUsers(string connectionString)
        {
            List<User> users = new List<User>();

            User user;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string spname = "Sp_FetchAllUsers";
                    using (SqlCommand command = new SqlCommand(spname, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = spname;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                user = new User();
                                user.UserID = reader.GetInt32(0);
                                user.Username = reader.GetString(1);
                                user.UserTypeId = (UserType)reader["UserTypeId"];
                                user.Active = (bool)reader["Active"];
                                user.Password = reader["Password"].ToString();
                                users.Add(user);
                            }
                        }
                    }
                }

                // Output the users or process them as needed
                //foreach (var user1 in users)
                //{
                //    Console.WriteLine($"Id: {user1.UserID}, Name: {user1.UserName}, IsActive: {user1.IsActive},UserTypeID: {user1.UserTypeID}");
                //}

            }
            catch (Exception ex)
            {
                throw;
            }
            //Console.ReadLine();
            return users;
        }
        public void SaveUser(User user, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //   string query = "INSERT INTO users (UserName, Password,Active) VALUES (@UserName, @Password,@Active)";
                string spname = "Sp_InsertUser";
                using (SqlCommand command = new SqlCommand(spname, connection))
                {
                    command.Parameters.AddWithValue("@UserName", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Active", 1);
                    command.Parameters.AddWithValue("@UserType", user.UserTypeId);

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spname;


                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine($"{rowsAffected} row(s) inserted.");
                }
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


                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine($"{rowsAffected} row(s) inserted.");
                }
            }
        }
        public void EnableOrDisableAccount(User user, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //   string query = "INSERT INTO users (UserName, Password,Active) VALUES (@UserName, @Password,@Active)";
                string spname = "Sp_EnableOrDisableAccount";
                using (SqlCommand command = new SqlCommand(spname, connection))
                {
                    command.Parameters.AddWithValue("@UserName", user.Username);
                    command.Parameters.AddWithValue("@Active", user.Active);

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spname;


                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine($"{rowsAffected} row(s) inserted.");
                }
            }
        }
        public List<Feature> FetchUserFeatures(string connectionString, UserType UserTypeId)
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
                        command.Parameters.Add("@UserTypeId", SqlDbType.Int).Value=(int)UserTypeId;
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
