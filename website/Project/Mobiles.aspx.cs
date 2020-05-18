using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
public partial class Mobiles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);

     
     
        SqlCommand cmd2 = new SqlCommand("showMobile", conn);
        cmd2.CommandType = CommandType.StoredProcedure;

        conn.Open();
        //IF the output is a table, then we can read the records one at a time
        SqlDataReader rdr = cmd2.ExecuteReader(CommandBehavior.CloseConnection); // its like buffer in java  
        while (rdr.Read())
        {
            //Get the value of the attribute name in the Company table
            String mobile_number = "";
            int x1 = rdr.GetOrdinal("mobile_number");
            if (!rdr.IsDBNull(x1))
            {
                mobile_number = rdr.GetString(rdr.GetOrdinal("mobile_number"));
            }

            String username = "";
            int x2 = rdr.GetOrdinal("username");
            if (!rdr.IsDBNull(x2))
            {
                username = rdr.GetString(rdr.GetOrdinal("username"));
            }

            Label lbl_pname = new Label();
            lbl_pname.Text = "mobile_number->" + mobile_number + "  , ";
            form1.Controls.Add(lbl_pname);

            Label lablser = new Label();
            lablser.Text = "username->" + username + "  <br /> <br />"; ;
            form1.Controls.Add(lablser);


        }
    }
}