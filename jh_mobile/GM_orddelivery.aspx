<%@ Page Title="訂單交期" Language="C#" MasterPageFile="~/JH_MP.master" AutoEventWireup="true" CodeFile="GM_orddelivery.aspx.cs" Inherits="GM_orddelivery" %>
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
        $('#multiOpenAccordion').multiAccordion({ active: [0, 1, 2, 3 ,4] });
    });

    
</script>

 <table  width="100%">
        <tr>
          <td style=" width:300px ;height:35px">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
           <ContentTemplate>
            <asp:Label ID="Label1" runat="server" Text="日期" Visible="False"></asp:Label>
            <asp:Button ID="Quy_btn" runat="server" Text="重新查詢"  Width="80px" Height="30px" 
                   onclick="Quy_btn_Click" />
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
    <asp:UpdateProgress ID="UpdateProgress5" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel2">
                <ProgressTemplate>
                    <asp:Image ID="Image31" runat="server" ImageUrl="~/images/Loading4.gif"  />
                    <asp:Label ID="L_load21" runat="server" Text="Loading"></asp:Label>
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
                    <HeaderStyle Width="80px" />
                </asp:TemplateField>
                <asp:BoundField DataField="PO_AMT" HeaderText="訂單數" 
                    SortExpression="PO_AMT" DataFormatString="{0:n0}" >
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="WT" HeaderText="未包重" 
                    SortExpression="WT" DataFormatString="{0:n0}" >
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PRICE_AMT" HeaderText="未出金額" 
                    SortExpression="PRICE_AMT" DataFormatString="{0:n0}" >
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

        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            DataSourceID="DS_cust" onrowcommand="GridView2_RowCommand" 
                onselectedindexchanging="GridView2_SelectedIndexChanging" 
                EmptyDataText="NO DATA">
            <Columns>
                <asp:TemplateField HeaderText="客戶" SortExpression="CO">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L_coid" runat="server" Text='<%# Bind("CO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="70px" />
                </asp:TemplateField>
                
                <asp:BoundField DataField="PO_NO_AMT" HeaderText="訂單數" 
                    SortExpression="PO_NO_AMT" DataFormatString="{0:n0}" >
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="WT" HeaderText="未包重" 
                    SortExpression="WT" DataFormatString="{0:n0}" >
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="CO_PRICE_AMT" HeaderText="未包金額" 
                    SortExpression="CO_PRICE_AMT" DataFormatString="{0:n0}" >
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="CURRENCY" HeaderText="幣別" 
                    SortExpression="CURRENCY"   >
                <HeaderStyle Width="70px" />
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
	<h3><a href="#">訂單</a>             
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
 
            </tr>
              <tr>
                  <td>
                      <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                          DataKeyNames="PO_NO" DataSourceID="DS_ordsum" EmptyDataText="NO DATA" 
                          onrowcommand="GridView3_RowCommand" onrowdatabound="GridView3_RowDataBound" 
                          onselectedindexchanging="GridView3_SelectedIndexChanging">
                          <Columns>
                              <asp:BoundField DataField="CO_ID" HeaderText="CO_ID" SortExpression="CO_ID" 
                                  Visible="False" />
                              <asp:TemplateField HeaderText="訂單號碼" SortExpression="PO_NO">
                                  <EditItemTemplate>
                                      <asp:Label ID="Label1" runat="server" Text='<%# Eval("PO_NO") %>'></asp:Label>
                                  </EditItemTemplate>
                                  <ItemTemplate>
                                      <asp:Label ID="L_pono" runat="server" Text='<%# Bind("PO_NO") %>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField DataField="DELIVERY" DataFormatString="{0:yyyy/MM/dd}" HeaderText="交期" 
                                  SortExpression="DELIVERY">
                              <HeaderStyle Width="70px" />
                              <ItemStyle HorizontalAlign="Left" />
                              </asp:BoundField>
                              <asp:BoundField DataField="WT" DataFormatString="{0:n0}" HeaderText="未包重" 
                                  SortExpression="WT">
                              <HeaderStyle Width="70px" />
                              <ItemStyle HorizontalAlign="Right" />
                              </asp:BoundField>
                              <asp:BoundField DataField="PO_NO_AMT" DataFormatString="{0:n0}" HeaderText="規格數" 
                                  SortExpression="PO_NO_AMT">
                              <HeaderStyle Width="70px" />
                              <ItemStyle HorizontalAlign="Right" />
                              </asp:BoundField>
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
                      <asp:SqlDataSource ID="DS_ordsum" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                          ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand="">
                      </asp:SqlDataSource>
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
	<h3><a href="#">訂單內容</a>   
    <asp:UpdateProgress ID="UpdateProgress4" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel5">
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
                <asp:TemplateField HeaderText="訂單" SortExpression="PO_NO">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("PO_NO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L_pono" runat="server" Text='<%# Bind("PO_NO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="90px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="產品種類" SortExpression="CAT">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CAT") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L_cat" runat="server" Text='<%# Bind("CAT") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="直徑" SortExpression="DIA">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("DIA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L_dia" runat="server" Text='<%# Bind("DIA") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="70px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="長度" SortExpression="LEN">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("LEN") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L_len" runat="server" Text='<%# Bind("LEN") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="牙型" SortExpression="TH">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("TH") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L_th" runat="server" Text='<%# Bind("TH") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="鍍別" SortExpression="PL">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("PL") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L_pl" runat="server" Text='<%# Bind("PL") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                </asp:TemplateField>
                <asp:BoundField DataField="KEG" DataFormatString="{0:n0}" HeaderText="訂購" 
                    SortExpression="KEG" >
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="BALANCE" DataFormatString="{0:n0}" HeaderText="未出" 
                    SortExpression="BALANCE">
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PACKKEG" DataFormatString="{0:n0}" HeaderText="已包" 
                    SortExpression="PACKKEG">
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PCS" DataFormatString="{0:n2}" HeaderText="支數" 
                    SortExpression="PCS">
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="WT" DataFormatString="{0:n0}" HeaderText="未包重量" 
                    SortExpression="WT">
                <HeaderStyle Width="60px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="O_PRICE" DataFormatString="{0:n0}" HeaderText="未出金額"
                     SortExpression="O_PRICE">
                <HeaderStyle Width="60px" />
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
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView3" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GridView2" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="Quy_btn" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
	</div>
    <h3><a href="#">批號</a>
    <asp:UpdateProgress ID="UpdateProgress6" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel6">
                <ProgressTemplate>
                    <asp:Image ID="Image51" runat="server" ImageUrl="~/images/Loading4.gif"  />
                    <asp:Label ID="L_load41" runat="server" Text="Loading"></asp:Label>
                </ProgressTemplate>
            </asp:UpdateProgress>
    </h3>
	<div>
		<asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
        <table  width="100%">
          <tr>
            <td>
            <asp:GridView ID="GridView5" runat="server" DataSourceID="DS_lot" 
            AutoGenerateColumns="False"  EmptyDataText="NO DATA">
            <Columns>
                <asp:BoundField DataField="LOT_NO" HeaderText="批號" 
                    SortExpression="LOT_NO" >
                <HeaderStyle Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="FM_WT" HeaderText="成型" 
                    SortExpression="FM_WT" >
                <HeaderStyle Width="80px" />
                </asp:BoundField>
                
                <asp:BoundField DataField="TH_WT" HeaderText="輾牙" 
                    SortExpression="TH_WT" >
                <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="HEAT_WT" HeaderText="熱處理" 
                    SortExpression="HEAT_WT" >
                <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="PLATE_WT" HeaderText="電鍍" 
                    SortExpression="PLATE_WT" >
                <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="CHOW" HeaderText="委外熱處理" 
                    SortExpression="CHOW" >
                <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="CPOW"   HeaderText="委外電鍍" 
                    SortExpression="CPOW" >
                <HeaderStyle Width="80px" />
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
        <asp:SqlDataSource ID="DS_lot" runat="server" 
            ConnectionString="<%$ ConnectionStrings:jh815 %>" 
            ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand="">
        </asp:SqlDataSource>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView5" EventName="RowCommand" />
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

