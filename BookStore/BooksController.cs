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
    public class BooksController : Controller
    {
        private BookStoreDB db = new BookStoreDB();


        // GET: UserOrderedBooks
        public ActionResult Index()
        {
            var Books = db.Books;
            return View(Books.ToList());
        }
        //to get the spicsific book 
        private Book GetBook(int? id)
        {
            return (db.Books.Find(id));
        }

        public ActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("../Home/index");
            }
            return View(book);
        }


        // GET: Movies/Edit
        public ActionResult EditBook(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Book Book_search = GetBook(id);
                if (Book_search == null)
                {
                    return HttpNotFound();
                }
                return View(Book_search);
        }


        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("../Home/index");
            }
            return View(book);
        }

        public ActionResult DeleteBook(int id)
        {
            return View(GetBook(id));
        }


        // POST: Books/Delete/5
        [HttpPost , ActionName("DeleteBook")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBookConfirm(int id)
        {
            db.Books.Remove(GetBook(id));
            db.SaveChanges();
            return RedirectToAction("../Home/Index");
        }

    }
}
