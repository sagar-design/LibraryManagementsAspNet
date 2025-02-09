using System;
using System.Web;

namespace LibraryManagement.Admin
{
    public partial class AdminSite : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Adminrole"] != null && Session["Adminrole"].ToString() == "Admin")
            {
                if (!IsPostBack)
                {
                    lblUserName.Text = "Hi," + Session["Adminusername"].ToString();

                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
                    Response.Cache.SetNoStore();
                }
            }
            else
            {
                Response.Redirect("~/SignOut.aspx");
            }


        }
    }
}