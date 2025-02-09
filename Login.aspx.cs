using System;
using System.Data;
using System.Data.SqlClient;
namespace LibraryManagement
{
    public partial class Login : System.Web.UI.Page
    {
        DBConnect dbcon = new DBConnect();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //for member login
            SqlCommand cmd = new SqlCommand("sp_UserLogin", dbcon.GetCon());
            dbcon.OpenCon();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@member_id", txtMemberID.Text);
            cmd.Parameters.AddWithValue("@password", txtPassword.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Response.Write("<script> alert('Login Successfully');</script>");
                    Session["role"] = "user";
                    Session["fullname"] = dr.GetValue(0).ToString();
                    Session["username"] = dr.GetValue(1).ToString();
                    Session["status"] = dr.GetValue(3).ToString();
                }
                Response.Redirect("~/UserScreen/UserHome.aspx");
            }
            else
            {
                Response.Write("<script>alert('Invalid Credentials...try again');</script>");
            }

        }

        protected void btnAdminLogin_Click(object sender, EventArgs e)
        {
            //Admin Login button

            SqlCommand cmd = new SqlCommand("sp_AdminLogin", dbcon.GetCon());
            dbcon.OpenCon();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", txtAdminID.Text.Trim());
            cmd.Parameters.AddWithValue("@password", txtAdminPass.Text.Trim());
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Response.Write("<script> alert('Login Successfully');</script>");
                    Session["Adminrole"] = "Admin";
                    Session["Adminusername"] = dr.GetValue(0).ToString();
                    Session["Adminfullname"] = dr.GetValue(2).ToString();

                    //Session["status"] = dr.GetValue(3).ToString();
                }
                Response.Redirect("~/Admin/AdminHomePage.aspx");
            }
             
            {
                Response.Write("<script>alert('Invalid Credentials...try again');</script>");
            }
        }
    }
}