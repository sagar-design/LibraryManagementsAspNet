using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace LibraryManagement.Admin
{
    public partial class AddPublisher : System.Web.UI.Page
    {
        DBConnect dbcon = new DBConnect();
        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Autogenrate();
                Bindrecord();
                btnAdd.Visible = true;
                btnupdate.Visible = false;
                btnCancel.Visible = true;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                insertpublisher();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Error','Validation Error! please enter valid data ...try again','error')", true);
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("sp_UpdatePublisher", dbcon.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", txtpublisherID.Text);
            cmd.Parameters.AddWithValue("@name", txtpublisherName.Text);
            dbcon.OpenCon();
            if (cmd.ExecuteNonQuery() == 1)
            {
                dbcon.CloseCon();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success','Updated successfully','success')", true);
                clrcontrol();
                Bindrecord();
                Autogenrate();
                btnAdd.Visible = true;
                btnupdate.Visible = false;
                btnCancel.Visible = false;
            }
            else
            {
                dbcon.CloseCon();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Error','Error! record not updated ...try again','error')", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminHomePage.aspx");
        }

        protected void Bindrecord()
        {
            cmd = new SqlCommand("sp_getPublisher", dbcon.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dbcon.OpenCon();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            RptPublisher.DataSource = dt;
            RptPublisher.DataBind();
        }

        protected void insertpublisher()
        {
            cmd = new SqlCommand("sp_InsertPublisher", dbcon.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", txtpublisherID.Text);
            cmd.Parameters.AddWithValue("@name", txtpublisherName.Text);
            dbcon.OpenCon();
            if (cmd.ExecuteNonQuery() == 1)
            {
                dbcon.CloseCon();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success','Saved successfully','success')", true);
                clrcontrol();
                Bindrecord();
                Autogenrate();
            }
            else
            {
                dbcon.CloseCon();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Error','Error! record not inserted ...try again','error')", true);
            }
        }

        public void Autogenrate()
        {
            cmd = new SqlCommand("SELECT MAX(publisher_id) AS ID FROM publisher_master_tbl", dbcon.GetCon());
            dbcon.OpenCon();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string d = dr["ID"].ToString();
                if (string.IsNullOrEmpty(d))
                {
                    txtpublisherID.Text = "P101";
                }
                else
                {
                    int r;
                    if (int.TryParse(d.Substring(1), out r))
                    {
                        r = r + 1;
                        txtpublisherID.Text = "P" + r.ToString();
                    }
                    else
                    {
                        // Handle the case where the ID is not a valid integer
                        txtpublisherID.Text = "P101"; // or some other default value
                    }
                }
                txtpublisherID.ReadOnly = true;
            }
            dbcon.CloseCon();
        }


        private void clrcontrol()
        {
            txtpublisherName.Text = txtpublisherID.Text = String.Empty;
            txtpublisherID.Focus();
        }

        protected void SearchRecordBy_ID(string idd)
        {
            cmd = new SqlCommand("sp_getPublisherByID", dbcon.GetCon());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id", idd);
            dbcon.OpenCon();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "dt");
            dbcon.CloseCon();
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtpublisherID.Text = ds.Tables[0].Rows[0]["publisher_id"].ToString();
                txtpublisherName.Text = ds.Tables[0].Rows[0]["publisher_name"].ToString();
                btnAdd.Visible = false;
                btnupdate.Visible = true;
                btnCancel.Visible = true;
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Error','Error! No Record Found try again...','error')", true);
            }
        }

        protected void RptPublisher_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { '&' });
                string id = commandArgs[0];
                SearchRecordBy_ID(id);
            }
            if (e.CommandName == "delete")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { '&' });
                string id = commandArgs[0];
                cmd = new SqlCommand("sp_DeletePublisherByID", dbcon.GetCon());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
                dbcon.OpenCon();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    dbcon.CloseCon();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success','Deleted successfully','success')", true);
                    clrcontrol();
                    Bindrecord();
                    Autogenrate();
                    btnAdd.Visible = true;
                    btnupdate.Visible = false;
                }
                else
                {
                    dbcon.CloseCon();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Error','Error! record not deleted ...try again','error')", true);
                }
            }
        }
    }
}
