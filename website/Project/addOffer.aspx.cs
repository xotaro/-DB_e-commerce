using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


public partial class addOffer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        /*
                SqlCommand cmd = new SqlCommand("addOffer", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                string offeramount = (String)Session["OfferAmount"];
                string expirydate = (String)(Session["exp"]);

                cmd.Parameters.Add(new SqlParameter("@offeramount", offeramount));
                cmd.Parameters.Add(new SqlParameter("@expiry_date", expirydate));*/
        Boolean x = false;
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);

        SqlCommand cmd2 = new SqlCommand("showOffer", conn);
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

            String offer_amount = "";
            int x2 = rdr.GetOrdinal("offer_amount");
            if (!rdr.IsDBNull(x2))
            {
                offer_amount = (rdr.GetInt32(rdr.GetOrdinal("offer_amount"))).ToString();
            }
            string expiry_date = "";
            int x3 = rdr.GetOrdinal("expiry_date");
            if (!rdr.IsDBNull(x3))
            {
                expiry_date = (rdr.GetDateTime(rdr.GetOrdinal("expiry_date"))).ToString();
            }
           
            Label lbl_pname = new Label();
            lbl_pname.Text = "offer_id->" + offer_id + "  , ";
            form1.Controls.Add(lbl_pname);

            Label lablser = new Label();
            lablser.Text = "offer_amount->" + offer_amount + "  ,  ";
            form1.Controls.Add(lablser);

            Label lbl_date = new Label();
            lbl_date.Text = " " + "expiry_date->" + expiry_date + "  <br /> <br />"; // new line 
            form1.Controls.Add(lbl_date);
        }
    }
    }
