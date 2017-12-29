<%@ Page Title="製程生產分析表" Language="C#" MasterPageFile="~/JH_MP.master" AutoEventWireup="true" CodeFile="GM_processanaly.aspx.cs" Inherits="GM_processanaly" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
#displayBox{ display:none;} 

</style>
<script src="js/jquery.blockUI.js" type="text/javascript"></script>
<script type="text/javascript">
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
                    left: '50%',
                    textAlign: 'left',
                    marginLeft: '-65x',
                    marginTop: '-65px',
                    width: '65px',
                    height: '65px'
                }
            });
            setTimeout($.unblockUI, 8000);
        });
    });
</script>
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
        $('#multiOpenAccordion').multiAccordion({ active: [0, 1, 2, 3, 4] });
    });

    
</script>

 <table  width="100%">
        <tr>
          <td style=" width:250px ;height:35px; vertical-align:bottom;">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
           <ContentTemplate>
              <table>
                <tr>
                  <td class="style1">
                    <asp:TextBox ID="TB_year" runat="server" Width="80px" Font-Size="Large"></asp:TextBox>
                  </td>
                  <td class="style1">
                   <asp:DropDownList ID="DDL_month" runat="server"   Font-Size="Large">
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
                  <td class="style1">
                       <asp:Button ID="Quy_btn" runat="server" Text="重新查詢"  Width="80px" Height="30px" 
                           onclick="Quy_btn_Click" />
                      
                  </td>
                </tr>
              </table>
               

              
           
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Quy_btn" EventName="Click" />
               
            </Triggers>
        </asp:UpdatePanel>  
          </td>
          <td style=" width:100px;">
          <div class="here2">
           <asp:Button ID="Report_btn" runat="server" Height="30px"  
                           Text="產生報表" onclick="Report_btn_Click" Width="80px" />
          </div>
          </td>
          <td>
               <a href="GM_processanaly_chart.aspx" target="_blank">
              <asp:Image ID="Image1" runat="server" Height="30px" 
                  ImageUrl="~/images/chart-bar.png"/>
               </a>
          
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
	<h3><a href="#">製程</a>                            
    <asp:UpdateProgress ID="UpdateProgress5" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel2">
                <ProgressTemplate>
                    <asp:Image ID="Image31" runat="server" ImageUrl="~/images/Loading4.gif"  />
                    <asp:Label ID="L_load21" runat="server" Text="Loading"></asp:Label>
                </ProgressTemplate>
            </asp:UpdateProgress>
    </h3>
	<div>

		<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
        <table>
          <tr>
            <td style=" vertical-align:top;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataSourceID="DS_process" onrowcommand="GridView1_RowCommand" 
                    onselectedindexchanging="GridView1_SelectedIndexChanging" 
                    EmptyDataText="NO DATA">
                <Columns>
                    <asp:TemplateField HeaderText="製程" SortExpression="PROCESS_NAME">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("PROCESS_NAME") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="L_processname" runat="server" Text='<%# Bind("PROCESS_NAME") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="80px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ROWSUM" HeaderText="重量" 
                        SortExpression="ROWSUM" DataFormatString="{0:n0}" >
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="PROCESS" SortExpression="PROCESS" 
                        Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("PROCESS") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="L_process" runat="server" Text='<%# Bind("PROCESS", "{0:n0}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="70px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                        
                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" 
                                ImageUrl="~/images/magnifier.png"  Width="32px"/>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="GridViewHeader" />
                <SelectedRowStyle BackColor="#86C2FF" BorderStyle="Double" />
            </asp:GridView>
            <asp:SqlDataSource ID="DS_process" runat="server" 
                ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
            </td>
            <td  style=" vertical-align:top;">
                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="DS_packmach">
                    <AlternatingRowStyle BackColor="#CCFFCC" />
                    <Columns>
                        <asp:BoundField DataField="ABBR" HeaderText="代號" SortExpression="ABBR" />
                        <asp:BoundField DataField="DESCRPT" HeaderText="包裝機" SortExpression="DESCRPT" />
                    </Columns>
                    <HeaderStyle CssClass="GridViewHeader2" />
                </asp:GridView>
            
                <asp:SqlDataSource ID="DS_packmach" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                    ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
            
            </td>
          </tr>
        </table>
        
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Quy_btn" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>  

	</div>
	<h3><a href="#">機台產能</a>        
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

        <asp:GridView ID="GridView2" runat="server" onrowcommand="GridView2_RowCommand" 
                onselectedindexchanging="GridView2_SelectedIndexChanging" 
                EmptyDataText="NO DATA" DataSourceID="DS_machine"  Width="100%">
             <AlternatingRowStyle BackColor="#D7D7D7" />
             <HeaderStyle CssClass="GridViewHeader" />
             <RowStyle HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#86C2FF" BorderStyle="Double" />
        </asp:GridView>
        <asp:SqlDataSource ID="DS_machine" runat="server" 
            ConnectionString="<%$ ConnectionStrings:jh815 %>" 
            ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
         </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="Quy_btn" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

	</div>
	
	 <table>
    
      <tr>
        <td>
            <asp:Label ID="message" runat="server"  ></asp:Label>
         <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
        WaitMessageFont-Size="14pt" Width="" Height="" ShowToolBar="False" Visible="False">
        <LocalReport ReportPath="report\R_process.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DS_process" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:jh815 %>" 
        ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" 
        SelectCommand="">
    </asp:SqlDataSource>
        </td>
      </tr>
    </table>
</div>
<img src="images/loading16.gif" alt="" id="displayBox" /> 
</asp:Content>

