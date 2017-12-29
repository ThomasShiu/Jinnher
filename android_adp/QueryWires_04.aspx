<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryWires_04.aspx.cs" Inherits="QueryWires_04" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>盤元盤點資料查詢</title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no">
    <style type="text/css">
        #t01
        {
            position: absolute;
            left: 130px;
            top: 100px;
            -webkit-transform: rotate(-90deg);
            -moz-transform: rotate(-90deg);
            -o-transform: rotate(-90deg);
            transform: rotate(-90deg);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            
               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" DataSourceID="SDS_wireinv1" ForeColor="#333333" 
                    GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="LOCATE" HeaderText="儲區" 
                            SortExpression="LOCATE" Visible="False" />
                        <asp:BoundField DataField="WIR_KIND" HeaderText="材質" 
                            SortExpression="WIR_KIND" >
                        <ItemStyle Font-Bold="True" Font-Size="Medium" ForeColor="#3366FF" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DRAW_DIA" HeaderText="線徑" 
                            SortExpression="DRAW_DIA" >
                        <ItemStyle Font-Size="Medium" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LUO_NO" HeaderText="爐號" SortExpression="LUO_NO" >
                        <ItemStyle Font-Size="Medium" ForeColor="Blue" />
                        </asp:BoundField>
                        <asp:BoundField DataField="COIL_WT" HeaderText="總重" SortExpression="COIL_WT" 
                            DataFormatString="{0:n0}" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CNT" HeaderText="卷數" SortExpression="CNT" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" Font-Size="Larger" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <asp:SqlDataSource ID="SDS_wireinv1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:jheip %>" 
                    ProviderName="<%$ ConnectionStrings:jheip.ProviderName %>" SelectCommand="SELECT  I.LOCATE,W.WIR_KIND,W.DRAW_DIA,W.LUO_NO,
             SUM(W.COIL_WT) COIL_WT,COUNT(*) CNT
FROM WIRES_INV I,V_ASK3412Q W
WHERE 1=1
AND I.COIL_NO = W.COIL_NO(+)
GROUP BY I.LOCATE,W.WIR_KIND,W.DRAW_DIA,W.LUO_NO"></asp:SqlDataSource>
            <br />
                <asp:DataList ID="DataList1" runat="server" DataKeyField="COIL_NO" 
                    DataSourceID="SDS_wires1"  CellPadding="4"  BackColor="White" BorderColor="#CCCCCC" 
                    BorderStyle="None" BorderWidth="1px"   ForeColor="Black" 
                    GridLines="Horizontal" Font-Size="Larger" >
                    <AlternatingItemStyle BackColor="White" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <ItemStyle BackColor="#EFF3FB" />
                    <ItemTemplate>
                    <table>
                        <tr>
                            <td colspan="3">
                            卷:<asp:Label ID="WIRE_IDSLabel" runat="server" Text='<%# Eval("COIL_NO") %>'  
                                    Font-Bold="True" Font-Size="Large" ForeColor="Blue" />
                            </td>
                        </tr>
                        <tr>
        
                            <td  colspan="3">
                            材質: <asp:Label ID="RAWMTRL_IDLabel" runat="server"  Text='<%# Eval("WIR_KIND") %>'  
                                    Font-Bold="True" Font-Size="Large" ForeColor="Blue" />
                            </td>
                            
                        </tr>
                        <tr>
                            <td>
                             線徑: <asp:Label ID="DIAMETERLabel" runat="server" Text='<%# Eval("DRAW_DIA") %>'  
                             Font-Bold="True" Font-Size="Large" ForeColor="Blue" />
                            </td>
                            <td colspan="2">
                             爐: <asp:Label ID="HEAT_NOLabel" runat="server" Text='<%# Eval("LUO_NO") %>'  
                             Font-Bold="True" Font-Size="Large" ForeColor="Blue" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                           現儲區: <asp:Label ID="LOCATIONLabel" runat="server" Text='<%# Eval("LOCATE") %>'  Font-Bold="True" ForeColor="Blue"/>
                           
                            </td>
                            <td >
                             原重: <asp:Label ID="WEIGHTLabel" runat="server" Text='<%# Eval("COIL_WT","{0:n0}") %>'  Font-Bold="True" ForeColor="Blue"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            原儲區: <asp:Label ID="MACHINELabel" runat="server" Text='<%# Eval("LOCATE2") %>'  Font-Bold="True" ForeColor="Blue"/>
                            </td>
                            <td colspan="2">
                            進貨日: <asp:Label ID="INPDATELabel" runat="server" 
                                    Text='<%# Eval("INP_DATE","{0:yyyy/MM/dd}") %>' Font-Bold="True" 
                                    ForeColor="Blue" Font-Size="Medium"/>
                            </td>
                        </tr>
                      
                  </table>
                    
                    </ItemTemplate>
                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                </asp:DataList>
                <asp:SqlDataSource ID="SDS_wires1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:jheip %>" 
                    ProviderName="<%$ ConnectionStrings:jheip.ProviderName %>" SelectCommand=""></asp:SqlDataSource>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
