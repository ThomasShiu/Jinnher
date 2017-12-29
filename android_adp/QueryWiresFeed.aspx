<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryWiresFeed.aspx.cs" Inherits="QueryWiresFeed" %>



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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:DataList ID="DataList1" runat="server" DataSourceID="SDS_wiresfeed">
                    <ItemTemplate>
                        JOB_NO:
                        <asp:Label ID="JOB_NOLabel" runat="server" Text='<%# Eval("JOB_NO") %>' />
                        <br />
                        PROC_DATE:
                        <asp:Label ID="PROC_DATELabel" runat="server" Text='<%# Eval("PROC_DATE") %>' />
                        <br />
                        PROC_NAME:
                        <asp:Label ID="PROC_NAMELabel" runat="server" Text='<%# Eval("PROC_NAME") %>' />
                        <br />
                        WIR_KIND:
                        <asp:Label ID="WIR_KINDLabel" runat="server" Text='<%# Eval("WIR_KIND") %>' />
                        <br />
                        DRAW_DIA:
                        <asp:Label ID="DRAW_DIALabel" runat="server" Text='<%# Eval("DRAW_DIA") %>' />
                        <br />
                        LUO_NO:
                        <asp:Label ID="LUO_NOLabel" runat="server" Text='<%# Eval("LUO_NO") %>' />
                        <br />
                        ASSM_FLOW_NAME:
                        <asp:Label ID="ASSM_FLOW_NAMELabel" runat="server" 
                            Text='<%# Eval("ASSM_FLOW_NAME") %>' />
                        <br />
                        TTLWT:
                        <asp:Label ID="TTLWTLabel" runat="server" Text='<%# Eval("TTLWT") %>' />
                        <br />
                        CNT:
                        <asp:Label ID="CNTLabel" runat="server" Text='<%# Eval("CNT") %>' />
                        <br />
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="SDS_wiresfeed2">
                        <Columns>
                            <asp:BoundField DataField="COIL_NO" HeaderText="COIL_NO" 
                                SortExpression="COIL_NO" />
                            <asp:BoundField DataField="COIL_WT" HeaderText="COIL_WT" 
                                SortExpression="COIL_WT" />
                        </Columns>
                        </asp:GridView>
                <asp:SqlDataSource ID="SDS_wiresfeed2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:JHERPDB2 %>" 
                    ProviderName="<%$ ConnectionStrings:JHERPDB2.ProviderName %>" SelectCommand="SELECT COIL_NO,COIL_WT
FROM V_APC_JOB_ISSU  
WHERE TO_CHAR(PROC_DATE,'YYYY/MM/DD') = '2013/12/05'
AND  JOB_NO = 'C13120001'
ORDER BY 1"></asp:SqlDataSource>
                    </ItemTemplate>
                </asp:DataList>
                
                <asp:SqlDataSource ID="SDS_wiresfeed" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:JHERPDB2 %>" 
                    ProviderName="<%$ ConnectionStrings:JHERPDB2.ProviderName %>" SelectCommand="SELECT JOB_NO,TO_CHAR(PROC_DATE,'YYYY/MM/DD') PROC_DATE,PROC_NAME,WIR_KIND,DRAW_DIA,LUO_NO,ASSM_FLOW_NAME,SUM(COIL_WT) TTLWT ,COUNT(*) CNT
FROM V_APC_JOB_ISSU  
WHERE TO_CHAR(PROC_DATE,'YYYY/MM/DD') = '2013/12/05'
GROUP BY JOB_NO,PROC_DATE,PROC_NAME,WIR_KIND,DRAW_DIA,LUO_NO,ASSM_FLOW_NAME 
ORDER BY 2 DESC ,1"></asp:SqlDataSource>        

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
