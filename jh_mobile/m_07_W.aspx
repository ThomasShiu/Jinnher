<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m_07_W.aspx.cs" Inherits="m_07_W" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<title>成型廠線材庫存</title>
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
        <h1>螺絲廠線材庫存</h1>
        <!--<a href="m_index.aspx" data-rel="back" data-icon="carat-l" data-iconpos="notext">Back</a>-->
        <!--<a href="#" onclick="window.location.reload()" data-icon="back" data-iconpos="notext">Refresh</a> -->
    </div><!-- /header -->
    <div>
     <asp:GridView ID="GridView1" runat="server" DataSourceID="SDS_wire" 
            AutoGenerateColumns="False" Width="100%" BackColor="White" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
            ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:BoundField DataField="STK_VEN_NO" HeaderText="廠區" SortExpression="STK_VEN_NO" Visible="False" >
                </asp:BoundField>
                <asp:BoundField DataField="COIL_NO" HeaderText="卷號"  
                    SortExpression="COIL_NO" >
                </asp:BoundField>
                <asp:BoundField DataField="WIR_KIND" HeaderText="材質"  
                    SortExpression="WIR_KIND" >
                </asp:BoundField>
                 
                <asp:BoundField DataField="LUO_NO" HeaderText="爐號" SortExpression="LUO_NO" />
                <asp:BoundField DataField="ORI_DRAW_DIA" 
                    HeaderText="原線徑" SortExpression="ORI_DRAW_DIA" Visible="False" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="DRAW_DIA" 
                    HeaderText="線徑" SortExpression="DRAW_DIA" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="COIL_WT" HeaderText="重量" SortExpression="COIL_WT" DataFormatString="{0:n0}" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="MAKER" 
                    HeaderText="廠牌" SortExpression="MAKER" Visible="False" >
                </asp:BoundField>
                 
                <asp:BoundField DataField="ASSM_NO" HeaderText="製程" SortExpression="ASSM_NO" />
                <asp:BoundField DataField="INP_DATE" HeaderText="進貨" SortExpression="INP_DATE" Visible="False" />
                 
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BorderWidth="0px" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
        <asp:SqlDataSource ID="SDS_wire" runat="server" 
            ConnectionString="<%$ ConnectionStrings:jheip %>" 
            ProviderName="<%$ ConnectionStrings:jheip.ProviderName %>" SelectCommand="SELECT '' STK_VEN_NO,'' COIL_NO,'' WIR_KIND,0 ORI_DRAW_DIA,
0 DRAW_DIA,'' MAKER,'' LUO_NO,'' ASSM_NO,0 COIL_WT,'' INP_DATE 
FROM DUAL"></asp:SqlDataSource>
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

