<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m_03_F_B0.aspx.cs" Inherits="m_03_F_B0" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<title>B0-成型達成率</title>
	<link rel="shortcut icon" href="images/jh.ico">
    <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans:300,400,700">
	<link rel="stylesheet" href="jqm/css/themes/default/jquery.mobile-1.4.5.min.css">
	<link rel="stylesheet" href="jqm/_assets/css/jqm-demos.css">
	<script src="jqm/js/jquery.js"></script>
	<script src="jqm/_assets/js/index.js"></script>
	<script src="jqm/js/jquery.mobile-1.4.5.min.js"></script>
    <script>
        window.addEventListener("load", function () {
            setTimeout(function () {
                window.scrollTo(0, 1);
            }, 10);
        });
     
      
    </script>
    <link rel="stylesheet" type="text/css" href="Styles/calendar_black.css" />
</head>
<body> 
 
    <form id="form1" runat="server" method="post" action="m_03_01_F.aspx">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Inherit">
    </asp:ScriptManager>
    <div data-role="header" data-position="fixed" data-theme="b">
        <h1>B0-成型達成率</h1>
        <!--<a href="m_index.aspx" data-rel="back" data-icon="carat-l" data-iconpos="notext">Back</a>-->
        <!--<a href="#" onclick="window.location.reload()" data-icon="back" data-iconpos="notext">Refresh</a> -->
    </div><!-- /header -->
   
  
   <div>
   <select name="SC_1" id="SC_1">
        <option value="B001">B001</option>
        <option value="B002">B002</option>
        <option value="B003">B003</option>
        <option value="B004">B004</option>
        <option value="B005">B005</option>
        <option value="B006">B006</option>
        <option value="B007">B007</option>
        <option value="B008">B008</option>
        <option value="B009">B009</option>
        <option value="B010">B010</option>
        <option value="B011">B011</option>
        <option value="B012">B012</option>
        <option value="B013">B013</option>
        <option value="B014">B014</option>
        <option value="B015">B015</option>
        <option value="B016">B016</option>
        <option value="B017">B017</option>
        <option value="B018">B018</option>
        <option value="B019">B019</option>
        <option value="B020">B020</option>
        <option value="B021">B021</option>
        <option value="B022">B022</option>
        <option value="B023">B023</option>
        <option value="B024">B024</option>
        <option value="B025">B025</option>
        <option value="B026">B026</option>
        <option value="B027">B027</option>
        <option value="B028">B028</option>
        <option value="B029">B029</option>
    </select>
   </div>
   
   <div >
         <label for="text-12">開始日期:</label>
          <asp:TextBox ID="TB_sdate" runat="server" ></asp:TextBox>
          <asp:CalendarExtender ID="TB_sdate_CalendarExtender" runat="server" 
                Enabled="True" TargetControlID="TB_sdate" DaysModeTitleFormat="yyyy,MMMM" 
                Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd" CssClass="black" >
         </asp:CalendarExtender>

         
    </div>
    <div >
    <label for="text-12">結束日期:</label>
    <asp:TextBox ID="TB_edate" runat="server" ></asp:TextBox>
          <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                Enabled="True" TargetControlID="TB_edate" DaysModeTitleFormat="yyyy,MMMM" 
                Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd"  CssClass="black" >
         </asp:CalendarExtender>
    
    </div>
    <div>
        <input id="Submit1" type="submit" value="送出查詢" />
    </div>
    <div>
        <ul data-role="listview" data-inset="true" data-divider-theme="b">
            <li data-role="list-divider">快速連結</li>
            <li><a href="m_03_F_B1.aspx" data-ajax="false">B1</a></li>
            <li><a href="m_03_F_B2.aspx" data-ajax="false">B2</a></li>
            <li><a href="m_03_F_B3.aspx" data-ajax="false">B3</a></li>
            <li><a href="m_03_F_B5.aspx" data-ajax="false">B5</a></li>
       </ul>
   </div>
	<div data-role="footer" data-position="fixed" data-theme="b">
        <div data-role="navbar">
        <ul>
            <li><a href="#" onclick=" window.history.back()" data-icon="back" data-iconpos="top">Back</a></li>
            <li><a href="m_03_F.aspx" data-rel="home" data-icon="home" data-iconpos="top">home</a></li>
            <li><a href="#" onclick="window.location.reload()" data-icon="refresh" data-iconpos="top">Refresh</a></li>
        </ul>
        </div><!-- /navbar -->
    </div><!-- /footer -->
 
    </form>
</body>
</html>

