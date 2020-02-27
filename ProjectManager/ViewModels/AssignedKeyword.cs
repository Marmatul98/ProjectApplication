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

        public override bool Equals(object obj)
        {
            return obj is AssignedKeyword keyword &&
                   KeywordID == keyword.KeywordID &&
                   Name == keyword.Name &&
                   Assigned == keyword.Assigned;
        }

        public override int GetHashCode()
        {
            var hashCode = 1192755596;
            hashCode = hashCode * -1521134295 + KeywordID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Assigned.GetHashCode();
            return hashCode;
        }
    }
}