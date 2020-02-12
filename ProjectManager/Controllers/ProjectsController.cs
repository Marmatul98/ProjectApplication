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
    public class ProjectsController : Controller
    {
        private ManagerContext db = new ManagerContext();

        // GET: Projects
        [Authorize]
        public ActionResult Index()
        {
            var projects = db.Projects.Include(p => p.Student);
            return View(projects.ToList());
        }

        // GET: Projects/Details/5
        [Authorize]
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

        // GET: Projects/Create
        [Authorize]
        public ActionResult Create()
        {
            var project = new Project();
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name");
            ViewBag.YearID = new SelectList(db.Years, "YearID", "YearValue");
            project.Keywords = new List<Keyword>();
            PopulateAssignedKeywordData(project);
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
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

        // GET: Projects/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           // Project project = db.Projects.Find(id);
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

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
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

        // GET: Projects/Delete/5
        [Authorize]
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

        // POST: Projects/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
    }
}
