<%@ Page Title="BBI READY TO SHIP" Language="C#" MasterPageFile="~/JH_MP.master" AutoEventWireup="true" CodeFile="GM_bbitoship2.aspx.cs" Inherits="GM_bbitoship2" %>

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
                    left: '40%',
                    textAlign: 'left',
                    marginLeft: '-65x',
                    marginTop: '-65px',
                    width: '65px',
                    height: '65px'
                }
            });
            setTimeout($.unblockUI, 35000);
        });
    });

    function clicklink(pdate1, machid1) {
        window.open('GM_prodlineDetail?pdate=' & pdate & '&machid=' & machid, 'Machine', config = 'height=300,width=700')
    };
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style=" width:100%;">
    <tr>
        <td>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <table>
                <tr>
                    <td>
                    <div class="here2">
                        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                            Text="查詢BBI轉單後統計" Height="30px" />
                    </div>
                    </td>
                    <td>
                    <div class="here2">
                         <asp:Button ID="Button2" runat="server" Text="查詢BBI出貨明細" 
                             onclick="Button2_Click" Height="30px" Visible="False" />
                     </div>
                    </td>
                    <td>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                                AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <asp:Image ID="Image2" runat="server" Height="35px" 
                                    ImageUrl="~/images/Loading3.gif" />
                                <asp:Label ID="L_load" runat="server" Text="Loading"></asp:Label>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
            </table>
                    
                    
                <script type="text/javascript">
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequest);
                    function EndRequest(sender, args) {
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
                            setTimeout($.unblockUI, 35000);
                        });

                    }   
</script>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            
        
        </td>
    </tr>
    <tr>
        <td>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                 <asp:Label ID="message" runat="server" ForeColor="Red"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            
        </td>
    </tr>
    <tr>
        <td>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                Font-Size="8pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
                WaitMessageFont-Size="14pt" Width="100%" ShowBackButton="False" 
                ShowFindControls="False" ShowPrintButton="False" ShowRefreshButton="False" 
                ShowZoomControl="False">
                <LocalReport ReportPath="report/R_BBI_TO_SHIP2.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="DS_bbitoship2" Name="DS_BBI_TO_SHIP2" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:SqlDataSource ID="DS_bbitoship2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
        </td>
    </tr>
</table>
    <img src="images/loading16.gif" alt="" id="displayBox" /> 
</asp:Content>

