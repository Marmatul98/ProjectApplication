using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public class Project
    {
        
        public int ProjectID { get; set; }
        
        public string Title { get; set; }
   
        public string Description { get; set; }
       
        public int YearID { get; set; }

        public virtual Year Year { get; set; }
        
        public int CourseID { get; set; }
        public virtual Course Course { get; set; }

        public int StudentID { get; set; }
 
        public virtual Student Student { get; set; }

        public virtual ICollection<Keyword> Keywords { get; set; }
    }
}