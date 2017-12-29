<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m_01_F.aspx.cs" Inherits="m_01_F" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<title>成型相關</title>
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
</head>
<body>
    <form id="form1" runat="server">
    <div data-role="header" data-position="fixed" data-theme="b">
        <h1>成型相關</h1>
        <!--<a href="m_index.aspx" data-rel="back" data-icon="carat-l" data-iconpos="notext">Back</a>-->
        <!--<a href="#" onclick="window.location.reload()" data-icon="back" data-iconpos="notext">Refresh</a> -->
    </div><!-- /header -->
    <div>
		<ul data-role="listview" data-filter="true" data-filter-placeholder="Search items..." data-inset="true">
			<li><a href="m_02_F.aspx">0-產量曲線</a></li>
            <li><a href="m_03_F.aspx">1-達成率</a></li>
            <li><a href="m_04_F.aspx">2-每日產量</a></li>
		</ul>
	</div>
	<div data-role="footer" data-position="fixed" data-theme="b">
        <div data-role="navbar">
        <ul>
            <li><a href="#" onclick=" window.history.back()" data-icon="back" data-iconpos="top">Back</a></li>
            <li><a href="m_index.aspx" data-rel="home" data-icon="home" data-iconpos="top">home</a></li>
            <li><a href="#" onclick="window.location.reload()" data-icon="refresh" data-iconpos="top">Refresh</a></li>
        </ul>
        </div><!-- /navbar -->
    </div><!-- /footer -->
    </form>
</body>
</html>