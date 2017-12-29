<%@ Page Title="內銷成品庫存BY種類" Language="C#" MasterPageFile="~/JH_MP.master" AutoEventWireup="true" CodeFile="GM_demostock12.aspx.cs" Inherits="GM_demostock12" %>

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
          $('#multiOpenAccordion').multiAccordion({ active: [0, 1, 2] });
      });

    
</script>

 <table  width="100%">
        <tr>
          <td style=" width:400px ;height:35px">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
           <ContentTemplate>
              
              <%-- <asp:TextBox ID="TB_year" runat="server" Height="25px" Width="100px" 
                   Font-Size="Large"></asp:TextBox>--%>
              
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
	
	<h3><a href="#">產品總類</a>            
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

            <asp:ListView ID="ListView1" runat="server" DataSourceID="DS_cust" 
                onitemcommand="ListView1_ItemCommand" 
                onitemdatabound="ListView1_ItemDataBound" 
                onselectedindexchanged="ListView1_SelectedIndexChanged" 
                onselectedindexchanging="ListView1_SelectedIndexChanging">
            <LayoutTemplate>
                    <table >
                    <thead class="GridViewHeader">
                        <tr>
                            <td style=" width:70px;text-align:center;border-bottom-style: ridge; border-bottom-width: 0.1em; border-bottom-color: #000000;">
                                <span>產品種類</span>
                            </td>
                            <td style=" width:70px;text-align:center;border-bottom-style: ridge; border-bottom-width: 0.1em; border-bottom-color: #000000;">
                                <span>棧板數</span>
                            </td>
                            <td style=" width:70px;text-align:center;border-bottom-style: ridge; border-bottom-width: 0.1em; border-bottom-color: #000000;">
                                <span>總噸</span>
                            </td>
                            <td style=" width:100px;text-align:center;border-bottom-style: ridge; border-bottom-width: 0.1em; border-bottom-color: #000000;">
                                <span>執行</span>
                            </td>
                        </tr>
                        </thead>
                        <tbody >
                        
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                        
                      </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                        <tr>
                            <td class=" GridViewBody2" style=" width:70px;text-align:left;border-bottom-style: ridge; border-bottom-width: 0.1em; border-bottom-color: #cccccc;">
                                <asp:Label ID="L_cat" runat="server" Text='<%# Eval("CAT") %>'></asp:Label>
                            </td>
                            <td class=" GridViewBody2" style=" width:70px;text-align:right;border-bottom-style: ridge; border-bottom-width: 0.1em; border-bottom-color: #cccccc;">
                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("TTLPLT", "{0:n0}") %>'></asp:Label>
                            </td>
                            <td class=" GridViewBody2" style=" width:70px;text-align:right;border-bottom-style: ridge; border-bottom-width: 0.1em; border-bottom-color: #cccccc;">
                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("TTLWT", "{0:n0}") %>' ></asp:Label>
                            </td>
                            <td class=" GridViewBody2" style=" width:100px;text-align:center;border-bottom-style: ridge; border-bottom-width: 0.1em; border-bottom-color: #cccccc;">
                                 <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Select" 
                            ImageUrl="~/images/toggle-expand-alt.png"  Width="35px"/>
                            </td>
                        </tr>
              
                </ItemTemplate>
                <SelectedItemTemplate >
                        <tr >
                            <td class=" GridViewBody" style=" width:70px;text-align:left;">
                                <asp:Label ID="L_cat" runat="server" Text='<%# Eval("CAT") %>'></asp:Label>
                            </td>
                            <td class=" GridViewBody" style=" width:70px;text-align:right;">
                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("TTLPLT", "{0:n0}") %>'></asp:Label>
                            </td>
                            <td class=" GridViewBody" style=" width:70px;text-align:right;">
                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("TTLWT", "{0:n0}") %>'></asp:Label>
                            </td>
                            <td class=" GridViewBody" style=" width:100px;text-align:center;">
                                 <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Disable" 
                            ImageUrl="~/images/toggle-collapse-alt.png"  Width="30px"/>
                            </td>
                        </tr>

                        <tr>
                          <td colspan="3" style=" background:white;">
                              <a href='GM_demostock121.aspx?id=<%#Eval("CAT") %>' target="blank">
                              <asp:Image ID="Image61" runat="server"  ImageUrl='<%#Eval("PATH")%>' Width="200px" />
                              </a>
                          </td>
                          <td>
                             <a href='GM_demostock121.aspx?id=<%#Eval("CAT") %>' target="blank">
                              <asp:Image ID="Image1" runat="server"  ImageUrl='images/zoom-in.png'/>
                              </a>
                          </td>
                        </tr>
                        <tr>
                            <td colspan="4" style=" background:white;">
                                <asp:Label ID="L_desc_c" runat="server" Text='<%# Eval("DESCRPT_C") %>' Width="300px"></asp:Label>
                            </td>
                        </tr>
    
                </SelectedItemTemplate>
            </asp:ListView>
        <asp:SqlDataSource ID="DS_cust" runat="server" 
            ConnectionString="<%$ ConnectionStrings:jh815 %>" 
            ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
         
         </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Quy_btn" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

	</div>
