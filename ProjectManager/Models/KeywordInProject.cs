using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public class KeywordInProject
    {
        public int KeywordInProjectID { get; set; }
        public int KeywordID { get; set; }

        public int ProjectID { get; set; }

        public virtual Keyword Keyword { get; set; }

        public virtual Project Project { get; set; }

    }
}