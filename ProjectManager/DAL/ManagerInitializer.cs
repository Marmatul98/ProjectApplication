using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.DAL
{
    public class ManagerInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ManagerContext>
    {
        protected override void Seed(ManagerContext context)
        {
            var students = new List<Student>
           {
               new Student{FirstName = "Marek", LastName = "Matula", Email = "r18439@student.osu.cz", PersonalNumber = "R18439"},
               new Student{FirstName = "Jan", LastName = "Kripner", Email = "r18435@student.osu.cz", PersonalNumber = "R18435"}
           };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();
        }
    }
}