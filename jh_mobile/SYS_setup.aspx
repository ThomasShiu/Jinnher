<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SYS_setup.aspx.cs" Inherits="SYS_setup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系統設定</title>
    <link href="Styles/jquery-ui-1.8.13.custom.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.13.custom.min.js" type="text/javascript"></script>
    <script>
        $(function () {
            $("#tabs").tabs();
        });
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="tabs">
	<ul>
		<li><a href="#tabs-1">TAB_1</a></li>
		<li><a href="#tabs-2">TAB_2</a></li>
		<li><a href="#tabs-3">TAB_3</a></li>
        <li><a href="#tabs-4">TAB_4</a></li>
	</ul>
	<div id="tabs-1">
        <asp:HyperLink ID="HyperLink1" runat="server">HyperLink</asp:HyperLink>
	</div>
	<div id="tabs-2">
        <asp:HyperLink ID="HyperLink2" runat="server">HyperLink</asp:HyperLink>
	</div>
	<div id="tabs-3">
        <asp:HyperLink ID="HyperLink3" runat="server">HyperLink</asp:HyperLink>
	</div>
    <div id="tabs-4">
        <asp:HyperLink ID="HyperLink4" runat="server">HyperLink</asp:HyperLink>
	</div>
</div>
    </div>
    </form>
</body>
</html>
