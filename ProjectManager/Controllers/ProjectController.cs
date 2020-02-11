using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectManager.DAL;
using ProjectManager.Models;
using ProjectManager.ViewModels;

namespace ProjectManager.Controllers
{
    public class ProjectController : Controller
    {
        private ManagerContext db = new ManagerContext();

        // GET: Project
        public ActionResult Index()
        {
            var projects = db.Projects.Include(p => p.Course).Include(p => p.Student).Include(p => p.Year);
            return View(projects.ToList());
        }

        // GET: Project/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            Project project = new Project();
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name");
            ViewBag.YearID = new SelectList(db.Years, "YearID", "YearValue");
            project.Keywords = new List<Keyword>();
            PopulateAssignedKeywordData(project);
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,Title,Description,YearID,CourseID,StudentID")] Project project, string[] selectedKeywords)
        {
            if (selectedKeywords != null)
            {
                project.Keywords = new List<Keyword>();
                foreach (var keyword in selectedKeywords)
                {
                    var keywordToAdd = db.Keywords.Find(int.Parse(keyword));
                    project.Keywords.Add(keywordToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateAssignedKeywordData(project);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", project.StudentID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", project.CourseID);
            ViewBag.YearID = new SelectList(db.Years, "YearID", "YearValue", project.YearID);
            return View(project);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects
            .Include(i => i.Keywords)
            .Where(i => i.ProjectID == id)
            .Single();
            PopulateAssignedKeywordData(project);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", project.StudentID);
            ViewBag.YearID = new SelectList(db.Years, "YearID", "YearValue", project.YearID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", project.CourseID);
            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedKeywords)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var projectToUpdate = db.Projects
               .Include(i => i.Keywords)
               .Where(i => i.ProjectID == id)
               .Single();

            if (TryUpdateModel(projectToUpdate, "", new string[] { "Title", "Description", "Year", "Course", "Student" }))
            {
                try
                {
                    UpdateProjectKeywords(selectedKeywords, projectToUpdate);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {

                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedKeywordData(projectToUpdate);
            return View(projectToUpdate);
        }

            // GET: Project/Delete/5
            public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Details", "Course", new { id = project.CourseID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateAssignedKeywordData(Project project)
        {
            var allKeywords = db.Keywords;
            var projectKeywords = new HashSet<int>(project.Keywords.Select(k => k.KeywordID));
            var viewModel = new List<AssignedKeyword>();
            foreach (var keyword in allKeywords)
            {
                viewModel.Add(new AssignedKeyword
                {
                    KeywordID = keyword.KeywordID,
                    Name = keyword.Name,
                    Assigned = projectKeywords.Contains(keyword.KeywordID)
                });
            }
            ViewBag.Keywords = viewModel;
        }

        private void UpdateProjectKeywords(string[] selectedKeywords, Project projectToUpdate)
        {
            if (selectedKeywords == null)
            {
                projectToUpdate.Keywords = new List<Keyword>();
                return;
            }

            var selectedKeywordsHS = new HashSet<string>(selectedKeywords);
            var projectKeywords = new HashSet<int>
                (projectToUpdate.Keywords.Select(p => p.KeywordID));
            foreach (var keyword in db.Keywords)
            {
                if (selectedKeywordsHS.Contains(keyword.KeywordID.ToString()))
                {
                    if (!projectKeywords.Contains(keyword.KeywordID))
                    {
                        projectToUpdate.Keywords.Add(keyword);
                    }
                }
                else
                {
                    if (projectKeywords.Contains(keyword.KeywordID))
                    {
                        projectToUpdate.Keywords.Remove(keyword);
                    }
                }
            }
        }
    }
}
