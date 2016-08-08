﻿using Common;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Common
{
    public class HasCredentialAttribute : AuthorizeAttribute
    {
        public string RoleID { set; get; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = (AdminLogin)HttpContext.Current.Session[Common.Constants.ADMIN_SESSION];
            if (session == null)
            {
                return false;
            }

            List<string> privilegeLevels = this.GetCredentialByLoggedInUser(session.UserName);

            if (privilegeLevels.Contains(this.RoleID) || session.GroupID == CommonConstants.ADMIN_GROUP)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var session = (AdminLogin)HttpContext.Current.Session[Common.Constants.ADMIN_SESSION];
            if (session == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Areas/Admin/Views/Login/Index.cshtml"
                };
            }
            else
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Areas/Admin/Views/Shared/401.cshtml"
                };
            }
        }

        private List<string> GetCredentialByLoggedInUser(string userName)
        {
            var credentials = (List<string>)HttpContext.Current.Session[Common.Constants.SESSION_CREDENTIALS];
            return credentials;
        }
    }
}