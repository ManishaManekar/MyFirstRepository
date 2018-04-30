using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCAssignment.Models;

namespace MVCAssignment.Controllers
{
    public class UserDataController : Controller
    {
        TestdbEntities db = new TestdbEntities();
        // GET: UserData
        public ActionResult Home()
        {
            return View(db.Users.ToList());
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        { 
            User obj = new User();
            if (obj.UserId == Convert.ToInt32(form["UserId"]))
            {
                return RedirectToAction("UserProfile");
            }
            else
            {
                return RedirectToAction("LoginFailed");
            }
        }

        [HttpGet]
        public ActionResult ShowUsers()
        {
            return View(db.Users.ToList());
        }

        [HttpPost, ActionName("ShowUsers")]
        public ActionResult UsersInfo()
        {
            return RedirectToAction("Home");
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FirstName,LastName,DOB,CreateTS")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Home");
            }

            return View(user);
        }
    }


}