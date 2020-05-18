<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 187px">
    <form id="form1" runat="server">
        <div>
            <h1>&nbsp;&nbsp;Activate Vendor</h1>
            <asp:Label ID="Label1" runat="server" Text="Vendor name"></asp:Label>
            <asp:TextBox ID="VendorN" runat="server"></asp:TextBox>
        </div>
        <p>
            <asp:Button ID="Button1" runat="server" Text="Activate Vendor" OnClick="activeVendor" Height="23px" Width="120px" />
        </p>
                    <asp:Button ID="Button5" runat="server" Text="View Vendors" OnClick="ViewVendors" Height="23px" Width="120px" />

        <br />

            <h1>Review all orders</h1>
                    <asp:Button ID="Button2" runat="server" Text="ReviewAllOrders" OnClick="reviewOrder" Height="27px" Width="128px"  />

          <h1>Update Order</h1>
         <asp:Label ID="Label2" runat="server" Text="Order_id"></asp:Label>
            <asp:TextBox ID="Ordid" runat="server"></asp:TextBox>
        <br />
        <br />

                    <asp:Button ID="Button3" runat="server" Text="UpdateOrderStatusINProcess" onclick="updatestat" Height="27px" Width="228px" />
         <br />
        <br />

                    <h1>Deals on Product</h1>
         <asp:Label ID="Label4" runat="server" Text="Deal_amount"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

        &nbsp;&nbsp;&nbsp;

        <asp:Label ID="Label5" runat="server" Text="Expiry_date"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>

               <br />
        <br />

               <asp:Button ID="Button6" runat="server" Height="25px" Text="Create Today deal" onclick="CreateTodayDeal" Width ="190px" style="margin-bottom: 0px" />

        <br />
        <br />

        <br />
         <asp:Label ID="Label6" runat="server" Text="Deal_ID"></asp:Label>
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>

        &nbsp;&nbsp;&nbsp;

        <asp:Label ID="Label7" runat="server" Text="Serial_NO"></asp:Label>
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <br />
        <br />

                       <asp:Button ID="Button7" runat="server" Height="25px" Text="addTodaysDealOnProduct" onclick="addDeal" Width ="190px" style="margin-bottom: 0px" />
        <br />
        <br />


          <asp:Label ID="Label8" runat="server" Text="Deal_ID"></asp:Label>
            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>

        &nbsp;&nbsp;&nbsp;

        <br />
        <br />

                       <asp:Button ID="Button8" runat="server" Height="25px" Text="removeExpiredDeal" onclick="removeExp" Width ="190px" style="margin-bottom: 0px" />


                             <br />
        <br />
                               <asp:Button ID="Button9" runat="server" Height="25px" Text="ViewTodaysDeal" onclick="TodayDeals" Width ="190px" style="margin-bottom: 0px" />

        <br />
        <br />

                                       <asp:Button ID="Button11" runat="server" Height="25px" Text="ViewDealsOnProduct" onclick="DealsProduct" Width ="190px" style="margin-bottom: 0px" />

         <br />
        <br />

                            



                             <h1>add a telephone number</h1>
             <asp:Label ID="Label3" runat="server" Text="Telephone: "></asp:Label>
              <asp:TextBox ID="TNumber" runat="server" Width="130px"></asp:TextBox>
  
       
       
        <p>
       <asp:Button ID="Button4" runat="server" Height="25px" Text="Add" onclick="AddMobile" Width ="190px" style="margin-bottom: 0px" />
        </p>
              <asp:Button ID="Button10" runat="server" Height="25px" Text="View Mobile Users" onclick="ViewMobile" Width ="190px" style="margin-bottom: 0px" />
      

        

     
      </div>

    </form>
</body>
</html>
