<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m_04_F.aspx.cs" Inherits="m_04_F" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<title>成型每日產量</title>
	<link rel="shortcut icon" href="images/jh.ico">
    <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans:300,400,700">
	<link rel="stylesheet" href="jqm/css/themes/default/jquery.mobile-1.4.5.min.css">
	<link rel="stylesheet" href="jqm/_assets/css/jqm-demos.css">
	<script src="jqm/js/jquery.js"></script>
	<script src="jqm/_assets/js/index.js"></script>
	<script src="jqm/js/jquery.mobile-1.4.5.min.js"></script>
    <script>
        window.addEventListener("load", function () {
            setTimeout(function () {
                window.scrollTo(0, 1);
            }, 10);
        });

</script>

</head>
<body>
    <form id="form1" runat="server">
     <div data-role="header" data-position="fixed" data-theme="b">
        <h1>成型每日產量</h1>
    </div><!-- /header -->
    <div>
        <table style="width: 100%" >
        <tr>
                     <td >
                        <asp:TextBox ID="TB_year" runat="server" Width="80px" Font-Size="Large"></asp:TextBox>
                      </td>
                      <td >
                       <asp:DropDownList ID="DDL_month" runat="server"   Font-Size="Large">
                           <asp:ListItem Value="01">一月</asp:ListItem>
                           <asp:ListItem Value="02">二月</asp:ListItem>
                           <asp:ListItem Value="03">三月</asp:ListItem>
                           <asp:ListItem Value="04">四月</asp:ListItem>
                           <asp:ListItem Value="05">五月</asp:ListItem>
                           <asp:ListItem Value="06">六月</asp:ListItem>
                           <asp:ListItem Value="07">七月</asp:ListItem>
                           <asp:ListItem Value="08">八月</asp:ListItem>
                           <asp:ListItem Value="09">九月</asp:ListItem>
                           <asp:ListItem Value="10">十月</asp:ListItem>
                           <asp:ListItem Value="11">十一月</asp:ListItem>
                           <asp:ListItem Value="12">十二月</asp:ListItem>
                       </asp:DropDownList>
                      </td>
            <td>
                <asp:Button ID="btn_query" runat="server" Text="查詢" OnClick="btn_query_Click" />
            </td>
            </tr>
            </table>
    </div>
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
            CellPadding="3" DataSourceID="SDS_f" ForeColor="Black" GridLines="Vertical" Width="100%">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:BoundField DataField="WORK_DATE" HeaderText="日期" 
                    SortExpression="WORK_DATE">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="NETWT" HeaderText="產量(噸)" SortExpression="NETWT"
                 DataFormatString="{0:n0}" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
        <asp:SqlDataSource ID="SDS_f" runat="server" 
            ConnectionString="<%$ ConnectionStrings:jh815 %>" 
            ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" 
            SelectCommand="SELECT '' WORK_DATE,0 NETWT FROM DUAL"></asp:SqlDataSource>
    
    </div>
   
         <div data-role="footer" data-position="fixed" data-theme="b">
        <div data-role="navbar">
        <ul>
            <li><a href="#" onclick=" window.history.back()" data-icon="back" data-iconpos="top">Back</a></li>
            <li><a href="m_index.aspx" data-rel="home" data-icon="home" data-iconpos="top">home</a></li>
            <li><a href="#" onclick="window.location.reload()" data-icon="refresh" data-iconpos="top">Refresh</a></li>
        </ul>
        </div><!-- /navbar -->
    </div><!-- /footer -->
    </form>
</body>
</html>