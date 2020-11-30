<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="PracticaASP.NET.admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TreeView ID="tvwCategorias" runat="server" ExpandDepth="0" OnSelectedNodeChanged="tvHoldingDetail_SelectedNodeChanged" 
                ForeColor="Black"
                HoverNodeStyle-ForeColor="Firebrick" SelectedNodeStyle-ForeColor="Firebrick"
                SelectedNodeStyle-Font-UnderLine="true" NodeStyle-HorizontalPadding="5" NodeIndent="20">
            </asp:TreeView>
        </div>
        <div>
            <asp:Table runat="server" ID="gvRutas"></asp:Table>
        </div>
        <div id="newruta" runat="server">
            <asp:Label ID="Label2" runat="server" Text="Origen :"></asp:Label>
            <asp:TextBox ID="Origen" runat="server" ></asp:TextBox>
            <asp:Label ID="Label1" runat="server" Text="Destino :"></asp:Label>
            <asp:TextBox ID="Destino" runat="server" ></asp:TextBox>
            <asp:Label ID="Label3" runat="server" Text="Categoria :"></asp:Label>
            <asp:DropDownList ID="dropCategorias" runat="server" DataTextField="name" DataValueField="id"></asp:DropDownList>
            <asp:Button ID="btn" Text="Crear nueva ruta" runat="server" OnClick="btn_Click"/>
        </div>
    </form>
</body>
</html>
