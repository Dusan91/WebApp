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
    public class NastavniciController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Nastavnici
        public ActionResult Index()
        {
            return View(db.Nastavniks.ToList());
        }

        // GET: Nastavnici/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nastavnik nastavnik = db.Nastavniks.Find(id);
            if (nastavnik == null)
            {
                return HttpNotFound();
            }
            return View(nastavnik);
        }

        // GET: Nastavnici/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nastavnici/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ime,DatumRodjenja")] Nastavnik nastavnik)
        {
            if (ModelState.IsValid)
            {
                db.Nastavniks.Add(nastavnik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nastavnik);
        }

        // GET: Nastavnici/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nastavnik nastavnik = db.Nastavniks.Find(id);
            if (nastavnik == null)
            {
                return HttpNotFound();
            }
            return View(nastavnik);
        }

        // POST: Nastavnici/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ime,DatumRodjenja")] Nastavnik nastavnik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nastavnik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nastavnik);
        }

        // GET: Nastavnici/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nastavnik nastavnik = db.Nastavniks.Find(id);
            if (nastavnik == null)
            {
                return HttpNotFound();
            }
            return View(nastavnik);
        }

        // POST: Nastavnici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nastavnik nastavnik = db.Nastavniks.Find(id);
            db.Nastavniks.Remove(nastavnik);
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
