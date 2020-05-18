<%@ Page Language="C#" AutoEventWireup="true" CodeFile="registration.aspx.cs" Inherits="registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Customer Registration</h1>
              <asp:Label ID="lbl_username" runat="server" Text="First_Name: "></asp:Label>
        <asp:TextBox ID="txt_Fname" runat="server"></asp:TextBox>
    
                      &nbsp;     &nbsp;
             <asp:Label ID="Label3" runat="server" Text="Last_Name: "></asp:Label>
        <asp:TextBox ID="txt_LName" runat="server"></asp:TextBox>
    
                      &nbsp;     &nbsp;


        <asp:Label ID="lbl_password" runat="server" Text="UserName "></asp:Label>
        <asp:TextBox ID="txt_UserName" runat="server"></asp:TextBox>
                            &nbsp;     &nbsp;

        <asp:Label ID="Label1" runat="server" Text="Email: "></asp:Label>
        <asp:TextBox ID="txt_Email" runat="server"></asp:TextBox>

            
        <asp:Label ID="Label2" runat="server" Text="Password: "></asp:Label>
        <asp:TextBox ID="txt_password" runat="server" TextMode="Password"></asp:TextBox>
               <br />
              <br />
              
           
            <h1>            &nbsp;
          <asp:Button ID="RegCust" runat="server" Height="34px" Text="Register(Customer)" onclick="RegCustomer"  Width="191px" style="margin-bottom: 0px" />

              
         
            <h1>            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;            Vendor Registration</h1>

               <asp:Label ID="Label4" runat="server" Text="First_Name: "></asp:Label>
        <asp:TextBox ID="TFname" runat="server"></asp:TextBox>
    
                      &nbsp;     &nbsp;
             <asp:Label ID="Label5" runat="server" Text="Last_Name: "></asp:Label>
        <asp:TextBox ID="TLname" runat="server"></asp:TextBox>
    
                      &nbsp;     &nbsp;


        <asp:Label ID="Label6" runat="server" Text="UserName "></asp:Label>
        <asp:TextBox ID="TUsern" runat="server"></asp:TextBox>
                            &nbsp;     &nbsp;

        <asp:Label ID="Label7" runat="server" Text="Email: "></asp:Label>
        <asp:TextBox ID="TEmail" runat="server"></asp:TextBox>

                  <asp:Label ID="Label9" runat="server" Text="Company_Name "></asp:Label>
        <asp:TextBox ID="TComp" runat="server"></asp:TextBox>
                            &nbsp;     &nbsp;

        <asp:Label ID="Label10" runat="server" Text="BankAccountNumber "></asp:Label>
        <asp:TextBox ID="TBank" runat="server"></asp:TextBox>
             
        <asp:Label ID="Label8" runat="server" Text="Password: "></asp:Label>
        <asp:TextBox ID="TPass" runat="server" TextMode="Password"></asp:TextBox>
         
        </div>
        <p>
            &nbsp;</p>
        <p>
         
            <asp:Button ID="RegVen" runat="server" Height="33px" Text="Register(Vendor)" onclick="RegVendor" Width="187px" style="margin-bottom: 15px" />
      
              </p>
    </form>
</body>
</html>
