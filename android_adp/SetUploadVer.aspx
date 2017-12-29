<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetUploadVer.aspx.cs" Inherits="SetUploadVer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>世宇斑馬更新管理</title>
    <style type="text/css">
        .style1
        {
            height: 31px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <table>
        <tr>
            <td>
            <fieldset class="login">
			<legend>版本更新內容填寫</legend>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="標題"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TB_tittle" runat="server" Width="250px"></asp:TextBox>
                        <asp:DropDownList ID="DDL_status" runat="server" Width="70px">
                            <asp:ListItem Value="Y">啟用</asp:ListItem>
                            <asp:ListItem Value="N">停用</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="說明"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TB_content" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="更新描述"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TB_descript" runat="server" MaxLength="1000" Rows="6" 
                            TextMode="MultiLine" Width="300px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label5" runat="server" Text="版本:"></asp:Label>
                        <asp:TextBox ID="TB_version" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td class="style1">
                        <asp:Label ID="Label1" runat="server" Text="APK:"></asp:Label>
                    </td>
                    <td class="style1">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" />
                    </td>
                    <td class="style1">
                        <asp:Button ID="btn_update" runat="server" Text="送出更新" 
                            onclick="btn_update_Click" />
                        <asp:Label ID="L_sn" runat="server" Visible="False"></asp:Label>
                        
                    </td>
                    <td>
                        <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/Download.aspx">Download Page</asp:HyperLink>
                    </td>
                </tr>
            </table>
            </fieldset>
            </td>
            
        </tr>
        <tr>
        <td>
                <asp:Label ID="message" runat="server" Font-Bold="True" Font-Size="Small" 
                    ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        CellPadding="3" DataSourceID="SDS_notification" ForeColor="Black" 
                        GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                        BorderStyle="Solid" BorderWidth="1px">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:BoundField DataField="TITTLE" HeaderText="TITTLE" 
                            SortExpression="TITTLE" Visible="False" />
                        <asp:BoundField DataField="CONTENT" HeaderText="CONTENT" 
                            SortExpression="CONTENT" Visible="False" />
                        <asp:BoundField DataField="VERSION" HeaderText="VERSION" 
                            SortExpression="VERSION" />
                        <asp:TemplateField HeaderText="DESCRIPT" SortExpression="DESCRIPT">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DESCRIPT") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# ((string)Eval("DESCRIPT")).Replace("\n", "<br/>") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="APK" SortExpression="APK">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("APK") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%--<asp:Label ID="Label2" runat="server" Text='<%# Bind("APK") %>'></asp:Label>--%>
                                <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Bind("APK") %>'  NavigateUrl='<%# "download/"+Eval("APK").ToString() %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="STATUS" HeaderText="STATUS" 
                            SortExpression="STATUS" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
                    <asp:SqlDataSource ID="SDS_notification" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:jheip %>" 
                        ProviderName="<%$ ConnectionStrings:jheip.ProviderName %>">
                    </asp:SqlDataSource>

                
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
