<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PD_Anetwt.aspx.cs" Inherits="PD_Anetwt" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <table style=" width:100%">
        <tr>
            <td>
            
            </td>
        </tr>
        <tr>
            <td>
            
            </td>
        </tr>
        <tr>
            <td>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                    Font-Size="8pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
                    WaitMessageFont-Size="14pt" Width="100%">
                    <LocalReport ReportPath="report\R_Anetwt.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="SDS_anetwt" Name="R_DS_wt" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
                <asp:SqlDataSource ID="SDS_anetwt" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                    ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand="SELECT YM,D,B0_NETWT,B1_NETWT,B2_NETWT,B3_NETWT,B4_NETWT,B5_NETWT 
FROM XLS_F_WT WHERE YM = '2011/10'"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
