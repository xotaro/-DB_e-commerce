using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration; // web.config isglobaly accesisablly in order to acces connection string from web.config
using System.Data.SqlClient; // TO USE SQL 
using System.Data; 


public partial class Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String AdminUserName = (string)(Session["field1"]);
        Response.Write("Welcome Admin->>>" + AdminUserName);
    }
    protected void TodayDeals(object sender, EventArgs e)
    {
        Response.Redirect("TodayDeals.aspx", true);
   
    }
    protected void DealsProduct(object sender, EventArgs e)
    {
        Response.Redirect("TodayDealsProduct.aspx", true);

    }
   
    protected void removeExp(object sender, EventArgs e)
    {
        Label lablser = new Label();
        bool helper = true;
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand("removeExpiredDeal", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        String deal_id = TextBox5.Text;
        cmd.Parameters.Add(new SqlParameter("@deal_iD", deal_id));
        conn.Open();
        /*  SqlParameter exvendor = cmd.Parameters.Add("@exvendor", SqlDbType.Int);
          exvendor.Direction = ParameterDirection.Output;
          SqlParameter already = cmd.Parameters.Add("@already", SqlDbType.Int);
          already.Direction = ParameterDirection.Output;
  */
        //     cmd.ExecuteNonQuery();


        Boolean x = true;
      

        if (System.String.IsNullOrEmpty(deal_id) == true )
        {
            x = false;

            lablser.Text = "  " + "->Please Enter a Valid Value fill in the box  <--";
            lablser.ForeColor = System.Drawing.Color.Red;
            form1.Controls.AddAt(42, lablser);
        }
        else
        {
           
             try
             {
                 int a = cmd.ExecuteNonQuery();
                    ///8114 errornumberfor  (ERROR Converting data type to nvchar to int )
                 if(a==-1)
                 {
                 
                     lablser.Text = "  " + "->Deal ID not valid  Enter avalid one  <--";
                     lablser.ForeColor = System.Drawing.Color.Red;
                     form1.Controls.AddAt(42, lablser);
                 }
                 
                 else
                 {
                 lablser.Text = "  " + "->Deal has been removed  <--";
                     lablser.ForeColor = System.Drawing.Color.Red;
                     form1.Controls.AddAt(42, lablser);
                 }


             }
             catch(SqlException ex)
             {
                 if(ex.Number==8114)
                 {
                     lablser.Text = "  " + "->Please Enter a Valid Value (INT)  <--";
                     lablser.ForeColor = System.Drawing.Color.Red;
                     form1.Controls.AddAt(42, lablser);
                 }
             }

        }
    }
    protected void addDeal(object sender, EventArgs e)
    {
        Label lablser = new Label();
        bool helper = true;
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand("addTodaysDealOnProduct", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        String deal_id = TextBox3.Text;
        String serial_no = TextBox4.Text;
        cmd.Parameters.Add(new SqlParameter("@deal_id", deal_id));
        cmd.Parameters.Add(new SqlParameter("@serial_no", serial_no));
        conn.Open();
          SqlParameter alreadyapply = cmd.Parameters.Add("@alreadyapply", SqlDbType.Int);
        alreadyapply.Direction = ParameterDirection.Output;
          SqlParameter feexpire = cmd.Parameters.Add("@feexpire", SqlDbType.Int);
        feexpire.Direction = ParameterDirection.Output;
  

        Boolean x = true;

        if (System.String.IsNullOrEmpty(deal_id) == true || System.String.IsNullOrEmpty(serial_no) == true)
        {
            x = false;

            lablser.Text = "  " + "->Please Enter a Valid Value fill in the box  <--";
            lablser.ForeColor = System.Drawing.Color.Red;
            form1.Controls.AddAt(36, lablser);
        }
        else
        {

            

              try
               {
                   int a = cmd.ExecuteNonQuery();
                ///8114 errornumberfor  (ERROR Converting data type to nvchar to int )
                  if (alreadyapply.Value.ToString().Equals("1"))
                   {

                       lablser.Text = "  " + "->The Product has an active deal  <--";
                       lablser.ForeColor = System.Drawing.Color.Red;
                       form1.Controls.AddAt(36, lablser);
                   }

                   else if (feexpire.Value.ToString().Equals("1"))
                   {
                       lablser.Text = "  " + "->The deal is expired Apply another offer <--";
                       lablser.ForeColor = System.Drawing.Color.Red;
                       form1.Controls.AddAt(36, lablser);
                   }
                  else if(a==-1)
                {

                    lablser.Text = "  " + "->Please Enter a Valid Value Serial or Deal_ID  <--";
                    lablser.ForeColor = System.Drawing.Color.Red;
                    form1.Controls.AddAt(36, lablser);
                }
            
                else 
                {
                    lablser.Text = "  " + "->Deal has been added to the product <--";
                    lablser.ForeColor = System.Drawing.Color.Red;
                    form1.Controls.AddAt(36, lablser);
                }


            }
               catch (SqlException ex)
               {
                   if (ex.Number == 8114)
                   {
                       lablser.Text = "  " + "->Please Enter a Valid Value (INT)  <--";
                       lablser.ForeColor = System.Drawing.Color.Red;
                       form1.Controls.AddAt(36, lablser);
                   }
                    if (ex.Number == 547)
                   {
                       lablser.Text = "  " + "->Please Enter a Valid Value Serial or Deal_ID  <--";
                       lablser.ForeColor = System.Drawing.Color.Red;
                       form1.Controls.AddAt(36, lablser);
                   }
               }

         }
        }

        protected void CreateTodayDeal(object sender, EventArgs e)
    {
        Label lablser = new Label();
        bool helper = true;
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand("createTodaysDeal", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        String AdminUserName = (string)(Session["field1"]);
        String deal_amount = TextBox1.Text;
        String expiry_date = TextBox2.Text;
        cmd.Parameters.Add(new SqlParameter("@deal_amount", deal_amount));
        cmd.Parameters.Add(new SqlParameter("@admin_username", AdminUserName));
        cmd.Parameters.Add(new SqlParameter("@expiry_date", expiry_date));
       conn.Open();
      /*  SqlParameter exvendor = cmd.Parameters.Add("@exvendor", SqlDbType.Int);
        exvendor.Direction = ParameterDirection.Output;
        SqlParameter already = cmd.Parameters.Add("@already", SqlDbType.Int);
        already.Direction = ParameterDirection.Output;
*/

        Boolean x = true;

        if (System.String.IsNullOrEmpty(AdminUserName) == true || System.String.IsNullOrEmpty(deal_amount) == true || System.String.IsNullOrEmpty(expiry_date) == true  )
        {
            x = false;

            lablser.Text = "  " + "->Please Enter a Valid Value fill in the box  <--";
            lablser.ForeColor = System.Drawing.Color.Red;
            form1.Controls.AddAt(26, lablser);
        }
        else
        {

            try
            {
                int a = cmd.ExecuteNonQuery();
                ///8114 errornumberfor  (ERROR Converting data type to nvchar to int )
                if (a == -1)
                {

                    lablser.Text = "  " + "->Deal Amount or SerialNo not valid value  Enter avalid value  <--";
                    lablser.ForeColor = System.Drawing.Color.Red;
                    form1.Controls.AddAt(26, lablser);
                }

                else
                {
                    lablser.Text = "  " + "->Deal has been created  <--";
                    lablser.ForeColor = System.Drawing.Color.Red;
                    form1.Controls.AddAt(26, lablser);
                }


            }
            catch (SqlException ex)
            {
                if (ex.Number == 8114)
                {
                    lablser.Text = "  " + "->Please Enter a Valid Value (INT)(Date)  <--";
                    lablser.ForeColor = System.Drawing.Color.Red;
                    form1.Controls.AddAt(26, lablser);
                }
            }
        }
    }
    protected void ViewVendors(object sender, EventArgs e)
    {
        Response.Redirect("VViewvendors.aspx", true);
    }
  

    protected void ViewMobile(object sender, EventArgs e)
    {
        Response.Redirect("Mobiles.aspx", true);
    }

    protected void AddMobile(object sender, EventArgs e)
    {
        Label lablser = new Label();


        // Session["MobileNumber"] = MobileNumber;

        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);

        SqlCommand cmd = new SqlCommand("addMobile", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        string user_name = (String)(Session["field1"]);
        String MobileNumber = TNumber.Text;

        cmd.Parameters.Add(new SqlParameter("@mobile_number", MobileNumber));
        cmd.Parameters.Add(new SqlParameter("@username", user_name));
        Boolean helper = true;
        conn.Open();
        try
        {
            cmd.ExecuteNonQuery();

        }
        catch (Exception)
        {
            helper = false;
            lablser.Text = "  " + "->Phone already exisit <--";
            lablser.ForeColor = System.Drawing.Color.Red;
            form1.Controls.AddAt(46, lablser);
        }
        Boolean x = true;
        if (System.String.IsNullOrEmpty((String)MobileNumber) == true)
        {
            x = false;

            lablser.Text = "  " + "->Please Enter a Valid Value fill in the box  <--";
            lablser.ForeColor = System.Drawing.Color.Red;
            form1.Controls.AddAt(46, lablser);
        }

        if (x == true && helper == true)
        {
            lablser.Text = "Telephone number has been successfully added";
            form1.Controls.AddAt(46, lablser);



        }
        helper = true;

    }


    protected void activeVendor(object sender, EventArgs e)
    {
        Label lablser = new Label();
        bool helper = true;
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand("activateVendors", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        String AdminUserName = (string)(Session["field1"]);
        String Vendorname = VendorN.Text;
        cmd.Parameters.Add(new SqlParameter("@admin_username", AdminUserName));
        cmd.Parameters.Add(new SqlParameter("@vendor_username", Vendorname));
        conn.Open();
        SqlParameter exvendor = cmd.Parameters.Add("@exvendor", SqlDbType.Int);  
        exvendor.Direction = ParameterDirection.Output;
        SqlParameter already = cmd.Parameters.Add("@already", SqlDbType.Int);
        already.Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();


        Boolean x = true;
    
        if (System.String.IsNullOrEmpty(Vendorname) == true)
        {
            x = false;

            lablser.Text = "  " + "->Please Enter a Valid Value fill in the box  <--";
            lablser.ForeColor = System.Drawing.Color.Red;
            form1.Controls.AddAt(6, lablser);
        }

        else if (exvendor.Value.ToString().Equals("0"))
        {
            lablser.Text = "  " + "->Vendor not exist Please Enter a Valid one  <--";
            lablser.ForeColor = System.Drawing.Color.Red;
            form1.Controls.AddAt(6, lablser);
        }
        else if (already.Value.ToString().Equals("1"))
        {
            lablser.Text = "  " + "->Venodor already activated<--";
            lablser.ForeColor = System.Drawing.Color.Red;
            form1.Controls.AddAt(6, lablser);
        }
       
       else

        {
            lablser.Text = "  " + "->Vendor has been activated <--";
            form1.Controls.AddAt(6, lablser);
        }
        x = true;
        conn.Close();



    }
    protected void reviewOrder(object sender, EventArgs e)
    {
        Response.Redirect("Orders.aspx", true);

    }
    protected void updatestat(object sender, EventArgs e)
    {
        Label lablser = new Label();
        Boolean lol = true;
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand("updateOrderStatusInProcess", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        String order_id = Ordid.Text;
        cmd.Parameters.Add(new SqlParameter("@order_no", order_id));
        conn.Open();
        SqlParameter exorder = cmd.Parameters.Add("@exorder", SqlDbType.Int);
        exorder.Direction = ParameterDirection.Output;
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch ( Exception)
        {
           
            lablser.Text = "  " + "->Please Enter a Valid Value (INT)  <--";
            lablser.ForeColor = System.Drawing.Color.Red;
            form1.Controls.AddAt(16, lablser);
            lol = false;
        }

        if (lol == true)
        {
            Boolean x = true;

            if (System.String.IsNullOrEmpty(order_id) == true)
            {
                x = false;

                lablser.Text = "  " + "->Please Enter a Valid Value fill in the box  <--";
                lablser.ForeColor = System.Drawing.Color.Red;
                form1.Controls.AddAt(16, lablser);
            }

            else if (exorder.Value.ToString().Equals("0"))
            {
                lablser.Text = "  " + "->Order not exist Please Enter a Valid one  <--";
                lablser.ForeColor = System.Drawing.Color.Red;
                form1.Controls.AddAt(16, lablser);
            }
        
              else

              {
                  lablser.Text = "  " + "->Order state has been changed to in process <--";
                lablser.ForeColor = System.Drawing.Color.Red;

                form1.Controls.AddAt(16, lablser);
              }
            x = true;
            conn.Close();
        }


    }
}