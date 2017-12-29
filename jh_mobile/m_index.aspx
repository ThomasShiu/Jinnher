<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m_index.aspx.cs" Inherits="mobile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<title>JH行動戰情</title>
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
<body bgcolor="Gray">
    <form id="form1" runat="server">
    <div data-role="header" data-position="fixed" data-theme="b">
        <h1>JH行動戰情</h1>
    </div><!-- /header -->

    <div  data-theme="c">
		<ul data-role="listview" data-filter="true" data-filter-placeholder="Search items..." data-inset="true">
			<li><a href="m_01_W.aspx">1-盤元線材</a></li>
            <li><a href="m_01_F.aspx">2-成型產能</a></li>
			<li><a href="m_01_SAL.aspx">3-業務相關</a></li>
			<li><a href="#">4-輾牙</a></li>
			<li><a href="#">5-熱處理</a></li>
			<li><a href="#">6-電鍍</a></li>
            <li><a href="#">7-包裝</a></li>
		</ul>
	</div>

<div data-role="footer" data-position="fixed" data-theme="b">
<a href="#" class="ui-btn-left ui-btn ui-btn-inline ui-mini ui-corner-all ui-btn-icon-left ui-icon-mail">Mail</a>
<span class="ui-title">JH 資訊室 #115</span>
</div><!-- /footer -->
    </form>
</body>
</html>
