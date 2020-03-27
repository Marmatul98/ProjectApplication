using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectManager.DAL;
using ProjectManager.Models;
using ProjectManager.ViewModels;

namespace ProjectManager.Controllers
{
    public class CourseController : Controller
    {
        private ManagerContext db = new ManagerContext();

        // GET: Course
        public ActionResult Index()
        {
            ViewBag.IsAuth = User.Identity.IsAuthenticated;
            return View(db.Courses.ToList());
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id, string[] selectedKeywords)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.IsAuth = User.Identity.IsAuthenticated;
            Course course = db.Courses.Find(id);

            List<Project> projects = course.Projects.ToList();
            ISet<Project> wantedProjects = new HashSet<Project>();
            List<Keyword> filteredKeywords = new List<Keyword>();

            if (selectedKeywords == null)
            {
                wantedProjects = projects.ToHashSet();
                ViewBag.AvailableKeywords = GetAvailableKeywords(course.Projects.ToList(), new string[] { });
            }
            else
            {
                foreach (var project in projects)
                {
                    foreach (var keyword in project.Keywords)
                    {
                        if (selectedKeywords.ToList().Contains(keyword.KeywordID.ToString()))
                        {
                            wantedProjects.Add(project);
                        }
                    }
                }
                ViewBag.AvailableKeywords = GetAvailableKeywords(course.Projects.ToList(), selectedKeywords);
            }


            GetUsedYears(course);
            ViewBag.Projects = wantedProjects;

            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Filter(string[] selectedKeywords)
        {

            return View();

        }



        // GET: Course/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,Name")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Course/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseID,Name")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Course/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void GetUsedYears(Course course)
        {
            var courseProjectsYears = new List<Year>(course.Projects.Select(y => y.Year));
            var usedYears = new HashSet<int>();
            foreach (var item in courseProjectsYears)
            {
                usedYears.Add(item.YearValue);
            }
            ViewBag.Years = usedYears;
        }

        private ICollection<AssignedKeyword> GetAvailableKeywords(List<Project> projects, string[] selectedKeywords)
        {
            ISet<AssignedKeyword> returnedList = new HashSet<AssignedKeyword>();
            foreach (var project in projects)
            {
                foreach (var keyword in project.Keywords)
                {
                    AssignedKeyword assignedKeyword = new AssignedKeyword() { Name = keyword.Name, KeywordID = keyword.KeywordID };
                    assignedKeyword.Assigned = selectedKeywords.Contains(keyword.KeywordID.ToString());
                    returnedList.Add(assignedKeyword);
                }
            }
            return returnedList;
        }
    }
}