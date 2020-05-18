using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration; // web.config isglobaly accesisablly in order to acces connection string from web.config
using System.Data.SqlClient; // TO USE SQL 
using System.Data; // parent class of sql

public partial class vendor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
     string field1 = (string)(Session["field1"]); 
       Response.Write("Welcome->"+field1);
    }

    protected void ViewMobile(object sender, EventArgs e)
    {
        Response.Redirect("Mobiles.aspx",true);
    }

    protected void OffersOnProduct(object sender, EventArgs e)
    {
        Response.Redirect("OfferProduct.aspx", true);

    }
    protected void ViewOffers(object sender, EventArgs e)
    {
        Response.Redirect("addOffer.aspx", true);

    }

    protected void showProduct(object sender, EventArgs e)
    {
        Response.Redirect("vendorproduct.aspx", true);
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
            form1.Controls.AddAt(88, lablser);
        }
        Boolean x = true;
        if (System.String.IsNullOrEmpty((String)MobileNumber) == true)
        {
            x = false;

            lablser.Text = "  " + "->Please Enter a Valid Value fill in the box  <--";
            lablser.ForeColor = System.Drawing.Color.Red;
            form1.Controls.AddAt(88, lablser);
        }

        if (x == true && helper == true)
        {
            lablser.Text = "Telephone number has been successfully added";
            form1.Controls.AddAt(88, lablser);
            


        }
        helper = true;

    }
    protected void addOffer(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        Label lablser = new Label();
        String offeramount = OfferAmoutTxt.Text;
        String username = (string)(Session["field1"]);

        String exp = ExpTxt.Text;
        Session["OfferAmount"] = offeramount;
        Session["exp"] = exp;
        SqlCommand cmd = new SqlCommand("addOffer", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        Boolean hel = true;
        cmd.Parameters.Add(new SqlParameter("@offeramount", offeramount));
        cmd.Parameters.Add(new SqlParameter("@expiry_date", exp));
        SqlCommand cmd2 = new SqlCommand("isactive", conn);
        cmd2.CommandType = CommandType.StoredProcedure;
        cmd2.Parameters.Add(new SqlParameter("@username", username));
        SqlParameter feactive = cmd2.Parameters.Add("@flag", SqlDbType.Int);
        feactive.Direction = ParameterDirection.Output;
        conn.Open();
        cmd2.ExecuteNonQuery();

        Boolean x = true;
        if( System.String.IsNullOrEmpty((String)Session["OfferAmount"]) == true || System.String.IsNullOrEmpty((String)Session["exp"]) == true)
        {
            x = false;
           
            lablser.Text = "  "+"->Please Enter a Valid Value fill in the box <--";
            lablser.ForeColor = System.Drawing.Color.Red;
            form1.Controls.AddAt(62, lablser);
        }

        if (x == true)
        {
            
               try
               {
                if (feactive.Value.ToString().Equals("0"))
                {
                    lablser.Text = "  " + "->you are not an activated vendor activate first!<--";
                    lablser.ForeColor = System.Drawing.Color.Red;
                    form1.Controls.AddAt(62, lablser);
                    hel = false;
                }
                else if(feactive.Value.ToString().Equals("1"))
                {
                    cmd.ExecuteNonQuery();

                }

            }

               catch (Exception ex )
               {
             
                   if (feactive.Value.ToString().Equals("0"))
                   {
                       lablser.Text = "  " + "->you are not an activated vendor activate first!<--";
                       lablser.ForeColor = System.Drawing.Color.Red;
                       form1.Controls.AddAt(62, lablser);
                       hel = false;
                   }
                   else
                   {
                       lablser.Text = "  " + "->Please Enter a Valid Value (INT)(DATE)<--";
                       lablser.ForeColor = System.Drawing.Color.Red;
                       form1.Controls.AddAt(62, lablser);
                       hel = false;
                   }
               }
            if (hel == true)
            {

                if (feactive.Value.ToString().Equals("0"))
                {
                    lablser.Text = "  " + "->you are not an activated vendor activate first!<--";
                    lablser.ForeColor = System.Drawing.Color.Red;
                    form1.Controls.AddAt(62, lablser);
                    hel = false;
                }
                else
                { 
                lablser.Text = "Offer has been added";
                lablser.ForeColor = System.Drawing.Color.Red;

                form1.Controls.AddAt(62, lablser);
                hel = true;
            }
            }


        }



    }
    protected void MyEdit(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand("EditProduct", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        String username = (string)(Session["field1"]);
        String Serial_no = SerialNumber1.Text;
        String Product_name = Product_name1.Text;                                                                                                                                                                                                                                                                                                           
        String Cateogry = Cateogry1.Text;
        String Product_description = Product_Description1.Text;
        String Price = (Price1.Text).ToString();
        String Color = Color1.Text;
        SqlParameter feserial = cmd.Parameters.Add("@feserial", SqlDbType.Int);
        feserial.Direction = ParameterDirection.Output;
        Boolean flag = false;
        Boolean flag2 = true;
        Boolean flagPrice = true;
        cmd.Parameters.Add(new SqlParameter("@vendorname", username));
        if (System.String.IsNullOrEmpty(Serial_no) == true )
        {
            
            Label lablser = new Label();
            lablser.Text = "  " + "->Please Enter a serialnumber  <--";
            lablser.ForeColor = System.Drawing.Color.Red;
            form1.Controls.AddAt(52, lablser);
        }
        else
        {
            flag = true;
            try
            {
                int seriall = System.Convert.ToInt32(Serial_no);

            }
            catch (Exception)
            {
                flag2 = false;
                Label lablser1 = new Label();
                lablser1.Text = "  " + "->Invalid value Please Enter aValid Serial_no(INT) <--";
                lablser1.ForeColor = System.Drawing.Color.Red;
                form1.Controls.AddAt(52, lablser1);


            }

            if (flag2 == true)
            {
                int seriall = System.Convert.ToInt32(Serial_no);

                cmd.Parameters.Add(new SqlParameter("@serialnumber", seriall));
                flag2 = true;

            }

        }
      
        if (System.String.IsNullOrEmpty(Cateogry))
        {
            cmd.Parameters.Add(new SqlParameter("@category", DBNull.Value));
        }
        else
        {
            cmd.Parameters.Add(new SqlParameter("@category", Cateogry));

        }
        if (System.String.IsNullOrEmpty(Product_description))
        {
            cmd.Parameters.Add(new SqlParameter("@product_description", DBNull.Value));
        }
        else
        {
            cmd.Parameters.Add(new SqlParameter("@product_description", Product_description));

        }
        if (System.String.IsNullOrEmpty(Color))
        {
            cmd.Parameters.Add(new SqlParameter("@Color", DBNull.Value));
        }
        else
        {
            cmd.Parameters.Add(new SqlParameter("@Color", Color));

        }
        if (System.String.IsNullOrEmpty(Price))
        {
            cmd.Parameters.Add(new SqlParameter("@Price", DBNull.Value));
        }
        else
        {
            try
            {
                decimal pricer = System.Convert.ToDecimal(Price);

            }
            catch(Exception)
            {
                flagPrice = false;
                Label lablser1 = new Label();
                lablser1.Text = "  " + "->Invalid value for price number Enter Avalid value(INT) <--";
                lablser1.ForeColor = System.Drawing.Color.Red;
                form1.Controls.AddAt(52, lablser1);

            }
            if(flagPrice==true)
            {
                decimal pricer = System.Convert.ToDecimal(Price);


                cmd.Parameters.Add(new SqlParameter("@Price", pricer));
            }

          

        }
        
        if (System.String.IsNullOrEmpty(Product_name))
        {
            cmd.Parameters.Add(new SqlParameter("@product_name", DBNull.Value));
        }
        else
        {
            cmd.Parameters.Add(new SqlParameter("@product_name", Product_name));

        }

        
        conn.Open();
        if (flag == true && flag2==true && flagPrice==true)
        {
            cmd.ExecuteNonQuery();

            if (feserial.Value.ToString().Equals("1"))
             {
                flag = false;
                Label lablser1 = new Label();
                lablser1.Text = "  " + "->Product has been edited <--";
                lablser1.ForeColor = System.Drawing.Color.Red;
                form1.Controls.AddAt(52, lablser1);
            }
            else
            {
                Label lablser1 = new Label();
                lablser1.Text = "  " + "->invalid serial no(NOTEXISIT) Enter AValid One <--";
                lablser1.ForeColor = System.Drawing.Color.Red;
                form1.Controls.AddAt(52, lablser1);
            }
          
        }

        conn.Close();

    }
    protected void removeexp(object sender, EventArgs e)
    {

        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand("checkandremoveExpiredoffer", conn);
        cmd.CommandType = CommandType.StoredProcedure;
     
        String OfferID = TextBox16.Text;
        //Save the output from the procedure
        cmd.Parameters.Add(new SqlParameter("@offerid", OfferID));

        SqlParameter faild = cmd.Parameters.Add("@faild", SqlDbType.Int);
        faild.Direction = ParameterDirection.Output;
        SqlParameter notexp = cmd.Parameters.Add("@notexp", SqlDbType.Int);
        notexp.Direction = ParameterDirection.Output;

        Boolean x = true;
        Boolean last = true;
        if (System.String.IsNullOrEmpty(OfferID) == true )
        {
            x = false;
            Label lablser = new Label();

            lablser.Text = "  " + "->Please Enter a Valid Value fill in the box<--";
            lablser.ForeColor = System.Drawing.Color.Red;
            form1.Controls.AddAt(78, lablser);
        }

        if (x == true)
        {
            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                if (ex.Number == 8114)
                {
                    Label lablser = new Label();
                    lablser.Text = "Please Enter aValid Values (INT)!";
                    lablser.ForeColor = System.Drawing.Color.Red;
                    form1.Controls.AddAt(78, lablser);
                    last = false;
                }
               else  if (faild.Value.ToString().Equals("1"))
                {
                    Label lablser = new Label();

                    lablser.Text = "This offer not exisit ";
                    lablser.ForeColor = System.Drawing.Color.Red;
                    lablser.ForeColor = System.Drawing.Color.Red;


                    form1.Controls.AddAt(78, lablser);
                }
                else if (notexp.Value.ToString().Equals("1"))
                {
                    Label lablser = new Label();

                    lablser.Text = "The Offer is not expired cant remove it";
                    lablser.ForeColor = System.Drawing.Color.Red;

                    form1.Controls.AddAt(78, lablser);
                }
                else
                {
                    Label lablser = new Label();
                    lablser.Text = "Offer has ben Removed";
                    lablser.ForeColor = System.Drawing.Color.Red;

                    form1.Controls.AddAt(78, lablser);
                }
            }
            if (last == true)
            {
                if (faild.Value.ToString().Equals("1"))
                {
                    Label lablser = new Label();

                    lablser.Text = "This offer not exisit ";
                    lablser.ForeColor = System.Drawing.Color.Red;
                    lablser.ForeColor = System.Drawing.Color.Red;


                    form1.Controls.AddAt(78, lablser);
                }
                else if (notexp.Value.ToString().Equals("1"))
                {
                    Label lablser = new Label();

                    lablser.Text = "The Offer is not expired cant remove it";
                    lablser.ForeColor = System.Drawing.Color.Red;

                    form1.Controls.AddAt(78, lablser);
                }
                else
                {
                    Label lablser = new Label();
                    lablser.Text = "Offer has ben Removed";
                    lablser.ForeColor = System.Drawing.Color.Red;

                    form1.Controls.AddAt(78, lablser);
                }
            }
            




        }

    }


    protected void Post_Product(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand("postProduct", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        String username = (string)(Session["field1"]);
        String Product_name = ProductName0.Text;
        String Cateogry = Category0.Text;
        String Product_description = Product_Description0.Text;
        String Price = (Price0.Text).ToString();
        String Color = Color00.Text;
        SqlCommand cmd2 = new SqlCommand("isactive", conn);
        cmd2.CommandType = CommandType.StoredProcedure;
        cmd2.Parameters.Add(new SqlParameter("@username", username));
        SqlParameter feactive = cmd2.Parameters.Add("@flag", SqlDbType.Int);
        feactive.Direction = ParameterDirection.Output;
        conn.Open();
        cmd2.ExecuteNonQuery();
        Boolean helperf = true;
       
        Boolean flag2 = true;
        if (System.String.IsNullOrEmpty(username) == true || System.String.IsNullOrEmpty(Product_name) == true || System.String.IsNullOrEmpty(Cateogry) == true || System.String.IsNullOrEmpty(Product_description) == true || System.String.IsNullOrEmpty(Price) == true || System.String.IsNullOrEmpty(Color) == true)
        {
            
            Label lablser = new Label();
             lablser.Text = "  " + "->Please Enter a Valid Value Fill in the boxes <--";
             lablser.ForeColor = System.Drawing.Color.Red;
             form1.Controls.AddAt(24, lablser);
           

        }
        else
        {

            try
            {
                decimal pricer = System.Convert.ToDecimal(Price);
              

            }
            catch(Exception)
            {
                flag2 = false;
                Label lablser1 = new Label();
                lablser1.Text = "  " + "->Invalid Price values Enter AValidValue(INT) <--";
                lablser1.ForeColor = System.Drawing.Color.Red;
                form1.Controls.AddAt(24, lablser1);

            }
            if(flag2==true)
            {
                decimal pricer = System.Convert.ToDecimal(Price);
                cmd.Parameters.Add(new SqlParameter("@price", pricer));


            }
            cmd.Parameters.Add(new SqlParameter("@vendorUsername", username));
            cmd.Parameters.Add(new SqlParameter("@product_name", Product_name));
            cmd.Parameters.Add(new SqlParameter("@category", Cateogry));
            cmd.Parameters.Add(new SqlParameter("@product_description", Product_description));
            cmd.Parameters.Add(new SqlParameter("@Color", Color));


         if(feactive.Value.ToString().Equals("0"))
            {
            
                Label lablser1 = new Label();
                lablser1.Text = "  " + "->Vendor is not activated Vendor must be activated first !<--";
                lablser1.ForeColor = System.Drawing.Color.Red;
                form1.Controls.AddAt(24, lablser1);
                
            }
              
            else if (flag2==true && feactive.Value.ToString().Equals("1"))
            {
               
                cmd.ExecuteNonQuery();
                Label lablser1 = new Label();
                lablser1.Text = "  " + "->Product has been posted <--";
                lablser1.ForeColor = System.Drawing.Color.Red;
                form1.Controls.AddAt(24, lablser1);
                conn.Close();
         
               
            }
               
         
              
            



        }
    }

    protected void AppyOffer(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand("applyOffer", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        String username = (string)(Session["field1"]);
        String OfferID = TextBox14.Text;
        String Serialno = TextBox15.Text;
        cmd.Parameters.Add(new SqlParameter("@vendorname", username));
        cmd.Parameters.Add(new SqlParameter("@offerid", OfferID));
        cmd.Parameters.Add(new SqlParameter("@serial", Serialno));
        SqlParameter notyou = cmd.Parameters.Add("@notyou", SqlDbType.Int);  // 3amlha bl 3aks :D 
        notyou.Direction = ParameterDirection.Output;
        SqlParameter feexpire = cmd.Parameters.Add("@feexpire", SqlDbType.Int);  // 3amlha bl 3aks :D 
        feexpire.Direction = ParameterDirection.Output;
        SqlParameter activeoffer = cmd.Parameters.Add("@activeoffer", SqlDbType.Int);
        activeoffer.Direction = ParameterDirection.Output;
        SqlParameter feserial = cmd.Parameters.Add("@feserial", SqlDbType.Int);  
        feserial.Direction = ParameterDirection.Output;
        SqlParameter feoffer = cmd.Parameters.Add("@feoffer", SqlDbType.Int);  
        feoffer.Direction = ParameterDirection.Output;
        Boolean help = true;
        Boolean x = true;
        Boolean last = true;
        int a = 55;
        if (System.String.IsNullOrEmpty(username) == true || System.String.IsNullOrEmpty(OfferID) == true || System.String.IsNullOrEmpty(Serialno) == true)
        {
            x = false;
            Label lablser = new Label();

            lablser.Text = "  " + "->Please Enter a Valid Value fill in the box <--";
            lablser.ForeColor = System.Drawing.Color.Red;
            form1.Controls.AddAt(72, lablser);
        }

        if (x == true)
        {
            conn.Open();
            try
            {
                a = cmd.ExecuteNonQuery();

            }
            catch(SqlException ex)
            {
                if(ex.Number == 8114)
                {
                    last = false;
                    Label lablser = new Label();

                    lablser.Text = "Invalid Value Please Enter a Valid Values(INT)";
                    form1.Controls.AddAt(72, lablser);
                }

               else  if (feoffer.Value.ToString().Equals("0"))
                {
                    Label lablser = new Label();

                    lablser.Text = "invalid offerid , please apply offer first";
                    form1.Controls.AddAt(72, lablser);
                }
                else if (notyou.Value.ToString().Equals("0") && feserial.Value.ToString().Equals("1"))
                {
                    Label lablser = new Label();

                    lablser.Text = "Its not your item to make Offer on it (DO OFFERS ON YOUR ITEMS ONLY !)";
                    form1.Controls.AddAt(72, lablser);
                }
                else if (activeoffer.Value.ToString().Equals("1"))
                {
                    Label lablser = new Label();

                    lablser.Text = "The Product has an Active Offer ";
                    form1.Controls.AddAt(72, lablser);
                }
                else if (feserial.Value.ToString().Equals("0"))
                {
                    Label lablser = new Label();

                    lablser.Text = "Invalid serialno please enter avalid serialno ";
                    form1.Controls.AddAt(72, lablser);
                }
                else if (feexpire.Value.ToString().Equals("1"))
                {
                    Label lablser = new Label();

                    lablser.Text = "Cant ApplyThisOffer Because its expired make anotherone ";
                    form1.Controls.AddAt(72, lablser);
                }
                else
                {
                    Label lablser = new Label();


                    lablser.Text = "The Offer has been applied";
                    form1.Controls.AddAt(72, lablser);
                }



            }
            if (last == true)
            {

                if (feoffer.Value.ToString().Equals("0"))
                {
                    Label lablser = new Label();

                    lablser.Text = "invalid offerid , please apply offer first";
                    form1.Controls.AddAt(72, lablser);
                }
                else if (notyou.Value.ToString().Equals("0") && feserial.Value.ToString().Equals("1"))
                {
                    Label lablser = new Label();

                    lablser.Text = "Its not your item to make Offer on it (DO OFFERS ON YOUR ITEMS ONLY !)";
                    form1.Controls.AddAt(72, lablser);
                }
                else if (activeoffer.Value.ToString().Equals("1"))
                {
                    Label lablser = new Label();

                    lablser.Text = "The Product has an Active Offer ";
                    form1.Controls.AddAt(72, lablser);
                }
                else if (feserial.Value.ToString().Equals("0"))
                {
                    Label lablser = new Label();

                    lablser.Text = "Invalid serialno please enter avalid serialno ";
                    form1.Controls.AddAt(72, lablser);
                }
                else if (feexpire.Value.ToString().Equals("1"))
                {
                    Label lablser = new Label();

                    lablser.Text = "Cant ApplyThisOffer Because its expired make anotherone ";
                    form1.Controls.AddAt(72, lablser);
                }
                else
                {
                    Label lablser = new Label();


                    lablser.Text = "The Offer has been applied";
                    form1.Controls.AddAt(72, lablser);
                }

            }





        }
      
      
    }
}



/*//@vendorname varchar(20),
@serialnumber int, 
@product_name varchar(20),
@category varchar(20),
@product_description text,
@price decimal (10,2),
@color varchar(20)*/
