﻿using System;
using System.Web.Mvc;

namespace Appfail.Reporting.Mvc
{
    /// <summary>
    /// A filter that causes all filters thrown by controller actions to be reported to AppFail.
    /// if you are using the MVC HandleError attribute, then it's important to define AppFailReport before
    /// HandleError, so that it is called for all exceptions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AppfailReportAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            // Only log handled exceptions. AppFail will catch all unhandled exceptions anyway.
            if (filterContext.ExceptionHandled)
            {
                filterContext.Exception.SendToAppFail();
            }
        }
    }
}
