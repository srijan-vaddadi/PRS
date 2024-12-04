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
        

        public void SavePatient(Patients patient,string filepath)
        {
            var patients = FetchAllPatients(filepath);
            patient.HospitalNumber=patient.HospitalNumber+"-"+(patients.Count+1).ToString();
            patients.Add(patient);
            WriteUsersToCsv(filepath, patients);
        }
        public List<Patients> FetchAllPatients(string filePath)
        {
            //var filePath = @"C:\temp\PRS\patients.csv";
            var patients = new List<Patients>();
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);


                for (int i = 0; i < lines.Length; i++)
                {
                    var values = lines[i].Split(',');

                    var patient = new Patients
                    {
                        FirstName = values[0],
                        Surname = values[1],
                        DateOfBirth = values[2],
                        PhoneNumber = values[3],
                        NHSNumber= values[4],
                        HospitalNumber=values[5]

                    };

                    patients.Add(patient);
                }



            }
            else
            {
                Console.WriteLine("CSV file not found.");
            }
            return patients;
        }

        public void WriteUsersToCsv(string filePath, List<Patients> patients)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var patient in patients)
                { writer.WriteLine($"{patient.FirstName},{patient.Surname},{patient.DateOfBirth},{patient.PhoneNumber},{patient.NHSNumber},{patient.HospitalNumber}"); }
            }
        }
    }
}
