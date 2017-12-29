<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Download.aspx.cs" Inherits="Download" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>新版本下載 New Version Release</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no">
    <meta http-equiv="content-type" content="text/html; charset=utf-8">
	<meta name="author" content="Goodat">
	<meta name="keywords" content="">
	<meta name="description" content="">
	<link rel="stylesheet" type="text/css" href="css/screen.css">
	<link rel="stylesheet" type="text/css" href="css/dropdown.css">
 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:DataList ID="DataList1" runat="server" DataSourceID="SDS_notifi" >
                    <ItemTemplate>

                     <p>世宇斑馬APP 下載更新</p>
		<fieldset class="login">
			<legend>APK Download Details</legend>
			<div>
				<label for="username">載點 Download Link:</label>
            </div>
            <div> 
                <label2><asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Bind("APK") %>'  NavigateUrl='<%# "download/"+Eval("APK").ToString() %>'/></label2>
			</div>
            <div> 
                 <a href='<%# "download/"+Eval("APK").ToString() %>' style="border:0">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/image/download-button.png"  Width="150"/>
                </a>
            </div>
			<div>
				<label for="password">版本 Version:</label> 
            </div>
            <div>
                <label2><asp:Label ID="VERSIONLabel" runat="server" Text='<%# Eval("VERSION") %>' /></label2>
			</div>
			<div>
			   <label >更新說明 Descript:</label>
            </div>
            <div>
                <label2><asp:Label ID="DESCRIPTLabel" runat="server" 
                        Text='<%# ((string)Eval("DESCRIPT")).Replace("\n", "<br/>") %>'  /></label3> 
			</div>
		</fieldset>
                    </ItemTemplate>
                </asp:DataList>
                <div>
                    其他應用下載 APP DOWNLOAD
                </div>
                    <div>
                     <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/download/app_lock.apk">APP LOCK</asp:HyperLink>
                    </div>
                    <div>
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/download/es_file_explorer.apk">ES FILE</asp:HyperLink>
                    </div>
                <asp:SqlDataSource ID="SDS_notifi" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:jheip %>" 
                    ProviderName="<%$ ConnectionStrings:jheip.ProviderName %>">
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
