<%@ Page Title="待電鍍" Language="C#" MasterPageFile="~/JH_MP.master" AutoEventWireup="true" CodeFile="GM_platheatraw.aspx.cs" Inherits="GM_platheatraw" %>
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

            setTimeout($.unblockUI, 6000);
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
            } });
            setTimeout($.unblockUI, 6000);
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table style=" width:100%;">
     <tr>
       <td>
        <table  width="100%">
        <tr>
          <td style=" width:100px ;height:35px; vertical-align:bottom;">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
           <ContentTemplate>
              <table>
                <tr>
                  <td >
                       <asp:Button ID="Quy_btn" runat="server" Text="重新查詢"  Width="80px" Height="30px" 
                           onclick="Quy_btn_Click" Visible="False" />
                      
                  </td>
                </tr>
              </table>
            </ContentTemplate>

        </asp:UpdatePanel>  
          </td>
          <td style=" width:100px;">
          <div id="here" class="here2">
           <asp:Button ID="Report_btn" runat="server" Height="30px"  
                           Text="產生報表" onclick="Report_btn_Click" Width="80px" />
          </div>
          </td>
          <td style=" width:100px;">
          <div id="here2"  class="here2">
              <asp:Button ID="Button1" runat="server" Height="30px" onclick="Button1_Click" 
                  Text="Excel" Width="66px" />
          </div>
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
       </td>
     </tr>
     <tr>
       <td>
           <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
           <ContentTemplate>
           

       <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
        WaitMessageFont-Size="14pt" Height="" Width="" ShowReportBody="False" 
                   ShowToolBar="False">
        <LocalReport ReportPath="report\R_platrawwt.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DS_WIP_PLT" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:jh815 %>" 
        ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" 
        SelectCommand=""></asp:SqlDataSource>

                   </ContentTemplate>
               <Triggers>
                   <asp:PostBackTrigger ControlID="Quy_btn" />
               </Triggers>
           </asp:UpdatePanel>
       </td>
     </tr>
     <tr>
       <td>
           <asp:Label ID="message" runat="server" Text=""></asp:Label>
       </td>
     </tr>
   </table> 
    <img src="images/loading16.gif" alt="" id="displayBox" /> 
</asp:Content>

