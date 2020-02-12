using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public class Year
    {
        public int YearID { get; set; }

        public int YearValue { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}