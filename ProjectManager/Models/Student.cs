using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Name
        {
            get { return FirstName + " " + LastName; }
        }
        public string PersonalNumber { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}