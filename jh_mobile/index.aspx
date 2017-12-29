<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Jinnher Mobile</title>
    <meta name="viewport" content="width=device-width; initial-scale=1.0; user-scalable=1;"/>
    <script src="js/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.wp7.js" type="text/javascript"></script>
    <link href="Styles/basic2.css" rel="stylesheet" type="text/css" />
    <link href="Styles/wp7.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style=" width:100%">
  <tr>
   <td style=" width:268px;">
   <table style=" margin:2px; padding:2px">
       <tr>
        <td style=" background-color:#007AFA;width:264px; height:60px;font-family:微軟正黑體,Arial; text-align:center;">
            <asp:Image ID="Image3" runat="server" CssClass="someClass" Text='平板電腦版本' 
                Height="32px" Width="32px" ImageAlign="Top" 
                ImageUrl="~/images/laptop.png"/>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ipad.aspx" Font-Bold="True" Font-Size="X-Large" 
                ForeColor="White">平板電腦版&gt;&gt;</asp:HyperLink>

        </td>
       </tr>
     </table>
     <table style=" margin:2px; padding:2px">
       <tr>
        <td style=" background-color:#00DC03;width:130px; height:130px">
            <div  class="run">
		    <ul>
			    <li>
                    <a href="GM_exportorder2.aspx">
                       <font style=" color:White; font-size:medium">
                        Export Orders 
                        </font>
                    </a>
                </li>
			    <li>
                <a href="GM_exportorder2.aspx">
                       <font style=" color:White; font-size:X-Large">
                        外銷接單 
                        </font>

                    </a>
                </li>
		    </ul>
	        </div>
        </td>
       <td style=" background-color:#00DC03;width:130px; height:130px;">
            <div class="run2">
		      <ul>
                <li>
                    <a href="GM_demosorder.aspx">
                       <font style=" color:White; font-size:Medium">
                       Domestic Orders
                        </font>
                    </a>
                </li>
			    <li>
                     <a href="GM_exportorder.aspx">
                       <font style=" color:White; font-size:X-Large">
                       內銷接單
                        </font>
                    </a>
                </li>
		    </ul>
	        </div>
        </td>
       </tr>
     </table>
     <table style=" margin:2px; padding:2px">
       <tr>
        <td style=" background-color:#00DC03;width:130px; height:130px">
            <div  class="run3">
		    <ul>
			    <li>
                    <a href="GM_exportship.aspx">
                       <font style=" color:White; font-size:Small">
                       Export Shipments
                      </font>
                    </a>
                </li>
			    <li>
                <a href="GM_exportship.aspx">
                       <font style=" color:White; font-size:Large">
                       外銷出貨統計
                      </font>
                    </a>
                </li>
		    </ul>
	        </div>
        </td>
       <td style=" background-color:#00DC03;width:130px; height:130px;">
            <div class="run4">
		      <ul>
                <li>
                    <a href="GM_demoship.aspx">
                       <font style=" color:White; font-size:Small">
                       Domestic Shipments
                      </font>
                    </a>
                </li>
			    <li>
                     <a href="GM_demoship.aspx">
                      <font style=" color:White; font-size:Large">
                       內銷出貨統計
                      </font>
                    </a>
                </li>
		    </ul>
	        </div>
        </td>
       </tr>
     </table>
     <table style=" margin:2px; padding:2px">
       <tr>
        <td style=" background-color:#007AFA;width:264px; height:60px;font-family:微軟正黑體,Arial; text-align:center;">
            <asp:Image ID="Weather_icon" runat="server" CssClass="someClass" Text='天氣預報' Height="32px" Width="32px"/>
            
            <asp:Label ID="L_weather" runat="server" Font-Bold="True" Font-Size="X-Large" 
                ForeColor="White"></asp:Label>
        </td>
       </tr>
     </table>

     <table style=" margin:2px; padding:2px">
       <tr>
        <td style=" background-color:#00DC03;width:130px; height:130px; text-align:center;">
            <div  class="run6">
            <ul>
			<li>
            <a href="GM_orddelivery.aspx">
                <asp:Label ID="Label11" runat="server" Text="Order Delivery" ForeColor="White"  Font-Size="Small"></asp:Label>

            </a>

            <a href="GM_orddelivery.aspx">
                <asp:Label ID="Label12" runat="server" Text="訂單交期" ForeColor="White"  Font-Size="X-Large"></asp:Label>

            </a>
            </li>
            </ul>
            </div>
        </td>
       <td style=" background-color:#00DC03;width:130px; height:130px; text-align:center;">
       <div  class="run6">
            <ul>
			<li> 
            <a href="GM_exportstock.aspx">
                <asp:Label ID="Label13" runat="server" Text="Export Delivery" ForeColor="White" 
                    Font-Size="Small"  ></asp:Label>

            </a><br />
            <a href="GM_exportstock.aspx">
                <asp:Label ID="Label14" runat="server" Text="外銷成品庫存" ForeColor="White"  Font-Size="Large"></asp:Label>

            </a>
            </li>
            </ul>
            </div>
        </td>
       </tr>
     </table>
     <table style=" margin:2px; padding:2px">
       <tr>
        <td style=" background-color:#007AFA;width:264px; height:60px;font-family:微軟正黑體,Arial; text-align:center;">
            <asp:Image ID="Image4" runat="server"   
                Height="32px" Width="32px" ImageUrl="~/images/box.png"/>
            
            <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="X-Large" 
                ForeColor="White">內銷成品庫存</asp:Label>
        </td>
       </tr>
     </table>

     <table style=" margin:2px; padding:2px">
       <tr>
        <td style=" background-color:#00DC03;width:130px; height:130px; text-align:center;">
        <div  class="run6">
            <ul>
			<li>
            <a href="GM_demostock12.aspx">
                <asp:Label ID="Label17" runat="server" Text="By產品種類" ForeColor="White"  
                    Font-Size="Large" ></asp:Label>

            </a>
            </li>
            </ul>
            </div>
        </td>
       <td style=" background-color:#00DC03;width:130px; height:130px; text-align:center;">
       <div  class="run6">
            <ul>
			<li>
            <a href="GM_demostock2.aspx">
                <asp:Label ID="Label16" runat="server" Text="By年份" ForeColor="White"  
                    Font-Size="Large" ></asp:Label>

            </a>
            </li>
            </ul>
            </div>
        </td>
       </tr>
     </table>

     <table style=" margin:2px; padding:2px">
       <tr>
        <td style=" background-color:#cccccc;width:130px; height:130px; text-align:center;">
        <div  class="run6">
            <ul>
			<li>    <a href="#">
                <asp:Label ID="Label18" runat="server" Text="盤元庫存" ForeColor="White"  
                Font-Size="Large" ></asp:Label>

            </a>
            </li>
            </ul>
            </div>
        </td>
       <td style=" background-color:#00DC03;width:130px; height:130px; text-align:center;">
       <div  class="run6">
            <ul>
			<li>
            <a href="GM_processanaly.aspx">
                <asp:Label ID="Label19" runat="server" Text="製程生產分析表" ForeColor="White"  
                Font-Size="Large"  ></asp:Label>
            </a>
            </li>
            </ul>
            </div>
        </td>
       </tr>
     </table>

     <table style=" margin:2px; padding:2px">
       <tr>
        <td style=" background-color:#00DC03;width:130px; height:130px; text-align:center;">
        <div  class="run6">
            <ul>
			<li>
            <a href="GM_wirereq.aspx">
                <asp:Label ID="Label20" runat="server" Text="盤元需求表" ForeColor="White"  
                    Font-Size="Large" ></asp:Label>

            </a>
            </li>
            </ul>
            </div>
        </td>
        <td style=" background-color:#cccccc;width:130px; height:130px; text-align:center;">
         <div  class="run6">
            <ul>
			<li>
               <a href="#">
                <asp:Label ID="Label23" runat="server" Text="成型預排" ForeColor="White"  
                    Font-Size="Large" ></asp:Label>

            </a>
            </li>
            </ul>
            </div>
        </td>
       </tr>
     </table>

     <table style=" margin:2px; padding:2px">
       <tr>
        <td style=" background-color:#cccccc;width:130px; height:130px; text-align:center;">
        <div  class="run6">
            <ul>
			<li>
            <a href=#">
                <asp:Label ID="Label21" runat="server" Text="待輾牙" ForeColor="White"  
                    Font-Size="Large" ></asp:Label>

            </a>
            </li>
            </ul>
            </div>
        </td>
       <td style=" background-color:#00DC03;width:130px; height:130px; text-align:center;">
       <div  class="run6">
            <ul>
			<li>
            <a href="GM_heatraw.aspx">
                <asp:Label ID="Label22" runat="server" Text="待熱處理" ForeColor="White"  
                Font-Size="Large" ></asp:Label>
            </a>
            </li>
            </ul>
            </div>
        </td>
       </tr>
     </table>

     <table style=" margin:2px; padding:2px">
       <tr>
        <td style=" background-color:#00DC03;width:130px; height:130px; text-align:center;">
        <div  class="run6">
            <ul>
			<li>
            <a href="GM_platheatraw.aspx">
                <asp:Label ID="Label24" runat="server" Text="待電鍍" ForeColor="White"  
                    Font-Size="Large" ></asp:Label>

            </a>
            </li>
            </ul>
            </div>
        </td>
       <td style=" background-color:#cccccc;width:130px; height:130px; text-align:center;">
       <div  class="run6">
            <ul>
			<li>
            <a href="#">
                <asp:Label ID="Label25" runat="server" Text="半成品倉待包" ForeColor="White"  
                Font-Size="Large" ></asp:Label>
            </a>
            </li>
            </ul>
            </div>
        </td>
       </tr>
     </table>

      <table style="margin:2px;padding:2px">
       <tr>
        <td style=" background-color:#ffffff;width:130px; height:130px; text-align:center">
          &nbsp;
        </td>
       <td style=" background-color:#00DC03;width:130px; height:130px">
            <div  class="run5" >
		     <ul>
                <li>
                    <div class="imgInner">
                    <asp:Image ID="Image2" runat="server"  ImageUrl="images/user-card.png" 
                            CssClass="imgPosition"  />        
                    </div>
                </li>
			    <li><asp:HyperLink ID="HyperLink9" runat="server" Text="個人資料" Font-Bold="True" 
                Font-Size="Large" ForeColor="White" NavigateUrl="GM_exportorder.aspx" /></li>
		    </ul>
	        </div>
        </td>
       </tr>
     </table>
     
     <table style=" margin:2px; padding:2px">
       <tr>
        <td style=" background-color:#007AFA;width:264px; height:80px;font-family:微軟正黑體,Arial; text-align:center; ">
             
            <asp:HyperLink ID="HyperLink2" runat="server" Text="設定" Font-Bold="True" 
                Font-Size="X-Large" ForeColor="White" NavigateUrl="~/SYS_setup.aspx"></asp:HyperLink>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/gears.png" 
                ImageAlign="Middle" />
        </td>
       </tr>
     </table>
   </td>
   <td style=" text-align:left; vertical-align:top; ">
       <a href="SYS_setup.aspx"   >
         <asp:Image ID="Image1" runat="server" ImageUrl="images/button-play.png" 
           Width="28px" BorderStyle="None"/>
       </a>
   </td>
  </tr>
</table>
    </div>
    </form>
</body>
</html>
