using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public enum ProjectCourse
    {
        UMINT,
        SOFTCO,
        APLUI
    }

    public class Project
    {
        public int ProjectID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public ProjectCourse ProjectCourse { get; set; }

        public int StudentID { get; set; }

        public virtual Student Student { get; set; }

        public virtual ICollection<Keyword> Keywords { get; set; }

    }
}