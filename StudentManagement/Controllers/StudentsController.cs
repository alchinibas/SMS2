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
    public class StudentsController : BaseController
    {
        private DBContext db = new DBContext();

        // GET: Students
        public ActionResult Index()
        {
            var student = db.Student.Where(x => x.RecordStatus == true).Include(s => s.Department);
            return View(student.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Department, "Id", "Name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,MiddleName,LastName,Email,Address,Mobile,EnrolledYear,DepartmentId,GuardianFirstName,GuardianMiddleName,GuardianLastName,GuardianContact")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = Guid.NewGuid();
                string middleName = student.GuardianMiddleName != string.Empty ? student.GuardianMiddleName + " " : "";
                student.GuardianFullName = student.GuardianFirstName +" "+ middleName + student.GuardianLastName;
                middleName = student.MiddleName != string.Empty ? student.MiddleName + " " : "";
                student.FullName = student.FirstName + " " +middleName + student.LastName;
                student.RecordStatus = true;
                
                db.Student.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Department, "Id", "Name", student.DepartmentId);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null || student.RecordStatus==false)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Department, "Id", "Name", student.DepartmentId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,MiddleName,Email,Address,Mobile,EnrolledYear,DepartmentId,GuardianFirstName,GuardianMiddleName,GuardianLastName,GuardianContact")] Student student)
        {
            if (ModelState.IsValid)
            {
                string middleName = " " + student.GuardianMiddleName != string.Empty ? student.GuardianMiddleName + " " : "";
                student.GuardianFullName = student.GuardianFirstName + middleName + student.GuardianLastName;
                middleName = " " + student.MiddleName != string.Empty ? student.MiddleName + " " : "";
                student.FullName = student.FirstName + middleName + student.LastName;
                student.RecordStatus = true;
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Department, "Id", "Name", student.DepartmentId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null || student.RecordStatus==false)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Student student = db.Student.Find(id);
            student.RecordStatus = false;
            db.Entry(student).State = EntityState.Modified;
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
