using System;

namespace LibraryManagement.UserScreen
{
    public partial class User : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] != null && Session["role"].ToString() == "user")
            {
                if (!IsPostBack)
                {
                    lblUserName.Text = "Hi," + Session["fullname"].ToString();

                }

            }
            else
            {
                Response.Redirect("~/SignOut.aspx");
            }
        }

    }
}