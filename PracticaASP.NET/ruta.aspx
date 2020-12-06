<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ruta.aspx.cs" Inherits="PracticaASP.NET.ruta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="rutaGridView" runat="server" />
        </div>
        <div id="divComents" runat="server">
           
        </div>
        <div id="newComent" runat="server">
            <textarea runat="server" id="comment" form="form1" cols="20" name="S1" rows="1"></textarea>
            <asp:FileUpload ID= "Uploader" runat = "server" />
            <asp:Button UseSubmitBehavior="true" ID="newComentClick" Text="Comentar" runat="server" OnClick="newComent_Click" />
            <asp:Label ID="labelError" runat="server" />
        </div>
    </form>
</body>
</html>
