<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GM_packmach.aspx.cs" Inherits="GM_packmach" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>包裝機</title>
    <link href="Styles/basic2.css" rel="stylesheet" type="text/css" />
</head>
<body style=" margin:0; padding:0; ">
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
            CellPadding="4" DataSourceID="DS_packmach" Width="330">
            <AlternatingRowStyle BackColor="#D7D7D7" />
            <Columns>
                <asp:BoundField DataField="ABBR" HeaderText="ABBR" SortExpression="ABBR" />
                <asp:BoundField DataField="DESCRPT" HeaderText="DESCRPT" 
                    SortExpression="DESCRPT" />
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="Black" 
                CssClass="GridViewHeader" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
        <asp:SqlDataSource ID="DS_packmach" runat="server" 
            ConnectionString="<%$ ConnectionStrings:jh815 %>" 
            ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand="SELECT  ABBR,DESCRPT
FROM   MACHINE_GROUPS_PACK
ORDER BY ABBR"></asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
