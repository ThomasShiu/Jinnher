<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GM_salesKPI_2.aspx.cs" Inherits="GM_salesKPI_2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>業務接單KPI Demo</title>
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

    <table>
    <tr>
        <td>
            <asp:TextBox ID="TB_year" runat="server" Height="25px" Width="86px">2013</asp:TextBox>
            <asp:Button ID="BTN_query" runat="server" Text="查詢" onclick="BTN_query_Click" />
        </td>
    </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td valign="top">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="DS_sales" onrowcommand="GridView1_RowCommand" 
                                onselectedindexchanging="GridView1_SelectedIndexChanging" 
                                EmptyDataText="NO DATA">
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

                                <asp:BoundField DataField="TTLAMT" HeaderText="金額(千)" 
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
                            </ContentTemplate>
                             <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td valign="top">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
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

                                <asp:BoundField DataField="TTLAMT" HeaderText="金額(千)" 
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
            <td>
            <input type="hidden" id="hTag" runat="server" />
            <div id="tabs" style="width: 400px">
	    <ul>
		    <li><a href="#tabs-1">金額曲線</a></li>	
            <li><a href="#tabs-3">金額數據</a></li>
            <li><a href="#tabs-2">重量曲線</a></li>
            <li><a href="#tabs-4">重量數據</a></li>
	    </ul>
	    <div id="tabs-1">
                
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                   <asp:Chart ID="Chart1" runat="server" Width="800px" Height="180px" BorderWidth="2" backcolor="#D3DFF0" BorderColor="26, 59, 105">
                        <Series>
                            
                            <asp:Series ChartArea="ChartArea1" ChartType="Spline" 
                                Legend="Legend1" MarkerStyle="Square" Name="今年" Color="Red">
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
                            <asp:Title Name="Title1" Text="JINNHER 業務接單 BY 客戶" ToolTip="JINNHER 業務接單">
                            </asp:Title>
                        </Titles>
                            <BorderSkin SkinStyle="Raised" />
                    </asp:Chart>             
                </ContentTemplate>
                </asp:UpdatePanel>
                 
          </div>
        <div id="tabs-2">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                   <asp:Chart ID="Chart2" runat="server" Width="800px" Height="180px" BorderWidth="2" backcolor="#D3DFF0" BorderColor="26, 59, 105">
                        <Series>
                            
                            <asp:Series ChartArea="ChartArea1" ChartType="Spline" 
                                Legend="Legend1" MarkerStyle="Square" Name="今年" Color="Blue">
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
                            <asp:Title Name="Title1" Text="JINNHER 業務接單 BY 客戶" ToolTip="JINNHER 業務接單">
                            </asp:Title>
                        </Titles>
                            <BorderSkin SkinStyle="Raised" />
                    </asp:Chart>             
                </ContentTemplate>
                </asp:UpdatePanel>
        </div>
                <div id="tabs-3">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                 <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="SDS_amt" onrowcommand="GridView1_RowCommand" 
                                onselectedindexchanging="GridView1_SelectedIndexChanging" 
                                EmptyDataText="NO DATA">
                            <Columns>
                                <asp:TemplateField HeaderText="客戶" SortExpression="CO_CO_ID"  >
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CO_CO_ID") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="L_CO_CO_ID" runat="server" Text='<%# Bind("CO_CO_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                

                                <asp:BoundField DataField="TTLAMT_1" HeaderText="一月" 
                                    SortExpression="TTLAMT_1" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLAMT_2" HeaderText="二月" 
                                    SortExpression="TTLAMT_2" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLAMT_3" HeaderText="三月" 
                                    SortExpression="TTLAMT_3" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLAMT_4" HeaderText="四月" 
                                    SortExpression="TTLAMT_4" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLAMT_5" HeaderText="五月" 
                                    SortExpression="TTLAMT_5" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLAMT_6" HeaderText="六月" 
                                    SortExpression="TTLAMT_6" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLAMT_7" HeaderText="七月" 
                                    SortExpression="TTLAMT_7" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLAMT_8" HeaderText="八月" 
                                    SortExpression="TTLAMT_8" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLAMT_9" HeaderText="九月" 
                                    SortExpression="TTLAMT_9" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLAMT_10" HeaderText="十月" 
                                    SortExpression="TTLAMT_10" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLAMT_11" HeaderText="十一月" 
                                    SortExpression="TTLAMT_11" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="TTLAMT_12" HeaderText="十二月" 
                                    SortExpression="TTLAMT_12" DataFormatString="{0:n2}" >
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
                        <asp:SqlDataSource ID="SDS_amt" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                            ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>

                </ContentTemplate>
                </asp:UpdatePanel>
                </div>
                <div id="tabs-4">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                 <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="SDS_wt" onrowcommand="GridView1_RowCommand" 
                                onselectedindexchanging="GridView1_SelectedIndexChanging" 
                                EmptyDataText="NO DATA">
                            <Columns>
                                <asp:TemplateField HeaderText="客戶" SortExpression="CO_CO_ID"  >
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CO_CO_ID") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="L_CO_CO_ID" runat="server" Text='<%# Bind("CO_CO_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                

                                <asp:BoundField DataField="TTLWT_1" HeaderText="一月" 
                                    SortExpression="TTLWT_1" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLWT_2" HeaderText="二月" 
                                    SortExpression="TTLWT_2" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLWT_3" HeaderText="三月" 
                                    SortExpression="TTLWT_3" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLWT_4" HeaderText="四月" 
                                    SortExpression="TTLWT_4" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLWT_5" HeaderText="五月" 
                                    SortExpression="TTLWT_5" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLWT_6" HeaderText="六月" 
                                    SortExpression="TTLWT_6" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLWT_7" HeaderText="七月" 
                                    SortExpression="TTLWT_7" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLWT_8" HeaderText="八月" 
                                    SortExpression="TTLWT_8" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLWT_9" HeaderText="九月" 
                                    SortExpression="TTLWT_9" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLWT_10" HeaderText="十月" 
                                    SortExpression="TTLWT_10" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TTLWT_11" HeaderText="十一月" 
                                    SortExpression="TTLWT_11" DataFormatString="{0:n2}" >
                                <HeaderStyle Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="TTLWT_12" HeaderText="十二月" 
                                    SortExpression="TTLWT_12" DataFormatString="{0:n2}" >
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
                        <asp:SqlDataSource ID="SDS_wt" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                            ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>

                </ContentTemplate>
                </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
