<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GM_mtacc.aspx.cs" Inherits="GM_mtacc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>物料領用統計</title>
<link href="js/start/jquery-ui-1.8.13.custom.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="js/jquery.blockUI.js" type="text/javascript"></script>
		<script type="text/javascript" src="js/jquery-ui-1.8.13.custom.min.js"></script>
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
    <img src="images/loading16.gif" alt="" id="displayBox" />

        <table  style=" width:100%;">
            <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                        <table >
                            <tr>
                            <td >
                        <asp:TextBox ID="TB_year" runat="server" Width="80px" Font-Size="Large"></asp:TextBox>
                      </td>
                      <td >
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
                            <td>
                               
                                <div id="here">
                                        <asp:Button ID="btn_query" runat="server" Text="查詢物料領用資料" Height="30px" 
                                            onclick="Button1_Click" />
                                 </div>
                              
                            </td>
                            <td>
                              <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                            AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <asp:Image ID="Image2" runat="server" Height="35px" 
                                    ImageUrl="~/images/Loading3.gif" />
                                <asp:Label ID="L_load" runat="server" Text="Loading" Font-Bold="True" 
                                    Font-Size="Medium"></asp:Label>
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
                                <asp:AsyncPostBackTrigger ControlID="btn_query" EventName="Click" />
                            </Triggers>
                    </asp:UpdatePanel> 
                </td>
            </tr>
            <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <asp:Label ID="message" runat="server" Font-Bold="True" ForeColor="Red" 
                        Font-Size="Medium"></asp:Label>
                </ContentTemplate>
                 <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_query" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
                
            </td>
            </tr>
            <tr>
                <td>
                
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" 
                        ShowFindControls="False" ShowPrintButton="False" ShowRefreshButton="False" 
                        ShowZoomControl="False" Font-Names="Verdana" Font-Size="8pt" 
                        InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
                        WaitMessageFont-Size="14pt">
                        <LocalReport ReportPath="report\R_mtacc.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DS_mtacc1" />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
                
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                        ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
                
                </td>
            </tr>
        </table>
         
    </div>
    
    </form>
    
</body>
</html>
