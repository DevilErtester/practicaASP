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
    </form>
</body>
</html>
