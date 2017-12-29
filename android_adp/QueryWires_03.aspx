<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryWires_03.aspx.cs" Inherits="QueryWires_03" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>盤元資料查詢</title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%--<meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no">--%>
    <meta name="viewport" content="width=320, initial-scale=0.33;"/>
    <%--<meta name="viewport" content="initial-scale=0.5, width=device-width, target-densitydpi=device-dpi"/>--%>
    <%--<meta name="viewport" content="initial-scale=0.5, width=device-width, target-densitydpi=device-dpi, minimum-scale=0.1, user-scalable=no" />--%>
    <style type="text/css">
        #t01
        {
            position: absolute;
            left: 180px;
            top: 130px;
            -webkit-transform: rotate(-90deg);
            -moz-transform: rotate(-90deg);
            -o-transform: rotate(-90deg);
            transform: rotate(-90deg);
        }
        @media all and (min-width: 480px) and (max-width: 800px) {
        #container {width: auto; max-width: 800px;}
        #main {width: 70%; float: left;}
        #sidebar {width: 30%; float: left; margin-bottom: 10px;}
        #pub {width: 30%; margin-left: 70%; float: none;} } @media all and (min-width: 400px) and (max-width: 480px) {
        #container {width: auto; max-width: 480px;}
        #main, #sidebar, #pub {width: 100%; float: none;}
        h1 {position: absolute; top: 0; height: 30px; width: 100%;}
        #sidebar {position: absolute; top: 40px; height: 40px;}
        #sidebar p {display: none;}
        #sidebar ul {padding: 0; margin-top: 10px; overflow: hidden;}
        #sidebar ul li {display: inline; font-size: 15px;}
        #main {position: absolute; top: 80px;}
        #pub, #footer {display: none;}
}
    </style>
</head>
<body style="margin:0;">
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
 
             <asp:DataList ID="DataList1" runat="server" DataKeyField="WIRE_ID" 
                    DataSourceID="SDS_wires1" BackColor="White" BorderColor="#CCCCCC" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
                    GridLines="Horizontal"  >
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                    <ItemStyle Font-Size="Small" />
                    <ItemTemplate>
 
                             <table>
                             <tr>
                              <td valign="top">
                                <asp:Label ID="CO_ABBRLabel" runat="server" Text='<%# Eval("CO_ABBR") %>' 
                                        Font-Bold="True" Font-Size="40px" /></td>
                            </tr>
                            <tr>
                                <td  >
                                <asp:Label ID="WIRE_IDSLabel" runat="server" 
                                        Text='<%# Eval("WIRE_IDS") %>' Font-Bold="True" Font-Size="40px"  /></td>
                              
                            </tr>
                             <tr>
                                <td style=" WEIGHT:800px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                 <asp:Label ID="RAWMTRL_IDLabel" runat="server" Text='<%# Eval("RAWMTRL_ID") %>' 
                                        Font-Bold="True" Font-Size="40px"  />
                                </td>
                                </tr>
                            <tr>
                                <td>
                                 <asp:Label ID="HEAT_NOLabel" runat="server" Text='<%# Eval("HEAT_NO") %>' 
                                        Font-Bold="True" Font-Size="40px"   />
                                </td>
                                </tr>
                            
                                <tr>
                                <td>
                                 <asp:Label ID="DIAMETERLabel" runat="server" Text='<%# Eval("DIAMETER") %>' 
                                        Font-Bold="True" Font-Size="40px"   />
                                        <asp:Label ID="Label2" runat="server" Text="mm" Font-Size="38px" />
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                <asp:Label ID="WEIGHTLabel" runat="server" Text='<%# Eval("WEIGHT","{0:n0}") %>' 
                                        Font-Bold="True" Font-Size="40px"   />
                                        <asp:Label ID="Label1" runat="server" Text="kg" Font-Size="38px" />
                                </td>
                            </tr>
                             <tr>
                                <td>
                                <asp:Label ID="HEAT_NO_NEWLabel" runat="server" 
                                        Text='<%# Eval("HEAT_NO_NEW") %>' Font-Bold="True" Font-Size="40px"   />
                                </td>
 
                            </tr>
                            <tr>
                                <td style=" weight:800px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Image ID="Image1" runat="server"  ImageUrl='<%# "~/code128.aspx?wireids="+Eval("WIRE_IDS") %>'   />
                                </td>
                            </tr>
 
                    </table>
                            <div id="t01">
                                    <asp:Image ID="Image2" runat="server"  ImageUrl='<%# "~/code128.aspx?wireids="+Eval("WIRE_IDS") %>' />
                                    </div>
 
                       
                    </ItemTemplate>
                    <SelectedItemStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                </asp:DataList>
                <asp:SqlDataSource ID="SDS_wires1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                    ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
 
           

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="4" DataSourceID="SDS_wires2" ForeColor="Black" 
                    GridLines="Horizontal" Width="266px" Visible="False">
                    <Columns>
                        <asp:BoundField DataField="WIRE_IDS" HeaderText="卷號" 
                            SortExpression="WIRE_IDS" Visible="False" />
                        <asp:BoundField DataField="PDATE" HeaderText="作業日" SortExpression="PDATE" />
                        <asp:BoundField DataField="PROCESS" HeaderText="製程" SortExpression="PROCESS" />
                        <asp:BoundField DataField="RAWMTRL_ID" HeaderText="材質" 
                            SortExpression="RAWMTRL_ID" Visible="False" />
                        <asp:BoundField DataField="DIAMETER" HeaderText="線徑" 
                            SortExpression="DIAMETER" Visible="False" />
                        <asp:BoundField DataField="HEAT_NO" HeaderText="爐號" SortExpression="HEAT_NO" 
                            Visible="False" />
                        <asp:BoundField DataField="CO_ID" SortExpression="CO_ID" Visible="False" />
                        <asp:BoundField DataField="CO_ABBR" HeaderText="廠商" SortExpression="CO_ABBR" 
                            Visible="False" />
                        <asp:BoundField DataField="WIRE_ID" HeaderText="編號" SortExpression="WIRE_ID" 
                            Visible="False" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" 
                        Font-Size="Small" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle Font-Size="Large" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#242121" />
                </asp:GridView>
                <asp:SqlDataSource ID="SDS_wires2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                    ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
 
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
