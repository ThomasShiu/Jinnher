<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GM_bbitoship1.aspx.cs" Inherits="GM_bbitoship1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BBI READY TO SHIP</title>
    <link href="js/start/jquery-ui-1.8.13.custom.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="js/jquery.blockUI.js" type="text/javascript"></script>
		<script type="text/javascript" src="js/jquery-ui-1.8.13.custom.min.js"></script>
        <script type="text/javascript">
            $(function () {
                //$('#tabs').tabs();

                // Tabs
                $('#tabs').tabs().css("width", "100%");

                //hover states on the static widgets
                $('#dialog_link, ul#icons li').hover(
                  function () { $(this).addClass('ui-state-hover'); },
                  function () { $(this).removeClass('ui-state-hover'); });

                $('#tabs').bind("tabsshow", function (event, ui) { GetIndex(); });

                //如果目前的  hTag 有暫存資料，則將 Tab 與暫存資料同步
                SyncTab();

            });

            function GetIndex() {// 將目前的 Tab Index 存入 hTag 暫存
                var $tabs = $('#tabs').tabs();
                var $tagIndex = $tabs.tabs('option', 'selected');
                $("input[id*=hTag]").val($tagIndex);
            }



            function SyncTab() {//如果目前的  hTag 有暫存資料，則將 Tab 與暫存資料同步   
                var $tagIndex = $("input[id*=hTag]").val();

                if ($tagIndex != '') {
                    $('#tabs').tabs('select', parseInt($tagIndex));
                }
            }

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
    
    <style type="text/css">

			/*demo page css*/

			body{ font: 62.5% "Trebuchet MS", sans-serif; margin: 10px;}

			.demoHeaders { margin-top: 2em; }

			#dialog_link {padding: .4em 1em .4em 20px;text-decoration: none;position: relative;}

			#dialog_link span.ui-icon {margin: 0 5px 0 0;position: absolute;left: .2em;top: 50%;margin-top: -8px;}

			ul#icons {margin: 0; padding: 0;}

			ul#icons li {margin: 2px; position: relative; padding: 4px 0; cursor: pointer; float: left;  list-style: none;}

			ul#icons span.ui-icon {float: left; margin: 0 4px;}

            
        #displayBox{ display:none;} 
    </style>	
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True" 
        EnableScriptGlobalization="True">
    </asp:ScriptManager>

     <input type="hidden" id="hTag" runat="server" />
        <div id="tabs">
	    <ul>
		    <li><a href="#tabs-1">BBI轉單後統計</a></li>	
            <li><a href="#tabs-2">BBI出貨明細</a></li>

	    </ul>
	    <div id="tabs-1">
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
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                                AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <asp:Image ID="Image2" runat="server" Height="35px" 
                                    ImageUrl="~/images/Loading3.gif" />
                                <asp:Label ID="L_load2" runat="server" Text="Loading"></asp:Label>
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

        </div>
        <div id="tabs-2">

        <table style=" width:100%;">
    <tr>
        <td>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <table>
                <tr>
                    <td>
                    <div class="here2">
                         <asp:Button ID="Button2" runat="server" Text="查詢BBI出貨明細" 
                             onclick="Button2_Click" Height="30px" Visible="True" />
                     </div>
                    </td>
                    <td>
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" 
                                AssociatedUpdatePanelID="UpdatePanel3">
                            <ProgressTemplate>
                                <asp:Image ID="Image3" runat="server" Height="35px" 
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

                    }   
</script>
            </ContentTemplate>
                <Triggers>
              
                    <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            
        
        </td>
    </tr>
    <tr>
        <td>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                 <asp:Label ID="message2" runat="server" ForeColor="Red"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            
        </td>
    </tr>
    <tr>
        <td>
            <rsweb:ReportViewer ID="ReportViewer2" runat="server" Font-Names="Verdana" 
                Font-Size="8pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
                WaitMessageFont-Size="14pt" Width="100%" ShowBackButton="False" 
                ShowFindControls="False" ShowPrintButton="False" ShowRefreshButton="False" 
                ShowZoomControl="False">
                <LocalReport ReportPath="report/R_BBI_TO_SHIP1.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="DS_bbitoship3" Name="DS_bbitoship1" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:SqlDataSource ID="DS_bbitoship3" runat="server" 
                ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
        </td>
    </tr>
</table>
        </div>
    </div>
    <img src="images/loading16.gif" alt="" id="displayBox" /> 
    </form>
</body>
</html>
