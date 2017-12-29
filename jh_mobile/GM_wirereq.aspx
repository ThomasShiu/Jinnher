<%@ Page Title="盤元需求檢核表" Language="C#" MasterPageFile="~/JH_MP.master" AutoEventWireup="true" CodeFile="GM_wirereq.aspx.cs" Inherits="GM_wirereq" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
#displayBox{ display:none;} 
</style>
    <script src="js/jquery.blockUI.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#pageDemo2').click(function() { 
            $.blockUI({ message: '<h1><img src="loading.gif" /> Just a moment...</h1>' }); 
            setTimeout($.unblockUI, 15000);
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
            setTimeout($.unblockUI, 15000);
        });
    });


</script>

<style type="text/css">
html, body { height: 100%; }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr>
          <td>
          <table>
            <tr>
              <td>
                  <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                  <ContentTemplate>--%>
                  <br />
                  <div id="here2" class="here">
                       <asp:Button ID="Button1" runat="server" Height="30px" onclick="Button1_Click" 
                    Text="匯出EXCEL" Width="85px" Visible="False" />
                    </div>
                   <div id="here" class="here2">
                    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="產生報表" 
                      Width="87px" Height="30px" />
                    </div>  

                     <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:jh815 %>" 
            ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
                  <%--</ContentTemplate>
                  </asp:UpdatePanel>--%>
               
              </td>

              <td>
                  <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server"       AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/Loading4.gif"  />
                    <asp:Label ID="L_load1" runat="server" Text="匯出報表中，請稍候..."></asp:Label>
                </ProgressTemplate>
            </asp:UpdateProgress> --%>
              </td>
            </tr>
          </table>
          
          </td>
        </tr>
        <tr>
          <td>
          
          </td>
        </tr>
    </table>
         <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" Height="" InteractiveDeviceInfos="(集合)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="" 
            ProcessingMode="Remote" ShowToolBar="False">
            <LocalReport ReportPath="report\R_wirereq.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="WIREREQ" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
       
<img src="images/loading16.gif" alt="" id="displayBox" /> 
</asp:Content>

