using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.DAL
{
    public class ManagerInitializer : System.Data.Entity.DropCreateDatabaseAlways<ManagerContext>
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

            var keywords = new List<Keyword>
           {
               new Keyword{Name = "car"},
               new Keyword{Name=  "plane"}
           };

            keywords.ForEach(k => context.Keywords.Add(k));
            context.SaveChanges();

            var projects = new List<Project>
           {
               new Project{Title = "TestTitle" , Description = "TestDescription", ProjectCourse = ProjectCourse.UMINT, Year = 2019,
                   StudentID = students.Single( s => s.PersonalNumber == "R18439").StudentID, Keywords = new List<Keyword>()},
               new Project{Title = "TestTitle2" , Description = "TestDescription2", ProjectCourse = ProjectCourse.SOFTCO, Year = 2018,
               StudentID = students.Single( s => s.PersonalNumber == "R18435").StudentID, Keywords = new List<Keyword>()},
           };

            projects.ForEach(p => context.Projects.Add(p));
            context.SaveChanges();

            AddKeyword(context, "TestTitle", "car");
            AddKeyword(context, "TestTitle", "plane");
            AddKeyword(context, "TestTitle2", "plane");
            context.SaveChanges();


        }
            void AddKeyword(ManagerContext context, string projectTitle, string keywordName)
            {
                var prj = context.Projects.SingleOrDefault(p => p.Title == projectTitle);
                var kwd = prj.Keywords.SingleOrDefault(k => k.Name == keywordName);
                if (kwd == null)
                    prj.Keywords.Add(context.Keywords.Single(k => k.Name == keywordName));
            }
        }
    }