using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using StudentManagement.Entity;
using StudentManagement.Entity.Users;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class UsersController : BaseController
    {
        private DBContext db = new DBContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Where(x=> x.RecordStatus ==true).Include(u => u.Role);
            return View(users.ToList());
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("~" + returnUrl);
            }
            return View();
        } public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login, string ReturnUrl)
        {
            User user = db.Users.Where(x=>x.Email == login.Email && x.Password==x.Password).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Email,true);
                return Redirect("~" + ReturnUrl);
            }
            ModelState.AddModelError("", "Incorrect Email or Password");
            return View();
        }
        public ActionResult Register()
        {
           
            ViewBag.RoleId = new SelectList(db.UserRole, "Id", "Name");
            return View();

        }
        // GET: Users/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,FirstName,MiddleName,LastName,Address,Mobile,RoleId,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(x => x.Email.ToLower() == user.Email.ToLower())){
                    ModelState.AddModelError("Email", new Exception("User with the email alerady exists"));
                    return View(user);
                }
                string middleName = user.MiddleName == string.Empty ? "" : user.MiddleName + " ";
                user.FullName = user.FirstName + " " + middleName + user.LastName;
                user.Id = Guid.NewGuid();
                user.RecordStatus = true;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.UserRole.Where(x=>x.RecordStatus==true), "Id", "Name", user.RoleId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null || user.RecordStatus==false)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.UserRole.Where(x=>x.RecordStatus==true), "Id", "Name", user.RoleId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,MiddleName,LastName,Address,Mobile,RoleId,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(x => x.Email.ToLower() == user.Email.ToLower() && x.RecordStatus==true && x.Id==user.Id))
                {
                    ModelState.AddModelError("Email", new Exception("User with the alerady exists"));
                    return View(user);
                }
                string middleName = user.MiddleName == string.Empty ? "" : user.MiddleName + " ";
                user.FullName = user.FirstName + " " + middleName + user.LastName;
                user.RecordStatus = true;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.UserRole, "Id", "Name", user.RoleId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null || user.RecordStatus==false)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            User user = db.Users.Find(id);
            user.RecordStatus = false;
            db.Entry(user).State = EntityState.Modified;
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
