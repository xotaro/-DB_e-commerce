using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration; // web.config isglobaly accesisablly in order to acces connection string from web.config
using System.Data.SqlClient; // TO USE SQL 
using System.Data; // parent class of sql


public partial class registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


    }
    protected void RegCustomer(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand("customerRegister", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        String username = txt_UserName.Text;
        String f_name = txt_Fname.Text;
        String l_name = txt_LName.Text;
        String password = txt_password.Text;
        String email = txt_Email.Text;
        //pass parameters to the stored procedure
        cmd.Parameters.Add(new SqlParameter("@username", username));
        cmd.Parameters.Add(new SqlParameter("@first_name", f_name));
        cmd.Parameters.Add(new SqlParameter("@last_name", l_name));
        cmd.Parameters.Add(new SqlParameter("@password", password));
        cmd.Parameters.Add(new SqlParameter("@email", email));
        if (System.String.IsNullOrEmpty(username) == true || System.String.IsNullOrEmpty(f_name) == true || System.String.IsNullOrEmpty(password) == true || System.String.IsNullOrEmpty(l_name) == true || System.String.IsNullOrEmpty(email) == true)
        {
            /* lablser.Text = "  " + "->Please Enter a Valid Value <--";
             lablser.ForeColor = System.Drawing.Color.Red;
             form1.Controls.AddAt(62, lablser);*/
            Response.Write("Please Enter a Valid Value ");

        }
        else

        {
            //Executing the SQLCommand
            conn.Open();
            int x = 0;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                ///;********************************************please updgrade it for all cases;********************************************8
                Response.Write("User already exist (Customer)");
                x = 1;

            }
            if (x == 0)
            {
                Response.Write("Successfully registered (Customer)");

            }

            conn.Close();
            x = 0;
        }
    }

    protected void RegVendor(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand("vendorRegister", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        String username = TUsern.Text;
        String f_name = TFname.Text;
        String l_name = TLname.Text;
        String password = TPass.Text;
        String email = TEmail.Text;
        String Bank = TBank.Text;
        String Company = TComp.Text;

        cmd.Parameters.Add(new SqlParameter("@username", username));
        cmd.Parameters.Add(new SqlParameter("@first_name", f_name));
        cmd.Parameters.Add(new SqlParameter("@last_name", l_name));
        cmd.Parameters.Add(new SqlParameter("@password", password));
        cmd.Parameters.Add(new SqlParameter("@email", email));
        cmd.Parameters.Add(new SqlParameter("@bank_acc_no", Bank));
        cmd.Parameters.Add(new SqlParameter("@company_name", Company));
        if (System.String.IsNullOrEmpty(username) == true || System.String.IsNullOrEmpty(f_name) == true || System.String.IsNullOrEmpty(password) == true || System.String.IsNullOrEmpty(l_name) == true || System.String.IsNullOrEmpty(email) == true)
        {
            /* lablser.Text = "  " + "->Please Enter a Valid Value <--";
             lablser.ForeColor = System.Drawing.Color.Red;
             form1.Controls.AddAt(62, lablser);*/
            Response.Write("Please Enter a Valid Value ");

        }
        else
        {
            //Executing the SQLCommand
            conn.Open();
            int y = 0;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                ///;********************************************please updgrade it for all cases;********************************************8
                Response.Write("User already exist (Vendor)");
                y = 1;

            }
            if (y == 0)
            {
                Response.Write("Successfully registered (Vendor)");

            }

            conn.Close();
            y = 0;

        }
    }


}

