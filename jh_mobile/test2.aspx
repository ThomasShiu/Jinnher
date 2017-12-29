<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test2.aspx.cs" Inherits="test2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="COL_1" HeaderText="COL_1" SortExpression="COL_1" />
                <asp:BoundField DataField="COL_2" HeaderText="COL_2" SortExpression="COL_2" />
                <asp:BoundField DataField="COL_3" HeaderText="COL_3" SortExpression="COL_3" />
                <asp:BoundField DataField="COL_4" HeaderText="COL_4" SortExpression="COL_4" />
                <asp:BoundField DataField="COL_5" HeaderText="COL_5" SortExpression="COL_5" />
                <asp:BoundField DataField="COL_6" HeaderText="COL_6" SortExpression="COL_6" />
                <asp:BoundField DataField="COL_7" HeaderText="COL_7" SortExpression="COL_7" />
                <asp:BoundField DataField="COL_8" HeaderText="COL_8" SortExpression="COL_8" />
                <asp:BoundField DataField="COL_9" HeaderText="COL_9" SortExpression="COL_9" />
                <asp:BoundField DataField="CAT" HeaderText="CAT" SortExpression="CAT" />
                <asp:BoundField DataField="DIA" HeaderText="DIA" SortExpression="DIA" />
                <asp:BoundField DataField="LEN" HeaderText="LEN" SortExpression="LEN" />
                <asp:BoundField DataField="TH" HeaderText="TH" SortExpression="TH" />
                <asp:BoundField DataField="PL" HeaderText="PL" SortExpression="PL" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:jh815 %>" 
            ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" 
            SelectCommand="SELECT * FROM &quot;ODS_JH_TEMP&quot;"></asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
