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
    public class DepartmentsController : BaseController
    {
        private DBContext db = new DBContext();

        // GET: Departments
        public ActionResult Index()
        {
            var department = db.Department.Where(x=>x.RecordStatus == true).Include(d => d.DepartmentHead);
            return View(department.ToList());
        }

        // GET: Departments/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Department.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentHeadId = new SelectList(db.Teacher.Where(x=>x.RecordStatus==true), "Id", "FirstName");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DepartmentHeadId,RecordStatus")] Department department)
        {
            if (ModelState.IsValid)
            {
                department.RecordStatus = true;
                department.Id = Guid.NewGuid();
                db.Department.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentHeadId = new SelectList(db.Teacher, "Id", "FirstName", department.DepartmentHeadId);
            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Department.Find(id);
            if (department == null || department.RecordStatus == false)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentHeadId = new SelectList(db.Teacher, "Id", "FirstName", department.DepartmentHeadId);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DepartmentHeadId,RecordStatus")] Department department)
        {
            if (ModelState.IsValid)
            {
                department.RecordStatus = true;
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentHeadId = new SelectList(db.Teacher, "Id", "FirstName", department.DepartmentHeadId);
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Department.Find(id);
            if (department == null || department.RecordStatus==false)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Department department = db.Department.Find(id);
            department.RecordStatus = false;
            db.Entry(department).State = EntityState.Modified;
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
