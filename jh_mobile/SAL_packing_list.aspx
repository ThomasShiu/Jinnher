<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL_packing_list.aspx.cs" Inherits="SAL_packing_list" %>

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
                
                    <asp:Label ID="Label1" runat="server" Text="Packing No :"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Text="查詢" onclick="Button1_Click" />
                
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="message" runat="server" Text=""></asp:Label>

                </td>
            </tr>
            <tr>
                <td>
                
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                        Font-Size="8pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
                        WaitMessageFont-Size="14pt" Width="100%" Height="600px">
                        <LocalReport ReportPath="report\R_packing_list2.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="SDS_PackingList" Name="DataSet2" />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
                    <asp:SqlDataSource ID="SDS_PackingList" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                        ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
                
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
