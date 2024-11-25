using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRS
{
    public class Nurse : Users
    {
        public override void DisplayFeatures(string connectionstring, UserType UserTypeId)
        {
            Console.WriteLine("Nurse Features:");
            Console.WriteLine("1. Add User");
            Console.WriteLine("2. View Users");
        }
    }
}
