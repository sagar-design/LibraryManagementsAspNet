using System;
using System.Data;
using System.Data.SqlClient;
namespace LibraryManagement.Admin
{
    public partial class Fine : System.Web.UI.Page
    {
        DBConnect dbcon = new DBConnect();
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Adminusername"].ToString() == "" || Session["Adminusername"] == null)
            {
                Response.Redirect("~/SigOut.aspx");

            }
            else
            {
                if (!this.IsPostBack)
                {
                    BindGridData();
                }
            }
        }


        private void BindGridData()
        {
            cmd = new SqlCommand("sp_FineDetails_forAdmin", dbcon.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            GridView1.DataSource = dbcon.Load_Data(cmd);
            GridView1.DataBind();
        }

    }
}