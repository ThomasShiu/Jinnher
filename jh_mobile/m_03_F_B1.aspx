<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m_03_F_B1.aspx.cs" Inherits="m_03_F_B1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<title>B1-成型達成率</title>
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
        <h1>B1-成型達成率</h1>
        <!--<a href="m_index.aspx" data-rel="back" data-icon="carat-l" data-iconpos="notext">Back</a>-->
        <!--<a href="#" onclick="window.location.reload()" data-icon="back" data-iconpos="notext">Refresh</a> -->
    </div><!-- /header -->
   
  
   <div>
   <select name="SC_1" id="SC_1">
        <option value="B101">B101</option>
        <option value="B102">B102</option>
        <option value="B103">B103</option>
        <option value="B104">B104</option>
        <option value="B105">B105</option>
        <option value="B106">B106</option>
        <option value="B107">B107</option>
        <option value="B108">B108</option>
        <option value="B109">B109</option>
        <option value="B110">B110</option>
        <option value="B111">B111</option>
        <option value="B112">B112</option>
        <option value="B113">B113</option>
        <option value="B114">B114</option>
        <option value="B115">B115</option>
        <option value="B116">B116</option>
        <option value="B117">B117</option>
        <option value="B118">B118</option>
        <option value="B119">B119</option>
        <option value="B120">B120</option>
        <option value="B121">B121</option>
        <option value="B122">B122</option>
        <option value="B123">B123</option>
        <option value="B124">B124</option>
        <option value="B125">B125</option>
        <option value="B126">B126</option>
        <option value="B127">B127</option>
        <option value="B128">B128</option>
        <option value="B129">B129</option>
        <option value="B130">B130</option>
        <option value="B131">B131</option>
        <option value="B132">B132</option>
        <option value="B133">B133</option>
        <option value="B134">B134</option>
        <option value="B135">B135</option>
        <option value="B136">B136</option>
        <option value="B137">B137</option>
        <option value="B138">B138</option>
        <option value="B139">B139</option>

    </select>
   </div>
      
    
   <div >
         <label for="text-12">開始日期:</label>
          <asp:TextBox ID="TB_sdate" runat="server" ></asp:TextBox>
          <asp:CalendarExtender ID="TB_sdate_CalendarExtender" runat="server" 
                Enabled="True" TargetControlID="TB_sdate" DaysModeTitleFormat="yyyy,MMMM" 
                Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd"  CssClass="black" >
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
            <li data-role="list-divider" data-ajax="false">快速連結</li>
            <li><a href="m_03_F_B0.aspx" data-ajax="false">B0</a></li>
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


