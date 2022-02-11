using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentManagement.Entity;
using StudentManagement.Entity.Students;

namespace StudentManagement.Controllers
{
    public class MajorSubjectsController : BaseController
    {
        private DBContext db = new DBContext();

        // GET: MajorSubjects
        public ActionResult Index()
        {
            return View(db.MajorSubject.Where(x=>x.RecordStatus==true).ToList());
        }

        // GET: MajorSubjects/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorSubject majorSubject = db.MajorSubject.Find(id);
            if (majorSubject == null || majorSubject.RecordStatus == false )
            {
                return HttpNotFound();
            }
            return View(majorSubject);
        }

        // GET: MajorSubjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MajorSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] MajorSubject majorSubject)
        {
            if (ModelState.IsValid)
            {
                majorSubject.RecordStatus = true;
                majorSubject.Id = Guid.NewGuid();
                db.MajorSubject.Add(majorSubject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(majorSubject);
        }

        // GET: MajorSubjects/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorSubject majorSubject = db.MajorSubject.Find(id);
            if (majorSubject == null || majorSubject.RecordStatus == false )
            {
                return HttpNotFound();
            }
            return View(majorSubject);
        }

        // POST: MajorSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,RecordStatus")] MajorSubject majorSubject)
        {
            if (ModelState.IsValid)
            {
                majorSubject.RecordStatus = true;
                db.Entry(majorSubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(majorSubject);
        }

        // GET: MajorSubjects/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorSubject majorSubject = db.MajorSubject.Find(id);
            if (majorSubject == null || majorSubject.RecordStatus == false)
            {
                return HttpNotFound();
            }
            return View(majorSubject);
        }

        // POST: MajorSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MajorSubject majorSubject = db.MajorSubject.Find(id);
            majorSubject.RecordStatus = false;
            db.Entry(majorSubject).State = EntityState.Modified;
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
