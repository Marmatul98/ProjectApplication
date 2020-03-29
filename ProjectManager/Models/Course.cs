using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public class Course
    {
        public int CourseID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

    }
}