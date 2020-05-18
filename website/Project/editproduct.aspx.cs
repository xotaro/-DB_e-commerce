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


public partial class edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand("postProduct", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        string username = (string)(Session["field1"]);


      
        cmd.Parameters.Add(new SqlParameter("@vendorUsername", username));
        cmd.Parameters.Add(new SqlParameter("@product_name", username));
        cmd.Parameters.Add(new SqlParameter("@category", username));
        cmd.Parameters.Add(new SqlParameter("@product_description", username));
        cmd.Parameters.Add(new SqlParameter("@price", username));
        cmd.Parameters.Add(new SqlParameter("@color", username));
    }
}