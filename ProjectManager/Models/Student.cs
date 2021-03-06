﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }

        public string Name
        {
            get { return FirstName + " " + LastName; }
        }
        [Required]
        public string PersonalNumber { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}