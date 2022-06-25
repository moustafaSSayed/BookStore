using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class UsersController : Controller
    {
        private BookStoreDB bookStoreDataBase = new BookStoreDB();

        // GET: Users/UserSignUp
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: Users/USerSignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                bookStoreDataBase.Users.Add(user);
                bookStoreDataBase.SaveChanges();
                Session["ID"] = user.userID;
                Session["username"] = user.userFirstName.ToString();
                return RedirectToAction("../Home/Index");
            }

            return View(user);
        }

        // GET: Users/EditUserInfo
        public ActionResult EditUserInfo(int id)
        {
            return View(FindUser(id));
        }

        // POST: Users/EditUserInfo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserInfo(User user, string userImage)
        {
            if (ModelState.IsValid)
            {
                user.userImage = CheckPhoto(user, userImage);
                bookStoreDataBase.Entry(user).State = EntityState.Modified;
                bookStoreDataBase.SaveChanges();
                return RedirectToAction("../Home/Index");
            }
            return View(user);
        }

        //Get: Users/FindUser
        public User FindUser(int id)
        {
            User user = bookStoreDataBase.Users.Find(id);
            return user;
        }

        //GET: Users/CheckPhoto
        public string CheckPhoto(User user,string userImage)
        {
            if (userImage != null)
            {
                return userImage;
            }
            else
            {
                return user.userImage;
            }
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // Post: Login
        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                var usr = bookStoreDataBase.Users.Single(u => u.userEmail == user.userEmail && u.userPassword == user.userPassword);
                Session["ID"] = usr.userID;
                Session["username"] = usr.userFirstName.ToString();
                return RedirectToAction("../Books/Index");
            }
            catch
            {
                ModelState.AddModelError("", "Username or Password is Wrong.");
            }
            return View(user);
        }

        public ActionResult MakeOrder(int bookID)
        {
            return RedirectToAction("../UserOrderedBooks/AddToCart/"+bookID);
        }

    }
}
