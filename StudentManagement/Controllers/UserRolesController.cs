using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentManagement.Entity;
using StudentManagement.Entity.Users;

namespace StudentManagement.Controllers
{
    public class UserRolesController : BaseController
    {
        private DBContext db = new DBContext();

        // GET: UserRoles
        public ActionResult Index()
        {
            return View(db.UserRole.Where(x=>x.RecordStatus==true).ToList());
        }

        // GET: UserRoles/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRole.Find(id);
            if (userRole == null || userRole.RecordStatus==false)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // GET: UserRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,RecordStatus")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {

                if (db.UserRole.Any(x => x.Name.ToLower() == userRole.Name.ToLower()))
                {
                    ModelState.AddModelError("Name", new Exception("UserRole with the name alerady exists"));
                    return View(userRole);
                }
                userRole.RecordStatus = true;
                userRole.Id = Guid.NewGuid();
                db.UserRole.Add(userRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userRole);
        }

        // GET: UserRoles/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRole.Find(id);
            if (userRole == null || userRole.RecordStatus==false)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // POST: UserRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,RecordStatus")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {

                if (db.UserRole.Any(x => x.Name.ToLower() == userRole.Name.ToLower()&& x.Id==userRole.Id))
                {
                    ModelState.AddModelError("Name", new Exception("UserRole with the name alerady exists"));
                    return View(userRole);
                }
                userRole.RecordStatus = true;
                db.Entry(userRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userRole);
        }

        // GET: UserRoles/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRole.Find(id);
            if (userRole == null ||userRole.RecordStatus==false)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // POST: UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            UserRole userRole = db.UserRole.Find(id);
            userRole.RecordStatus = false;
            db.Entry(userRole).State = EntityState.Modified;
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
