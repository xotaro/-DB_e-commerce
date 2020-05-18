using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration; // web.config isglobaly accesisablly in order to acces connection string from web.config
using System.Data.SqlClient; // TO USE SQL 
using System.Data; // parent class of sql

public partial class Customer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void makeOrder(object sender, EventArgs e)
    {
        Label l1 = new Label();
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand("makeOrder", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        conn.Open();
        String username = (string)(Session["field1"]);

        cmd.Parameters.Add(new SqlParameter("@customername", username));
        cmd.ExecuteNonQuery();
        SqlCommand cmd2 = new SqlCommand("select order_no, customer_name, total_amount from Orders order by order_no DESC", conn);
        SqlDataReader rdr = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
        Boolean flag = false;
        Boolean flag2 = false;
        int orderID;
        String name;
        while (rdr.Read() && flag == false)
        {

            name = rdr.GetString(rdr.GetOrdinal("customer_name"));
            if (name.Equals(username))
            {




                try
                {
                    Session["total_amount"] = rdr.GetInt32(rdr.GetOrdinal("total_amount"));

                }
                catch (System.Data.SqlTypes.SqlNullValueException exc)
                {
                    flag2 = true;
                }

                if (flag2 == true)
                {
                    Label5.Text = "Your Cart Is Empty, Cannot Make Order!";
                    Label5.ForeColor = System.Drawing.Color.Red;




                }
                else
                {
                    orderID = rdr.GetInt32(rdr.GetOrdinal("order_no"));

                    Session["orderID"] = orderID;
                    Label5.Text = "Success! " + "Order ID: " + Session["orderID"] + ", " + "Total Amount: " + Session["total_amount"];
                    Label5.ForeColor = System.Drawing.Color.Green;
                }
                flag = true;





            }
        }
        rdr.Close(); // <- too easy to forget
        rdr.Dispose();
        conn.Open();
        SqlCommand cmd3 = new SqlCommand("delete from Orders where total_amount = null", conn);

        cmd3.ExecuteNonQuery();

        //Response.Redirect("makeOrder.aspx", true);

    }

    protected void specifyAmount(object sender, EventArgs e)
    {
        Label l1 = new Label();
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd0 = new SqlCommand("specifyAmount", conn);
        cmd0.CommandType = CommandType.StoredProcedure;
        conn.Open();
        String username1 = (string)(Session["field1"]);

        String Order_ID = OrderID.Text;
        String Cash_Amount = (Cash.Text).ToString();
        String Credit_Amount = (Credit.Text).ToString();
        Boolean flag = false;
        Boolean flags = false;
        Boolean flags2 = false;
        Boolean flags3 = false;
        if (System.String.IsNullOrEmpty(Order_ID))
        {
            Label6.Text = "Please Enter an Order ID";
            Label6.ForeColor = System.Drawing.Color.Red;

        }
        else
        {

            try
            {

                Decimal Cash_A = System.Convert.ToDecimal(Cash_Amount);



            }
            catch (Exception)
            {

                if (!System.String.IsNullOrEmpty(Cash_Amount))
                {
                    flags = true;
                }
            }
            try
            {
                Decimal Credit_A = System.Convert.ToDecimal(Credit_Amount);
            }
            catch (Exception)
            {
                if (!System.String.IsNullOrEmpty(Credit_Amount))
                {
                    flags2 = true;
                }
            }
            try
            {
                int IDO = System.Convert.ToInt32(Order_ID);
            }
            catch (Exception)
            {
                flags3 = true;
            }

            if (flags == false && flags2 == false && flags3 == false)
            {
                Decimal Cash_A;
                Decimal Credit_A;
                int IDO = System.Convert.ToInt32(Order_ID);
                if (System.String.IsNullOrEmpty(Cash_Amount))
                {
                    Cash_A = 0;
                }
                else
                {
                    Cash_A = System.Convert.ToDecimal(Cash_Amount);
                }
                if (System.String.IsNullOrEmpty(Credit_Amount))
                {
                    Credit_A = 0;
                }
                else
                {
                    Credit_A = System.Convert.ToDecimal(Credit_Amount);
                }
                if (Credit_A > 0 && Cash_A > 0)
                {
                    Label6.Text = "Please Only Choose One Payment Method";
                    Label6.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("select username, points from Customer", conn);
                    SqlDataReader rdr = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                    Boolean flag2 = false;
                    Boolean flag3 = false;
                    while (rdr.Read() && flag2 == false)
                    {
                        String name1 = rdr.GetString(rdr.GetOrdinal("username"));
                        if (username1 == name1)
                        {
                            Session["Points"] = rdr.GetInt32(rdr.GetOrdinal("points"));
                            flag2 = true;
                        }
                    }
                    rdr.Close(); // <- too easy to forget
                    rdr.Dispose();
                    conn.Open();
                    SqlCommand cmd3 = new SqlCommand("select order_no, total_amount,payment_type from Orders", conn);
                    SqlDataReader rdr2 = cmd3.ExecuteReader(CommandBehavior.CloseConnection);
                    Boolean flag4 = false;
                    Boolean flag5 = false;
                    while (rdr2.Read() && flag3 == false)
                    {
                        int id = rdr2.GetInt32(rdr2.GetOrdinal("order_no"));


                        if (id == IDO)
                        {
                            try
                            {
                                String type = rdr2.GetString(rdr2.GetOrdinal("payment_type"));
                            }
                            catch
                            {
                                flag4 = true;

                            }
                            Session["Amount"] = rdr2.GetInt32(rdr2.GetOrdinal("total_amount"));
                            flag3 = true;
                        }
                    }
                    if (flag3 == false)
                    {
                        Label6.Text = "Invalid Order ID";
                        Label6.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        int points = (Int32)Session["Points"];
                        int amount1 = (Int32)Session["Amount"];
                        if (points + Cash_A + Credit_A < amount1)
                        {

                            Label l2 = new Label();
                            Label6.Text = "Not Enough Cash Or Credit And Points";
                            Label6.ForeColor = System.Drawing.Color.Red;


                        }
                        else
                        {
                            if (flag4 == false)
                            {

                                Label6.Text = "Order Had Already Been Paid For";
                                Label6.ForeColor = System.Drawing.Color.Red;

                            }
                            else
                            {
                                rdr2.Close(); // <- too easy to forget
                                rdr2.Dispose();
                                conn.Open();

                                cmd0.Parameters.Add(new SqlParameter("@customername", username1));
                                cmd0.Parameters.Add(new SqlParameter("@orderID", IDO));
                                cmd0.Parameters.Add(new SqlParameter("@cash", Cash_A));
                                cmd0.Parameters.Add(new SqlParameter("@credit", Credit_A));
                                cmd0.ExecuteNonQuery();

                                Label6.Text = "Payment Successful";
                                Label6.ForeColor = System.Drawing.Color.Green;

                            }
                        }
                    }
                }
            }
        }
    }

    protected void chooseCredit(object sender, EventArgs e)
    {

        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand("ChooseCreditCard", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        conn.Open();
        String username1 = (string)(Session["field1"]);

        String cc = credit_card.Text;
        String Order_ID = Order_ID1.Text;
        Boolean flags3 = false;
        if (System.String.IsNullOrEmpty(cc) || System.String.IsNullOrEmpty(Order_ID))
        {
            Label7.Text = "Please Enter a Value In The Specified Boxes";
            Label7.ForeColor = System.Drawing.Color.Red;

        }
        else
        {
            try
            {
                int IDO = System.Convert.ToInt32(Order_ID);
            }
            catch (Exception)
            {
                flags3 = true;
            }
            if (flags3 == false)
            {
                int IDO = System.Convert.ToInt32(Order_ID);
                SqlCommand cmd4 = new SqlCommand("select order_no from Orders", conn);
                SqlDataReader rdr3 = cmd4.ExecuteReader(CommandBehavior.CloseConnection);
                Boolean temp = false;

                int Order;

                while (rdr3.Read() && temp == false)
                {
                    Order = rdr3.GetInt32(rdr3.GetOrdinal("order_no"));

                    if (Order == IDO)
                    {
                        temp = true;
                    }

                }

                if (temp == false)
                {
                    Label7.Text = "Invalid Order ID";
                    Label7.ForeColor = System.Drawing.Color.Red;
                }

                else
                {
                    rdr3.Close(); // <- too easy to forget
                    rdr3.Dispose();
                    conn.Open();
                    SqlCommand cmd2 = new SqlCommand("select cc_number, customer_name from Customer_CreditCard", conn);
                    SqlDataReader rdr = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                    Boolean flag = false;
                    String num;
                    String customer;
                    while (rdr.Read() && flag == false)
                    {
                        num = rdr.GetString(rdr.GetOrdinal("cc_number"));
                        customer = rdr.GetString(rdr.GetOrdinal("customer_name"));
                        if (username1 == customer && cc == num)
                        {
                            flag = true;


                        }
                    }
                    if (flag == false)
                    {
                        Label7.Text = "Invalid Credit Card Number";
                        Label7.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {

                        rdr.Close(); // <- too easy to forget
                        rdr.Dispose();
                        conn.Open();
                        SqlCommand cmd3 = new SqlCommand("select number, expiry_date from Credit_Card", conn);
                        SqlDataReader rdr2 = cmd3.ExecuteReader(CommandBehavior.CloseConnection);
                        Boolean flag3 = false;
                        Boolean flag4 = false;
                        String num1;
                        DateTime date;
                        while (rdr2.Read() && flag3 == false)
                        {
                            num1 = rdr2.GetString(rdr2.GetOrdinal("number"));
                            date = rdr2.GetDateTime(rdr2.GetOrdinal("expiry_date"));
                            if (num1 == cc)
                            {
                                if (date < DateTime.Now.Date)
                                {
                                    flag4 = true;
                                }
                                flag3 = true;
                            }

                        }
                        if (flag4 == true)
                        {
                            Label7.Text = "The Credit Card You Have Entered Has Expired";
                            Label7.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {

                            rdr2.Close(); // <- too easy to forget
                            rdr2.Dispose();
                            conn.Open();

                            cmd.Parameters.Add(new SqlParameter("@creditcard", cc));
                            cmd.Parameters.Add(new SqlParameter("@orderid", IDO));

                            cmd.ExecuteNonQuery();

                            Label7.Text = "Successful";
                            Label7.ForeColor = System.Drawing.Color.Green;

                        }


                    }
                }
            }

        }
    }

    protected void cancelOrder(object sender, EventArgs e)
    {


        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand("cancelOrder", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        conn.Open();
        String username1 = (string)(Session["field1"]);

        String Order_ID = Order_ID2.Text;
        Boolean flags3 = false;
        if (System.String.IsNullOrEmpty(Order_ID))
        {
            Label10.Text = "Please Enter a Value In The Specified Boxes";
            Label10.ForeColor = System.Drawing.Color.Red;

        }

        else
        {
            try
            {
                int IDO = System.Convert.ToInt32(Order_ID);
            }
            catch (Exception)
            {
                flags3 = true;
            }

            if (flags3 == false)
            {
                int IDO = System.Convert.ToInt32(Order_ID);
                SqlCommand cmd2 = new SqlCommand("select order_no, customer_name, order_status from Orders", conn);
                SqlDataReader rdr = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                Boolean flag = false;
                String customer;
                int num;
                while (rdr.Read() && flag == false)
                {

                    num = rdr.GetInt32(rdr.GetOrdinal("order_no"));
                    customer = rdr.GetString(rdr.GetOrdinal("customer_name"));
                    if (customer == username1 && num == IDO)
                    {

                        flag = true;
                        Session["status"] = rdr.GetString(rdr.GetOrdinal("order_status"));

                    }

                }
                if (flag == false)
                {
                    Label10.Text = "Invalid Order ID";
                    Label10.ForeColor = System.Drawing.Color.Red;

                }

                else
                {

                    if (!((String)Session["status"]).Equals("not processed") && !((String)Session["status"]).Equals("in process"))
                    {
                        Label10.Text = "Sorry, Cannot Cancel Order";
                        Label10.ForeColor = System.Drawing.Color.Red;


                    }

                    else
                    {

                        rdr.Close(); // <- too easy to forget
                        rdr.Dispose();
                        conn.Open();


                        cmd.Parameters.Add(new SqlParameter("@orderid", IDO));

                        cmd.ExecuteNonQuery();

                        Label10.Text = "Successful";
                        Label10.ForeColor = System.Drawing.Color.Green;


                    }

                }
            }


        }

    }



    protected void addtelephonenumber(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString(); // it seems like password to connect to sql connection with my database

        //create a new connection
        SqlConnection conn = new SqlConnection(connStr);

        /*create a new SQL command which takes as parameters the name of the stored procedure and
         the SQLconnection name*/
        SqlCommand cmd = new SqlCommand("addMobile", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        //To read the input from the user
        string number = telephonenumber.Text;




        //pass parameters to the stored procedure
        string customerName = (string)(Session["field1"]);
        cmd.Parameters.Add(new SqlParameter("@username", customerName));
        cmd.Parameters.Add(new SqlParameter("@mobile_number", number));


        conn.Open();
        try
        {
            cmd.ExecuteNonQuery();
            tnmsg.Text = "number added";
        }
        catch (SqlException e1)
        {
            if (e1.Number == 2627)
                tnmsg.Text = "number already exists";

            else
            { tnmsg.Text = "unexpected error, please refresh and try again"; };


        }

        conn.Close();
    }
    protected void viewProducts(object sender, EventArgs e)
    {
        //customer name  ,wish name, product serial
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString(); // it seems like password to connect to sql connection with my database

        //create a new connection
        SqlConnection conn = new SqlConnection(connStr);

        /*create a new SQL command which takes as parameters the name of the stored procedure and
         the SQLconnection name*/
        SqlCommand cmd = new SqlCommand("ShowProductsbyPrice", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        using (conn)
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("ShowProductsbyPrice", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            bGridView1.DataSource = dt;
            bGridView1.DataBind();
        }

        conn.Close();

    }
    public void addCreditCard(object sender, EventArgs e)
    {
        //customer name  ,wish name, product serial
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString(); // it seems like password to connect to sql connection with my database

        //create a new connection
        SqlConnection conn = new SqlConnection(connStr);

        /*create a new SQL command which takes as parameters the name of the stored procedure and
         the SQLconnection name*/
        SqlCommand cmd = new SqlCommand("AddCreditCard", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        //To read the input from the user
        string cardnumber = cardnumber1.Text;
        string cvv1 = cvv.Text;
        string exp = cardexpireydate1.Text;

        Boolean empty = false;
        if (cardnumber.Length == 0 || cvv1.Length == 0 || exp.Length == 0)
            empty = true;

        //pass parameters to the stored procedure
        string customerName = (string)(Session["field1"]);
        cmd.Parameters.Add(new SqlParameter("@customername", customerName));
        cmd.Parameters.Add(new SqlParameter("@creditcardnumber", cardnumber));
        cmd.Parameters.Add(new SqlParameter("@expirydate", exp));
        cmd.Parameters.Add(new SqlParameter("@cvv", cvv1));

        //Save the output from the procedure
        //SqlParameter type = cmd.Parameters.Add("@type", SqlDbType.Int);
        //type.Direction = ParameterDirection.Output;
        //SqlParameter succ = cmd.Parameters.Add("@success", SqlDbType.Int);
        //succ.Direction = ParameterDirection.Output;
        //Executing the SQLCommand
        conn.Open();
        try
        {
            if (empty)
                creditcardlabel.Text = "please fill all required information";
            else
            {
                cmd.ExecuteNonQuery();
                creditcardlabel.Text = "Credit Card added successfully";
            }
        }
        catch (SqlException e1)
        {
            if (e1.Number == 2627)
                creditcardlabel.Text = "credit card already exists";
            else if (e1.Number == 8114)
                creditcardlabel.Text = "Enter valid date";
            else
            { creditcardlabel.Text = e1.Number + ""; };


        }

        conn.Close();

    }
    public void removeFromCart(object sender, EventArgs e)
    {
        //customer name  ,wish name, product serial
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString(); // it seems like password to connect to sql connection with my database

        //create a new connection
        SqlConnection conn = new SqlConnection(connStr);

        /*create a new SQL command which takes as parameters the name of the stored procedure and
         the SQLconnection name*/
        SqlCommand cmd = new SqlCommand("removefromCart ", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        //To read the input from the user
        int pserial = 0;
        Int32.TryParse(cartproduct.Text, out pserial);
        Boolean empty = false;



        //pass parameters to the stored procedure
        string customerName = (string)(Session["field1"]);
        cmd.Parameters.Add(new SqlParameter("@customername", customerName));
        cmd.Parameters.Add(new SqlParameter("@serial", pserial));
        conn.Open();
        try
        {

            cmd.ExecuteNonQuery();
            removepfromcart.Text = " Item removed from cart";
            //handle the rest of the exceptions and do view products
        }
        catch (SqlException e1)
        {
            removepfromcart.Text = e1.Number + "";


        }

        conn.Close();


    }
    public void addToCart(object sender, EventArgs e)
    {//customer name  ,wish name, product serial
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString(); // it seems like password to connect to sql connection with my database

        //create a new connection
        SqlConnection conn = new SqlConnection(connStr);

        /*create a new SQL command which takes as parameters the name of the stored procedure and
         the SQLconnection name*/
        SqlCommand cmd = new SqlCommand("addToCart", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        //To read the input from the user
        string wishname = txt_wishlist2.Text;
        int pserial = 0;
        Int32.TryParse(cartproduct.Text, out pserial);



        //pass parameters to the stored procedure
        string customerName = (string)(Session["field1"]);
        cmd.Parameters.Add(new SqlParameter("@customername", customerName));
        cmd.Parameters.Add(new SqlParameter("@serial", pserial));

        //Save the output from the procedure
        //SqlParameter type = cmd.Parameters.Add("@type", SqlDbType.Int);
        //type.Direction = ParameterDirection.Output;
        //SqlParameter succ = cmd.Parameters.Add("@success", SqlDbType.Int);
        //succ.Direction = ParameterDirection.Output;
        //Executing the SQLCommand
        conn.Open();
        try
        {
            cmd.ExecuteNonQuery();
            addtocartlabel.Text = "Item added to cart";
        }
        catch (SqlException e1)
        {
            if (e1.Number == 2627)
                addtocartlabel.Text = "Item already exists in cart";
            else if (e1.Number == 547)
            {
                addtocartlabel.Text = "No product with this serial no exists";
            }
            else
            { addtocartlabel.Text = e1.Number + ""; };


        }

        conn.Close();


    }
    protected void addToWishlist(Object sender, EventArgs e)
    {
        //customer name  ,wish name, product serial
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString(); // it seems like password to connect to sql connection with my database

        //create a new connection
        SqlConnection conn = new SqlConnection(connStr);

        /*create a new SQL command which takes as parameters the name of the stored procedure and
         the SQLconnection name*/
        SqlCommand cmd = new SqlCommand("AddtoWishlist", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        //To read the input from the user
        string wishname = txt_wishlist2.Text;
        int pserial = 0;
        Int32.TryParse(txt_wishlistItem.Text, out pserial);



        //pass parameters to the stored procedure
        string customerName = (string)(Session["field1"]);
        cmd.Parameters.Add(new SqlParameter("@customername", customerName));
        cmd.Parameters.Add(new SqlParameter("@wishlistname", wishname));
        cmd.Parameters.Add(new SqlParameter("@serial", pserial));

        //Save the output from the procedure
        //SqlParameter type = cmd.Parameters.Add("@type", SqlDbType.Int);
        //type.Direction = ParameterDirection.Output;
        //SqlParameter succ = cmd.Parameters.Add("@success", SqlDbType.Int);
        //succ.Direction = ParameterDirection.Output;
        //Executing the SQLCommand
        conn.Open();
        try
        {
            cmd.ExecuteNonQuery();
            addtowishlabel.Text = "Item added";
        }
        catch (SqlException e1)
        {
            if (e1.Number == 2627)
                addtowishlabel.Text = "item already exists";
            else if (e1.Number == 547)
                addtowishlabel.Text = "please enter valid wishlist/product";

            //addtowishlabel.Text = e1.Number+"";
        }

        conn.Close();

    }

    protected void removeFromWishlist(Object sender, EventArgs e)
    {//customer name  ,wish name, product serial
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString(); // it seems like password to connect to sql connection with my database

        //create a new connection
        SqlConnection conn = new SqlConnection(connStr);

        /*create a new SQL command which takes as parameters the name of the stored procedure and
         the SQLconnection name*/
        SqlCommand cmd = new SqlCommand("removefromWishlist", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        //To read the input from the user
        string wishname = txt_wishlist3.Text;
        int pserial = 0;
        Int32.TryParse(txt_wishlistItem2.Text, out pserial);



        //pass parameters to the stored procedure
        string customerName = (string)(Session["field1"]);
        cmd.Parameters.Add(new SqlParameter("@customername", customerName));
        cmd.Parameters.Add(new SqlParameter("@wishlistname", wishname));
        cmd.Parameters.Add(new SqlParameter("@serial", pserial));

        //Save the output from the procedure
        //SqlParameter type = cmd.Parameters.Add("@type", SqlDbType.Int);
        //type.Direction = ParameterDirection.Output;
        //SqlParameter succ = cmd.Parameters.Add("@success", SqlDbType.Int);
        //succ.Direction = ParameterDirection.Output;
        //Executing the SQLCommand
        conn.Open();
        try
        {
            cmd.ExecuteNonQuery();
            removefromwishlabel.Text = "Item removed";
        }
        catch (SqlException e1)
        {
            removefromwishlabel.Text = "something went wrong try again";
        }

        conn.Close();

    }
    protected void createWishlist(Object sender, EventArgs e)
    {
        //Get the information of the connection to the database
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString(); // it seems like password to connect to sql connection with my database

        //create a new connection
        SqlConnection conn = new SqlConnection(connStr);
        Boolean empty = false;
        /*create a new SQL command which takes as parameters the name of the stored procedure and
         the SQLconnection name*/
        SqlCommand cmd = new SqlCommand("createWishlist", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        //To read the input from the user
        string wishname = txt_wishlist.Text;
        if (wishname.Length == 0)
            empty = true;

        //pass parameters to the stored procedure
        string customerName = (string)(Session["field1"]);

        cmd.Parameters.Add(new SqlParameter("@customername", customerName));
        cmd.Parameters.Add(new SqlParameter("@name", wishname));


        conn.Open();
        try
        {
            if (empty)
                wishLabel.Text = "please fill required information";
            else
            {
                cmd.ExecuteNonQuery();
                wishLabel.Text = "Wishlist added ";
            }
        }
        catch (SqlException e1)
        {
            if (e1.Number == 2627)
                wishLabel.Text = "This Wishlist already exists";


        }

        conn.Close();

    }



}
