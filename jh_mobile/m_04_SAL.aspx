<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m_04_SAL.aspx.cs" Inherits="m_04_SAL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<title>外銷統計</title>
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
        <h1>外銷統計</h1>
    </div><!-- /header -->

    <div>
    <table width="100%">
    <tr>
        <td>
        
              <table>
                <tr>
                  <td class="style1">
                    <asp:TextBox ID="TB_year" runat="server" Width="80px" Font-Size="Large"></asp:TextBox>
                  </td>
                  <%--<td class="style1">
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
                  </td>--%>
                  <td class="style1">
                       <asp:Button ID="Quy_btn" runat="server" Text="重新查詢"  Width="80px" Height="30px" 
                           onclick="Quy_btn_Click" />
                      
                  </td>
                </tr>
              </table>
        </td>
    </tr>
    
    <tr>
        <td>
            
            
            <asp:Chart ID="Chart1" runat="server" DataSourceID="DS_SALORDER" 
                Palette="SemiTransparent" Height="350px" Width="350px" onclick="Chart1_Click">
                <Series>
                    <asp:Series Name="Series1" ChartType="Pie" XValueMember="CONTINENT" 
                        YValueMembers="TTLAMT" CustomProperties="CollectedSliceExploded=True, PieLabelStyle=Outside" 
                        Label="#AXISLABEL: #PERCENT" Legend="Legend1" 
                        IsValueShownAsLabel="True">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                        <Position Height="99" Width="99" X="3" Y="3" />
                        <Area3DStyle Enable3D="True" />
                    </asp:ChartArea>
                </ChartAreas>
                <Legends>
                    <asp:Legend Docking="Bottom" Name="Legend1" TableStyle="Wide" 
                        AutoFitMinFontSize="8"  ShadowOffset="3">
                    </asp:Legend>
                </Legends>
                <Titles>
                    <asp:Title Name="Title1" Text="外銷接單-總金額">
                    </asp:Title>
                </Titles>
            </asp:Chart>
            <asp:SqlDataSource ID="DS_SALORDER" runat="server" 
                ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand="
                SELECT '' CONTINENT,0 TTLAMT,0 TTLNW FROM DUAL"></asp:SqlDataSource>
            
        </td>
    </tr>
    <tr>
        <td>
          
            <asp:GridView ID="GridView1" runat="server" DataSourceID="DS_SALORDER" 
                AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" 
                GridLines="Vertical" Width="100%">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:BoundField DataField="CONTINENT" HeaderText="洲別" 
                        SortExpression="CONTINENT" />
                    <asp:BoundField DataField="TTLAMT" DataFormatString="{0:N0}" 
                        HeaderText="金額(仟元)" SortExpression="TTLAMT">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TTLNW" DataFormatString="{0:N0}" HeaderText="重量(噸)" 
                        SortExpression="TTLNW">
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
        </td>
    </tr>

     <tr>
        <td>     
            <asp:Chart ID="Chart2" runat="server" DataSourceID="DS_SALORDER" 
                Palette="Pastel" Height="350px" Width="350px" onclick="Chart2_Click">
                <Series>
                    <%--<asp:Series Name="Series1" ChartType="Pie" XValueMember="CONTINENT" 
                        YValueMembers="TTLNW" CustomProperties="CollectedSliceExploded=True, PieLabelStyle=Outside" 
                        Label="#AXISLABEL -- #VALY{n0} -- #PERCENT" Legend="Legend1" 
                        IsValueShownAsLabel="True">
                    </asp:Series>--%>
                    <asp:Series Name="Series1" ChartType="Pie" XValueMember="CONTINENT" 
                        YValueMembers="TTLNW" CustomProperties="CollectedSliceExploded=True, PieLabelStyle=Outside" 
                        Label="#AXISLABEL: #PERCENT" Legend="Legend1" 
                        IsValueShownAsLabel="True">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                        <Position Height="99" Width="99" X="3" Y="3" />
                        <Area3DStyle Enable3D="True" />
                    </asp:ChartArea>
                </ChartAreas>
                <Legends>
                    <asp:Legend Docking="Bottom" Name="Legend1" TableStyle="Wide" 
                        AutoFitMinFontSize="8"  ShadowOffset="3">
                    </asp:Legend>
                </Legends>
                <Titles>
                    <asp:Title Name="Title1" Text="外銷接單-總重量">
                    </asp:Title>
                </Titles>
            </asp:Chart>

        </td>
    </tr>
 
</table>

<script type="text/javascript">
    function onSliceClicked(pointIndex) {
        var objSeries = document.Form1.elements["SeriesTooltip"];
        var objLegend = document.Form1.elements["LegendTooltip"];

        var parameters = "seriesTooltip=" + objSeries.options[objSeries.selectedIndex].value;
        parameters = parameters + "&legendTooltip=" + objLegend.options[objLegend.selectedIndex].value;

        document.Form1.elements["Chart1"].ImageUrl = "ImageMapToolTipsChart.aspx?" + parameters;

        document.images["Chart1"].src = document.Form1.elements["Chart1"].ImageUrl;

    }

    function onSliceClicked2(pointIndex) {
        var objSeries = document.Form1.elements["SeriesTooltip"];
        var objLegend = document.Form1.elements["LegendTooltip"];

        var parameters = "seriesTooltip=" + objSeries.options[objSeries.selectedIndex].value;
        parameters = parameters + "&legendTooltip=" + objLegend.options[objLegend.selectedIndex].value;

        document.Form1.elements["Chart2"].ImageUrl = "ImageMapToolTipsChart.aspx?" + parameters;

        document.images["Chart2"].src = document.Form1.elements["Chart2"].ImageUrl;
    } 
			 
		</script>
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
