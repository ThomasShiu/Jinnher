<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GM_demostock121.aspx.cs" Inherits="GM_demostock121" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </title>
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table style=" border: 0.1em ridge #C0C0C0; width:100%">
      <tr>
        <td class="style1">
            <asp:Label ID="message" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
      </tr>
        <tr>
          <td>
              <asp:Image ID="Img_pic" runat="server" />
          </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="L_desc_c" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="L_desc_e" runat="server" Text=""></asp:Label>
            </td>
        </tr>
      </table>
    </div>
    </form>
</body>
</html>
