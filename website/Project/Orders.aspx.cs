using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
public partial class Orders : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("reviewOrders", conn);
            conn.Open();

            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // its like buffer in java  
            while (rdr.Read())
            {
                String order_no = "";
                int x1 = rdr.GetOrdinal("order_no");
                if (!rdr.IsDBNull(x1))
                {
                    order_no = (rdr.GetInt32(rdr.GetOrdinal("order_no"))).ToString();
                }
                String order_date = "";
                int x11 = rdr.GetOrdinal("order_date");
                if (!rdr.IsDBNull(x11))
                {
                    order_date = (rdr.GetDateTime(rdr.GetOrdinal("order_date"))).ToString();
                }

                String total_amount = "";
                int x2 = rdr.GetOrdinal("total_amount");
                if (!rdr.IsDBNull(x2))
                {
                    total_amount = (rdr.GetDecimal(rdr.GetOrdinal("total_amount"))).ToString();

                }

                String cash_amount = "";
                int x3 = rdr.GetOrdinal("cash_amount");
                if (!rdr.IsDBNull(x3))
                {
                    cash_amount = (rdr.GetDecimal(rdr.GetOrdinal("cash_amount"))).ToString();
                }

                String credit_amount = "";
                int x33 = rdr.GetOrdinal("credit_amount");
                if (!rdr.IsDBNull(x33))
                {
                    credit_amount = (rdr.GetDecimal(rdr.GetOrdinal("credit_amount"))).ToString();
                }

                string payment_type = "";
                int x4 = rdr.GetOrdinal("payment_type");
                if (!rdr.IsDBNull(x4))
                {
                    payment_type = rdr.GetString(rdr.GetOrdinal("payment_type"));
                }
                string order_status = "";
                int x5 = rdr.GetOrdinal("order_status");
                if (!rdr.IsDBNull(x5))
                {
                    order_status = rdr.GetString(rdr.GetOrdinal("order_status"));
                }
                String remaining_days = "";
                int x6 = rdr.GetOrdinal("remaining_days");
                if (!rdr.IsDBNull(x6))
                {
                    remaining_days = (rdr.GetInt32(rdr.GetOrdinal("remaining_days"))).ToString();
                }

                String time_limit = "";
                int x7 = rdr.GetOrdinal("time_limit");
                if (!rdr.IsDBNull(x7))
                {
                    time_limit = (rdr.GetInt32(rdr.GetOrdinal("time_limit"))).ToString();
                }



                string customer_name = "";
                int x8 = rdr.GetOrdinal("customer_name");
                if (!rdr.IsDBNull(x8))
                {
                    customer_name = rdr.GetString(rdr.GetOrdinal("customer_name"));
                }


                String delivery_id = "";
                int x9 = rdr.GetOrdinal("delivery_id");
                if (!rdr.IsDBNull(x9))
                {
                    delivery_id = (rdr.GetInt32(rdr.GetOrdinal("delivery_id"))).ToString();
                }


                string creditCard_number = "";
                int x10 = rdr.GetOrdinal("creditCard_number");
                if (!rdr.IsDBNull(x10))
                {
                    creditCard_number = rdr.GetString(rdr.GetOrdinal("creditCard_number"));
                }

                string Gift_Card_code_used = "";
                int x12 = rdr.GetOrdinal("Gift_Card_code_used");
                if (!rdr.IsDBNull(x12))
                {
                    Gift_Card_code_used = rdr.GetString(rdr.GetOrdinal("Gift_Card_code_used"));
                }




                Label lbl_pname = new Label();
                lbl_pname.Text = "order_no->" + order_no + "  , ";
                form1.Controls.Add(lbl_pname);

                Label lablser = new Label();
                lablser.Text = "order_date->" + order_date + "  ,  ";
                form1.Controls.Add(lablser);

                Label lbl_cat = new Label();
                lbl_cat.Text = "total_amount->" + total_amount + "  ,  ";
                form1.Controls.Add(lbl_cat);

                Label lbl_price = new Label();
                lbl_price.Text = "cash_amount->" + cash_amount + "  ,  ";
                form1.Controls.Add(lbl_price);

                Label lbl_final = new Label();
                lbl_final.Text = "credit_amount->" + credit_amount + "  ,  ";
                form1.Controls.Add(lbl_final);

                Label lbl_color = new Label();
                lbl_color.Text = "payment_type->" + payment_type + "  ,  ";
                form1.Controls.Add(lbl_color);

                Label lbl_ava = new Label();
                lbl_ava.Text = "order_status->" + order_status + "  ,  ";
                form1.Controls.Add(lbl_ava);

                Label lbl_rate = new Label();
                lbl_rate.Text = "remaining_days->" + remaining_days + "  ,  ";
                form1.Controls.Add(lbl_rate);

                Label lbl_custuser = new Label();
                lbl_custuser.Text = "time_limit->" + time_limit + "  ,  ";
                form1.Controls.Add(lbl_custuser);

                Label lbl_custid = new Label();
                lbl_custid.Text = "customer_name->" + customer_name + "  ,  ";
                form1.Controls.Add(lbl_custid);

                Label lvl = new Label();
                lvl.Text = "delivery_id->" + delivery_id + "  ,  ";
                form1.Controls.Add(lvl);

                Label lbl_CCN = new Label();
                lbl_CCN.Text = "creditCard_number->" + creditCard_number + "  ,  ";
                form1.Controls.Add(lbl_CCN);

                Label lbl_desc = new Label();
                lbl_desc.Text = " " + "Gift_Card_code_used->" + Gift_Card_code_used + "  <br /> <br />"; // new line 
                form1.Controls.Add(lbl_desc);
            }
        }
    }
}