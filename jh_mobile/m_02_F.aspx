<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m_02_F.aspx.cs" Inherits="m_02_F" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<title>成型產量</title>
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
        <h1>成型產量</h1>
    </div><!-- /header -->
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
            CellPadding="3" DataSourceID="SDS_f" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:BoundField DataField="WORK_DATE" HeaderText="年月" 
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
    <div>
    <asp:Chart ID="Chart1" runat="server" Width="550px" Height="220px" 
                        BorderWidth="2" backcolor="#D3DFF0" BorderColor="26, 59, 105" 
                        Palette="SemiTransparent">
                    <Series>
                        <asp:Series Name="前年" ChartType="Line" IsValueShownAsLabel="False"
                            Legend="Legend1" MarkerStyle="Square">
                        </asp:Series>
                        <asp:Series Name="去年" ChartType="Line" IsValueShownAsLabel="True"
                            Legend="Legend1" MarkerStyle="Square">
                        </asp:Series>
                        <asp:Series ChartArea="ChartArea1" ChartType="Column" IsValueShownAsLabel="True"
                            Legend="Legend1" MarkerStyle="Square" Name="今年">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" ShadowOffset="5">
                            <AxisY>
                                <MajorGrid LineColor="Gainsboro" />
                                <MinorGrid LineColor="Gainsboro" />
                            </AxisY>
                            <AxisX Interval="1" >
                                <MajorGrid LineColor="Gainsboro" />
                                <MinorGrid LineColor="Gainsboro" />
                                <MajorTickMark LineColor="Gainsboro" />
                            </AxisX>
                            <Position Height="90" Width="98" X="0" Y="15" />
                            <Area3DStyle IsClustered="True" PointDepth="10" PointGapDepth="10" 
                                WallWidth="1" />
                        </asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Name="Legend1"  BackColor="Transparent" MaximumAutoSize="10" 
                            TableStyle="Wide">
                            <Position Height="21.22905" Width="45" X="60" Y="4" />
                        </asp:Legend>
                    </Legends>
                    <Titles>
                        <asp:Title Name="Title1" Text="JINNHER 產能比較表" ToolTip="JINNHER 產能比較表">
                        </asp:Title>
                    </Titles>
                        <%--<BorderSkin SkinStyle="Raised" />--%>
                </asp:Chart>
    </div>
    <div>
    <asp:Chart ID="Chart2" runat="server" Width="550px" Height="220px" 
                        BorderWidth="2" backcolor="#D3DFF0" BorderColor="26, 59, 105" 
                        Palette="SemiTransparent">
                    <Series>
                        <asp:Series Name="前年" ChartType="Line" IsValueShownAsLabel="False"
                            Legend="Legend1" MarkerStyle="Square">
                        </asp:Series>
                        <asp:Series Name="去年" ChartType="Line" IsValueShownAsLabel="True"
                            Legend="Legend1" MarkerStyle="Square">
                        </asp:Series>
                        <asp:Series ChartArea="ChartArea1" 
                            Legend="Legend1" MarkerStyle="Square" Name="今年" IsValueShownAsLabel="True"
                            BackImageTransparentColor="128, 255, 255" Color="128, 255, 255" 
                            Palette="SemiTransparent" >
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" ShadowOffset="5">
                            <AxisY>
                                <MajorGrid LineColor="Gainsboro" />
                                <MinorGrid LineColor="Gainsboro" />
                            </AxisY>
                            <AxisX Interval="1" >
                                <MajorGrid LineColor="Gainsboro" />
                                <MinorGrid LineColor="Gainsboro" />
                                <MajorTickMark LineColor="Gainsboro" />
                            </AxisX>
                            <Position Height="90" Width="98" X="0" Y="15" />
                            <Area3DStyle IsClustered="True" PointDepth="10" PointGapDepth="10" 
                                WallWidth="1" />
                        </asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Name="Legend1"  BackColor="Transparent" MaximumAutoSize="10" 
                            TableStyle="Wide">
                            <Position Height="17.3515987" Width="45" X="60" Y="4" />
                        </asp:Legend>
                    </Legends>
                    <Titles>
                        <asp:Title Name="Title1" Text="JINNHER 產能比較表-累計" ToolTip="JINNHER 產能比較表-累計">
                        </asp:Title>
                    </Titles>
                </asp:Chart>
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