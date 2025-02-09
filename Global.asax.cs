using System;

namespace LibraryManagement
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            //Exception ex = Server.GetLastError(); Server.ClearError();
            //if (ex.InnerException != null)
            //{
            //    Response.Redirect("ErrorPage.aspx?ErrorMessage=" + ex.InnerException.Message);
            //}
            //else
            //{
            //    Response.Redirect("ErrorPage.aspx?ErrorMessage=" + ex.Message);

            //}
        }
    }
}