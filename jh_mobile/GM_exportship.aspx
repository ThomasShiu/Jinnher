<%@ Page Title="外銷出貨統計" Language="C#" MasterPageFile="~/JH_MP.master" AutoEventWireup="true" CodeFile="GM_exportship.aspx.cs" Inherits="GM_exportship" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script>
    $(function () {
        $("#accordion").accordion({
            autoHeight: false,
            navigation: true
        });
    });

    $(function () {
        $('#multiOpenAccordion').multiAccordion({ active: [0, 1, 2, 3] });
    });

    
</script>

 <table  width="100%">
        <tr>
          <td style=" width:450px ;height:35px">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
           <ContentTemplate>
              <asp:Label ID="Label1" runat="server" Text="日期"></asp:Label>
      
             <asp:TextBox ID="TB_sdate" runat="server" Height="25px" Width="90px" 
                   Font-Size="Medium"></asp:TextBox>
            <asp:CalendarExtender ID="TB_sdate_CalendarExtender" runat="server" 
                Enabled="True" TargetControlID="TB_sdate" DaysModeTitleFormat="yyyy,MMMM" 
                Format="yyyy/MM/dd" PopupButtonID="Img_sdate" TodaysDateFormat="yyyy/MM/dd" 
                   CssClass="cal_Theme1">
            </asp:CalendarExtender>
            <asp:ImageButton ID="Img_sdate" runat="server"  Width="28px"
                ImageUrl="~/images/calendar.png" />
            <asp:Label ID="Label2" runat="server" Text="~"></asp:Label>
            <asp:TextBox ID="TB_edate" runat="server" Width="90px" Font-Size="Medium" 
                   Height="25px"></asp:TextBox>
            <asp:CalendarExtender ID="TB_edate_CalendarExtender" runat="server" 
                Enabled="True" PopupButtonID="Img_edate" TargetControlID="TB_edate" 
                DaysModeTitleFormat="yyyy,MMMM" Format="yyyy/MM/dd" 
                   TodaysDateFormat="yyyy/MM/dd" CssClass="cal_Theme1">
            </asp:CalendarExtender>
            <asp:ImageButton ID="Img_edate" runat="server"   Width="28px"
                ImageUrl="~/images/calendar.png" />
            <asp:Button ID="Quy_btn" runat="server" Text="查詢"  Width="80px" Height="30px" onclick="Quy_btn_Click" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Quy_btn" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>  
          </td>
          <td>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                 AssociatedUpdatePanelID="UpdatePanel1"  >
                <ProgressTemplate>
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/Loading3.gif" 
                        Height="35px" /><asp:Label ID="L_load" runat="server" Text="Loading"></asp:Label>
                </ProgressTemplate>
            </asp:UpdateProgress>
          </td>
        </tr>

        </table>


