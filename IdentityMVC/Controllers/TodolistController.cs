using IdentityMVC.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityMVC.Controllers
{
    public class TodolistController : Controller
    {
        ApplicationDbContext con = new ApplicationDbContext();

        // GET: Todolist
        public ActionResult Index()
        {
            List<TodolistViewModel> todoList = new List<TodolistViewModel>();
            List<TodolistViewModel> userTodoList = new List<TodolistViewModel>();
            todoList = con.Todolists.ToList();
            foreach (TodolistViewModel item in todoList)
            {
                if (item.UserID == System.Web.HttpContext.Current.User.Identity.GetUserId())
                {
                    userTodoList.Add(item);
                }
            }
             
            return View(userTodoList);
        }

        // GET: Todolist/Details/5
        public ActionResult Details(int id)
        {
            return View(con.Todolists.Where(x => x.Id == id).FirstOrDefault());
        }

        // GET: Todolist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Todolist/Create
        [HttpPost]
        public ActionResult Create(TodolistViewModel todolist)
        {
            try
            {
                // TODO: Add insert logic here
                todolist.UserID = System.Web.HttpContext.Current.User.Identity.GetUserId();
                con.Todolists.Add(todolist);
                con.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Todolist/Edit/5
        public ActionResult Edit(int id)
        {
            return View(con.Todolists.Where(x => x.Id == id).FirstOrDefault());
        }

        // POST: Todolist/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TodolistViewModel todolist)
        {
            try
            {
                // TODO: Add update logic here
                con.Entry(todolist).State = EntityState.Modified;
                con.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Todolist/Delete/5
        public ActionResult Delete(int id)
        {
            return View(con.Todolists.Where(x => x.Id == id).FirstOrDefault());
        }

        // POST: Todolist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                TodolistViewModel todolist = con.Todolists.Where(x => x.Id == id).FirstOrDefault();
                con.Todolists.Remove(todolist);
                con.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
