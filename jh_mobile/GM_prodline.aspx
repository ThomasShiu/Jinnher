<%@ Page Title="各產線產能狀況" Language="C#" MasterPageFile="~/JH_MP.master" AutoEventWireup="true" CodeFile="GM_prodline.aspx.cs" Inherits="GM_prodline" %>

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
                    left: '45%',
                    textAlign: 'left',
                    marginLeft: '-68x',
                    marginTop: '-65px',
                    width: '65px',
                    height: '65px'
                }
            });
            setTimeout($.unblockUI, 8000);
        });
    });

    function clicklink(pdate1,machid1)
    {
        window.open('GM_prodlineDetail?pdate=' & pdate & '&machid=' & machid, 'Machine', config = 'height=300,width=700')
    };
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style=" width:100%;">
    <tr>
        <td>
        
            <table>
                <tr>
                    <td style="width: 50%; height: 35px; vertical-align: bottom;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TB_year" runat="server" Font-Size="Large" Width="80px"></asp:TextBox>
                                        </td>
                                        <td >
                                            <asp:DropDownList ID="DDL_month" runat="server" Font-Size="Large">
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
                                        <td style=" width:450px;">
                                        <table>
                                            <tr>
                                                <td>
                                                    <div class="here2">
                                                      <asp:Button ID="Quy_btn" runat="server" Height="30px" onclick="Quy_btn_Click" 
                                                       Text="重新查詢" Width="80px" Visible="False" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="here2">
                                                        <asp:Button ID="B0_btn" runat="server" Text="B0" Width="70px" Height="30px" 
                                                         onclick="B0_btn_Click" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="here2">
                                                     <asp:Button ID="B1_btn" runat="server" Text="B1" Width="70px" Height="30px" 
                                                          onclick="B1_btn_Click"  />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="here2">
                                                    <asp:Button ID="B2_btn" runat="server" Text="B2" Width="70px" Height="30px" 
                                                          onclick="B2_btn_Click"  />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="here2">
                                                     <asp:Button ID="B3_btn" runat="server" Text="B3" Width="70px" Height="30px" 
                                                         onclick="B3_btn_Click"  />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="here2">
                                                    <asp:Button ID="B4_btn" runat="server" Text="B4" Width="70px" Height="30px" 
                                                          onclick="B4_btn_Click"  />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="here2">
                                                    <asp:Button ID="B5_btn" runat="server" Text="B5" Width="70px" Height="30px" 
                                                          onclick="B5_btn_Click"  />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
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
            setTimeout($.unblockUI, 6000);
        });

 }   
</script>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Quy_btn" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="B0_btn" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="B1_btn" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="B2_btn" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="B3_btn" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="B4_btn" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="B5_btn" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td">
                        <div class="here2">
                            <asp:Button ID="Report_btn" runat="server" Height="30px" 
                                onclick="Report_btn_Click" Text="產生報表" Width="80px" Visible="False" />
                        </div>
                    </td>
                    <td>
                        <a href="GM_processanaly_chart.aspx" target="_blank">
                        <asp:Image ID="Image1" runat="server" Height="30px" 
                            ImageUrl="~/images/chart-bar.png" Visible="False" />
                        </a>
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
        
        </td>
    </tr>

    <tr>
        <td>
        
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                Font-Size="8pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
                WaitMessageFont-Size="14pt" Width="100%" ShowFindControls="False" 
                ShowPrintButton="False" ShowRefreshButton="False" Height="420px" 
                HyperlinkTarget="_blank" PageCountMode="Actual">
                <LocalReport ReportPath="" EnableHyperlinks="True">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="DS_prodline" Name="DS_PRODLINE" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:SqlDataSource ID="DS_prodline" runat="server" 
                ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" 
                SelectCommand="">
            </asp:SqlDataSource>
        
        </td>
    </tr>
</table>
<img src="images/loading16.gif" alt="" id="displayBox" /> 
</asp:Content>

