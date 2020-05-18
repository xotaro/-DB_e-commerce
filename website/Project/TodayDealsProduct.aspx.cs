using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration; // web.config isglobaly accesisablly in order to acces connection string from web.config
using System.Data.SqlClient; // TO USE SQL 
using System.Data;


public partial class TodayDealsProduct : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Boolean x = false;
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);

        SqlCommand cmd2 = new SqlCommand("ShowDealsOnProduct", conn);
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

            String serial_no = "";
            int x2 = rdr.GetOrdinal("serial_no");
            if (!rdr.IsDBNull(x2))
            {
                serial_no = (rdr.GetInt32(rdr.GetOrdinal("serial_no"))).ToString();
            }
           


            Label lbl_pname = new Label();
            lbl_pname.Text = "deal_id->" + deal_id + "  , ";
            form1.Controls.Add(lbl_pname);

            Label lablser = new Label();
            lablser.Text = "serial_no->" + serial_no + "  <br /> <br /> ";
            form1.Controls.Add(lablser);



        }
    }
}

