<%@ Page Title="機台狀況" Language="C#" MasterPageFile="~/JH_MP.master" AutoEventWireup="true" CodeFile="GM_prodlineDetail.aspx.cs" Inherits="GM_prodlineDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Styles/superTables.css" rel="stylesheet" type="text/css" />
    <script  type="text/javascript"  src="./js/jquery-1.4.2.js"></script>  
    <script type="text/javascript"  src="./js/superTables.js"></script>
    <script type="text/javascript" src="./js/jquery.superTable.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style=" width:100%;">
<tr>
        <td>
            <asp:Label ID="L_pdate" runat="server" Text=""></asp:Label>
            <asp:Label ID="Label1" runat="server" Text="-"></asp:Label>
            <asp:Label ID="L_machineid" runat="server" Text=""></asp:Label>
            <asp:Label ID="message" runat="server" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="生產紀錄"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="3" DataSourceID="SqlDataSource2" 
                onrowdatabound="GridView3_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="LOT_NO" HeaderText="批號" SortExpression="LOT_NO" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TOTALQTY" HeaderText="千支數" SortExpression="TOTALQTY">
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TOTAL_WT" HeaderText="公噸" SortExpression="TOTAL_WT">
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CAT" HeaderText="種類" SortExpression="CAT" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PDT_SIZE" HeaderText="規格" 
                        SortExpression="PDT_SIZE" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="STARTDATE" DataFormatString="{0:yyyy/MM/dd}" 
                        HeaderText="開工日" SortExpression="STARTDATE" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ENDDATE" DataFormatString="{0:yyyy/MM/dd}" 
                        HeaderText="完工日" SortExpression="ENDDATE" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MINDELIVERY" DataFormatString="{0:yyyy/MM/dd}" 
                        HeaderText="交貨日" SortExpression="MINDELIVERY" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DRA_DRAW_ID" HeaderText="圖號" 
                        SortExpression="DRA_DRAW_ID" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" 
                SelectCommand="SELECT '' AS LOT_NO,0 AS TOTALQTY,0 AS TOTAL_WT,'' AS CAT,'' AS PDT_SIZE,SYSDATE AS STARTDATE,SYSDATE AS ENDDATE,SYSDATE AS MINDELIVERY,'' AS DRA_DRAW_ID FROM DUAL">
            </asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td>
        <asp:Label ID="Label3" runat="server" Text="未完工"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="3" DataSourceID="SqlDataSource3" 
                onrowdatabound="GridView2_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="LOT_NO" HeaderText="批號" SortExpression="LOT_NO" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="C_TOTALQTY" HeaderText="支數" 
                        SortExpression="C_TOTALQTY" >
                    <HeaderStyle ForeColor="Black" />
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CAT" HeaderText="種類" SortExpression="CAT" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PDT_SIZE" HeaderText="規格" 
                        SortExpression="PDT_SIZE" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DRA_DRAW_ID" HeaderText="圖號" 
                        SortExpression="DRA_DRAW_ID" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="WIRE_DIA" HeaderText="線徑" 
                        SortExpression="WIRE_DIA" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RAWMTRL_RAWMTRL_ID" HeaderText="材質" 
                        SortExpression="RAWMTRL_RAWMTRL_ID" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SPEED" HeaderText="速度" SortExpression="SPEED" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="WDAY" HeaderText="天" SortExpression="WDAY" 
                        DataFormatString="{0:n1}" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MINDELIVERY" DataFormatString="{0:yyyy/MM/dd}" 
                        HeaderText="交貨日" SortExpression="MINDELIVERY" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRIORITY" HeaderText="序" SortExpression="PRIORITY" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" 
                SelectCommand="SELECT '' AS LOT_NO,0 AS C_TOTALQTY,'' AS CAT,'' AS PDT_SIZE,'' AS DRA_DRAW_ID,'' AS WIRE_DIA,'' AS RAWMTRL_RAWMTRL_ID,0 AS SPEED,0 AS WDAY,SYSDATE AS MINDELIVERY,0 AS PRIORITY FROM DUAL">
            </asp:SqlDataSource>
        </td>
    </tr>
     <tr>
        <td>
            <asp:Label ID="Label4" runat="server" Text="機台狀態"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
        
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#DEDFDE" 
                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
                GridLines="Vertical" EmptyDataText="查無資料" AllowSorting="True" 
                onsorted="GridView1_Sorted" onrowdatabound="GridView1_RowDataBound">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="MACHINEID" HeaderText="機台" 
                        SortExpression="MACHINEID" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LOT_NO" HeaderText="批號" 
                        SortExpression="LOT_NO" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRODDATE" HeaderText="日期" 
                        SortExpression="PRODDATE" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRODTIME_B" HeaderText="時間" 
                        SortExpression="PRODTIME_B" >
                    <HeaderStyle Font-Underline="True" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CAT" HeaderText="種類" 
                        SortExpression="CAT" >
                    <HeaderStyle Font-Underline="True" ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="P_SIZE" HeaderText="規格" 
                        SortExpression="P_SIZE" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DOWN_CAUSE" HeaderText="停車原因" 
                        SortExpression="DOWN_CAUSE" >
                    <HeaderStyle ForeColor="Black" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>"
                 SelectCommand="">
                </asp:SqlDataSource>
        
        </td>
    </tr>
    
</table>
</asp:Content>