<div id="multiOpenAccordion">
	<h3><a href="#">幣別</a>             
    <asp:UpdateProgress ID="UpdateProgress4" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel2">
                <ProgressTemplate>
                    <asp:Image ID="Image41" runat="server" ImageUrl="~/images/Loading4.gif"  />
                    <asp:Label ID="L_load31" runat="server" Text="Loading"></asp:Label>
                </ProgressTemplate>
            </asp:UpdateProgress>
    </h3>
	<div>

		<asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="DS_curr" onrowcommand="GridView1_RowCommand" 
                onselectedindexchanging="GridView1_SelectedIndexChanging" 
                EmptyDataText="NO DATA">
            <Columns>
                <asp:TemplateField HeaderText="幣別" SortExpression="CURRENCY">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CURRENCY") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L_currency" runat="server" Text='<%# Bind("CURRENCY") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="60px" />
                </asp:TemplateField>
                <asp:BoundField DataField="SUM_AMT" HeaderText="出貨總金額" 
                    SortExpression="SUM_AMT" DataFormatString="{0:N2}" >
                <HeaderStyle Width="90px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="SUM_NW" HeaderText="總重量" 
                    SortExpression="SUM_NW" DataFormatString="{0:N0}" >
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" 
                            ImageUrl="~/images/magnifier.png"  Width="32px"/>
                    </ItemTemplate>
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="GridViewHeader" />
            <SelectedRowStyle BackColor="#86C2FF" BorderStyle="Double" />
        </asp:GridView>
        <asp:SqlDataSource ID="DS_curr" runat="server" 
            ConnectionString="<%$ ConnectionStrings:jh815 %>" 
            ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Quy_btn" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>  

	</div>
	<h3><a href="#">客戶</a>     
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel3">
                <ProgressTemplate>
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/Loading4.gif"  />
                    <asp:Label ID="L_load2" runat="server" Text="Loading"></asp:Label>
                </ProgressTemplate>
            </asp:UpdateProgress>
            
    </h3>
	<div>

		<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
           <asp:Panel ID="Panel1" runat="server" Visible="False">
             <asp:Label ID="Label3" runat="server" Text="金額(千)"></asp:Label>
            <asp:Label ID="L_amt" runat="server" BackColor="#CCCCCC" Width="80px" 
                   CssClass="textleft"></asp:Label>&nbsp;
            <asp:Label ID="Label4" runat="server" Text="台幣" BackColor="#CCCCCC" Width="80px"></asp:Label><br />
            <asp:Label ID="Label5" runat="server" Text="總重(噸)"></asp:Label>
            <asp:Label ID="L_wt" runat="server" BackColor="#CCCCCC" Width="80px" 
                   CssClass="textleft"></asp:Label><br />
         </asp:Panel>

        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            DataSourceID="DS_cust" onrowcommand="GridView2_RowCommand" 
                onselectedindexchanging="GridView2_SelectedIndexChanging" 
                EmptyDataText="NO DATA">
            <Columns>
                <asp:TemplateField HeaderText="公司" SortExpression="CO_ID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CO_ID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L_coid" runat="server" Text='<%# Bind("CO_ID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="60px" />
                </asp:TemplateField>
                <asp:BoundField DataField="CURRENCY" HeaderText="幣別" 
                    SortExpression="CURRENCY" Visible="False" />
                
                <asp:BoundField DataField="SUM_AMT" HeaderText="出貨總金額" 
                    SortExpression="SUM_AMT" DataFormatString="{0:n0}" >
                <HeaderStyle Width="100px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="SUM_NW" HeaderText="總重量" 
                    SortExpression="SUM_NW" DataFormatString="{0:n0}" >
                <HeaderStyle Width="100px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:TemplateField>
                <ItemTemplate>
                        <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Select" 
                            ImageUrl="~/images/magnifier.png" />
                    </ItemTemplate>
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
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
                <asp:AsyncPostBackTrigger ControlID="Quy_btn" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

	</div>
	<h3><a href="#">訂單明細</a> 
    <asp:UpdateProgress ID="UpdateProgress3" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel4">
                <ProgressTemplate>
                    <asp:Image ID="Image4" runat="server" ImageUrl="~/images/Loading4.gif"  />
                    <asp:Label ID="L_load3" runat="server" Text="Loading"></asp:Label>
                </ProgressTemplate>
            </asp:UpdateProgress>
    </h3>
	<div>
        
		<asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <table  width="100%">
            <tr>
              <td>
                  <asp:TextBox ID="TB_coname" runat="server" BackColor="#0033CC" ForeColor="White" 
                    Width="100%" ReadOnly="True" Visible="False" CssClass="textleft"></asp:TextBox>
              </td>
            </tr>
            <tr>
              <td>
            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
              DataSourceID="DS_ordsum" 
                onrowcommand="GridView3_RowCommand" 
                onselectedindexchanging="GridView3_SelectedIndexChanging" 
                onrowdatabound="GridView3_RowDataBound"  EmptyDataText="NO DATA">
                <AlternatingRowStyle BackColor="#FFFFCC" />
            <Columns>
                <asp:BoundField DataField="CO_ID" HeaderText="CO_ID" SortExpression="CO_ID" 
                    Visible="False" />
                <asp:BoundField DataField="CLOSING" HeaderText="結關日" SortExpression="CLOSING" 
                 DataFormatString="{0:yyyy/MM/dd}">
                <HeaderStyle Width="100px" />
                <ItemStyle Height="32px" />
                </asp:BoundField>
                <asp:BoundField DataField="SUM_AMT" HeaderText="出貨總金額" 
                    SortExpression="SUM_AMT" DataFormatString="{0:N2}" >
                <HeaderStyle Width="100px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="CURRENCY" HeaderText="幣別" 
                    SortExpression="CURRENCY" Visible="False" />
                <asp:BoundField DataField="SUM_NW" HeaderText="總重量" SortExpression="SUM_NW" 
                    DataFormatString="{0:N2}" >
                <HeaderStyle Width="100px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>

                <asp:BoundField DataField="PAC_NO" HeaderText="出貨單號" 
                    SortExpression="PAC_NO"   >

                <HeaderStyle Width="120px" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

            </Columns>
             <HeaderStyle CssClass="GridViewHeader" />
            <SelectedRowStyle BackColor="#86C2FF" BorderStyle="Double" />
        </asp:GridView>
        <asp:SqlDataSource ID="DS_ordsum" runat="server" 
            ConnectionString="<%$ ConnectionStrings:jh815 %>" 
            ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
              </td>
            </tr>
        </table>

        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GridView2" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="Quy_btn" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel> 

	</div>

	
</div>
</asp:Content>

