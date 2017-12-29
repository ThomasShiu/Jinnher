<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GM_salesKPI_5.aspx.cs" Inherits="GM_salesKPI_5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>外銷業務出貨KPI</title>
     <meta name="viewport" content="width=device-width; initial-scale=1.0; user-scalable=1;"/>
     <link href="Styles/basic2.css" rel="stylesheet" type="text/css" />
     <link href="js/start/jquery-ui-1.8.13.custom.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.blockUI.js" type="text/javascript"></script>
		<script type="text/javascript" src="js/jquery-ui-1.8.13.custom.min.js"></script>
        <script type="text/javascript">
            $(function () {
                //$('#tabs').tabs();

                // Tabs
                $('#tabs').tabs().css("width", "100%");

                //hover states on the static widgets
                $('#dialog_link, ul#icons li').hover(
                  function () { $(this).addClass('ui-state-hover'); },
                  function () { $(this).removeClass('ui-state-hover'); });

                $('#tabs').bind("tabsshow", function (event, ui) { GetIndex(); });

                //如果目前的  hTag 有暫存資料，則將 Tab 與暫存資料同步
                SyncTab();

            });

            function GetIndex() {// 將目前的 Tab Index 存入 hTag 暫存
                var $tabs = $('#tabs').tabs();
                var $tagIndex = $tabs.tabs('option', 'selected');
                $("input[id*=hTag]").val($tagIndex);
            }



            function SyncTab() {//如果目前的  hTag 有暫存資料，則將 Tab 與暫存資料同步   
                var $tagIndex = $("input[id*=hTag]").val();

                if ($tagIndex != '') {
                    $('#tabs').tabs('select', parseInt($tagIndex));
                }
            }

            $(document).ready(function () {
                $('.here').click(function () {
                    $.blockUI({ message: $('<h1 style="text-align:center"><img src="images/loading3.gif" /> <br/>loading...</h1>') });

                    setTimeout($.unblockUI, 8000);
                });

                $('.here2').click(function () {
                    $.blockUI({
                        message: $('img#displayBox'),
                        css: {
                            top: '40%',
                            left: '45%',
                            textAlign: 'left',
                            marginLeft: '-65x',
                            marginTop: '-65px',
                            width: '65px',
                            height: '65px'
                        }
                    });
                    setTimeout($.unblockUI, 60000);
                });
            });

            function clicklink(pdate1, machid1) {
                window.open('GM_prodlineDetail?pdate=' & pdate & '&machid=' & machid, 'Machine', config = 'height=300,width=700')
            };
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <input type="hidden" id="hTag" runat="server" />
    <div id="tabs" style="width: 400px">
	    <ul>
		    <li><a href="#tabs-1">業務出貨統計</a></li>	
            <%--<li><a href="#tabs-2">訂單出貨統計</a></li>
            <li><a href="#tabs-3">訂單庫存統計</a></li>--%>
            <li><a href="#tabs-4">出貨彙總表</a></li>
            <%--<li><a href="#tabs-5">預測值設定</a></li>--%>
	    </ul>
	    <div id="tabs-1">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="年度"></asp:Label>
                                    <asp:DropDownList ID="DDL_year" runat="server" DataSourceID="SDS_year" 
                                        DataTextField="YY" DataValueField="YY">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SDS_year" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                                        ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" 
                                        SelectCommand="">
                                    </asp:SqlDataSource>
                                </td>
                              
                                <td>
                                
                                    <asp:RadioButtonList ID="RBL_switch1" runat="server" 
                                        RepeatDirection="Horizontal" AutoPostBack="True" 
                                        onselectedindexchanged="RBL_switch1_SelectedIndexChanged">
                                        <asp:ListItem Value="spline" Selected="True">Line</asp:ListItem>
                                        <asp:ListItem Value="column" >Column</asp:ListItem>
                                        
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                   <div class="here2">
                                      <asp:ImageButton ID="IBtn_refresh1" runat="server" 
                                          ImageUrl="~/images/magnifier.png" onclick="IBtn_refresh1_Click" 
                                          ToolTip="重新整理" />
                                  </div>
                                </td>
                                <td>
                                 <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                                   AssociatedUpdatePanelID="UpdatePanel1">
                                   <ProgressTemplate>
                                       <asp:Image ID="Image8" runat="server" ImageUrl="~/images/Loading4.gif" />
                                       <asp:Label ID="LabelA" runat="server" Text="LOADING" ForeColor="Red"></asp:Label>
                                   </ProgressTemplate>
                                  </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    <table>
                    <tr>
                        <td valign="top">

                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="DS_sales" onrowcommand="GridView1_RowCommand" 
                                onselectedindexchanging="GridView1_SelectedIndexChanging" 
                                EmptyDataText="NO DATA"  >
                            <Columns>
                                <asp:TemplateField HeaderText="業務" SortExpression="EMPLOY_NO" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("EMPLOY_NO") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="L_EMPLOY_NO" runat="server" Text='<%# Bind("EMPLOY_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="EMP_NAME" HeaderText="業務人員" 
                                    SortExpression="EMP_NAME" DataFormatString="{0:d}" >
                                <HeaderStyle Width="70px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLWT" HeaderText="重量(噸)" 
                                    SortExpression="TTLWT" DataFormatString="{0:n0}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLAMT" HeaderText="金額" 
                                    SortExpression="TTLAMT" DataFormatString="{0:n0}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" 
                                            ImageUrl="~/images/magnifier.png"  Width="32px"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="GridViewHeader" />
                            <SelectedRowStyle BackColor="#86C2FF" BorderStyle="Double" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="DS_sales" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                            ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
                        </td>
                        <td valign="top">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="DS_cust" onrowcommand="GridView2_RowCommand" 
                                onselectedindexchanging="GridView2_SelectedIndexChanging" 
                                EmptyDataText="NO DATA">
                            <Columns>
                                <asp:TemplateField HeaderText="客戶" SortExpression="CO_CO_ID">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CO_CO_ID") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="L_CO_CO_ID" runat="server" Text='<%# Bind("CO_CO_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                              

                                <asp:BoundField DataField="TTLWT" HeaderText="重量(噸)" 
                                    SortExpression="TTLWT" DataFormatString="{0:n0}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLAMT" HeaderText="金額" 
                                    SortExpression="TTLAMT" DataFormatString="{0:n0}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                
                               
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" 
                                            ImageUrl="~/images/magnifier.png"  Width="32px"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="GridViewHeader" />
                            <SelectedRowStyle BackColor="#86C2FF" BorderStyle="Double" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="DS_cust" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                            ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
                            </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
                                    <asp:AsyncPostBackTrigger ControlID="GridView2" EventName="RowCommand" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                    <table>
                        <tr>
                            <td>
                                <asp:Chart ID="Chart1" runat="server" Width="750px" Height="350px">
                                <Series>
                                    <asp:Series Name="今年" ChartArea="ChartArea1" ChartType="Spline" IsValueShownAsLabel="True" 
                                        Legend="Legend1"    BorderWidth="2" Color="#DC0300">
                                    </asp:Series>
                                     <asp:Series Name="去年" ChartArea="ChartArea1" ChartType="Spline" IsValueShownAsLabel="True" 
                                        Legend="Legend1"     BorderWidth="2"  Color="#006BDC">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" ShadowOffset="1" BorderColor="LightGray">
                                        <AxisY InterlacedColor="224, 224, 224" Title="重量-噸" LineColor="Gainsboro">
                                            <MajorGrid LineColor="Gainsboro" />
                                            <MinorGrid LineColor="Gainsboro" />
                                        </AxisY>
                                        <AxisX Interval="1" Title="時間(年月)" LineColor="Gainsboro" MaximumAutoSize="90">
                                            <MajorGrid LineColor="Gainsboro" />
                                            <MinorGrid LineColor="Gainsboro" />
                                        </AxisX>
                       
                                    </asp:ChartArea>
                                </ChartAreas>

                                <Legends>
 
                                    <asp:Legend Name="Legend1">
                                       <%-- <Position Height="4" Width="90.507507" X="60" Y="1" />--%>
                                    </asp:Legend>
                                </Legends>
                                <Titles>
                                   <%-- <asp:Title Name="Title1" Text="業務接單統計" ToolTip="業務接單統計">
                                    </asp:Title>--%>
                                </Titles>
                            </asp:Chart>    
                            </td>
                            <td valign="top">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                    DataSourceID="SDS_orders1" EmptyDataText="NO DATA" CellPadding="4" 
                                    ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="CO_ID" HeaderText="客戶代碼" SortExpression="CO_ID" 
                                            Visible="False" >
                                        <ControlStyle ForeColor="Black" />
                                        <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" ForeColor="Black" />
                                        <ItemStyle ForeColor="Black" />
                                        </asp:BoundField>
                                     
                                        <asp:BoundField DataField="YM" HeaderText="月份" SortExpression="YM" >
                                        <HeaderStyle BackColor="#CCCCCC" ForeColor="Black" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TTLAMT" HeaderText="金額" 
                                            SortExpression="TTLAMT" DataFormatString="{0:n0}">
                                        <HeaderStyle BackColor="#CCCCCC" ForeColor="Black" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TTLWT" HeaderText="重量(噸)" SortExpression="TTLWT" >
                                        <HeaderStyle BackColor="#CCCCCC" ForeColor="Black" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="SDS_orders1" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                                    ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" 
                                    SelectCommand="">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                      
                    </td>
                </tr>
               
            </table>
                         
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="IBtn_refresh1" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="DDL_year"      EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="RBL_switch1"   EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="GridView2" EventName="RowCommand" />
                    <asp:AsyncPostBackTrigger ControlID="GridView3" EventName="RowCommand" />
                </Triggers>
            </asp:UpdatePanel> 
        </div>
        <%--<div id="tabs-2">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="tabs-3">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>--%>
     <%--   接單彙總表、前十大客戶--%>
        <div id="tabs-4">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <table>
                <tr>
                    <td>
                    <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="年度"></asp:Label>
                                    <asp:DropDownList ID="DDL_year2" runat="server" DataSourceID="SDS_year2" 
                                        DataTextField="YY" DataValueField="YY" 
                                        onselectedindexchanged="DDL_year2_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SDS_year2" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                                        ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" 
                                        SelectCommand="">
                                    </asp:SqlDataSource>
                                </td>
                              
                                <td>
                                
                                    <asp:RadioButtonList ID="RBL_switch2" runat="server" 
                                        RepeatDirection="Horizontal" AutoPostBack="True" 
                                        onselectedindexchanged="RBL_switch2_SelectedIndexChanged">
                                        <asp:ListItem Value="spline" Selected="True">Line</asp:ListItem>
                                        <asp:ListItem Value="column" >Column</asp:ListItem>
                                        
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                   <div class="here2">
                                      <asp:ImageButton ID="IBtn_refresh2" runat="server" 
                                          ImageUrl="~/images/magnifier.png" onclick="IBtn_refresh2_Click" 
                                          ToolTip="重新整理" />
                                  </div>
                                </td>
                                <td>
                                 <asp:UpdateProgress ID="UpdateProgress4" runat="server" 
                                   AssociatedUpdatePanelID="UpdatePanel4">
                                   <ProgressTemplate>
                                       <asp:Image ID="Image88" runat="server" ImageUrl="~/images/Loading4.gif" />
                                       <asp:Label ID="LabelAA" runat="server" Text="LOADING" ForeColor="Red"></asp:Label>
                                   </ProgressTemplate>
                                  </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                    
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:Chart ID="Chart3" runat="server" Height="350px" Width="1100px">
                            <Series>
                                <asp:Series Name="今年" BorderWidth="2" ChartArea="ChartArea1" ChartType="Line" 
                                    Color="#DC0300" IsValueShownAsLabel="True" Legend="Legend1">
                                </asp:Series>
                                <asp:Series BorderWidth="2" ChartArea="ChartArea1" ChartType="Line" 
                                    Color="#006BDC" IsValueShownAsLabel="True" Legend="Legend1" Name="去年">
                                </asp:Series>
                                <asp:Series Name="預測值" BorderWidth="2" ChartArea="ChartArea1" ChartType="Line" 
                                    Color="#00BE02" IsValueShownAsLabel="True" Legend="Legend1">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" BorderColor="LightGray" ShadowOffset="1">
                                    <AxisY InterlacedColor="224, 224, 224" LineColor="Gainsboro" Title="重量-噸">
                                        <MajorGrid LineColor="Gainsboro" />
                                        <MinorGrid LineColor="Gainsboro" />
                                    </AxisY>
                                    <AxisX Interval="1" LineColor="Gainsboro" MaximumAutoSize="90" Title="時間(年月)">
                                        <MajorGrid LineColor="Gainsboro" />
                                        <MinorGrid LineColor="Gainsboro" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                            <Legends>
                                <asp:Legend Name="Legend1">
                                    <%-- <Position Height="4" Width="90.507507" X="60" Y="1" />--%>
                                </asp:Legend>
                            </Legends>
                            <Titles>
                                <%--<asp:Title Name="Title1" Text="業務接單統計" ToolTip="業務接單統計">
                                </asp:Title>--%>
                            </Titles>
                        </asp:Chart>
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:Chart ID="Chart2" runat="server" Height="350px" Width="1100px">
                            <Series>
                                <%--<asp:Series Name="今年" BorderWidth="2" ChartArea="ChartArea1" ChartType="Line" 
                                    Color="#DC0300" IsValueShownAsLabel="True" Legend="Legend1">
                                </asp:Series>
                                <asp:Series BorderWidth="2" ChartArea="ChartArea1" ChartType="Line" 
                                    Color="#5200A0" IsValueShownAsLabel="True" Legend="Legend1" Name="去年">
                                </asp:Series>--%>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" BorderColor="LightGray" ShadowOffset="1">
                                    <AxisY InterlacedColor="224, 224, 224" LineColor="Gainsboro" Title="重量-噸">
                                        <MajorGrid LineColor="Gainsboro" />
                                        <MinorGrid LineColor="Gainsboro" />
                                    </AxisY>
                                    <AxisX Interval="1" LineColor="Gainsboro" MaximumAutoSize="90" Title="時間(年月)">
                                        <MajorGrid LineColor="Gainsboro" />
                                        <MinorGrid LineColor="Gainsboro" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                            <Legends>
                                <asp:Legend Name="Legend1">
                                    <%-- <Position Height="4" Width="90.507507" X="60" Y="1" />--%>
                                </asp:Legend>
                            </Legends>
                            <Titles>
                                <%--<asp:Title Name="Title1" Text="業務接單統計" ToolTip="業務接單統計">
                                </asp:Title>--%>
                            </Titles>
                        </asp:Chart>  
                    
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="message4" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
              
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="IBtn_refresh2" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <%--<div id="tabs-5">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>--%>
        </div>
    </div>
    </form>
</body>
</html>
