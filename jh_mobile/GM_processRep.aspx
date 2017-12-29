<%@ Page Title="" Language="C#" MasterPageFile="~/JH_MP.master" AutoEventWireup="true" CodeFile="GM_processRep.aspx.cs" Inherits="GM_processRep" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
      <tr>
        <td>
            <asp:Button ID="Query_btn" runat="server" Text="查詢" Height="30px" 
                onclick="Query_btn_Click" Width="80px" />
            <asp:Button ID="Export_btn" runat="server" Text="產生報表" Height="30px" 
                 Width="80px" onclick="Export_btn_Click" />
        </td>
      </tr>

      <tr>
        <td>
         <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
        WaitMessageFont-Size="14pt" Width="" Height="">
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
   
</asp:Content>

