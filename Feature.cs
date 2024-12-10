using Microsoft.SqlServer.Server;
using PRS.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public void ExecuteFeature(string selectedFeature, User user)
        {
            try
            {
                if (selectedFeature != null)
                {
                    switch (selectedFeature)
                    {
                        case "Login":
                            Login();
                            break;
                        case "Logout":
                            Logout();
                            Console.ReadLine();
                            break;
                        case "FetchAllUsers":
                            FetchAllUsers();                          
                            break;
                        case "AddUser":
                            AddUser();                          
                            break;
                        case "ChangePassword":
                            ChangePassword();                                                   
                            break;
                        case "EnableUser":
                            EnableUser();                            
                            break;
                        case "DisableUser":
                            DisableUser(user);                                                      
                            break;
                        case "UpdateUserType":
                            UpdateUserType();                                                      
                            break;
                        case "AddNewPatient":
                            AddNewPatient();                                                     
                            break;
                        case "FetchAllPatients":
                            FetchAllPatients();                           
                            break;
                        case "SearchPatient":
                            SearchPatient();                           
                            break;
                        case "AddAppointment":
                            AddAppointment();                                                     
                            break;
                        case "AddPrescription":
                            AddPrescription();                                                      
                            break;
                        case "AddPatientNotes":
                            AddPatientNotes();                     
                            break;
                        case "ViewAppointments":
                            ViewAppoinments();                         
                            break;
                        case "ViewPrescriptions":
                            ViewPrescriptions();                          
                            break;
                        case "ViewPatientNotes":
                            ViewPatientNotes();                          
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

        #region User
        public void Logout()
        {
            Console.WriteLine("LoggedOut Successfully");

            Console.WriteLine("");
            Console.WriteLine("Select your choice");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. PasswordRecovery");
            Console.WriteLine("3. Exit");
            var choice = Console.ReadLine();
          
            switch (choice)
            {
                case "1":
                    Login();
                    break;
                case "2":
                    PasswordRecovery();
                    break;
                case "3":
                    Exit();
                    break;
                default:
                    return;

            }
            
        }

        public void PasswordRecovery()
        {
            var filePath = ConfigurationManager.AppSettings["userfilepath"];
            List<User> users = new List<User>();
            UserRepository userRepository = new UserRepository();
            users = userRepository.FetchAllUsers(filePath);
            Console.WriteLine("Enter Username:");
            string username = Console.ReadLine();
            if (!IsValidEmail(username))
            {
                Console.WriteLine("Please enter a Valid Username(Email)");
                return;
            }
            User usr = users.Find(u => u.Username == username);
            if (usr == null)
            {
                Console.WriteLine("User does not exist");
            }
            else
            {
                Console.WriteLine("Password of the user "+ username + " is "+ usr.Password);
            }

        }

            public void Login()
        {
            var filePath = ConfigurationManager.AppSettings["userfilepath"];
            List<User> users = new List<User>();
            UserRepository userRepository = new UserRepository();
            users = userRepository.FetchAllUsers(filePath);
            if (users != null)
            {
                Console.WriteLine("Enter Username:");
                string username = Console.ReadLine();
                if (!IsValidEmail(username))
                {
                    Console.WriteLine("Please enter a Valid Username(Email)");
                    return;
                }
                User usr = users.Find(u => u.Username == username);

                if (usr == null)
                {
                    Console.WriteLine("User does not exist");
                }
                else
                {
                    Console.WriteLine("Enter Password: ");
                    string password = Console.ReadLine();
                    User usr1 = users.Find(u => u.Username == username && u.Password == password);

                    if (usr1 == null)
                    {
                        Console.WriteLine("Username or password is incorrect");
                    }
                    else if (usr1.Active == false)
                    {
                        Console.WriteLine("Given user is Disabled.Can't login.");
                    }
                    else
                    {
                        switch (usr1.UserType)
                        {
                            case "Admin":
                                Users adm = new Admin();
                                adm.DisplayFeatures(usr1);
                                break;
                            case "Doctor":
                                Users doctor = new Doctor();
                                doctor.DisplayFeatures(usr1);
                                break;
                            case "Nurse":
                                Users nurse = new Nurse();
                                nurse.DisplayFeatures(usr1);
                                break;
                            default:
                                return;
                        }
                    }

                }
            }
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public void Exit()
        {       
            return;
        }
        public void FetchAllUsers()
        {
            UserRepository userrepo = new UserRepository();
            var filepath = ConfigurationManager.AppSettings["userfilepath"];
            List<User> users = userrepo.FetchAllUsers(filepath);
            Console.WriteLine("Following is the Users List");
            Console.WriteLine();
            Console.WriteLine("{0,-20} {1,-20} {2,-10}", "UserName", "Enabled", "UserType");
            Console.WriteLine(new string('-', 50));           
            foreach (User user in users)
            {
                Console.WriteLine("{0,-20} {1,-20} {2,-10}", user.Username, user.Active, user.UserType);
            }
        }      
        public void AddUser()
        {
            UserRepository userrepo = new UserRepository();
            var filepath = ConfigurationManager.AppSettings["userfilepath"];
            User usr = new User();
            Console.WriteLine("Enter UserName");
            string username = Console.ReadLine();
            if (!IsValidEmail(username))
            {
                Console.WriteLine("Email Address that you entered in Invalid");
                return;
            }
            usr.Username= username;
            List<User> users = userrepo.FetchAllUsers(filepath);
            if (users.Find(u => u.Username == usr.Username) == null)
            {
                Console.WriteLine("Enter Password");
                usr.Password = Console.ReadLine();                
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
                    Console.WriteLine("User is Added.");
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            else
            {
                Console.WriteLine("User " + usr.Username + " already exists");
            }
        }    
        public void ChangePassword()
        {
            UserRepository userrepo = new UserRepository();
            var filepath = ConfigurationManager.AppSettings["userfilepath"];
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
                    Console.WriteLine("Password Changed Successfully");
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
        public void EnableUser()
        {           
            UserRepository userrepo = new UserRepository();
            var filepath = ConfigurationManager.AppSettings["userfilepath"];
            Console.WriteLine("Enter UserName");
            string Username = Console.ReadLine();
            List<User> users = userrepo.FetchAllUsers(filepath);
            User usr = users.Find(u => u.Username == Username);
            if (usr != null)
            {
                try
                {
                    usr.Active = true;                    
                    userrepo.WriteUsersToCsv(filepath, users);
                    Console.WriteLine("User Enabled Successfully");
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
        public void DisableUser(User user)
        {
            UserRepository userrepo = new UserRepository();
            var filepath = ConfigurationManager.AppSettings["userfilepath"];
            Console.WriteLine("Enter UserName");
            string Username = Console.ReadLine();
            //Admin user cannot disable own account
            if (Username != user.Username)
            {
                List<User> users = userrepo.FetchAllUsers(filepath);
                User usrfound = users.FirstOrDefault(u => u.Username == Username);
                if (usrfound != null)
                {
                    try
                    {
                        usrfound.Active = false;                        
                        userrepo.WriteUsersToCsv(filepath, users);
                        Console.WriteLine("Account has been disabled");
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
                Console.WriteLine("Admin user cannot disable own account");
            }
        }
        public void UpdateUserType()
        {
            UserRepository userrepo = new UserRepository();
            var filepath = ConfigurationManager.AppSettings["userfilepath"];
            Console.WriteLine("Enter UserName");
            string Username = Console.ReadLine();
            List<User> users = userrepo.FetchAllUsers(filepath);
            User usrfound = users.FirstOrDefault(u => u.Username == Username);
            if (usrfound != null)
            {
                try
                {                   
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
                    Console.WriteLine("UserType Updated Successfully");                 
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
        public void FetchAllPatients()
        {
            PatientRepository patientrepo = new PatientRepository();
            var filepath = ConfigurationManager.AppSettings["patientbasicfilepath"];
            List<Patients> patients = patientrepo.FetchAllPatients(filepath);
            Console.WriteLine("Following is the Patients List");
            Console.WriteLine();
            Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-15} {4,-15} {5,-15}", "FirstName", "Surname", "DateOfBirth", "PhoneNumber","NHSNumber","HospitalNumber");
            Console.WriteLine(new string('-', 75));
            foreach (Patients patient in patients)
            {
                Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-15} {4,-15} {5,-15}", patient.FirstName, patient.Surname, patient.DateOfBirth, patient.PhoneNumber, patient.NHSNumber,patient.HospitalNumber);              
            }
        }
        public void AddNewPatient()
        {
            Patients patient = new Patients();
            var filepath = ConfigurationManager.AppSettings["patientbasicfilepath"];
            Console.WriteLine("Enter FirstName");
            patient.FirstName = Console.ReadLine();
            Console.WriteLine("Enter Surname");
            patient.Surname = Console.ReadLine();
            Console.WriteLine("Enter DOB in (MM-DD-YYYY) format");
            patient.DateOfBirth = Console.ReadLine();
            Console.WriteLine("Enter PhoneNumber");
            patient.PhoneNumber = Console.ReadLine();
            Console.WriteLine("Enter NHS Number");
            patient.NHSNumber = Console.ReadLine();
            patient.HospitalNumber = "PRS-" + DateTime.Now.ToString("yyyyMMdd");
            PatientRepository patientrepo = new PatientRepository();
            patientrepo.SavePatient(patient, filepath);
            Console.WriteLine("Patient  Created Successfully");
        }
        public void SearchPatient()
        {
            PatientRepository patientrepo = new PatientRepository();
            var filepath = ConfigurationManager.AppSettings["patientbasicfilepath"];
            List<Patients> patients = patientrepo.FetchAllPatients(filepath);
            Patients patient;
            Console.WriteLine("Select one of the below options that you want to search");
            Console.WriteLine("1. Firstname");
            Console.WriteLine("2. Surname");
            Console.WriteLine("3. DOB");
            Console.WriteLine("4. NHS Number");
            Console.WriteLine("5. Hospital Number");
            Console.WriteLine("6. Phone Number");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter First Name of Patient");
                    string firstname = Console.ReadLine();
                    patient = patients.Find(u => u.FirstName.ToLower() == firstname.ToLower());
                    break;
                case "2":
                    Console.WriteLine("Enter Surname of Patient");
                    string surname = Console.ReadLine();
                    patient = patients.Find(u => u.Surname.ToLower() == surname.ToLower());
                    break;
                case "3":
                    Console.WriteLine("Enter Date of Birth(MM-DD-YYYY) of Patient");
                    string dob = Console.ReadLine();
                    patient = patients.Find(u => u.DateOfBirth.ToLower() == dob.ToLower());
                    break;
                case "4":
                    Console.WriteLine("Enter NHS Number of Patient");
                    string nhsnumber = Console.ReadLine();
                    patient = patients.Find(u => u.NHSNumber.ToLower() == nhsnumber.ToLower());
                    break;
                case "5":
                    Console.WriteLine("Enter Hospital Number of Patient");
                    string hosnumber = Console.ReadLine();
                    patient = patients.Find(u => u.HospitalNumber.ToLower() == hosnumber.ToLower());
                    break;
                case "6":
                    Console.WriteLine("Enter Phone Number of Patient");
                    string phonenumber = Console.ReadLine();
                    patient = patients.Find(u => u.PhoneNumber == phonenumber);
                    break;
                default:
                    return;

            }       
            
            if (patient == null)
            {
                Console.WriteLine("Patient not found");
            }
            else
            {
                Console.WriteLine("Following is the Patient Detail");
                Console.WriteLine();
                Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-15} {4,-15} {5,-15}", "FirstName", "Surname", "DateOfBirth", "PhoneNumber", "NHSNumber", "HospitalNumber");
                Console.WriteLine(new string('-', 75));
                Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-15} {4,-15} {5,-15}", patient.FirstName, patient.Surname, patient.DateOfBirth, patient.PhoneNumber, patient.NHSNumber, patient.HospitalNumber);
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
            Console.WriteLine("Please enter the appointment date (format: yyyy-MM-dd):");
            DateTime appointmentdate ;
            if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out appointmentdate))
            {
                appoinment.AppointmentDate = appointmentdate.ToString("yyyy-MM-dd");
            }
            else
            {
                Console.WriteLine("Invalid date format. Please ensure the date is in the format yyyy-MM-dd.");
                return;
            }           
            Console.WriteLine("Please enter the appointment time (format: HH:mm, 24-hour format):");
            DateTime appointmenttime;
            string format = "HH:mm"; // 24-hour format
            if (DateTime.TryParseExact(Console.ReadLine(), format, null, System.Globalization.DateTimeStyles.None, out appointmenttime))
            {
                appoinment.AppointmentTime = appointmenttime.ToString(format);
            }
            else
            {
                Console.WriteLine("Invalid time format. Please ensure the time is in the format HH:mm (24-hour format).");
                return;
            }            
            Console.WriteLine("Enter Doctor Name");
            appoinment.DoctorName = Console.ReadLine();
            appoinment.Active = true;
            apprepo.SaveAppointment(appoinment, appointmentfilepath);
            Console.WriteLine("Appointment  Added Successfully");
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
                Console.WriteLine("Following are the Appointment Details");
                Console.WriteLine();
                Console.WriteLine("{0,-20} {1,-20} {2,-10} {3,-10} {4,-10}", "HospitalNumber", "AppointmentDate", "AppointmentTime", "DoctorName","Active");
                Console.WriteLine(new string('-', 70));
                foreach (var app in appointment)
                {
                    Console.WriteLine("{0,-20} {1,-20} {2,-10} {3,-10} {4,-10}", app.HospitalNumber, app.AppointmentDate, app.AppointmentTime, app.DoctorName,app.Active);
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
                Console.WriteLine("Following are the Prescription Details");
                Console.WriteLine();
                Console.WriteLine("{0,-20} {1,-20}", "HospitalNumber", "Medicine");
                Console.WriteLine(new string('-', 40));           
                foreach (var app in prescription)
                {
                    Console.WriteLine("{0,-20} {1,-20}", app.HospitalNumber,app.Medicine);
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
                Console.WriteLine("Following are the Patient Notes");
                Console.WriteLine();
                Console.WriteLine("{0,-20} {1,-20}", "HospitalNumber", "Notes");
                Console.WriteLine(new string('-', 40));
                foreach (var app in note)
                {
                    Console.WriteLine("{0,-20} {1,-20}", app.HospitalNumber, app.Notes);
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
            Console.WriteLine("Prescription  Added Successfully");
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
            Console.WriteLine("Patient Notes  Added Successfully");
        }

        #endregion
    }
}