<h3><a href="#">產品規格明細</a>               
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
            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                DataSourceID="DS_plt" 
                onrowcommand="GridView3_RowCommand" 
                onselectedindexchanging="GridView3_SelectedIndexChanging" 
                onrowdatabound="GridView3_RowDataBound" EmptyDataText="NO DATA">
            <Columns>
                 <asp:TemplateField HeaderText="種類" SortExpression="CAT">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CAT") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <a href='<%#"GM_showdescript.aspx?sn="+ Eval("CAT") %>' rel="shadowbox;height=200;width=300" title="規格描述" >
                          <asp:Label ID="L_cat" runat="server" Text='<%# Bind("CAT") %>'></asp:Label>
                        </a>
                    </ItemTemplate>
                    <HeaderStyle Width="70px" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="直徑" SortExpression="DIA">
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("DIA") %>'></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="L_dia" runat="server" Text='<%# Bind("DIA") %>'></asp:Label>
                     </ItemTemplate>
                     <HeaderStyle Width="50px" />
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="長度" SortExpression="LEN">
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("LEN") %>'></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="L_len" runat="server" Text='<%# Bind("LEN") %>'></asp:Label>
                     </ItemTemplate>
                     <HeaderStyle Width="50px" />
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="牙型" SortExpression="TH">
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("TH") %>'></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="L_th" runat="server" Text='<%# Bind("TH") %>'></asp:Label>
                     </ItemTemplate>
                     <HeaderStyle Width="50px" />
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="鍍別" SortExpression="PL">
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("PL") %>'></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="L_pl" runat="server" Text='<%# Bind("PL") %>'></asp:Label>
                     </ItemTemplate>
                     <HeaderStyle Width="50px" />
                 </asp:TemplateField>
                <asp:BoundField DataField="TTLPLT" DataFormatString="{0:n0}" HeaderText="棧板數" 
                    SortExpression="TTLPLT" >
                <HeaderStyle Width="50px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="TTLWT" DataFormatString="{0:n0}" HeaderText="總重(噸)" 
                    SortExpression="TTLWT">
                <HeaderStyle Width="80px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                 <asp:TemplateField HeaderText="入庫日" SortExpression="DELIYEAR">
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("DELIYEAR") %>'></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="L_deliyear" runat="server" 
                             Text='<%# Bind("DELIYEAR", "{0:yyyy/MM/dd}") %>'></asp:Label>
                     </ItemTemplate>
                     <HeaderStyle Width="80px" />
                 </asp:TemplateField>
                <asp:TemplateField>
                <ItemTemplate>
                        <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Select" 
                            ImageUrl="~/images/magnifier.png" />
                    </ItemTemplate>
                    <HeaderStyle   Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
             <HeaderStyle CssClass="GridViewHeader" />
            <SelectedRowStyle BackColor="#86C2FF" BorderStyle="Double" />
        </asp:GridView>
                 

        <asp:SqlDataSource ID="DS_plt" runat="server" 
            ConnectionString="<%$ ConnectionStrings:jh815 %>" 
            ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
              </td>
            </tr>
        </table>

        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ListView1" EventName="ItemCommand" />
                <asp:AsyncPostBackTrigger ControlID="Quy_btn" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel> 

	</div>
	<h3><a href="#">規格庫存</a>
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
                <asp:BoundField DataField="STOCK_DATE" HeaderText="入庫日" DataFormatString="{0:yyyy/MM/dd}"
                    SortExpression="STOCK_DATE" >
                <HeaderStyle Width="80px" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="種類" SortExpression="CAT">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CAT") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L_cat" runat="server" Text='<%# Bind("CAT") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                </asp:TemplateField>
                <asp:BoundField DataField="DIA" HeaderText="直徑" 
                    SortExpression="DIA" >
                <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="LEN" HeaderText="長度" 
                    SortExpression="LEN" >
                <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="TTLWT" DataFormatString="{0:n3}" HeaderText="噸" 
                    SortExpression="TTLWT" >
                <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="PACKKEG" DataFormatString="{0:n0}" HeaderText="箱數" 
                    SortExpression="PACKKEG" >
                <HeaderStyle Width="50px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PCSQTY" DataFormatString="{0:n3}" HeaderText="總支數" 
                    SortExpression="PCSQTY">
                <HeaderStyle Width="80px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="STOCK_ID"   HeaderText="棧板ID" 
                    SortExpression="STOCK_ID">
                <HeaderStyle Width="90px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="LOCATION"  HeaderText="儲位" 
                    SortExpression="LOCATION">
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="WEIGHTPERK" DataFormatString="{0:n}" 
                    HeaderText="千支重" SortExpression="WEIGHTPERK" >
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
<script type="text/javascript">
    Shadowbox.init({
        handleOversize: "drag",
        handleUnsupported: "remove",
        autoplayMovies: false
    });

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequest);
    function EndRequest(sender, args) {
        Shadowbox.setup();


    }   
</script>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView4" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GridView3" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="ListView1" EventName="ItemCommand" />
                <asp:AsyncPostBackTrigger ControlID="Quy_btn" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
	</div>
</div>
</asp:Content>

