using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.ViewModels
{
    public class AssignedKeyword
    {
        public int KeywordID { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }
}