<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ipad.aspx.cs" Inherits="ipad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Jinnher Mobile</title>
    <meta name="viewport" content="width=device-width; initial-scale=1.0; user-scalable=1;"/>
    <link rel="shortcut icon" href="images/jh.ico" />
    <link rel="apple-touch-icon" href="images/jh.ico"/>
    <link href="styles/MetroJs.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="Scripts/MetroJs.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $(".live-tile,.flip-list").liveTile();
                $(".appbar").applicationBar();
            });
	    </script>
    
 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="L_id" runat="server" Text="Label" Visible="False"></asp:Label>
        <div class="tiles">
     <table style="margin:0px; padding:0px">
  <tr>
   <td>

     <table>
       <tr>
        <td  >
           
         <div id="Div2" class="live-tile blue" data-direction="vertical" data-stops="50%,100%,50%,0">       
           <span class="tile-title">外銷接單</span>
            <div>
               <a href="GM_exportorder2.aspx" target="_blank">
                <img clsas="full" src="./images/dataset.png" alt="1" />
               </a>
            </div>
            <div class="myClass">
                <a href="GM_exportorder2.aspx"  target="_blank">
                  <img src="images/export_1.png" />
                </a>
            </div>
        </div>
        </td>
       <td style=" width:130px; height:130px;">
           
       <div class="live-tile" data-stops="100%" data-speed="900" data-delay="9000">
            <span class="tile-title">業務</span>
            <div><a href='GM_demosorder.aspx' target="_blank"><img src="./images/sales01.png" alt="1" /></a></div>
            <div>
                <a href='GM_demosorder.aspx' target="_blank">
                    <li>內銷接單</li>
                </a>
            </div>        
        </div>

        </td>
        <td style=" width:130px; height:130px">

         <div id="Div1" class="live-tile blue" data-direction="vertical" data-stops="50%,100%,50%,0" data-speed="900" data-delay="7500">       
           <span class="tile-title">外銷出貨統計</span>
            <div>
               <a href="GM_salesKPI_5.aspx" target="_blank">
                <img clsas="full" src="./images/dataset.png" alt="1" />
               </a>
            </div>
            <div class="myClass">
                <a href="GM_salesKPI_5.aspx"  target="_blank">
                  <img src="images/export_2.png" />
                </a>
            </div>
        </div>
        </td>
        <td style="width:130px; height:130px;">

         <div id="Div3" class="live-tile blue" data-direction="vertical" data-stops="50%,100%,50%,0"  data-speed="900" data-delay="8500">       
           <span class="tile-title">內銷出貨統計</span>
            <div>
               <a href="GM_salesKPI_L.aspx" target="_blank">
                <img clsas="full" src="./images/dataset.png" alt="1" />
               </a>
            </div>
            <div class="myClass">
                <a href="GM_salesKPI_L.aspx"  target="_blank">
                  <img src="images/import_2.png" />
                </a>
            </div>
        </div>
        </td>

       <td >
         <div id="Div4" class="live-tile blue" data-direction="vertical" data-stops="50%,100%,50%,0"  data-speed="900" data-delay="6500">       
           <span class="tile-title">訂單交期</span>
            <div>
               <a href="GM_orddelivery.aspx" target="_blank">
                <img clsas="full" src="./images/dataset.png" alt="1" />
               </a>
            </div>
            <div class="myClass">
                <a href="GM_orddelivery.aspx"  target="_blank">
                  <img src="images/order_pdate.png" />
                </a>
            </div>
        </div>
        </td>

       </tr>
     </table>
    
     <table  >
       <tr>
        <td >
        <div style=" background-color:#00DC03;width:335px; height:60px;font-family:微軟正黑體,Arial; text-align:center; position: relative; top: 5px; right: 5px; bottom: 15px; left: 5px;">
        
            <asp:Image ID="Image3" runat="server" ImageUrl="~/images/box.png" />
            <asp:Label ID="Label5" runat="server" Text="內銷成品庫存" Font-Bold="True" Font-Size="X-Large" 
                ForeColor="White"></asp:Label>
        </div>
        </td>
        <td>
        <div  style=" background-color:#00DC03;width:508px; height:60px;font-family:微軟正黑體,Arial; text-align:center; position: relative; top: 5px; right: 5px; bottom: 15px; left: 15px;">
           <a href="GM_exportstock.aspx">
              <asp:Image ID="Image2" runat="server" ImageUrl="~/images/anchor.png" 
                BorderStyle="None" />
             <asp:Label ID="Label10" runat="server" Text="外銷成品庫存" ForeColor="White"  Font-Size="X-Large"></asp:Label>

           </a>
        </div>
        </td>
       </tr>
     </table>

     <table  >
       <tr>
        <td>
         <div id="Div5" class="live-tile blue" data-direction="vertical" data-stops="50%,100%,50%,0"  data-speed="900" data-delay="7000">       
           <span class="tile-title">內銷成品庫存</span>
            <div>
               <a href="GM_demostock12.aspx" target="_blank">
                <img clsas="full" src="./images/dataset.png" alt="1" />
               </a>
            </div>
            <div class="myClass">
                <a href="GM_demostock12.aspx"  target="_blank">
                  <img src="images/demostock_1.png" />
                </a>
            </div>
        </div>
 
        </td>
       <td >
        <div id="Div6" class="live-tile blue" data-direction="vertical" data-stops="50%,100%,50%,0"  data-speed="900" data-delay="8000">       
           <span class="tile-title">內銷成品庫存</span>
            <div>
               <a href="GM_demostock2.aspx" target="_blank">
                <img clsas="full" src="./images/dataset.png" alt="1" />
               </a>
            </div>
            <div class="myClass">
                <a href="GM_demostock2.aspx"  target="_blank">
                  <img src="images/demostock_2.png" />
                </a>
            </div>
        </td>
        <td>
    
        <div id="Div7" class="live-tile blue" data-direction="vertical" data-stops="50%,100%,50%,0"  data-speed="900" data-delay="7500">       
           <span class="tile-title">盤元庫存</span>
            <div>
               <a href="GM_demostock2.aspx" target="_blank">
                <img clsas="full" src="./images/man01.png" alt="1" />
               </a>
            </div>
            <div class="myClass">
                <a href="GM_demostock2.aspx"  target="_blank">
                  <img src="images/wire_stock.png" />
                </a>
            </div>
        </td>
       <td>
         
          <div id="Div8" class="live-tile blue" data-direction="vertical" data-stops="50%,100%,50%,0"  data-speed="900" data-delay="9500">       
           <span class="tile-title">製程生產分析表</span>
            <div>
               <a href="GM_processanaly.aspx" target="_blank">
                <img clsas="full" src="./images/produce01.png" alt="1" />
               </a>
            </div>
            <div class="myClass">
                <a href="GM_processanaly.aspx"  target="_blank">
                  <img src="images/proce_analy.png" />
                </a>
            </div>
        </td>
        <td >
        
            <div id="Div9" class="live-tile blue" data-direction="vertical" data-stops="50%,100%,50%,0"  data-speed="900" data-delay="6500">       
           <span class="tile-title">盤元需求表</span>
            <div>
               <a href="GM_wirereq.aspx" target="_blank">
                <img clsas="full" src="./images/dataset.png" alt="1" />
               </a>
            </div>
            <div class="myClass">
                <a href="GM_wirereq.aspx"  target="_blank">
                  <img src="images/wire_req.png" />
                </a>
            </div>
        </td>
       </tr>
     </table>

      <table >
       <tr>
        <td >
      
            <div id="Div14" class="live-tile blue" data-direction="vertical" data-stops="50%,100%,50%,0"  data-speed="900" data-delay="8500">       
           <span class="tile-title">成型預排</span>
            <div>
               <a href="#" target="_blank">
                <img clsas="full" src="./images/dataset.png" alt="1" />
               </a>
            </div>
            <div class="myClass">
                <a href="#"  target="_blank">
                  <img src="images/pre_form.png" />
                </a>
            </div>
        </td>
       <td >
    
           <div id="Div12" class="live-tile blue" data-direction="vertical" data-stops="50%,100%,50%,0"  data-speed="900" data-delay="9000">       
           <span class="tile-title">待輾牙</span>
            <div>
               <a href="#" target="_blank">
                <img clsas="full" src="./images/dataset.png" alt="1" />
               </a>
            </div>
            <div class="myClass">
                <a href="#"  target="_blank">
                  <img src="images/theat.png" />
                </a>
            </div>
        </td>
        <td >
        
            <div id="Div10" class="live-tile blue" data-direction="vertical" data-stops="50%,100%,50%,0"  data-speed="900" data-delay="7500">       
           <span class="tile-title">待熱處理</span>
            <div>
               <a href="GM_wirereq.aspx" target="_blank">
                <img clsas="full" src="./images/dataset.png" alt="1" />
               </a>
            </div>
            <div class="myClass">
                <a href="GM_wirereq.aspx"  target="_blank">
                  <img src="images/h.png" />
                </a>
            </div>
           </td>
          
       <td >
    
           <div id="Div11" class="live-tile blue" data-direction="vertical" data-stops="50%,100%,50%,0"  data-speed="900" data-delay="6200">       
           <span class="tile-title">待電鍍</span>
            <div>
               <a href="GM_platheatraw.aspx" target="_blank">
                <img clsas="full" src="./images/dataset.png" alt="1" />
               </a>
            </div>
            <div class="myClass">
                <a href="GM_platheatraw.aspx"  target="_blank">
                  <img src="images/f.png" />
                </a>
            </div>
        </td>
        <td>
       
         <div class="live-tile" data-stops="100%" data-speed="900" data-delay="5500">
            <span class="tile-title">業務接單KPI</span>
            <div><a href='GM_salesKPI_3.aspx' target="_blank"><img src="./images/chart-area-up.png" alt="1" /></a></div>
            <div>
                <a href='GM_salesKPI_3.aspx' target="_blank">
                    <img src="images/sal_order.png" />
                </a>
            </div>        
        </div>

        
        </div>
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
    </div>
    </form>
</body>
</html>
