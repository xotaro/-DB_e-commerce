using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Configuration; // web.config isglobaly accesisablly in order to acces connection string from web.config
using System.Data.SqlClient; // TO USE SQL 
using System.Data;
public partial class VViewvendors : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);



        SqlCommand cmd2 = new SqlCommand("showVendors", conn);
        cmd2.CommandType = CommandType.StoredProcedure;

        conn.Open();
        //IF the output is a table, then we can read the records one at a time
        SqlDataReader rdr = cmd2.ExecuteReader(CommandBehavior.CloseConnection); // its like buffer in java  
        while (rdr.Read())
        {
            //Get the value of the attribute name in the Company table
            String username = "";
            int x1 = rdr.GetOrdinal("username");
            if (!rdr.IsDBNull(x1))
            {
                username = rdr.GetString(rdr.GetOrdinal("username"));
            }
            String activated = "";
            int x7 = rdr.GetOrdinal("activated");
            if (!rdr.IsDBNull(x7))
            {
                activated = (rdr.GetBoolean(rdr.GetOrdinal("activated"))).ToString();
            }

            String company_name = "";
            int x2 = rdr.GetOrdinal("company_name");
            if (!rdr.IsDBNull(x2))
            {
                company_name = rdr.GetString(rdr.GetOrdinal("company_name"));
            }
            String bank_acc_no = "";
            int x3 = rdr.GetOrdinal("bank_acc_no");
            if (!rdr.IsDBNull(x3))
            {
                bank_acc_no = rdr.GetString(rdr.GetOrdinal("bank_acc_no"));
            }
            String admin_username = "";
            int x4 = rdr.GetOrdinal("admin_username");
            if (!rdr.IsDBNull(x4))
            {
                admin_username = rdr.GetString(rdr.GetOrdinal("admin_username"));
            }

            Label lbl_pname22 = new Label();
            lbl_pname22.Text = "user_name->" + username + "  , ";
            form1.Controls.Add(lbl_pname22);

            Label lbl_pname1 = new Label();
            lbl_pname1.Text = "activated->" + activated + "  , ";
            form1.Controls.Add(lbl_pname1);


            Label lablser2 = new Label();
            lablser2.Text = "company_name->" + company_name + " ,"; ;
            form1.Controls.Add(lablser2);

            Label lbl_pname3 = new Label();
            lbl_pname3.Text = "bank_acc_no->" + bank_acc_no + "  , ";
            form1.Controls.Add(lbl_pname3);

            Label lablser4 = new Label();
            lablser4.Text = "admin_username->" + admin_username + "  <br /> <br />"; ;
            form1.Controls.Add(lablser4);
        }
    }
}