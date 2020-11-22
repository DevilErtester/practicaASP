<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="loginForm.aspx.cs" Inherits="PracticaASP.NET.loginForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label2" runat="server" Text="UserId :"></asp:Label>    
            <asp:TextBox ID="username" runat="server" ></asp:TextBox>
            <asp:Label ID="Label1" runat="server" Text="Password :"></asp:Label> 
            <asp:TextBox ID="password" runat="server"  TextMode="Password"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" BorderStyle="None" OnClick="Button1_Click" Text="Log In" />
            <asp:Label ID="Label4" runat="server" Text=""></asp:Label> 
        </div>
    </form>
</body>
</html>
