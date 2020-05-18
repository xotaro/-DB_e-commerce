<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vendor.aspx.cs" Inherits="vendor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

              <h1
                  <asp:Button ID="Button2" runat="server" Text="Button" />Post Product</h1>

            
            
            
              <asp:Label ID="a123" runat="server" Text="Productname: "></asp:Label>
              <asp:TextBox ID="ProductName0" runat="server"></asp:TextBox>
            
               <asp:Label ID="a22" runat="server" Text="category: "></asp:Label>
              <asp:TextBox ID="Category0" runat="server"></asp:TextBox>
          
              <asp:Label ID="a33" runat="server" Text="Product_Description: "></asp:Label>
              <asp:TextBox ID="Product_Description0" runat="server"></asp:TextBox>
            &nbsp;
          
             <asp:Label ID="a44" runat="server" Text="Price: "></asp:Label>
              <asp:TextBox ID="Price0" runat="server"></asp:TextBox>
             &nbsp;
             <asp:Label ID="alol" runat="server" Text="Color: "></asp:Label>
             <asp:TextBox ID="Color00" runat="server"></asp:TextBox>
            <br />
              <br />
          <asp:Button ID="Button3" runat="server" Height="34px" Text="PostProduct" OnClick="Post_Product" Width="191px" style="margin-bottom: 0px" />

             <p>
                 &nbsp;</p>
            <h1>ViewProducts</h1>
            <asp:Button ID="viewProductBut" runat="server" Height="35px" Text="ViewProducts" OnClick="showProduct" Width="283px" style="margin-bottom: 0px" />

           <br />
            <h1>Edit Product</h1>
            <asp:Label ID="Label5" runat="server" Text="SerialNumber: "></asp:Label>
             <asp:TextBox ID="SerialNumber1" runat="server" Width="108px"></asp:TextBox> 
             &nbsp;
            <asp:Label ID="Label6" runat="server" Text="Product_name: "></asp:Label>
             <asp:TextBox ID="Product_name1" runat="server" Width="130px"></asp:TextBox>
             &nbsp;
              <asp:Label ID="Label7" runat="server" Text="Cateogry: "></asp:Label>
             <asp:TextBox ID="Cateogry1" runat="server" Width="130px"></asp:TextBox>
             &nbsp;

              <asp:Label ID="Label8" runat="server" Text="Product_Description: "></asp:Label>
             <asp:TextBox ID="Product_Description1" runat="server" Width="130px"></asp:TextBox>
             &nbsp;

              <asp:Label ID="Label9" runat="server" Text="Price: "></asp:Label>
             <asp:TextBox ID="Price1" runat="server" Width="130px"></asp:TextBox>
             &nbsp;
            
              <asp:Label ID="Label10" runat="server" Text="Color: "></asp:Label>
             <asp:TextBox ID="Color1" runat="server" Width="130px"></asp:TextBox>
          <br />
            <br />

                      <asp:Button ID="Button4" runat="server" Height="25px" Text="EditProduct" onclick="MyEdit" Width="190px" style="margin-bottom: 0px" />
              <br />


        
            <h1>OfferOnProduct</h1>

             <asp:Label ID="Label11" runat="server" Text="OfferAmount: "></asp:Label>
              <asp:TextBox ID="OfferAmoutTxt" runat="server" Width="130px"></asp:TextBox>

                  &nbsp;&nbsp;
              <asp:Label ID="Label12" runat="server" Text="expiry_date: "></asp:Label>
              <asp:TextBox ID="ExpTxt" runat="server" Width="130px"></asp:TextBox>
            <br />
            <br />

            <asp:Button ID="Button5" runat="server" Height="25px" Text="CreateOffer" onClick="addOffer" Width="190px" style="margin-bottom: 0px" />

  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

  <br />
            <br />
             <asp:Label ID="Label13" runat="server" Text="OfferId: "></asp:Label>
              <asp:TextBox ID="TextBox14" runat="server" Width="130px"></asp:TextBox>

                  &nbsp;&nbsp;
              <asp:Label ID="Label14" runat="server" Text="Serial_No: "></asp:Label>
              <asp:TextBox ID="TextBox15" runat="server" Width="130px"></asp:TextBox>
            <br />
            <br />

            <asp:Button ID="Button6" runat="server" Height="25px" Text="ApplyOffer" OnClick="AppyOffer" Width="190px" style="margin-bottom: 0px" />

             <br />
            <br />
             <asp:Label ID="Label15" runat="server" Text="OfferId: "></asp:Label>
              <asp:TextBox ID="TextBox16" runat="server" Width="130px"></asp:TextBox>

         
            <br />
            <br />

            <asp:Button ID="Button7" runat="server" Height="25px" Text="RemoveExpiredOffer" onclick="removeexp" Width="190px" style="margin-bottom: 0px" />
              <br />
              <br />


               <br />
              <br />
                        <asp:Button ID="Button8" runat="server" Height="25px" Text="ViewOffers" onclick="ViewOffers" Width="190px" style="margin-bottom: 0px" />


              <br />
              <br />
                        <asp:Button ID="Button9" runat="server" Height="25px" Text="ViewOffersOnProduct" onclick="OffersOnProduct" Width="190px" style="margin-bottom: 0px" />


              <br />
              <br />



                        <h1>add a telephone number</h1>
             <asp:Label ID="Label1" runat="server" Text="Telephone: "></asp:Label>
              <asp:TextBox ID="TNumber" runat="server" Width="130px"></asp:TextBox>
        </div>
       
       
        <p>
       <asp:Button ID="Button1" runat="server" Height="25px" Text="Add" onclick="AddMobile" Width ="190px" style="margin-bottom: 0px" />
        </p>
              <asp:Button ID="Button10" runat="server" Height="25px" Text="View Mobile Users" onclick="ViewMobile" Width ="190px" style="margin-bottom: 0px" />

       
    </form>

 
        </body>
</html>
