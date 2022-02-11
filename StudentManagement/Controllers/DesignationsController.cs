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
    public class DesignationsController : BaseController
    {
        private DBContext db = new DBContext();

        // GET: Designations
        public ActionResult Index()
        {
            return View(db.Designation.Where(x => x.RecordStatus == true).ToList());
        }

        // GET: Designations/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = db.Designation.Find(id);
            if (designation == null || designation.RecordStatus==false)
            {
                return HttpNotFound();
            }
            return View(designation);
        }

        // GET: Designations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Designations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,RecordStatus")] Designation designation)
        {
            if (ModelState.IsValid)
            {
                designation.RecordStatus = true;
                designation.Id = Guid.NewGuid();
                db.Designation.Add(designation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(designation);
        }

        // GET: Designations/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = db.Designation.Find(id);
            if (designation == null || designation.RecordStatus == false )
            {
                return HttpNotFound();
            }
            return View(designation);
        }

        // POST: Designations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,RecordStatus")] Designation designation)
        {
            if (ModelState.IsValid)
            {
                designation.RecordStatus = true;
                db.Entry(designation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(designation);
        }

        // GET: Designations/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = db.Designation.Find(id);
            if (designation == null || designation.RecordStatus == false)
            {
                return HttpNotFound();
            }
            return View(designation);
        }

        // POST: Designations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Designation designation = db.Designation.Find(id);
            designation.RecordStatus = false;
            db.Entry(designation).State = EntityState.Modified;
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
