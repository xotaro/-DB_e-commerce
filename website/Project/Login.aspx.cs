using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration; // web.config isglobaly accesisablly in order to acces connection string from web.config
using System.Data.SqlClient; // TO USE SQL 
using System.Data; // parent class of sql

public partial class Login : System.Web.UI.Page
{
    
    // The page_load method is called before loading the corresponding HTML file to the browser
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void login(object sender, EventArgs e)
    {
        //Get the information of the connection to the database
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString(); // it seems like password to connect to sql connection with my database

        //create a new connection
        SqlConnection conn = new SqlConnection(connStr);

        /*create a new SQL command which takes as parameters the name of the stored procedure and
         the SQLconnection name*/
        SqlCommand cmd = new SqlCommand("userLogin", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        //To read the input from the user
        string username = txt_username.Text;
        string password = txt_password.Text;

        //pass parameters to the stored procedure
        cmd.Parameters.Add(new SqlParameter("@username", username));
        cmd.Parameters.Add(new SqlParameter("@password", password));

        //Save the output from the procedure
        SqlParameter type = cmd.Parameters.Add("@type", SqlDbType.Int);
        type.Direction = ParameterDirection.Output;
        SqlParameter succ = cmd.Parameters.Add("@success", SqlDbType.Int);
        succ.Direction = ParameterDirection.Output;
        //Executing the SQLCommand
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        /// type ( Customer–> 0,Vendor–> 1, Admin–> 2,delivery–> 3)

        if (succ.Value.ToString().Equals("1")) // you cant treat SQL value as int so we say . valvue then we can convert any value tostring then we compare . equals with straing to see if it 1 or no
        {
            //To send response data to the client side (HTML)

            /*ASP.NET session state enables you to store and retrieve values for a user
            as the user navigates ASP.NET pages in a Web application.
            This is how we store a value in the session*/
            Session["field1"] = username; // its global array to access from another C#  indexed with string  

            //To navigate to another webpage
            if(type.Value.ToString().Equals("1"))
            {
                Response.Redirect("vendor.aspx", true);

            }
            else if (type.Value.ToString().Equals("0"))
            {
                Response.Redirect("Customer.aspx", true);

            }
            else if (type.Value.ToString().Equals("2"))
            {
                Response.Redirect("Admin.aspx", true);


            }



        }
        else
        {
            Response.Write("Username or password wrong Enter aValid one !");
        }
    }
}

