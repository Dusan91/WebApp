using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PredmetiController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private void SetInstructorsVariable()
        {
            ViewBag.Instructors = db.Nastavniks.Select(x => new SelectListItem()
            {
                Text = x.Ime,
                Value = x.Id.ToString(),
            }).ToList();
        }

        // GET: Predmeti
        public ActionResult Index()
        {
            return View(db.Predmeti.ToList());
        }

        // GET: Predmeti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predmet predmet = db.Predmeti.Find(id);
            if (predmet == null)
            {
                return HttpNotFound();
            }
            return View(predmet);
        }

        // GET: Predmeti/Create
        public ActionResult Create()
        {
            SetInstructorsVariable();
            return View();
        }

        // POST: Predmeti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ime,Opis,Ocena")] Predmet predmet, int nastavnikId)
        {
            if (ModelState.IsValid)
            {
                //// NOVO
                predmet.Nastavnik = db.Nastavniks.Find(nastavnikId);
                ///// NOVO
                db.Predmeti.Add(predmet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            SetInstructorsVariable();
            return View(predmet);
        }

        // GET: Predmeti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predmet predmet = db.Predmeti.Find(id);
            if (predmet == null)
            {
                return HttpNotFound();
            }

            SetInstructorsVariable();
            return View(predmet);
        }

        // POST: Predmeti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Ime,Opis,Ocena")] Predmet predmet, int nastavnikId)
        public ActionResult Edit(int id, int nastavnikId)
        {
            Predmet predmet = db.Predmeti.Find(id);

            if (TryUpdateModel(predmet, new string[] { "Ime", "Opis", "Ocena"}))
            {
                predmet.Nastavnik = db.Nastavniks.Find(nastavnikId);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            SetInstructorsVariable();
            return View(predmet);
        }

        // GET: Predmeti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predmet predmet = db.Predmeti.Find(id);
            if (predmet == null)
            {
                return HttpNotFound();
            }
            return View(predmet);
        }

        public ActionResult DodajStudenta(int? id, int? studentId)
        {
            if (id == null || studentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Predmet predmet = db.Predmeti.Find(id);
            Student student = db.Students.Find(studentId);

            if (predmet == null)
            {
                return HttpNotFound();
            }

            if (student == null)
            {
                return HttpNotFound();
            }

            predmet.Studenti.Add(student);

            db.SaveChanges();


            return RedirectToAction("Index");
        }

        // POST: Predmeti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Predmet predmet = db.Predmeti.Find(id);
            db.Predmeti.Remove(predmet);
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
