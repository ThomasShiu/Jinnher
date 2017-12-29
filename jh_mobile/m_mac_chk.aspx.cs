using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Text.RegularExpressions;

public partial class m_mac_chk : System.Web.UI.Page
{
    string v_adr;
    protected void Page_Load(object sender, EventArgs e)
    {
        //var macs = from nic in NetworkInterface.GetAllNetworkInterfaces()
        //           where nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet
        //           select nic.GetPhysicalAddress().ToString();
        //Label1.Text = macs.ToString();

        //List<string> macList = new List<string>();
        //foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
        //{
        //    //if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet){
        //    macList.Add(nic.GetPhysicalAddress().ToString());
        //    DropDownList1.Items.Add(nic.GetPhysicalAddress().ToString() + nic.NetworkInterfaceType.ToString());
        //    //}
        //}

        //NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();   //取得所有網路介面類別(封裝本機網路資料)
        //foreach (NetworkInterface adapter in nics)
        //{
            
        //        //取得IPInterfaceProperties(可提供網路介面相關資訊)
        //        IPInterfaceProperties ipProperties = adapter.GetIPProperties();

        //        if (ipProperties.UnicastAddresses.Count > 0)
        //        {
        //            PhysicalAddress mac = adapter.GetPhysicalAddress();                     //取得Mac Address
        //            string name = adapter.Name;                                             //網路介面名稱
        //            string description = adapter.Description;                               //網路介面描述
        //            string ip = ipProperties.UnicastAddresses[0].Address.ToString();        //取得IP
        //            string netmask = ipProperties.UnicastAddresses[0].IPv4Mask.ToString();  //取得遮罩

        //            v_adr = string.Format("{0,13:S},{1,18:S},{2,18:S}", mac, ip, netmask);
        //            DropDownList1.Items.Add(v_adr);
        //        }
           
        //}         
        string v_remoteIP = Request.ServerVariables.Get("Remote_Addr").ToString();
        Response.Write(this.GetMac(v_remoteIP));
    }
    //獲取遠端用戶端的網卡物理位址（MAC）
    public string GetMac(string IP) //para IP is the client's IP
    {
        string dirResults = "";
        ProcessStartInfo psi = new ProcessStartInfo();
        Process proc = new Process();
        psi.FileName = "nbtstat";
        psi.RedirectStandardInput = false;
        psi.RedirectStandardOutput = true;
        psi.Arguments = "-A " + IP;
        psi.UseShellExecute = false;
        proc = Process.Start(psi);
        dirResults = proc.StandardOutput.ReadToEnd();
        proc.WaitForExit();
        dirResults = dirResults.Replace("\r", "").Replace("\n", "").Replace("\t", "");
        Regex reg = new Regex("Mac[ ]{0,}Address[ ]{0,}=[ ]{0,}(?<key>((.)*?))__MAC", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        Match mc = reg.Match(dirResults + "__MAC");
        if (mc.Success)
        {
            return mc.Groups["key"].Value;
        }
        else
        {
            reg = new Regex("Host not found", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            mc = reg.Match(dirResults);
            if (mc.Success)
            {
                return "Host not found!";
            }
            else
            {
                return "";
            }
        }
    }
    
}