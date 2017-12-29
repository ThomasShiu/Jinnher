<%@ Page Title="內銷訂單統計" Language="C#" MasterPageFile="~/JH_MP.master" AutoEventWireup="true" CodeFile="GM_demosorder.aspx.cs" Inherits="GM_demosorder" %>
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
            <asp:UpdateProgress ID="UpdateProgress1" runat="server"  >
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
    
    </h3>
	<div>

		<asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="DS_curr" onrowcommand="GridView1_RowCommand" 
                onselectedindexchanging="GridView1_SelectedIndexChanging" 
                EmptyDataText="NO DATA">
            <Columns>
                <asp:BoundField DataField="PO_COUNT" HeaderText="訂單數" 
                    SortExpression="PO_COUNT" DataFormatString="{0:n0}" >
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Right" Height="32px" />
                </asp:BoundField>
                <asp:BoundField DataField="CO_AMT" HeaderText="金額(千)" 
                    SortExpression="CO_AMT" DataFormatString="{0:n0}" >
                <HeaderStyle Width="80px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="CUS_COUNT" HeaderText="客戶數" 
                    SortExpression="CUS_COUNT" DataFormatString="{0:n0}" >
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
               
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
                    SortExpression="CURRENCY" Visible="False" >
                <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="PO_COUNT" HeaderText="訂單數" 
                    SortExpression="PO_COUNT" DataFormatString="{0:n0}" >
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="CO_AMT" HeaderText="金額(元)" 
                    SortExpression="CO_AMT" DataFormatString="{0:n0}" >
                <HeaderStyle Width="80px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="CUS_WT" HeaderText="總重(公斤)" 
                    SortExpression="CUS_WT" DataFormatString="{0:n0}" >
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
	<h3><a href="#">訂單匯總</a>  
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
            DataKeyNames="PO_NO" DataSourceID="DS_ordsum" 
                onrowcommand="GridView3_RowCommand" 
                onselectedindexchanging="GridView3_SelectedIndexChanging" 
                onrowdatabound="GridView3_RowDataBound" EmptyDataText="NO DATA">
            <Columns>
                <asp:TemplateField HeaderText="CO_ID" SortExpression="CO_ID" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("CO_ID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("CO_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="訂單號碼" SortExpression="PO_NO">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("PO_NO") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L_pono" runat="server" Text='<%# Bind("PO_NO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="120px" />
                </asp:TemplateField>
                <asp:BoundField DataField="PO_AMT" HeaderText="總金額" 
                    SortExpression="PO_AMT" DataFormatString="{0:n0}" >
                <HeaderStyle Width="80px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PO_WT" HeaderText="總重量" SortExpression="PO_WT" 
                    DataFormatString="{0:n0}" >
                <HeaderStyle Width="80px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="TERM" HeaderText="交易方式" 
                    SortExpression="TERM">
                <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="TERM1" HeaderText="交易方式" SortExpression="TERM1" >
                <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="確認" SortExpression="CONFIRMED">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CONFIRMED") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L_confirm" runat="server" Text='<%# Bind("CONFIRMED") %>' Visible="False"></asp:Label>
                        <asp:Image ID="Img_Y" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="取消" SortExpression="CANCELED">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CANCELED") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L_cancel" runat="server" Text='<%# Bind("CANCELED") %>' Visible="False"></asp:Label>
                        <asp:Image ID="Img_N" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                <ItemTemplate>
                        <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Select" 
                            ImageUrl="~/images/magnifier.png" />
                </ItemTemplate>
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
             <HeaderStyle CssClass="GridViewHeader" />
            <SelectedRowStyle BackColor="#86C2FF" BorderStyle="Double" />
        </asp:GridView>
        <asp:Label ID="Label6" runat="server" Text="總重量：" Font-Bold="True" 
                      ForeColor="Black" Font-Size="Medium"></asp:Label><asp:Label ID="L_ttlwt" 
                      runat="server" BackColor="#FF5050" ForeColor="White" 
                      Visible="False" Font-Size="Medium"></asp:Label>
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
	<h3><a href="#">訂單明細</a>
    <asp:UpdateProgress ID="UpdateProgress4" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel4">
                <ProgressTemplate>
                    <asp:Image ID="Image5" runat="server" ImageUrl="~/images/Loading4.gif"  />
                    <asp:Label ID="L_load4" runat="server" Text="Loading"></asp:Label>
                </ProgressTemplate>
            </asp:UpdateProgress>
    </h3>
	<div>
		<asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
        <table  width="100%">
          <tr>
            <td>
            <asp:GridView ID="GridView4" runat="server" DataSourceID="DS_order" 
            AutoGenerateColumns="False" onrowcommand="GridView4_RowCommand" 
                onselectedindexchanging="GridView4_SelectedIndexChanging" 
                    EmptyDataText="NO DATA">
            <Columns>
                <asp:BoundField DataField="PO_NO" HeaderText="PO_NO" 
                    SortExpression="PO_NO"  Visible="false" />
                <asp:TemplateField HeaderText="種類" SortExpression="CAT">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CAT") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <a href='<%#"GM_showdescript.aspx?sn="+ Eval("CAT") %>' rel="shadowbox;height=200;width=300" title="規格描述" >
                          <asp:Label ID="L_cat" runat="server" Text='<%# Bind("CAT") %>'></asp:Label>
                        </a>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                </asp:TemplateField>
                <asp:BoundField DataField="PDT_SIZE" HeaderText="規格" 
                    SortExpression="PDT_SIZE" >
                <HeaderStyle Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="FIN_ID" HeaderText="鍍別" 
                    SortExpression="FIN_ID" >
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="PACK_CODE" DataFormatString="{0:n0}" HeaderText="包裝" 
                    SortExpression="PACK_CODE" >
                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="ODR_PCS" DataFormatString="{0:n3}" HeaderText="訂單(千支)" 
                    SortExpression="ODR_PCS">
                <HeaderStyle Width="80px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="STAND_WT" DataFormatString="{0:n0}" HeaderText="標準重" 
                    SortExpression="STAND_WT">
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="TTLWT" DataFormatString="{0:n0}" HeaderText="總重" 
                    SortExpression="TTLWT">
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PCSPRICE" DataFormatString="{0:n0}" HeaderText="千支單價" 
                    SortExpression="PCSPRICE">
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="NTKGPRICE" DataFormatString="{0:n2}" 
                    HeaderText="公斤價" SortExpression="NTKGPRICE">
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="STNDRD_PCS_PRICE" DataFormatString="{0:n2}" 
                    HeaderText="標準千支價" SortExpression="STNDRD_PCS_PRICE">
                <HeaderStyle Width="80px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="STNDRD_WT_PRICE" DataFormatString="{0:n2}" 
                    HeaderText="標準公斤價" SortExpression="STNDRD_WT_PRICE">
                <HeaderStyle Width="80px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PRICE_RATIO" DataFormatString="{0:n2}" 
                    HeaderText="價差比率" SortExpression="PRICE_RATIO">
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:TemplateField>
                <ItemTemplate>
                     <%--<a href='<%#"GM_showdescript.aspx?sn="+ Eval("PDT_CAT_CAT") %>' rel="shadowbox;height=100;width=300" title="規格描述" >
                        <asp:Image ID="Img_descript" runat="server" ImageUrl="~/images/magnifier.png"    BorderStyle="None" />
                     </a>--%>
                    <asp:ImageButton ID="Img_showdescript" runat="server" ImageUrl="~/images/magnifier.png" CommandName="Select" />
                    </ItemTemplate>
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="GridViewHeader" />
            <SelectedRowStyle BackColor="#86C2FF" BorderStyle="Double" />
        </asp:GridView>
            </td>
          </tr>
          
        </table>
        <asp:SqlDataSource ID="DS_order" runat="server" 
            ConnectionString="<%$ ConnectionStrings:jh815 %>" 
            ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand="">
        </asp:SqlDataSource>
<script type="text/javascript">
    Shadowbox.init({
        handleOversize: "drag",
        handleUnsupported: "remove",
        autoplayMovies: false
    });

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequest);
    function EndRequest(sender, args) {
        Shadowbox.setup();

        $(function () {
            $("#floatdiv").floatdiv("leftbottom");
        });
    }   
</script>
        </ContentTemplate>
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GridView4" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GridView3" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GridView2" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="Quy_btn" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
	</div>
</div>

</asp:Content>

