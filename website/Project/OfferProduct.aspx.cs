using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


public partial class OfferProduct : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Boolean x = false;
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);

        SqlCommand cmd2 = new SqlCommand("showOfferProduct", conn);
        cmd2.CommandType = CommandType.StoredProcedure;

        conn.Open();

        SqlDataReader rdr = cmd2.ExecuteReader(CommandBehavior.CloseConnection); // its like buffer in java  
        while (rdr.Read())
        {
            String offer_id = "";
            int x1 = rdr.GetOrdinal("offer_id");
            if (!rdr.IsDBNull(x1))
            {
                offer_id = (rdr.GetInt32(rdr.GetOrdinal("offer_id"))).ToString();
            }

            String serial_no = "";
            int x2 = rdr.GetOrdinal("serial_no");
            if (!rdr.IsDBNull(x2))
            {
                serial_no = (rdr.GetInt32(rdr.GetOrdinal("serial_no"))).ToString();
            }
          

            Label lbl_pname = new Label();
            lbl_pname.Text = "offer_id->" + offer_id + "  , ";
            form1.Controls.Add(lbl_pname);

            Label lablser = new Label();
            lablser.Text = "serial_no->" + serial_no + "  <br /> <br />";
            form1.Controls.Add(lablser);

         
        }
    }
}
