 using System;
using System.Collections.Generic;
using System.Linq;           
using System.Web;
using System.Web.UI;                            
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Companies : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);

        SqlCommand cmd = new SqlCommand("vendorviewProducts", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        string username = (string)(Session["field1"]);


        //pass parameters to the stored procedure
        cmd.Parameters.Add(new SqlParameter("@vendorname", username));

        conn.Open(); //we opened connection fast because 
     
        //IF the output is a table, then we can read the records one at a time
        SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // its like buffer in java  
        while (rdr.Read())
        {
            //Get the value of the attribute name in the Company table
             String serial_no ="";
            int x1 = rdr.GetOrdinal("serial_no");
              if (!rdr.IsDBNull(x1))
            {
               serial_no =  (rdr.GetInt32(rdr.GetOrdinal("serial_no"))).ToString();
            }


            string productname = "";
            int x2 = rdr.GetOrdinal("product_name");
            if (!rdr.IsDBNull(x2))
            {
                productname = rdr.GetString(rdr.GetOrdinal("product_name"));
            }
            string cate = "";
            int x3 = rdr.GetOrdinal("category");
            if (!rdr.IsDBNull(x3))
            {
                cate = rdr.GetString(rdr.GetOrdinal("category"));
            }
            string descr = "";
            int x4 = rdr.GetOrdinal("category");
            if (!rdr.IsDBNull(x4))
            {
                 descr = rdr.GetString(rdr.GetOrdinal("product_description"));
            }
            String price = "";
            int x5 = rdr.GetOrdinal("price");
            if (!rdr.IsDBNull(x5))
            {
                price = (rdr.GetDecimal(rdr.GetOrdinal("price"))).ToString();
            }

            String final_price = "";
            int x6 = rdr.GetOrdinal("final_price");
            if (!rdr.IsDBNull(x6))
            {
                final_price = (rdr.GetDecimal(rdr.GetOrdinal("final_price"))).ToString();
            }
            string color = "";
            int x7 = rdr.GetOrdinal("color");
            if (!rdr.IsDBNull(x7))
            {
                color = rdr.GetString(rdr.GetOrdinal("color"));
            }
            String available = "";
            string available1 = "";
            int xend = rdr.GetOrdinal("available");
            if (!rdr.IsDBNull(xend))
            {
                available = (rdr.GetBoolean(rdr.GetOrdinal("available"))).ToString();
                if (available=="True")
                {
                    available1 = "1";
                }
                else
                {
                    available1 = "0";
                }
            }


            String rate = "";
            int x9 = rdr.GetOrdinal("rate");
            if (!rdr.IsDBNull(x9))
            {
                rate = (rdr.GetInt32(rdr.GetOrdinal("rate"))).ToString();
            }
            string vendor_username = rdr.GetString(rdr.GetOrdinal("vendor_username"));
            int x8 = rdr.GetOrdinal("vendor_username");
            if (!rdr.IsDBNull(x8))
            {
                vendor_username = rdr.GetString(rdr.GetOrdinal("vendor_username"));
            }
            String customer_username = "";
         int   x = rdr.GetOrdinal("customer_username");
            if(!rdr.IsDBNull(x))
            {
                customer_username = rdr.GetString(rdr.GetOrdinal("customer_username"));
            }
            String customer_order_id = "";
            int x11 = rdr.GetOrdinal("customer_order_id");
            if (!rdr.IsDBNull(x11))
            {
                customer_order_id = (rdr.GetInt32(rdr.GetOrdinal("customer_order_id"))).ToString();
            }
        

            //Create a new label and add it to the HTML form
            Label lbl_pname = new Label();
            lbl_pname.Text ="productname->"+ productname + "  , ";
               form1.Controls.Add(lbl_pname);

            Label lablser = new Label();
            lablser.Text = "serialno->" + serial_no + "  ,  ";
            form1.Controls.Add(lablser);

            Label lbl_cat = new Label();
            lbl_cat.Text = "categ->" + cate + "  ,  ";
            form1.Controls.Add(lbl_cat);

            Label lbl_price = new Label();
            lbl_price.Text = "price->" + price + "  ,  ";
            form1.Controls.Add(lbl_price);

            Label lbl_final = new Label();
            lbl_final.Text = "final_price->" + final_price + "  ,  ";
            form1.Controls.Add(lbl_final);

            Label lbl_color = new Label();
            lbl_color.Text = "color->" + color + "  ,  ";
            form1.Controls.Add(lbl_color);

              Label lbl_ava = new Label();
              lbl_ava.Text = "available->" + available1 + "  ,  ";
               form1.Controls.Add(lbl_ava);

            Label lbl_rate = new Label();
            lbl_rate.Text = "rate->" + rate + "  ,  ";
            form1.Controls.Add(lbl_rate);

            Label lbl_custuser = new Label();
            lbl_custuser.Text = "customerusername->" + customer_username + "  ,  ";
            form1.Controls.Add(lbl_custuser);

            Label lbl_custid = new Label();
            lbl_custid.Text = "customer_order_id->" + customer_order_id + "  ,  ";
            form1.Controls.Add(lbl_custid);

            Label lbl_vndrname = new Label();
            lbl_vndrname.Text = "vendor_username->" + vendor_username + "  ,  ";
            form1.Controls.Add(lbl_vndrname);


            Label lbl_desc = new Label();
            lbl_desc.Text = " "+ "description->"+descr + "  <br /> <br />"; // new line 
             form1.Controls.Add(lbl_desc);
        }
        //this is how you retrive data from session variable.
     //   string field1 = (string)(Session["field1"]); // type casting becuz seession array of objects 
     //   Response.Write(field1);
    }
}