using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration; // web.config isglobaly accesisablly in order to acces connection string from web.config
using System.Data.SqlClient; // TO USE SQL 
using System.Data;


public partial class TodayDeals : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Boolean x = false;
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);

        SqlCommand cmd2 = new SqlCommand("showDeals", conn);
        cmd2.CommandType = CommandType.StoredProcedure;

        conn.Open();

        SqlDataReader rdr = cmd2.ExecuteReader(CommandBehavior.CloseConnection); // its like buffer in java  
        while (rdr.Read())
        {
            String deal_id = "";
            int x1 = rdr.GetOrdinal("deal_id");
            if (!rdr.IsDBNull(x1))
            {
                deal_id = (rdr.GetInt32(rdr.GetOrdinal("deal_id"))).ToString();
            }

            String deal_amount = "";
            int x2 = rdr.GetOrdinal("deal_amount");
            if (!rdr.IsDBNull(x2))
            {
                deal_amount = (rdr.GetInt32(rdr.GetOrdinal("deal_amount"))).ToString();
            }
            string expiry_date = "";
            int x3 = rdr.GetOrdinal("expiry_date");
            if (!rdr.IsDBNull(x3))
            {
                expiry_date = (rdr.GetDateTime(rdr.GetOrdinal("expiry_date"))).ToString();
            }


            String admin_username = "";
            int x5 = rdr.GetOrdinal("admin_username");
            if (!rdr.IsDBNull(x5))
            {
                admin_username = rdr.GetString(rdr.GetOrdinal("admin_username"));
            }



            Label lbl_pname = new Label();
            lbl_pname.Text = "deal_id->" + deal_id + "  , ";
            form1.Controls.Add(lbl_pname);

            Label lablser = new Label();
            lablser.Text = "deal_amount->" + deal_amount + "  ,  ";
            form1.Controls.Add(lablser);

            Label lbl_date = new Label();
            lbl_date.Text = " " + "expiry_date->" + expiry_date + " ,"; // new line 
            form1.Controls.Add(lbl_date);

            Label l1 = new Label();
            l1.Text = "admin_username->" + admin_username + "  <br /> <br /> ";
            form1.Controls.Add(l1);

        }
    }
}
    
