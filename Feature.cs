using PRS.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRS
{
    public class Feature
    {
        public int FeatureId { get; set; }
        public string FeatureName { get; set; }
        public Feature(int id, string name)
        {
            FeatureId = id;
            FeatureName = name;
        }
        public Feature() { }
        public void ExecuteFeature(string selectedFeature, User user, string filePath, string patientFilepath)
        {
            try
            {
                if (selectedFeature != null)
                {
                    switch (selectedFeature)
                    {
                        case "Logout":
                            Logout();
                            Console.ReadLine();
                            break;
                        case "FetchAllUsers":
                            FetchAllUsers(filePath);
                            Console.ReadLine();
                            break;
                        case "AddUser":
                            AddUser(filePath);
                            Console.WriteLine("User Created Successfully");
                            Console.ReadLine();
                            break;
                        case "ChangePassword":
                            ChangePassword(filePath);
                            Console.WriteLine("Password Changed Successfully");
                            Console.ReadLine();
                            break;
                        case "EnableUser":
                            EnableUser(filePath);
                            Console.WriteLine("User Enabled Successfully");
                            Console.ReadLine();
                            break;
                        case "DisableUser":
                            DisableUser(filePath, user);
                            Console.WriteLine("User Disabled Successfully");
                            Console.ReadLine();
                            break;
                        case "UpdateUserType":
                            UpdateUserType(filePath);
                            Console.WriteLine("UserType Updated Successfully");
                            Console.ReadLine();
                            break;
                        case "AddNewPatient":
                            AddNewPatient(patientFilepath);
                            Console.WriteLine("Patient  Created Successfully");
                            Console.ReadLine();
                            break;
                        case "FetchAllPatients":
                            FetchAllPatients(patientFilepath);
                            // Console.WriteLine("Patient  Created Successfully");
                            Console.ReadLine();
                            break;
                        case "SearchPatient":
                            SearchPatient(patientFilepath);
                            // Console.WriteLine("Patient  Created Successfully");
                            Console.ReadLine();
                            break;
                        case "AddAppointment":
                            AddAppointment();
                            Console.WriteLine("Appointment  Added Successfully");
                            Console.ReadLine();
                            break;
                        case "AddPrescription":
                            AddPrescription();
                            Console.WriteLine("Prescription  Added Successfully");
                            Console.ReadLine();
                            break;
                        case "AddPatientNotes":
                            AddPatientNotes();
                            Console.WriteLine("Patient Notes  Added Successfully");
                            Console.ReadLine();
                            break;
                        case "ViewAppointments":
                            ViewAppoinments();
                          //  Console.WriteLine("Patient Notes  Added Successfully");
                            Console.ReadLine();
                            break;
                        case "ViewPrescriptions":
                            ViewPrescriptions();
                            //  Console.WriteLine("Patient Notes  Added Successfully");
                            Console.ReadLine();
                            break;
                        case "ViewPatientNotes":
                            ViewPatientNotes();
                            //  Console.WriteLine("Patient Notes  Added Successfully");
                            Console.ReadLine();
                            break;
                        default:
                            Console.WriteLine("Unknown feature.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Selected feature is not accessible for this user.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        public void Logout()
        {
            Console.WriteLine("LoggedOut Successfully");
        }

        #region User
        public void FetchAllUsers(string filepath)
        {
            UserRepository userrepo = new UserRepository();

            List<User> users = userrepo.FetchAllUsers(filepath);
            Console.WriteLine("Following is the Users List");
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("UserName " + "        |         " + " Enabled " + "       |         " + "UserType");
            Console.WriteLine("-------------------------------------------------------");
            foreach (User user in users)
            {
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine(user.Username + "           |            " + user.Active + "       |         " + user.UserType);
                Console.WriteLine("-------------------------------------------------------");
            }
        }      
        public void AddUser(string filepath)
        {
            UserRepository userrepo = new UserRepository();
            //   string connectionString = "Server=PRECISION-SRIJ\\SQLEXPRESS;Database=PRS;Trusted_Connection=True;";
            User usr = new User();
            Console.WriteLine("Enter UserName");
            usr.Username = Console.ReadLine();
            List<User> users = userrepo.FetchAllUsers(filepath);
            if (users.Find(u => u.Username == usr.Username) == null)
            {

                Console.WriteLine("Enter Password");
                usr.Password = Console.ReadLine();
                // usr.Active = true;
                Console.WriteLine("Select UserType");
                Console.WriteLine("1. Admin");
                Console.WriteLine("2. Doctor");
                Console.WriteLine("3. Nurse");
                string userType = Console.ReadLine();
                switch (userType)
                {
                    case "1":
                        usr.UserType = "Admin";
                        break;

                    case "2":
                        usr.UserType = "Doctor";
                        break;
                    case "3":
                        usr.UserType = "Nurse";
                        break;

                }
                usr.Active = true;
                try
                {
                    userrepo.SaveUser(usr, filepath);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            else
            {
                Console.WriteLine("User " + usr.Username + " already exists");
            }
        }    
        public void ChangePassword(string filepath)
        {
            UserRepository userrepo = new UserRepository();
            // User usr = new User();
            Console.WriteLine("Enter UserName");
            string Username = Console.ReadLine();
            List<User> users = userrepo.FetchAllUsers(filepath);
            User usr = users.Find(u => u.Username == Username);
            if (usr != null)
            {
                Console.WriteLine("Enter new Password");
                usr.Password = Console.ReadLine();
                try
                {
                    userrepo.WriteUsersToCsv(filepath, users);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to Change User Password. Please check the exception. " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("User " + usr.Username + " doesn't exist.");
            }
        }
        public void EnableUser(string filepath)
        {
            // User usr = new User();
            UserRepository userrepo = new UserRepository();
            Console.WriteLine("Enter UserName");
            string Username = Console.ReadLine();
            List<User> users = userrepo.FetchAllUsers(filepath);
            User usr = users.Find(u => u.Username == Username);
            if (usr != null)
            {

                try
                {
                    usr.Active = true;
                    //userrepo.EnableuserAccount(Username, connectionstring);
                    userrepo.WriteUsersToCsv(filepath, users);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to Enable User. Please check the exception. " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("User " + Username + " doesn't exist.");
            }
        }
        public void DisableUser(string filepath, User user)
        {
            UserRepository userrepo = new UserRepository();
            //  User usr = new User();
            Console.WriteLine("Enter UserName");
            string Username = Console.ReadLine();
            if (Username != user.Username)
            {
                List<User> users = userrepo.FetchAllUsers(filepath);
                User usrfound = users.FirstOrDefault(u => u.Username == Username);
                if (usrfound != null)
                {

                    try
                    {
                        usrfound.Active = false;
                        //userrepo.EnableuserAccount(Username, connectionstring);
                        userrepo.WriteUsersToCsv(filepath, users);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Unable to Disable User . Please check the exception. " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("User " + Username + " doesn't exist.");
                }
            }
            else
            {
                Console.WriteLine("User Can't disable his own account");
            }
        }
        public void UpdateUserType(string filepath)
        {
            UserRepository userrepo = new UserRepository();
            //User usr = new User();
            Console.WriteLine("Enter UserName");
            string Username = Console.ReadLine();
            List<User> users = userrepo.FetchAllUsers(filepath);
            User usrfound = users.FirstOrDefault(u => u.Username == Username);
            if (usrfound != null)
            {

                try
                {
                    // usr.Username = Username;
                    Console.WriteLine("Select UserType");
                    Console.WriteLine("1. Admin");
                    Console.WriteLine("2. Doctor");
                    Console.WriteLine("3. Nurse");
                    string userType = Console.ReadLine();
                    switch (userType)
                    {
                        case "1":
                            usrfound.UserType = "Admin";
                            break;

                        case "2":
                            usrfound.UserType = "Doctor";
                            break;
                        case "3":
                            usrfound.UserType = "Nurse";
                            break;

                    }
                    userrepo.WriteUsersToCsv(filepath, users);
                    // userrepo.UpdateUserType(usr, connectionstring);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to Change User Password. Please check the exception. " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("User " + Username + " doesn't exist.");
            }
        }
        #endregion

        #region Patient
        public void FetchAllPatients(string filepath)
        {
            PatientRepository patientrepo = new PatientRepository();
            List<Patients> patients = patientrepo.FetchAllPatients(filepath);
            Console.WriteLine("Following is the Patients List");
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("FirstName " + "        |         " + " Surname " + "       |       " + "DOB" + "       |       " + "Contact");
            Console.WriteLine("-------------------------------------------------------");
            foreach (Patients patient in patients)
            {
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine(patient.FirstName + "        |        " + patient.Surname + "       |         " + patient.DateOfBirth + "       |       " + patient.PhoneNumber);
                Console.WriteLine("-------------------------------------------------------");
            }
        }
        public void AddNewPatient(string filepath)
        {
            Patients patient = new Patients();
            Console.WriteLine("Enter FirstName");
            patient.FirstName = Console.ReadLine();
            Console.WriteLine("Enter Surname");
            patient.Surname = Console.ReadLine();
            Console.WriteLine("Enter DOB");
            patient.DateOfBirth = Console.ReadLine();
            Console.WriteLine("Enter PhoneNumber");
            patient.PhoneNumber = Console.ReadLine();
            Console.WriteLine("Enter NHS Number");
            patient.NHSNumber = Console.ReadLine();
            patient.HospitalNumber = "PRS-" + DateTime.Now.ToString("yyyyMMdd");
            PatientRepository patientrepo = new PatientRepository();
            patientrepo.SavePatient(patient, filepath);
        }
        public void SearchPatient(string filepath)
        {
            PatientRepository patientrepo = new PatientRepository();

            List<Patients> patients = patientrepo.FetchAllPatients(filepath);
            Console.WriteLine("Enter Hopital number of Patient");
            string hospitalnumber = Console.ReadLine();

            Patients patient = patients.Find(u => u.HospitalNumber == hospitalnumber);
            if (patient == null)
            {
                Console.WriteLine("Patient not found");
            }
            else
            {
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("FirstName " + "        |         " + " Surname " + "       |       " + "DOB" + "       |       " + "Contact");
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine(patient.FirstName + "        |        " + patient.Surname + "       |         " + patient.DateOfBirth + "       |       " + patient.PhoneNumber);
                Console.WriteLine("-------------------------------------------------------");


            }
        }
        public void AddAppointment()
        {
            var appointmentfilepath = ConfigurationManager.AppSettings["patientappointmentsfilepath"];
            PatientRepository apprepo = new PatientRepository();
            //List<Appointment> appointments= apprepo.FetchAllAppointments(appointmentfilepath);
            Appointment appoinment = new Appointment();
            Console.WriteLine("Enter Patient Hospital Number");
            appoinment.HospitalNumber = Console.ReadLine();
            Console.WriteLine("Enter Appointment Date");
            appoinment.AppointmentDate = Console.ReadLine();
            Console.WriteLine("Enter Appointment Time");
            appoinment.AppointmentTime = Console.ReadLine();
            Console.WriteLine("Enter Doctor Name");
            appoinment.DoctorName = Console.ReadLine();
            appoinment.Active = true;
            apprepo.SaveAppointment(appoinment, appointmentfilepath);
        }

        public void ViewAppoinments()
        {
            PatientRepository apprepo = new PatientRepository();
            var appointmentfilepath = ConfigurationManager.AppSettings["patientappointmentsfilepath"];
            List<Appointment> appointments = apprepo.FetchAllAppointments(appointmentfilepath);
            Console.WriteLine("Enter Patient Hospital Number");
            string patientnumber = Console.ReadLine();
            List<Appointment> appointment = new List<Appointment>();
            foreach(Appointment  app in appointments.Where(u => u.HospitalNumber == patientnumber).ToList())
            {
                appointment.Add(app);
            }
            if (appointment.Count == 0)
            {
                Console.WriteLine("Don't have any appointments for the given patient.");
            }
            else
            {
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("HospitalNumber " + "        |         " + " AppointmentDate " + "       |       " + "AppointmentTime" + "       |       " + "DoctorName");
                Console.WriteLine("-------------------------------------------------------");
                foreach (var app in appointment)
                {
                    Console.WriteLine("-------------------------------------------------------");
                    Console.WriteLine(app.HospitalNumber + "        |        " + app.AppointmentDate + "       |         " + app.AppointmentTime + "       |       " + app.DoctorName);
                    Console.WriteLine("-------------------------------------------------------");
                }

            }

        }

        public void ViewPrescriptions()
        {
            PatientRepository apprepo = new PatientRepository();
            var prescriptionfilepath = ConfigurationManager.AppSettings["patientprescriptionsfilepath"];
            List<Prescription> prescriptions = apprepo.FetchPrescriptions(prescriptionfilepath);
            Console.WriteLine("Enter Patient Hospital Number");
            string patientnumber = Console.ReadLine();
            List<Prescription> prescription = new List<Prescription>();
            foreach (Prescription app in prescriptions.Where(u => u.HospitalNumber == patientnumber).ToList())
            {
                prescription.Add(app);
            }
            if (prescription.Count == 0)
            {
                Console.WriteLine("Don't have any Prescriptions for the given patient.");
            }
            else
            {
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("HospitalNumber " + "        |         " + " Medicine ");
                Console.WriteLine("-------------------------------------------------------");
                foreach (var app in prescription)
                {
                    Console.WriteLine("-------------------------------------------------------");
                    Console.WriteLine(app.HospitalNumber + "        |        " + app.Medicine );
                    Console.WriteLine("-------------------------------------------------------");
                }

            }

        }

        public void ViewPatientNotes()
        {
            PatientRepository apprepo = new PatientRepository();
            var notesfilepath = ConfigurationManager.AppSettings["patientnotesfilepath"];
            List<PatientNotes> notes = apprepo.FetchPatientNotes(notesfilepath);
            Console.WriteLine("Enter Patient Hospital Number");
            string patientnumber = Console.ReadLine();
            List<PatientNotes> note = new List<PatientNotes>();
            foreach (PatientNotes app in notes.Where(u => u.HospitalNumber == patientnumber).ToList())
            {
                note.Add(app);
            }
            if (note.Count == 0)
            {
                Console.WriteLine("Don't have any Patient Notes for the given patient.");
            }
            else
            {
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("HospitalNumber " + "        |         " + " Notes ");
                Console.WriteLine("-------------------------------------------------------");
                foreach (var app in note)
                {
                    Console.WriteLine("-------------------------------------------------------");
                    Console.WriteLine(app.HospitalNumber + "        |        " + app.Notes);
                    Console.WriteLine("-------------------------------------------------------");
                }

            }

        }
        public void AddPrescription()
        {
            var prescriptionfilepath = ConfigurationManager.AppSettings["patientprescriptionsfilepath"];
            PatientRepository apprepo = new PatientRepository();
            //List<Appointment> appointments= apprepo.FetchAllAppointments(appointmentfilepath);
            Prescription prescription = new Prescription();
            Console.WriteLine("Enter Patient Hospital Number");
            prescription.HospitalNumber = Console.ReadLine();
            Console.WriteLine("Enter Medicine Name");
            prescription.Medicine = Console.ReadLine();          
            apprepo.SavePatientPrescription(prescription, prescriptionfilepath);
        }

        public void AddPatientNotes()
        {
            var notesfilepath = ConfigurationManager.AppSettings["patientnotesfilepath"];
            PatientRepository apprepo = new PatientRepository();
            //List<Appointment> appointments= apprepo.FetchAllAppointments(appointmentfilepath);
            PatientNotes note = new PatientNotes();
            Console.WriteLine("Enter Patient Hospital Number");
            note.HospitalNumber = Console.ReadLine();
            Console.WriteLine("Enter Notes");
            note.Notes = Console.ReadLine();
            apprepo.SavePatientNotes(note, notesfilepath);
        }

        #endregion
    }
}
