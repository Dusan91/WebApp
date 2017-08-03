using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private void SetCoursesVariable()
        {
            ViewBag.Courses = db.Predmeti.Select(x => new SelectListItem(){
                Text = x.Ime,
                Value = x.Id.ToString(),
            }).ToList();
        }

        // GET: Students
        public ActionResult Index(string search = "",
            string sortBy = "Name",
            string sortOrder = "ASC")
        {
            var studenti = db.Students.
                Where(x => x.Name.Contains(search) 
                || x.Surname.Contains(search));

            studenti = studenti.
                OrderBy(
                string.Format("{0} {1}", sortBy, sortOrder));

            //switch (sortBy)
            //{
            //    case "Name":
            //        //if (sortOrder.Equals("ASC"))
            //        //{
            //        //    studenti = studenti.OrderBy(x => x.Name).ToList();
            //        //}
            //        //else
            //        //{
            //        //    studenti = studenti.OrderByDescending(x => x.Name).ToList();
            //        //}
            //        studenti = studenti.OrderBy(x => x.Name).ToList();
            //        break;
            //    case "Surname":
            //        studenti = studenti.OrderBy(x => x.Surname).ToList();
            //        break;
            //    default:
            //        break;
            //}

            //if (sortOrder.Equals("DESC"))
            //{
            //    studenti.Reverse();
            //}

            return View(studenti.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            SetCoursesVariable();
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student, int[] selectedCourses)
        {
            if (ModelState.IsValid)
            {
                if (selectedCourses != null)
                {
                    student.Predmeti = db.Predmeti.Where(x => selectedCourses.Contains(x.Id)).ToList();
                }
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            SetCoursesVariable();
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            SetCoursesVariable();
            return View(student);
            //return Json(student, JsonRequestBehavior.AllowGet);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, int[] selectedCourses)
        {
            ////// -- NOVO --
            Student student = db.Students.Find(id);

            if (TryUpdateModel(student, new string[] { "Id", "Name", "Surname", "IndexNumber", "DatumRodjenja" }))
            {
                if (student.Adresa == null)
                {
                    student.Adresa = new Address();
                }
                
                if (TryUpdateModel(student.Adresa, "Adresa", new string[] { "Ulica", "Broj"}))
                {
                    student.Predmeti.Clear();
                    student.Predmeti = db.Predmeti.Where(x => selectedCourses.Contains(x.Id)).ToList();
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            SetCoursesVariable();
            ////// -- NOVO --

            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            if (student.Adresa != null)
            {
                db.Adrese.Remove(student.Adresa);
            }
            db.Students.Remove(student);
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
