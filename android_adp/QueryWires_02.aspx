<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryWires_02.aspx.cs" Inherits="QueryWires_02" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>盤元資料查詢</title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:DataList ID="DataList1" runat="server" DataKeyField="WIRE_ID" 
                    DataSourceID="SDS_wires1" BackColor="White" BorderColor="#CCCCCC" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
                    GridLines="Horizontal" Width="266px">
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                    <ItemStyle Font-Size="Small" />
                    <ItemTemplate>
                        <table style=" width:100%">
                             <tr>
                                <td colspan="2">廠商:<br/>
                                <asp:Label ID="CO_ABBRLabel" runat="server" Text='<%# Eval("CO_ABBR") %>' 
                                        Font-Bold="True" Font-Size="X-Large" /></td>
                            </tr>
                            <tr>
                                <td colspan="2">卷號:<br/><asp:Label ID="WIRE_IDSLabel" runat="server" 
                                        Text='<%# Eval("WIRE_IDS") %>' Font-Bold="True" Font-Size="X-Large" /></td>
                              
                            </tr>
                            
                            
                            
                            <tr>
                                <td>
                                材質:<br/><asp:Label ID="RAWMTRL_IDLabel" runat="server" Text='<%# Eval("RAWMTRL_ID") %>' 
                                        Font-Bold="True" Font-Size="X-Large" />
                                </td>
                                <td>
                                線徑:<br/><asp:Label ID="DIAMETERLabel" runat="server" Text='<%# Eval("DIAMETER") %>' 
                                        Font-Bold="True" Font-Size="X-Large"  />mm
                                </td>
                            </tr>
                            <tr>
                                <td>
                                爐號:<br/><asp:Label ID="HEAT_NOLabel" runat="server" Text='<%# Eval("HEAT_NO") %>' 
                                        Font-Bold="True" Font-Size="X-Large"  />
                                </td>
                                <td>
                                重量:<br/><asp:Label ID="WEIGHTLabel" runat="server" Text='<%# Eval("WEIGHT","{0:n0}") %>' 
                                        Font-Bold="True" Font-Size="X-Large"  />kg
                                </td>
                            </tr>
                             <tr>
                                <td colspan='2'>
                                新爐:<br/><asp:Label ID="HEAT_NO_NEWLabel" runat="server" 
                                        Text='<%# Eval("HEAT_NO_NEW") %>' Font-Bold="True" Font-Size="X-Large"  />
                                </td>
 
                            </tr>
                             <tr>
                                <td>
                                進貨:<br/><asp:Label ID="STORE_DATELabel" runat="server" Text='<%# Eval("STORE_DATE") %>' 
                                        Font-Bold="True" Font-Size="X-Large"  />
                                </td>
                                <td>
                                儲位:<br/><asp:Label ID="LOCATIONLabel1" runat="server" Text='<%# Eval("LOCATION") %>' 
                                        Font-Bold="True" Font-Size="X-Large"  />
                                </td>
                            </tr>
                            
                        </table>
                    </ItemTemplate>
                    <SelectedItemStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                </asp:DataList>
                <asp:SqlDataSource ID="SDS_wires1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                    ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="4" DataSourceID="SDS_wires2" ForeColor="Black" 
                    GridLines="Horizontal" Width="266px">
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
