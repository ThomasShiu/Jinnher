<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GM_salesKPI_3.aspx.cs" Inherits="GM_salesKPI_3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>外銷業務接單KPI</title>
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
      <style type="text/css">
          .style1
          {
              height: 77px;
          }
          .style2
          {
              height: 85px;
          }
      </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <input type="hidden" id="hTag" runat="server" />
    <div id="tabs" style="width: 400px">
	    <ul>
		    <li><a href="#tabs-1">業務接單統計</a></li>	
            <li><a href="#tabs-2">業務接單統計by月</a></li>
                <%--<li><a href="#tabs-3">訂單庫存統計</a></li>--%>
            <li><a href="#tabs-4">接單彙總表</a></li>
            <li><a href="#tabs-6">接單國家比例</a></li>
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
                                
                                <asp:BoundField DataField="AVG_AMT" HeaderText="平均單價" 
                                    SortExpression="AVG_AMT" DataFormatString="{0:n0}" >
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
                                <asp:Chart ID="Chart1" runat="server" Width="750px" Height="500px">
                                <Series>
                                   <asp:Series Name="去年" ChartArea="ChartArea1" ChartType="Spline" IsValueShownAsLabel="True" 
                                        Legend="Legend1"     BorderWidth="2"  Color="#006BDC">
                                    </asp:Series>
                                    <asp:Series Name="今年" ChartArea="ChartArea1" ChartType="Spline" IsValueShownAsLabel="True" 
                                        Legend="Legend1"    BorderWidth="2" Color="#DC0300">
                                    </asp:Series>

                                    <asp:Series Name="成長率" ChartArea="ChartArea2" ChartType="Column" IsValueShownAsLabel="True" 
                                        Legend="Legend2"     BorderWidth="2"  Color="#FF73B7">
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
                       
                                        <Position Height="55.5" Width="94" X="3" Y="6" />
                       
                                    </asp:ChartArea>
                                    <asp:ChartArea Name="ChartArea2" ShadowOffset="1" BorderColor="LightGray">
                                        <AxisY InterlacedColor="224, 224, 224" Title="成長率" LineColor="Gainsboro">
                                            <MajorGrid LineColor="Gainsboro" />
                                            <MinorGrid LineColor="Gainsboro" />
                                        </AxisY>
                                        <AxisX Interval="1" Title="" LineColor="Gainsboro" MaximumAutoSize="90">
                                            <MajorGrid LineColor="Gainsboro" />
                                            <MinorGrid LineColor="Gainsboro" />
                                        </AxisX>
                       
                                        <Position Height="35.5" Width="94" X="3" Y="60.5" />
                       
                                    </asp:ChartArea>
                                </ChartAreas>

                                <Legends>
 
                                    <asp:Legend Name="Legend1" DockedToChartArea="ChartArea1" 
                                    BackColor="Transparent" MaximumAutoSize="10"  TableStyle="Wide">
                                       <Position Height="4" Width="90.507507" X="70" Y="3" />
                                    </asp:Legend>
                                    <asp:Legend Name="Legend2" DockedToChartArea="ChartArea2" 
                                     BackColor="Transparent" MaximumAutoSize="10"  TableStyle="Wide">
                                       <Position Height="4" Width="90.507507" X="70" Y="59" />
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
                                        <asp:BoundField DataField="EMP_NAME" HeaderText="負責業務" 
                                            SortExpression="EMP_NAME" Visible="False" >
                                        <HeaderStyle BackColor="#CCCCCC" ForeColor="Black" />
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
        <div id="tabs-2">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="年度"></asp:Label>
                                    <asp:DropDownList ID="DDL_yearM" runat="server" DataSourceID="SDS_yearM" 
                                        DataTextField="YY" DataValueField="YY">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SDS_yearM" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                                        ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" 
                                        SelectCommand="">
                                    </asp:SqlDataSource>
                                    <asp:Label ID="Label4" runat="server" Text=" 月份"></asp:Label>
                                    <asp:DropDownList ID="DDL_monthM" runat="server">
                                        <asp:ListItem Selected="True">01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                    </asp:DropDownList>
                                </td>

                                <td>
                                   <div class="here2">
                                      <asp:ImageButton ID="IB_queryM" runat="server" 
                                          ImageUrl="~/images/magnifier.png" onclick="IB_queryM_Click" 
                                          ToolTip="重新整理" />
                                  </div>
                                </td>
                                <td>
                                 <asp:UpdateProgress ID="UpdateProgress2" runat="server" 
                                   AssociatedUpdatePanelID="UpdatePanel2">
                                   <ProgressTemplate>
                                       <asp:Image ID="ImageM" runat="server" ImageUrl="~/images/Loading4.gif" />
                                       <asp:Label ID="LabelM" runat="server" Text="LOADING" ForeColor="Red"></asp:Label>
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
                            <asp:Label ID="Label5" runat="server" Text="年度彙總"></asp:Label>
                            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="DS_salesM"  EmptyDataText="NO DATA"  >
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
                                

                            </Columns>
                            <HeaderStyle CssClass="GridViewHeader" />
                            <SelectedRowStyle BackColor="#86C2FF" BorderStyle="Double" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="DS_salesM" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                            ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
                        </td>
                        <td valign="top">
                        <asp:Label ID="Label6" runat="server" Text="月彙總"></asp:Label>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="DS_month" EmptyDataText="NO DATA">
                            <Columns>
                                <asp:TemplateField HeaderText="業務人員" SortExpression="EMP_NAME">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("EMP_NAME") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="L_EMP_NAME" runat="server" Text='<%# Bind("EMP_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                              
                                <asp:BoundField DataField="YM" HeaderText="年月" 
                                    SortExpression="YM" DataFormatString="{0:D}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Left" />
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
                              
                            </Columns>
                            <HeaderStyle CssClass="GridViewHeader" />
                            <SelectedRowStyle BackColor="#86C2FF" BorderStyle="Double" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="DS_month" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                            ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
                            </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="GridView4" EventName="RowCommand" />
                                    <asp:AsyncPostBackTrigger ControlID="GridView5" EventName="RowCommand" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                    </td>
                </tr>
                <tr>
                <td>
                    <asp:Label ID="L_messM" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                </table>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <%-- <asp:Title Name="Title1" Text="業務接單統計" ToolTip="業務接單統計">
                                    </asp:Title>--%>     <%-- <div id="tabs-3">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>--%>
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
                    <asp:Chart ID="Chart3" runat="server" Height="450px" Width="1100px">
                            <Series>
                                
                                <asp:Series Name="去年" BorderWidth="2" ChartArea="ChartArea1" ChartType="Spline" 
                                    Color="#006BDC" IsValueShownAsLabel="True" Legend="Legend1" >
                                </asp:Series>
                                <asp:Series Name="今年" BorderWidth="2" ChartArea="ChartArea1" ChartType="Spline" 
                                    Color="#DC0300" IsValueShownAsLabel="True" Legend="Legend1">
                                </asp:Series>
                                <asp:Series Name="預測值" BorderWidth="2" ChartArea="ChartArea1" ChartType="Spline" 
                                    Color="#00BE02" IsValueShownAsLabel="True" Legend="Legend1">
                                </asp:Series>
                                 <asp:Series Name="成長率" ChartArea="ChartArea2" ChartType="Column" IsValueShownAsLabel="True" 
                                        Legend="Legend2"     BorderWidth="2"  Color="#FF73B7">
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
                                    <Position Height="55.5" Width="94" X="3" Y="5" />
                                </asp:ChartArea>
                                <asp:ChartArea Name="ChartArea2" ShadowOffset="1" BorderColor="LightGray">
                                        <AxisY InterlacedColor="224, 224, 224" Title="成長率" LineColor="Gainsboro">
                                            <MajorGrid LineColor="Gainsboro" />
                                            <MinorGrid LineColor="Gainsboro" />
                                        </AxisY>
                                        <AxisX Interval="1" Title="" LineColor="Gainsboro" MaximumAutoSize="90">
                                            <MajorGrid LineColor="Gainsboro" />
                                            <MinorGrid LineColor="Gainsboro" />
                                        </AxisX>
                       
                                        <Position Height="35.5" Width="94" X="3" Y="60.5" />
                       
                                    </asp:ChartArea>
                            </ChartAreas>
                            <Legends>
                                <asp:Legend Name="Legend1"  DockedToChartArea="ChartArea1"
                                 BackColor="Transparent" MaximumAutoSize="10"  TableStyle="Wide"> 
                                    <Position Height="4" Width="90.507507" X="60" Y="3" />
                                </asp:Legend>
                                <asp:Legend Name="Legend2"  DockedToChartArea="ChartArea2" 
                                 BackColor="Transparent" MaximumAutoSize="10"  TableStyle="Wide">
                                       <Position Height="4" Width="90.507507" X="60" Y="58" />
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
                    <asp:Chart ID="Chart4" runat="server" Height="250px" Width="1100px" 
                            Palette="SemiTransparent">
                            <Series>
                                <asp:Series Name="前年" BorderWidth="2" ChartArea="ChartArea1" 
                                     Legend="Legend1" MarkerStyle="Circle">
                                </asp:Series>
                                <asp:Series BorderWidth="2" ChartArea="ChartArea1" 
                                     IsValueShownAsLabel="True" Legend="Legend1" Name="去年" 
                                    MarkerStyle="Triangle">
                                </asp:Series>
                                <asp:Series Name="今年" BorderWidth="2" ChartArea="ChartArea1" 
                                    IsValueShownAsLabel="True" Legend="Legend1" MarkerStyle="Circle">
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
                                    <Position Height="95.5" Width="94" X="3" Y="4.5" />
                                </asp:ChartArea>
                             
                            </ChartAreas>
                            <Legends>
                                <asp:Legend Name="Legend1" BackColor="Transparent" DockedToChartArea="ChartArea1"> 
                                    <%-- <Position Height="4" Width="90.507507" X="60" Y="1" />--%>
                                    <Position Height="10.8882523" Width="16.82438564" X="15" Y="3.530909" />
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
                                <asp:Legend Name="Legend1" BackColor="Transparent" >
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
       <div id="tabs-6">
          
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
    <table >
    <tr>
        <td class="style2">
        
              <table>
                <tr>
                  <td class="style1">
                     <asp:Label ID="Label7" runat="server" Text="年度"></asp:Label>
                         
                    <asp:DropDownList ID="DDL_year3" runat="server" DataSourceID="SDS_year3" 
                        DataTextField="YY" DataValueField="YY" >
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SDS_year3" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                        ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" 
                        SelectCommand="">
                    </asp:SqlDataSource>
                    <td class="style1">
                   <asp:DropDownList ID="DDL_month4" runat="server"   Font-Size="Small" Visible="False">
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
                    <asp:RadioButtonList ID="RBL_ym" runat="server" 
                        RepeatDirection="Horizontal" AutoPostBack="True" 
                          onselectedindexchanged="RBL_ym_SelectedIndexChanged" >
                        <asp:ListItem Value="y" Selected="True">By年(洲別)</asp:ListItem>
                        <asp:ListItem Value="m" >By月(國別)</asp:ListItem>               
                    </asp:RadioButtonList>
                              
                  </td>
                    <%--   接單彙總表、前十大客戶--%>
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
            
            
            <asp:Chart ID="Chart5" runat="server" DataSourceID="DS_SALORDER" 
                Palette="SemiTransparent" Height="500px" Width="900px">
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
          <table>
          <tr>
            <td>
            <asp:GridView ID="GridView6" runat="server" DataSourceID="DS_SALORDER" 
                AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" 
                GridLines="Vertical">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:BoundField DataField="CONTINENT" HeaderText="洲別(國別)" 
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
            <td>
                &nbsp;</td>
          </tr>
          </table>
            
        </td>
    </tr>

     <tr>
        <td>     
            <asp:Chart ID="Chart6" runat="server" DataSourceID="DS_SALORDER" 
                Palette="Pastel" Height="500px" Width="900px" >
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
            
        </div>
        </div>
    </div>
    </form>
</body>
</html>
