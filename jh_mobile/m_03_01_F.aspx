<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m_03_01_F.aspx.cs" Inherits="m_03_01_F" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<title>成型達成率</title>
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
        <h1>成型達成率</h1>
        <!--<a href="m_index.aspx" data-rel="back" data-icon="carat-l" data-iconpos="notext">Back</a>-->
        <!--<a href="#" onclick="window.location.reload()" data-icon="back" data-iconpos="notext">Refresh</a> -->
    </div><!-- /header -->
   <div>
      <asp:Label ID="L_message" runat="server" Text=""></asp:Label>
   </div>
   <div>
      <asp:Label ID="L_machine" runat="server" Font-Size="Large"></asp:Label>
   </div>
   <div>
      <asp:Label ID="L_sdate" runat="server" Font-Size="Large"></asp:Label>
      <asp:Label ID="L_edate" runat="server" Font-Size="Large"></asp:Label>
  </div>
    <div>
    
        <asp:GridView ID="GridView1" runat="server" DataSourceID="SDS_f" 
            AutoGenerateColumns="False" Width="100%" BackColor="White" 
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
            ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:BoundField DataField="日期" HeaderText="日期" SortExpression="日期" >
                </asp:BoundField>
               <asp:BoundField DataField="機台群組" 
                    HeaderText="群組" SortExpression="機台群組" >
                </asp:BoundField>
                <asp:BoundField DataField="機台" 
                    HeaderText="機台" SortExpression="機台" >
                </asp:BoundField>
                <asp:BoundField DataField="生產支數" HeaderText="生產支數" SortExpression="生產支數" 
                    Visible="False" />
                <asp:BoundField DataField="標準支數" HeaderText="標準支數" SortExpression="標準支數" 
                    Visible="False" />
                <asp:BoundField DataField="生產效率" HeaderText="效率" SortExpression="生產效率" 
                    DataFormatString="{0:n1}" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="批數" HeaderText="批數" SortExpression="批數" 
                    Visible="False" />
                <asp:BoundField DataField="稼動工時" HeaderText="稼動工時" SortExpression="稼動工時"
                DataFormatString="{0:n1}" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="停機時間" HeaderText="停機" SortExpression="停機時間"
                DataFormatString="{0:n1}" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="可扣時間" HeaderText="可扣時間" SortExpression="可扣時間" 
                    Visible="False" />
                <asp:BoundField DataField="達成率" HeaderText="達成率" SortExpression="達成率" 
                DataFormatString="{0:n0}">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
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
        <asp:SqlDataSource ID="SDS_f" runat="server" 
            ConnectionString="<%$ ConnectionStrings:jh815 %>" 
            ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" 
            SelectCommand=""></asp:SqlDataSource>
    
    </div>
	<div data-role="footer" data-position="fixed" data-theme="b">
        <div data-role="navbar">
        <ul>
            <li><a href="#" onclick=" window.history.back()" data-icon="back" data-iconpos="top">Back</a></li>
            <li><a href="m_03_F.aspx" data-rel="home" data-icon="home" data-iconpos="top">home</a></li>
            <li><a href="#" onclick="window.location.reload()" data-icon="refresh" data-iconpos="top">Refresh</a></li>
        </ul>
        </div><!-- /navbar -->
    </div><!-- /footer -->
 
    </form>
</body>
</html>
