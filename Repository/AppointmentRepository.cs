using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRS.Repository
{
    public class AppointmentRepository
    {

        public void SaveAppointment(Appointment appointment, string filepath)
        {
            var appointments = FetchAllAppointments(filepath);
            //  patient.HospitalNumber = patient.HospitalNumber + "-" + (patients.Count + 1).ToString();
            appointments.Add(appointment);
            WriteUsersToCsv(filepath, appointments);
        }
        public List<Appointment> FetchAllAppointments(string filePath)
        {
            //var filePath = @"C:\temp\PRS\patients.csv";
            var appointments = new List<Appointment>();
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);


                for (int i = 0; i < lines.Length; i++)
                {
                    var values = lines[i].Split(',');

                    var appointment = new Appointment
                    {
                        HospitalNumber = values[0],
                        AppointmentDate = values[1],
                        AppointmentTime = values[2],
                        DoctorName = values[3],
                        Active = Convert.ToBoolean(values[4])
                    };
                    appointments.Add(appointment);
                }
            }
            else
            {
                Console.WriteLine("CSV file not found.");
            }
            return appointments;
        }

        public void WriteUsersToCsv(string filePath, List<Appointment> appointments)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var appointment in appointments)
                {
                    writer.WriteLine($"{appointment.HospitalNumber},{appointment.AppointmentDate},{appointment.AppointmentTime},{appointment.DoctorName},{appointment.Active}");
                }
            }
        }
        //public void SaveAppointment(Appointment appointment, string connectionString)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        //   string query = "INSERT INTO users (UserName, Password,Active) VALUES (@UserName, @Password,@Active)";
        //        string spname = "Sp_InsertAppointment";
        //        using (SqlCommand command = new SqlCommand(spname, connection))
        //        {
        //            command.Parameters.AddWithValue("@FirstName", appointment.FirstName);
        //            command.Parameters.AddWithValue("@Surname", appointment.Surname);
        //            command.Parameters.AddWithValue("@DateOfBirth", appointment.DateOfBirth);
        //            command.Parameters.AddWithValue("@PhoneNumber", appointment.PhoneNumber);
        //            command.Parameters.AddWithValue("@NHSNumber", appointment.NHSNumber);
        //            command.Parameters.AddWithValue("@HospitalNumber", appointment.HospitalNumber);

        //            command.CommandType = CommandType.StoredProcedure;
        //            command.CommandText = spname;


        //            int rowsAffected = command.ExecuteNonQuery();

        //            Console.WriteLine($"{rowsAffected} row(s) inserted.");
        //        }
        //    }
        //}
        //public List<Appointment> FetchAllAppointments(string connectionString)
        //{
        //    List<Appointment> appointments = new List<Appointment>();

        //    Appointment appointment;
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            string spname = "Sp_FetchAllAppointments";
        //            using (SqlCommand command = new SqlCommand(spname, connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = spname;
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        appointment = new Appointment();
        //                        appointment.AppointmentId = reader.GetInt32(0);
        //                        appointment.UserID = reader.GetInt32(1);
        //                        appointment.PatientID = reader.GetInt32(2);
        //                        appointment.AppointmentDate = reader.GetDateTime(3);
        //                        appointment.AppointmentTime = reader.GetDateTime(4);
        //                        appointment.CreatedDate = reader.GetDateTime(5);
        //                        appointment.Active = reader.GetBoolean(6);
        //                        appointments.Add(appointment);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return appointments;
        //}
    }
}
