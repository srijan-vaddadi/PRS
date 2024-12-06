﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRS
{
    public abstract class Users
    {     
        public abstract void DisplayFeatures(User usr,string filepath,string patientFilepath);

        
    }
    public class User
    {
      //  public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public bool Active { get; set; }

    }
    public enum UserType
    {
        Admin = 1,
        Doctor = 2,
        Nurse = 3
    }
}
