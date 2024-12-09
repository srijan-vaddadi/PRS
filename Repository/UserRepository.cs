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
      

        public List<User> FetchAllUsers(string filePath)
        {
          


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
        
        public void SaveUser(User user,string filepath)
        {
        
            var users = FetchAllUsers(filepath);
            users.Add(user);
                WriteUsersToCsv(filepath, users);
          }
            public void UpdateUser(User user, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();               
                string spname = "Sp_UpdateUser";
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

        public void WriteUsersToCsv(string filePath, List<User> users) 
        { using (StreamWriter writer = new StreamWriter(filePath)) 
            { foreach (var user in users) 
                { writer.WriteLine($"{user.Username},{user.Password},{user.UserType},{user.Active}");}
            } 
        }     
    }
}
