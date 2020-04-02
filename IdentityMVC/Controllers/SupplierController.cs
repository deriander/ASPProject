using IdentityMVC.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityMVC.Controllers
{
    [AuthorizeRedirect(Roles = "admin")]
    public class SupplierController : Controller
    {
        IdentityMVCEntities1 con = new IdentityMVCEntities1();
        // GET: Supplier
        public ActionResult Index()
        {
            return View(con.TB_T_Supplier.ToList());
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class AuthorizeRedirect : AuthorizeAttribute
        {
            private const string IS_AUTHORIZED = "isAuthorized";

            public string RedirectUrl = "~/Denied/Index";

            protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
            {
                bool isAuthorized = base.AuthorizeCore(httpContext);

                httpContext.Items.Add(IS_AUTHORIZED, isAuthorized);

                return isAuthorized;
            }

            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                base.OnAuthorization(filterContext);

                var isAuthorized = filterContext.HttpContext.Items[IS_AUTHORIZED] != null
                    ? Convert.ToBoolean(filterContext.HttpContext.Items[IS_AUTHORIZED])
                    : false;

                if (!isAuthorized)
                {
                    filterContext.RequestContext.HttpContext.Response.Redirect(RedirectUrl);
                }
            }
        }
    }
}
