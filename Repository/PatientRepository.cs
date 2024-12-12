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
        #region Save
        public void SavePatient(Patients patient,string filepath)
        {
            var patients = FetchAllPatients(filepath);
            patient.HospitalNumber=patient.HospitalNumber+"-"+(patients.Count+1).ToString();
            patients.Add(patient);
            WritePatientsToCsv(filepath, patients);
        }
        public void SaveAppointment(Appointment appointment, string filepath)
        {
            var appointments = FetchAllAppointments(filepath);          
            appointments.Add(appointment);
            WriteAppointmentsToCsv(filepath, appointments);
        }
        public void SavePatientPrescription(Prescription prescription, string filepath)
        {
            var prescriptions = FetchPrescriptions(filepath);
            prescriptions.Add(prescription);
            WritePrescriptionsToCsv(filepath, prescriptions);
        }
        public void SavePatientNotes(PatientNotes note, string filepath)
        {
            var notes = FetchPatientNotes(filepath);           
            notes.Add(note);
            WritePatientNotesToCsv(filepath, notes);
        }

        #endregion

        #region Fetch
        public List<Patients> FetchAllPatients(string filePath)
        {
          
            var patients = new List<Patients>();
            if (!File.Exists(filePath))
            {
                FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                fs.Close();
            }
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
            
            return patients;
        }
        public List<Prescription> FetchPrescriptions(string filePath)
        {
            var prescriptions = new List<Prescription>();
            if (!File.Exists(filePath))
            {
                FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                fs.Close();
            }
            var lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    var values = lines[i].Split(',');            
                    var prescription = new Prescription
                    {
                        HospitalNumber = values[0],
                        Medicine = values[1],
                        Dosage = values[2],
                        Active=Convert.ToBoolean(values[3])
                    };
                    prescriptions.Add(prescription);
              
                }
           
            return prescriptions;
        }
        public List<Appointment> FetchAllAppointments(string filePath)
        {           
            var appointments = new List<Appointment>();

            if (!File.Exists(filePath))
            {
                FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                fs.Close();
            }

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
            
            return appointments;
        }
        public List<PatientNotes> FetchPatientNotes(string filePath)
        {
            var notes = new List<PatientNotes>();
            if (!File.Exists(filePath))
            {
                FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                fs.Close();
            }
            var lines = File.ReadAllLines(filePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    var values = lines[i].Split(',');
                    var note = new PatientNotes
                    {
                        HospitalNumber = values[0],
                        Notes = values[1],
                        Subject = values[2],
                        Active = Convert.ToBoolean(values[3])
                    };
                    notes.Add(note);
                }
            
            return notes;
        }

        #endregion

        #region Write
        public void WritePatientsToCsv(string filePath, List<Patients> patients)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var patient in patients)
                { writer.WriteLine($"{patient.FirstName},{patient.Surname},{patient.DateOfBirth},{patient.PhoneNumber},{patient.NHSNumber},{patient.HospitalNumber}"); }
            }
        }
        public void WritePrescriptionsToCsv(string filePath, List<Prescription> prescriptions)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var prescription in prescriptions)
                { writer.WriteLine($"{prescription.HospitalNumber},{prescription.Medicine},{prescription.Dosage},{prescription.Active}"); }
            }
        }
        public void WriteAppointmentsToCsv(string filePath, List<Appointment> appointments)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var appointment in appointments)
                {
                    writer.WriteLine($"{appointment.HospitalNumber},{appointment.AppointmentDate},{appointment.AppointmentTime},{appointment.DoctorName},{appointment.Active}");
                }
            }
        }
        public void WritePatientNotesToCsv(string filePath, List<PatientNotes> notes)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var note in notes)
                {
                    writer.WriteLine($"{note.HospitalNumber},{note.Notes},{note.Subject},{note.Active}");
                }
            }
        }

        #endregion
    }
}
 