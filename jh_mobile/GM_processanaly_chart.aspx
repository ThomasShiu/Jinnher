<%@ Page Title="各製程生產比例" Language="C#" MasterPageFile="~/JH_MP.master" AutoEventWireup="true" CodeFile="GM_processanaly_chart.aspx.cs" Inherits="GM_processanaly_chart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
    <tr>
        <td>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
           <ContentTemplate>
              <table>
                <tr>
                  <td class="style1">
                    <asp:TextBox ID="TB_year" runat="server" Width="80px" Font-Size="Large"></asp:TextBox>
                  </td>
                  <td class="style1">
                   <asp:DropDownList ID="DDL_month" runat="server"   Font-Size="Large">
                       <asp:ListItem Value="01">一月</asp:ListItem>
                       <asp:ListItem Value="02">二月</asp:ListItem>
                       <asp:ListItem Value="03">三月</asp:ListItem>
                       <asp:ListItem Value="04">四月</asp:ListItem>
                       <asp:ListItem Value="05">五月</asp:ListItem>
                       <asp:ListItem Value="06">六月</asp:ListItem>
                       <asp:ListItem Value="07">七月</asp:ListItem>
                       <asp:ListItem Value="08">八月</asp:ListItem>
                       <asp:ListItem Value="09">九月</asp:ListItem>
                       <asp:ListItem Value="10">十月</asp:ListItem>
                       <asp:ListItem Value="11">十一月</asp:ListItem>
                       <asp:ListItem Value="12">十二月</asp:ListItem>
                   </asp:DropDownList>
                  </td>
                  <td class="style1">
                       <asp:Button ID="Quy_btn" runat="server" Text="重新查詢"  Width="80px" Height="30px" 
                           onclick="Quy_btn_Click" />
                      
                  </td>
                </tr>
              </table>
               

              
           
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Quy_btn" EventName="Click" />
               
            </Triggers>
        </asp:UpdatePanel> 
        </td>
    </tr>
    <tr>
        <td>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            
            <asp:Chart ID="Chart1" runat="server" DataSourceID="DS_process" 
                Palette="Pastel" Height="600px" Width="900px" onclick="Chart1_Click">
                <Series>
                    <asp:Series Name="Series1" ChartType="Pie" XValueMember="PROCESS_NAME" 
                        YValueMembers="ROWSUM" CustomProperties="CollectedSliceExploded=True, PieLabelStyle=Outside" 
                        Label="#AXISLABEL -- #VALY{n0} -- #PERCENT" Legend="Legend1" 
                        IsValueShownAsLabel="True">
                        <Points>
                            <asp:DataPoint YValues="0" />
                        </Points>
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                        <Area3DStyle Enable3D="True" />
                    </asp:ChartArea>
                </ChartAreas>
                <Legends>
                    <asp:Legend Docking="Bottom" Name="Legend1" TableStyle="Wide" 
                        AutoFitMinFontSize="8"  ShadowOffset="3">
                    </asp:Legend>
                </Legends>
            </asp:Chart>
            <asp:SqlDataSource ID="DS_process" runat="server" 
                ConnectionString="<%$ ConnectionStrings:jh815 %>" 
                ProviderName="<%$ ConnectionStrings:jh815.ProviderName %>" SelectCommand=""></asp:SqlDataSource>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Quy_btn" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="Chart1" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
<script type="text/javascript">
    function onSliceClicked(pointIndex) {
        var objSeries = document.Form1.elements["SeriesTooltip"];
        var objLegend = document.Form1.elements["LegendTooltip"];

        var parameters = "seriesTooltip=" + objSeries.options[objSeries.selectedIndex].value;
        parameters = parameters + "&legendTooltip=" + objLegend.options[objLegend.selectedIndex].value;

        document.Form1.elements["Chart1"].ImageUrl = "ImageMapToolTipsChart.aspx?" + parameters;

        document.images["Chart1"].src = document.Form1.elements["Chart1"].ImageUrl;
    } 
			 
		</script>
</asp:Content>

