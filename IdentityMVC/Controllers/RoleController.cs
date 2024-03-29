﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace IdentityMVC.Models
{
    [AuthorizeRedirect(Roles = "admin")]
    public class RoleController : Controller
    {
        private ApplicationRoleManager _roleManager;

        public RoleController()
        {
        }

        public RoleController(ApplicationRoleManager roleManager)
        {
            RoleManager = roleManager;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        // GET: Role
        public ActionResult Index()
        {
            
            List<RoleViewModel> list = new List<RoleViewModel>();
            foreach(var role in RoleManager.Roles)
            {
                list.Add(new RoleViewModel(role));
            }
            return View(list);

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel model)
        {
            var role = new ApplicationRole() { Name = model.Name };
            await RoleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModel model)
        {
            var role = new ApplicationRole() { Id = model.Id, Name = model.Name };
            await RoleManager.UpdateAsync(role);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        public async Task<ActionResult> Delete(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            await RoleManager.DeleteAsync(role);
            return View(new RoleViewModel(role));
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