using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRS.Repository
{
    public class PatientRepository
    {
        public void SavePatient(Patients patient, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //   string query = "INSERT INTO users (UserName, Password,Active) VALUES (@UserName, @Password,@Active)";
                string spname = "Sp_InsertUser";
                using (SqlCommand command = new SqlCommand(spname, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", patient.FirstName);
                    command.Parameters.AddWithValue("@Surname", patient.Surname);
                    command.Parameters.AddWithValue("@DateOfBirth", patient.DateOfBirth);
                    command.Parameters.AddWithValue("@PhoneNumber", patient.PhoneNumber); 
                    command.Parameters.AddWithValue("@NHSNumber", patient.NHSNumber);
                    command.Parameters.AddWithValue("@HospitalNumber", patient.HospitalNumber);

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spname;


                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine($"{rowsAffected} row(s) inserted.");
                }
            }
        }
        public List<Patients> FetchAllPatients(string connectionString)
        {
            List<Patients> patients = new List<Patients>();

            Patients patient;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string spname = "Sp_FetchAllPatients";
                    using (SqlCommand command = new SqlCommand(spname, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = spname;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                patient = new Patients();
                                patient.HospitalNumber = reader.GetString(0);
                                patient.FirstName = reader.GetString(1);
                                patient.Surname = reader.GetString(2);
                                patient.DateOfBirth = reader.GetDateTime(3);
                                patient.PhoneNumber = reader.GetString(4);
                                patient.NHSNumber = reader.GetString(5);
                                patients.Add(patient);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return patients;
        }
    }
}
