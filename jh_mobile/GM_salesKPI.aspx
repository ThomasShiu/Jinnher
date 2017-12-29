<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GM_salesKPI.aspx.cs" Inherits="GM_salesKPI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>外銷出貨KPI Demo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table>
            <tr>
                <td>
                <table>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            
                                <asp:Label ID="Label1" runat="server" Text="業務員："></asp:Label>
                            <asp:DropDownList ID="DDL_sales" runat="server" Width="100px" 
                                DataSourceID="SqlDataSource1" DataTextField="LOGIN_NAME" 
                                DataValueField="EMPLOY_NO" AppendDataBoundItems="true" 
                                    onselectedindexchanged="DDL_sales_SelectedIndexChanged" 
                                    AutoPostBack="True">
                                <asp:ListItem>  </asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:jheip %>" 
                                ProviderName="<%$ ConnectionStrings:jheip.ProviderName %>" 
                                SelectCommand="SELECT * FROM SYS_USER WHERE DEPT_NO = 'SALE' AND SALES_CODE = 'E'AND SALES_TYPE = 'I' "></asp:SqlDataSource>
                            </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="DDL_sales" 
                                        EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="Label2" runat="server" Text="客戶1："></asp:Label>
                            <asp:DropDownList ID="DDL_cust" runat="server" Width="100px" 
                                    DataSourceID="SqlDataSource2" DataTextField="CUS_ID" DataValueField="CUS_ID"
                                    AppendDataBoundItems="true" AutoPostBack="True" 
                                    onselectedindexchanged="DDL_cust_SelectedIndexChanged" >
                                    <asp:ListItem>  </asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:jheip %>" 
                                    ProviderName="<%$ ConnectionStrings:jheip.ProviderName %>" 
                                    SelectCommand=""></asp:SqlDataSource>
                            </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="DDL_sales" 
                                        EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                         <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="Label3" runat="server" Text="客戶2："></asp:Label>
                            <asp:DropDownList ID="DDL_cust2" runat="server" Width="100px" 
                                    DataSourceID="SqlDataSource3" DataTextField="CUS_ID" DataValueField="CUS_ID"
                                    AppendDataBoundItems="true" AutoPostBack="True" 
                                    onselectedindexchanged="DDL_cust2_SelectedIndexChanged" >
                                    <asp:ListItem>  </asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:jheip %>" 
                                    ProviderName="<%$ ConnectionStrings:jheip.ProviderName %>" 
                                    SelectCommand=""></asp:SqlDataSource>
                            </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="DDL_sales" 
                                        EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                </td>
            </tr>
            <tr>
                <td>
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Chart ID="Chart1" runat="server" Width="800px" Height="380px" BorderWidth="2" backcolor="#D3DFF0" BorderColor="26, 59, 105">
                                <Series>
                                    <asp:Series ChartArea="ChartArea1" ChartType="Line" 
                                        Legend="Legend1" MarkerStyle="Square"  Name="去年">
                                    </asp:Series>
                                    <asp:Series ChartArea="ChartArea1" ChartType="Line" 
                                        Legend="Legend1" MarkerStyle="Square" Name="今年">
                                    </asp:Series>

                                     <asp:Series ChartArea="ChartArea2" ChartType="Line" 
                                        Legend="Legend2" MarkerStyle="Square"  Name="去年2">
                                    </asp:Series>
                                    <asp:Series ChartArea="ChartArea2" ChartType="Line" 
                                        Legend="Legend2" MarkerStyle="Square" Name="今年2">
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
                                        <Area3DStyle IsClustered="True" PointDepth="10" PointGapDepth="10" 
                                            WallWidth="1" />
                                    </asp:ChartArea>
                                    <asp:ChartArea Name="ChartArea2" ShadowOffset="5">
                                        <AxisY>
                                            <MajorGrid LineColor="Gainsboro" />
                                            <MinorGrid LineColor="Gainsboro" />
                                        </AxisY>
                                        <AxisX Interval="1" >
                                            <MajorGrid LineColor="Gainsboro" />
                                            <MinorGrid LineColor="Gainsboro" />
                                            <MajorTickMark LineColor="Gainsboro" />
                                        </AxisX>
                                        <Area3DStyle IsClustered="True" PointDepth="10" PointGapDepth="10" 
                                            WallWidth="1" />
                                    </asp:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <asp:Legend Name="Legend1" MaximumAutoSize="10" DockedToChartArea="ChartArea1" 
                                        IsDockedInsideChartArea="False">
                                    </asp:Legend>
                                    <asp:Legend Name="Legend2" MaximumAutoSize="10" DockedToChartArea="ChartArea2" 
                                        IsDockedInsideChartArea="False">
                                    </asp:Legend>
                                </Legends>
                                <Titles>
                                    <asp:Title Name="Title1" Text="JINNHER 業務KPI" ToolTip="JINNHER 業務KPI">
                                    </asp:Title>
                                </Titles>
                                    <BorderSkin SkinStyle="Raised" />
                            </asp:Chart>
                             </ContentTemplate>
                     <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="DDL_cust2" 
                             EventName="SelectedIndexChanged" />
                     </Triggers>
                    </asp:UpdatePanel>

                             <asp:Chart ID="Chart2" runat="server" Width="800px" Height="180px" BorderWidth="2" backcolor="#D3DFF0" BorderColor="26, 59, 105">
                                <Series>
                                    <asp:Series Name="大前年" 
                                        Legend="Legend1" MarkerStyle="Square">
                                    </asp:Series>
                                    <asp:Series Name="前年" 
                                        Legend="Legend1" MarkerStyle="Square">
                                    </asp:Series>
                                    <asp:Series Name="去年" 
                                        Legend="Legend1" MarkerStyle="Square">
                                    </asp:Series>
                                    <asp:Series ChartArea="ChartArea1" ChartType="Spline" 
                                        Legend="Legend1" MarkerStyle="Square" Name="今年" Color="Green">
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
                                        <Position Height="90" Width="85" X="0" Y="15" />
                                        <Area3DStyle IsClustered="True" PointDepth="10" PointGapDepth="10" 
                                            WallWidth="1" />
                                    </asp:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <asp:Legend Name="Legend1" MaximumAutoSize="10">
                                    </asp:Legend>
                                </Legends>
                                <Titles>
                                    <asp:Title Name="Title1" Text="JINNHER 業務KPI-美金" ToolTip="JINNHER 業務KPI">
                                    </asp:Title>
                                </Titles>
                                    <BorderSkin SkinStyle="Raised" />
                            </asp:Chart>
                    <asp:Chart ID="Chart3" runat="server" Width="800px" Height="180px" BorderWidth="2" backcolor="#D3DFF0" BorderColor="26, 59, 105">
                                <Series>
                                    <asp:Series Name="大前年" 
                                        Legend="Legend1"  ChartType="Spline" MarkerStyle="Square">
                                    </asp:Series>
                                    <asp:Series Name="前年" 
                                        Legend="Legend1"  ChartType="Spline" MarkerStyle="Square">
                                    </asp:Series>
                                    <asp:Series Name="去年" ChartType="Spline" 
                                        Legend="Legend1" MarkerStyle="Square">
                                    </asp:Series>
                                    <asp:Series ChartArea="ChartArea1" ChartType="Spline" 
                                        Legend="Legend1" MarkerStyle="Square" Name="今年" Color="#00CC00">
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
                                        <Position Height="90" Width="85" X="0" Y="15" />
                                        <Area3DStyle IsClustered="True" PointDepth="10" PointGapDepth="10" 
                                            WallWidth="1" />
                                    </asp:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <asp:Legend Name="Legend1" MaximumAutoSize="10">
                                    </asp:Legend>
                                </Legends>
                                <Titles>
                                    <asp:Title Name="Title1" Text="JINNHER 業務KPI-歐元" ToolTip="JINNHER 業務KPI">
                                    </asp:Title>
                                </Titles>
                                    <BorderSkin SkinStyle="Raised" />
                            </asp:Chart>
                   
                </td>
            </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
