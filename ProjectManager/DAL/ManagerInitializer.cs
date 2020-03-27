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
            var users = new List<User>
            {
                new User{UserName = "admin", Password = "admin"}
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();
            var students = new List<Student>
           {
               new Student{FirstName = "Marek", LastName = "Matula", Email = "r18439@student.osu.cz", PersonalNumber = "R18439"},
               new Student{FirstName = "Jan", LastName = "Kripner", Email = "r18435@student.osu.cz", PersonalNumber = "R18435"}
           };
            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();
            var years = new List<Year>
            {
                new Year{YearValue = 2019}
            };
            years.ForEach(y => context.Years.Add(y));
            context.SaveChanges();
            var keywords = new List<Keyword>
           {
               new Keyword{Name = "car"},
               new Keyword{Name=  "plane"}
           };
            keywords.ForEach(k => context.Keywords.Add(k));
            context.SaveChanges();
            var courses = new List<Course>
           {
               new Course{Name = "UMINT"},
               new Course{Name = "SOFTCO"}
           };
            courses.ForEach(c => context.Courses.Add(c));
            context.SaveChanges();
            var projects = new List<Project>
           {
               new Project{Title = "TestTitle" , Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Ut enim ad minim veniam, " +
               "quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat." +
               "Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. " +
               "Praesent dapibus. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus " +
               "asperiores repellat. Vestibulum erat nulla, ullamcorper nec, rutrum non, nonummy ac, erat. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit " +
               "aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Praesent id justo in neque elementum ultrices. " +
               "Etiam posuere lacus quis dolor.",
                   CourseID = courses.Single(c => c.Name == "UMINT").CourseID,
                   YearID = years.Single( y => y.YearValue == 2019).YearID,
                   StudentID = students.Single( s => s.PersonalNumber == "R18439").StudentID, Keywords = new List<Keyword>()},

                new Project{Title = "TestTitle2" , Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Ut enim ad minim veniam, " +
               "quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat." +
               "Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. " +
               "Praesent dapibus. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus " +
               "asperiores repellat. Vestibulum erat nulla, ullamcorper nec, rutrum non, nonummy ac, erat. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit " +
               "aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Praesent id justo in neque elementum ultrices. " +
               "Etiam posuere lacus quis dolor.",
                CourseID = courses.Single(c => c.Name == "SOFTCO").CourseID,
               YearID = years.Single( y => y.YearValue == 2019).YearID,
               StudentID = students.Single( s => s.PersonalNumber == "R18435").StudentID, Keywords = new List<Keyword>()},


                new Project{Title = "TestTitle3" , Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Ut enim ad minim veniam, " +
               "quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat." +
               "Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. " +
               "Praesent dapibus. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus " +
               "asperiores repellat. Vestibulum erat nulla, ullamcorper nec, rutrum non, nonummy ac, erat. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit " +
               "aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Praesent id justo in neque elementum ultrices. " +
               "Etiam posuere lacus quis dolor.",
                   CourseID = courses.Single(c => c.Name == "UMINT").CourseID,
                   YearID = years.Single( y => y.YearValue == 2019).YearID,
                   StudentID = students.Single( s => s.PersonalNumber == "R18439").StudentID, Keywords = new List<Keyword>()},

                new Project{Title = "TestTitle4" , Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Ut enim ad minim veniam, " +
               "quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat." +
               "Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. " +
               "Praesent dapibus. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus " +
               "asperiores repellat. Vestibulum erat nulla, ullamcorper nec, rutrum non, nonummy ac, erat. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit " +
               "aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Praesent id justo in neque elementum ultrices. " +
               "Etiam posuere lacus quis dolor.",
                   CourseID = courses.Single(c => c.Name == "UMINT").CourseID,
                   YearID = years.Single( y => y.YearValue == 2019).YearID,
                   StudentID = students.Single( s => s.PersonalNumber == "R18439").StudentID, Keywords = new List<Keyword>()},

                new Project{Title = "TestTitle5" , Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Ut enim ad minim veniam, " +
               "quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat." +
               "Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. " +
               "Praesent dapibus. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus " +
               "asperiores repellat. Vestibulum erat nulla, ullamcorper nec, rutrum non, nonummy ac, erat. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit " +
               "aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Praesent id justo in neque elementum ultrices. " +
               "Etiam posuere lacus quis dolor.",
                   CourseID = courses.Single(c => c.Name == "UMINT").CourseID,
                   YearID = years.Single( y => y.YearValue == 2019).YearID,
                   StudentID = students.Single( s => s.PersonalNumber == "R18439").StudentID, Keywords = new List<Keyword>()},


           };

            projects.ForEach(p => context.Projects.Add(p));
            context.SaveChanges();
            AddKeyword(context, "TestTitle", "car");
            AddKeyword(context, "TestTitle", "plane");
            AddKeyword(context, "TestTitle2", "plane");
            AddKeyword(context, "TestTitle3", "plane");
            AddKeyword(context, "TestTitle3", "car");
            AddKeyword(context, "TestTitle4", "plane");
            AddKeyword(context, "TestTitle5", "car");
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