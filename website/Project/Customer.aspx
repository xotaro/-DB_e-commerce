<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Customer.aspx.cs" Inherits="Customer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>WELCOME!</h1>
        </div>



        


        <asp:Button ID="bButton1" runat="server" Font-Bold="true" OnClick="viewProducts" Text="View Products" />

        <asp:GridView ID="bGridView1" visible="true" runat="server"></asp:GridView>
        <br />
        <asp:Label ID="lbl_username" runat="server" Font-Bold="true" Text="Create Wishlist: "></asp:Label>
        <br />
        <asp:TextBox ID="txt_wishlist" runat="server"></asp:TextBox>
        <asp:Button ID="bButton2" OnClick="createWishlist" runat="server" Text="Create " />
        <asp:Label ID="wishLabel" runat="server" Text=""></asp:Label>
        <br />





        <asp:Label ID="bLabel1" runat="server" Font-Bold="true" Text="Add to wishlist"></asp:Label>
        <br />
        <asp:Label ID="bLabel2" runat="server" Text="Wishname "></asp:Label>
        <asp:TextBox ID="txt_wishlist2" runat="server"></asp:TextBox>
        <asp:Label ID="bLabel3" runat="server" Text="Product serial_no "></asp:Label>
        <asp:TextBox ID="txt_wishlistItem" runat="server"></asp:TextBox>
        <asp:Button ID="bButton3" OnClick="addToWishlist" runat="server" Text="Add" />
        <asp:Label ID="addtowishlabel" runat="server" Text=""></asp:Label>




        <br />
         <asp:Label ID="bLabel4" runat="server" Font-Bold="true" Text="Remove from wishlist "></asp:Label>
        <br />
        <asp:Label ID="bLabel5" runat="server" Text="Wishname "></asp:Label>
        <asp:TextBox ID="txt_wishlist3" runat="server"></asp:TextBox>
        <asp:Label ID="bLabel6" runat="server" Text="Product serial_no "></asp:Label>
        <asp:TextBox ID="txt_wishlistItem2"  runat="server"></asp:TextBox>
        <asp:Button ID="bButton4" OnClick="removeFromWishlist" runat="server" Text="remove" />
        <asp:Label ID="removefromwishlabel" runat="server" Text=""></asp:Label>


        <br />
        <asp:Label ID="bLabel7" runat="server" Font-Bold="true" Text="Add product to cart "></asp:Label>
        <br />
        <asp:Label ID="bLabel8" runat="server" Text="Product serial_no "></asp:Label>
        <asp:TextBox ID="cartproduct" runat="server"></asp:TextBox>
        
         <asp:Button ID="bButton5" OnClick="addToCart" runat="server" Text="Add" />
        <asp:Label ID="addtocartlabel" runat="server" Text=""></asp:Label>
        <br />

        <asp:Label ID="bLabel9" runat="server" Font-Bold="true" Text="remove product from cart "></asp:Label>
        <br />
        <asp:Label ID="bLabel10" runat="server"  Text="Product serial_no "></asp:Label>
        <asp:TextBox ID="removeproductcart" runat="server"></asp:TextBox>
        
         <asp:Button ID="bButton6" OnClick="removeFromCart" runat="server" Text="Remove" />
        <asp:Label ID="removepfromcart" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="bLabel11" runat="server" Font-Bold="true" Text="Add Credit Cart"></asp:Label>
        <br />
        <asp:Label ID="bLabel12" runat="server"  Text="Credit Card Number "></asp:Label>
        <asp:TextBox ID="cardnumber1" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="bLabel13" runat="server"  Text="Expiry date in format:YYYY-MM-DD"></asp:Label>
        <asp:TextBox ID="cardexpireydate1" runat="server"></asp:TextBox>
         <br />
        <asp:Label ID="bLabel14" runat="server"  Text="CVV"></asp:Label>
        <asp:TextBox ID="cvv" runat="server"></asp:TextBox>
        <asp:Button ID="bButton8" OnClick="addCreditCard" runat="server" Text="Add Card" />
        <asp:Label ID="creditcardlabel" runat="server" Text=""></asp:Label>

        <br />
        <asp:Label ID="bLabel15" runat="server" Font-Bold="true" Text="Add telephone number "></asp:Label>
        <br />
        <asp:Label ID="bLabel16" runat="server"  Text="Telephone number "></asp:Label>
        <asp:TextBox ID="telephonenumber" runat="server"></asp:TextBox>
        
         <asp:Button ID="bButton9" OnClick="addtelephonenumber" runat="server" Text="Add" />
        <asp:Label ID="tnmsg" runat="server" Text=""></asp:Label>










        <h2> Make an Order:</h2>
        <asp:Button ID="Button1" runat="server" Text="Make Order" OnClick = "makeOrder" />
        <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
        <h2>Pay for an Order:</h2>
        <asp:Label ID="Label1" runat="server" Text="Order ID: "></asp:Label>
        <asp:TextBox ID="OrderID" runat="server"></asp:TextBox>
        <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="OrderID" ErrorMessage="Value must be an Integer" />
        <br/> <br/>
        <asp:Label ID="Label2" runat="server" Text="Cash: "></asp:Label>
        <asp:TextBox ID="Cash" runat="server"></asp:TextBox>
        
        <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="Cash" ErrorMessage="Value must be an Integer" />
        <br/> <br/> 
        <asp:Label ID="Label3" runat="server" Text="Credit: "></asp:Label>
        <asp:TextBox ID="Credit" runat="server"></asp:TextBox>
        <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="Credit" ErrorMessage="Value must be an Integer" />
        <br/> <br/> 
        <asp:Button ID="Button2" runat="server" Text="Pay" OnClick ="specifyAmount" />
        <asp:Label ID="Label6" runat="server" Text=""></asp:Label>

        <h2>Choose A Credit Card:</h2>
        <asp:Label ID="Label4" runat="server" Text="Credit Card Number: "></asp:Label>
        <asp:TextBox ID="credit_card" runat="server"></asp:TextBox>
        <br /> <br />
        <asp:Label ID="Label8" runat="server" Text="Order ID: "></asp:Label>
        <asp:TextBox ID="Order_ID1" runat="server"></asp:TextBox>
        <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="Order_ID1" ErrorMessage="Value must be an Integer" />
        <br /> <br />
        <asp:Button ID="Button3" runat="server" Text="Submit" OnClick = "chooseCredit" />
        <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
        
        <h2>Cancel An Order:</h2>
        <asp:Label ID="Label9" runat="server" Text="Order ID: "></asp:Label>
        <asp:TextBox ID="Order_ID2" runat="server"></asp:TextBox>
        <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="Order_ID2" ErrorMessage="Value must be an Integer" />
        <br /> <br />
        <asp:Button ID="Button4" runat="server" Text="Cancel Order" OnClick ="cancelOrder" />
        <asp:Label ID="Label10" runat="server" Text=""></asp:Label>








    </form>
    
</body>
</html>
