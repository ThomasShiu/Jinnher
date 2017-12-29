<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenCode128.aspx.cs" Inherits="GenCode128" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style=" width:300px">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="SCR440"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="1BXXX"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="1994kg"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="17mm"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="SCR440"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="1BXXX"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Image ID="Image1" runat="server" ImageUrl="code128.aspx" />
            </td>
 
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
