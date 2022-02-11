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
    public class TeachersController : BaseController
    {
        private DBContext db = new DBContext();

        // GET: Teachers
        public ActionResult Index()
        {
            var teacher = db.Teacher.Where(x=>x.RecordStatus==true).Include(t => t.Designation).Include(t => t.MajorSubject);
            return View(teacher.ToList());
        }

        // GET: Teachers/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null || teacher.RecordStatus==false)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            ViewBag.DesignationId = new SelectList(db.Designation.Where(x=>x.RecordStatus==true), "Id", "Name");
            ViewBag.MajorSubjectId = new SelectList(db.MajorSubject.Where(x => x.RecordStatus == true), "Id", "Name");
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,MiddleName,LastName,MajorSubjectId,DesignationId,Startyear,Address,Email,Mobile")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacher.RecordStatus = true;
                string middleName = teacher.MiddleName == string.Empty ? "" : teacher.MiddleName + " ";
                teacher.FullName = teacher.FirstName + " " + middleName+teacher.LastName;
                teacher.Id = Guid.NewGuid();
                db.Teacher.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DesignationId = new SelectList(db.Designation.Where(x => x.RecordStatus == true), "Id", "Name", teacher.DesignationId);
            ViewBag.MajorSubjectId = new SelectList(db.MajorSubject.Where(x => x.RecordStatus == true), "Id", "Name", teacher.MajorSubjectId);
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null || teacher.RecordStatus==false)
            {
                return HttpNotFound();
            }
            ViewBag.DesignationId = new SelectList(db.Designation.Where(x=>x.RecordStatus==true), "Id", "Name", teacher.DesignationId);
            ViewBag.MajorSubjectId = new SelectList(db.MajorSubject.Where(x => x.RecordStatus == true), "Id", "Name", teacher.MajorSubjectId);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,MiddleName,LastName,MajorSubjectId,DesignationId,Startyear,Address,Email,Mobile")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacher.RecordStatus = true;
                string middleName = teacher.MiddleName == string.Empty ? "" : teacher.MiddleName + " ";
                teacher.FullName = teacher.FirstName + " " + middleName + teacher.LastName;
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DesignationId = new SelectList(db.Designation.Where(x => x.RecordStatus == true), "Id", "Name", teacher.DesignationId);
            ViewBag.MajorSubjectId = new SelectList(db.MajorSubject.Where(x => x.RecordStatus == true), "Id", "Name", teacher.MajorSubjectId);
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null || teacher.RecordStatus==false)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Teacher teacher = db.Teacher.Find(id);
            teacher.RecordStatus = false;
            db.Entry(teacher).State = EntityState.Modified;
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
