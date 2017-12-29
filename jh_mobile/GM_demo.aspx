<%@ Page Title="" Language="C#" MasterPageFile="~/JH_MP.master" AutoEventWireup="true" CodeFile="GM_demo.aspx.cs" Inherits="GM_demo" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        $(function () {
            $("#tabs").tabs();
        });
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="tabs">
	<ul>
		<li><a href="#tabs-1">公告</a></li>
		<li><a href="#tabs-2">營收</a></li>
		<li><a href="#tabs-3">連結</a></li>
        <li><a href="#tabs-4">資訊</a></li>
	</ul>
	<div id="tabs-1">
	 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataKeyNames="SN" DataSourceID="DS_anno">
                <Columns>
                    <asp:BoundField DataField="SN" HeaderText="SN" ReadOnly="True" 
                        SortExpression="SN" Visible="False" />
                    <asp:BoundField DataField="DEP_NAME" HeaderText="部門" 
                        SortExpression="DEP_NAME" />
                    <asp:BoundField DataField="EMP_NAME" HeaderText="人員" 
                        SortExpression="EMP_NAME" Visible="False" />
                    <asp:BoundField DataField="POST_TITTLE" HeaderText="標題" 
                        SortExpression="POST_TITTLE" />
                    <asp:BoundField DataField="POST_DATE" HeaderText="日期" 
                        SortExpression="POST_DATE" />
                    <asp:BoundField DataField="CREATE_EMP" HeaderText="CREATE_EMP" 
                        SortExpression="CREATE_EMP" Visible="False" />
                    <asp:BoundField DataField="CREATE_DATE" HeaderText="CREATE_DATE" 
                        SortExpression="CREATE_DATE" Visible="False" />
                    <asp:BoundField DataField="BUSI_TYPE" HeaderText="BUSI_TYPE" 
                        SortExpression="BUSI_TYPE" Visible="False" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
        <asp:SqlDataSource ID="DS_anno" runat="server" 
            ConnectionString="<%$ ConnectionStrings:JHmobile %>" 
            ProviderName="<%$ ConnectionStrings:JHmobile.ProviderName %>" 
            SelectCommand="">
        </asp:SqlDataSource>
	</div>
	<div id="tabs-2">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
              <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="選擇年月"></asp:Label>
                    <asp:DropDownList ID="DDL_year" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DDL_month" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label1" runat="server" Text="~"></asp:Label>
                    <asp:DropDownList ID="DDL_monthE" runat="server">
                    </asp:DropDownList>
                    <asp:Button ID="B_qryincome" runat="server" Text="查詢" 
                        onclick="B_qryincome_Click" />
                        <asp:Label ID="Label3" runat="server" Text="DEMO" ForeColor="Red"></asp:Label>
                </td>
              </tr>
              <tr>
                <td>
                  <asp:Chart ID="Chart1" runat="server" DataSourceID="DS_income" Width="320px">
                    <Series>
                        <asp:Series Name="Series1" IsValueShownAsLabel="True" XValueMember="YEARMONTH" 
                            YValueMembers="INCOME">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisX IsLabelAutoFit="False">
                                <LabelStyle Angle="30" />
                            </AxisX>
                            <Area3DStyle Enable3D="True" PointDepth="50" PointGapDepth="50" />
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart> 
	            <asp:SqlDataSource ID="DS_income" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:JHmobile %>" 
                    ProviderName="<%$ ConnectionStrings:JHmobile.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
                </td>
              </tr>
            </table>
        </ContentTemplate>
        </asp:UpdatePanel>    
	</div>
	<div id="tabs-3">
		<table>
          <tr>
            <td>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/GM_exportorder.aspx" Target="_self">外銷接單統計</asp:HyperLink>
            </td>
          </tr>
        </table>
	</div>
    <div id="tabs-4">
		  <asp:Image ID="Weather_icon" runat="server" CssClass="someClass" Text='天氣預報' Height="32px" Width="32px"/>
            
           <asp:Label ID="L_weather" runat="server" Text=""></asp:Label>
	</div>
</div>
</asp:Content>

