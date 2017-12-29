<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryWires_05.aspx.cs" Inherits="QueryWires_05" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                <asp:DataList ID="DataList1" runat="server" DataKeyField="COIL_NO" 
                    DataSourceID="SDS_wires1" BackColor="White" BorderColor="#CCCCCC" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
                    GridLines="Horizontal" Width="266px">
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                    <ItemStyle Font-Size="Small" />
                    <ItemTemplate>
                        <table style=" width:100%">
                             <tr>
                                <td >廠商:<br/>
                                <asp:Label ID="CO_ABBRLabel" runat="server" Text='<%# Eval("MAKER") %>' 
                                        Font-Bold="True" Font-Size="X-Large" /></td>
                                <td>
                                製程:<br/>
                                    <asp:Label ID="ASSM_noLabel" runat="server" Text='<%# Eval("ASSM_ABBR") %>' 
                                Font-Bold="True" Font-Size="X-Large" /></td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">卷號:<br/><asp:Label ID="WIRE_IDSLabel" runat="server" 
                                        Text='<%# Eval("COIL_NO") %>' Font-Bold="True" Font-Size="X-Large" /></td>
                              
                            </tr>
                            
                            
                            
                            <tr>
                                <td>
                                材質:<br/><asp:Label ID="RAWMTRL_IDLabel" runat="server" Text='<%# Eval("WIR_KIND") %>' 
                                        Font-Bold="True" Font-Size="X-Large" />
                                </td>
                                <td>
                                線徑:<br/><asp:Label ID="DIAMETERLabel" runat="server" Text='<%# Eval("DRAW_DIA") %>' 
                                        Font-Bold="True" Font-Size="X-Large"  />mm
                                </td>
                            </tr>
                            <tr>
                                <td>
                                爐號:<br/><asp:Label ID="HEAT_NOLabel" runat="server" Text='<%# Eval("LUO_NO") %>' 
                                        Font-Bold="True" Font-Size="X-Large"  />
                                </td>
                                <td>
                                目前重量:<br/><asp:Label ID="WEIGHTLabel" runat="server" Text='<%# Eval("NOW_WT","{0:n0}") %>' 
                                        Font-Bold="True" Font-Size="X-Large"  />kg
                                </td>
                            </tr>
                  
                             <tr>
                                <td colspan="2">
                                現況:<br/><asp:Label ID="W_STATUSLabel" runat="server" Text='<%# Eval("W_STATUS") %>' 
                                        Font-Bold="True" Font-Size="Large"  />
                                </td>
                            
                            </tr>
                            <tr>
                                <td colspan="2">
                                歷程:<br/><asp:Label ID="ASSM_NAMELabel" runat="server" Text='<%# Eval("ASSM_NAME") %>' 
                                        Font-Bold="True" Font-Size="Large"  />
                                </td>
                            
                            </tr>
                            
                        </table>
                    </ItemTemplate>
                    <SelectedItemStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                </asp:DataList>
                <asp:SqlDataSource ID="SDS_wires1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:JHERPDB %>" 
                    ProviderName="<%$ ConnectionStrings:JHERPDB.ProviderName %>" SelectCommand=""></asp:SqlDataSource>

              
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>

