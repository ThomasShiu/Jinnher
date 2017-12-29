<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 
    <link href="Styles/jquery-ui-1.8.13.custom.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.6.1.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.13.custom.min.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>

    <div class="demo">aaaaa</div>

    <div>aaaaa</div>
    <div>aaaaa</div>
    <div>aaaaa</div>
    <div>aaaaa</div>
    <div>aaaaa</div>
    <div>aaaaa</div>
    <div>aaaaa</div>
    <div>aaaaa</div>
    <div>aaaaa</div>
    <div>aaaaa</div>
    <asp:ListView ID="ListView1" runat="server"     >
                <LayoutTemplate>
                    <table class="GridViewHeader">
                    <thead>
                        <tr>
                            <td style=" width:70px;text-align:center;">
                                <span>產品種類</span>
                            </td>
                            <td style=" width:70px;text-align:center;">
                                <span>棧板數</span>
                            </td>
                            <td style=" width:70px;text-align:center;">
                                <span>總噸</span>
                            </td>
                            <td style=" width:100px;text-align:center;">
                                <span>執行</span>
                            </td>
                        </tr>
                        </thead>
                         <tbody>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                      </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <table class="GridViewBody2">
                        <tr>
                            <td style=" width:70px;text-align:left;">
                                <asp:Label ID="L_cat" runat="server" Text='<%# Eval("CAT") %>'></asp:Label>
                            </td>
                            <td style=" width:70px;text-align:right;">
                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("TTLPLT", "{0:n0}") %>'></asp:Label>
                            </td>
                            <td style=" width:70px;text-align:right;">
                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("TTLWT", "{0:n0}") %>' ></asp:Label>
                            </td>
                            <td style=" width:100px;text-align:center;">
                                 <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Select" 
                            ImageUrl="~/images/magnifier.png" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <SelectedItemTemplate>
                    <table class="GridViewBody">
                        <tr>
                            <td style=" width:70px;text-align:left;">
                                <asp:Label ID="L_cat" runat="server" Text='<%# Eval("CAT") %>'></asp:Label>
                            </td>
                            <td style=" width:70px;text-align:right;">
                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("TTLPLT", "{0:n0}") %>'></asp:Label>
                            </td>
                            <td style=" width:70px;text-align:right;">
                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("TTLWT", "{0:n0}") %>'></asp:Label>
                            </td>
                            <td style=" width:100px;text-align:center;">
                                 <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Select" 
                            ImageUrl="~/images/magnifier.png" />
                            </td>
                        </tr>
                    </table>
                    <table class="GridViewBody">
                        <tr>
                          <td>
                              <asp:Image ID="Image6" runat="server"  ImageUrl='<%#"desc_pic/"+ Eval("CAT")+".jpg" %>'/>
                          </td>
                        </tr>
                    </table>
                </SelectedItemTemplate>
            </asp:ListView>
     <script type="text/javascript">
         $(document).ready(function () {
             $("#float").smartFloat();
         });
</script>
     <div class="float" id="float"> test
     </div>
    </div>
    </form>
</body>
</html>
