﻿<%@ Page Title="內銷成品庫存" Language="C#" MasterPageFile="~/JH_MP.master" AutoEventWireup="true" CodeFile="GM_exportstock.aspx.cs" Inherits="GM_exportstock" %>

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
          <td style=" width:400px ;height:35px">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
           <ContentTemplate>
              
               <asp:TextBox ID="TB_year" runat="server" Height="25px" Width="100px" 
                   Font-Size="Large"></asp:TextBox>
              
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
                <asp:TemplateField HeaderText="顧客" SortExpression="CO_CO_ID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CO_CO_ID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L_coid" runat="server" Text='<%# Bind("CO_CO_ID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="70px" />
                </asp:TemplateField>
                <asp:BoundField DataField="PLTCNT" HeaderText="棧板數" 
                    SortExpression="PLTCNT" DataFormatString="{0:n0}" >
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="WTTON" HeaderText="總噸" 
                    SortExpression="WTTON" DataFormatString="{0:n0}" >
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="EMP_NAME" HeaderText="業務代表" 
                    SortExpression="EMP_NAME"   >
                <HeaderStyle Width="70px" />
 
                </asp:BoundField>
                <asp:TemplateField>
                <ItemTemplate>
                        
                        <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Select" 
                            ImageUrl="~/images/magnifier.png"  />
                        
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
                <asp:AsyncPostBackTrigger ControlID="Quy_btn" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

	</div>
	<h3><a href="#">棧板總重</a>            
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
                <asp:TemplateField HeaderText="顧客" SortExpression="COCO_CO_ID_ID" 
                    Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CO_CO_ID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L_coid2" runat="server" Text='<%# Bind("CO_CO_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="年份" SortExpression="DELIYEAR">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("DELIYEAR") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="L_year" runat="server" Text='<%# Bind("DELIYEAR") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PLTCNT" HeaderText="棧板數" 
                    SortExpression="PLTCNT" DataFormatString="{0:n0}" >
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="WTTON" HeaderText="總重量" SortExpression="WTTON" 
                    DataFormatString="{0:n2}" >
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
               
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
                <asp:AsyncPostBackTrigger ControlID="GridView2" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="Quy_btn" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel> 

	</div>
	<h3><a href="#">訂單明細</a>
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
                <asp:BoundField DataField="PO_NO" HeaderText="訂單" 
                    SortExpression="PO_NO" >
                <HeaderStyle Width="130px" />
                </asp:BoundField>
                <asp:BoundField DataField="DELIVERY" DataFormatString="{0:yyyy/MM/dd}"  HeaderText="交期" 
                    SortExpression="DELIVERY" >
                <HeaderStyle Width="70px" />
                </asp:BoundField>
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
                <asp:BoundField DataField="DIA" HeaderText="直徑" 
                    SortExpression="DIA" >
                <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="LEN" HeaderText="長度" 
                    SortExpression="LEN" >
                <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="TH" HeaderText="牙型" 
                    SortExpression="TH" >
                <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="PL" HeaderText="鍍別" 
                    SortExpression="PL" >
                <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="PACKKEG" DataFormatString="{0:n0}" HeaderText="箱數" 
                    SortExpression="PACKKEG" >
                <HeaderStyle Width="50px" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PCSQTY" DataFormatString="{0:n3}" HeaderText="訂單(千支)" 
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
                <asp:BoundField DataField="CO_CO_ID" HeaderText="廠商代碼" 
                    SortExpression="CO_CO_ID" Visible="False">
                <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="WEIGHTPERK" DataFormatString="{0:n}" 
                    HeaderText="WEIGHTPERK" SortExpression="WEIGHTPERK" Visible="False">
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
                <asp:AsyncPostBackTrigger ControlID="Quy_btn" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
	</div>
</div>
</asp:Content>

