using System;
using System.Data.OracleClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;

public partial class SAL_packing_list : System.Web.UI.Page
{
    thomas_function tsfun = new thomas_function();
    thomas_Conn tsconn = new thomas_Conn();
    OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["jh815"].ConnectionString);
    //OracleDataReader dr;

    string Sql_str;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Sql_str = "SELECT B1.LOT_NO,B1.HEAT_NO, A.PAC_NO,A.DESCRPT_E ";
        Sql_str += "     ,WMSYS.WM_CONCAT(A.PLT1) PLT1,A.PDT_SIZE,SUM(B1.LOT_PK_QTY)  SUM_PK_QTY ";
        Sql_str += "FROM  ";
        Sql_str += "( ";
        Sql_str += "    SELECT pac_no,job_no,odr_item_odr_po_no,odr_item_pdt_cat_cat,odr_item_pdt_dia,odr_item_pdt_length,SEQUENCE ";
        Sql_str += "           ,odr_item_pdt_thread,odr_item_pdt_fin_fin_id,tctn,pk_qty,pac_id,/*mixed,*/weight,descrpt_e,pdt_size ";
        Sql_str += "           ,stndrd_wt,weight,co_co_id,pack_code,qtyperctn,price,cust_prt_no,port,pk_shpmrk3,port_mix,smlqty ";
        Sql_str += "           ,mic,po_no,tarriff_code,cst_prt_no,unit,ctn2,ctn1,plt2,plt1,pk_plt ";
        Sql_str += "           ,SUM(PK_QTY)/SF_GET_ASSEMB_NO(ODR_ITEM_PDT_CAT_CAT, ODR_ITEM_PDT_DIA, ODR_ITEM_PDT_LENGTH, ODR_ITEM_PDT_THREAD) SUM_PK_QTY ";
        Sql_str += "      FROM ( ";
        Sql_str += "            SELECT ";
        Sql_str += "             K.PAC_NO,M.ODR_ITEM_ODR_PO_NO,M.JOB_NO ";
        Sql_str += "            ,M.ODR_ITEM_PDT_CAT_CAT,M.ODR_ITEM_PDT_DIA        ";
        Sql_str += "            ,M.ODR_ITEM_PDT_LENGTH ,M.ODR_ITEM_PDT_THREAD     ";
        Sql_str += "            ,M.ODR_ITEM_PDT_FIN_FIN_ID ,M.TCTN ";
        Sql_str += "            ,SUM(M.PK_QTY) pk_qty,i.smlqty ";
        Sql_str += "            ,M.PAC_ID,D.WEIGHT,A.DESCRPT_E,D.PDT_SIZE ";
        Sql_str += "            ,D.STNDRD_WT,O.CO_CO_ID,I.PACK_CODE, I.CUST_PRT_NO ";
        Sql_str += "            ,I.PRICE,k.port ,k.ship_mark3  pk_shpmrk3 ";
        Sql_str += "            ,k.port_mix,O.MIC,I.SEQUENCE,O.PO_NO ";
        Sql_str += "            ,nvl(o.TARRIFF_CODE,'N') TARRIFF_CODE ";
        Sql_str += "            ,O.CST_PRT_NO,O.UNIT,ctn2,ctn1,plt2,plt1 ";
        Sql_str += "            ,COUNT(PLT1) pk_plt ";
        Sql_str += "            ,(M.QTYPERCTN/nvl(sf_get_value_nox2('assembs','go_with','cat_cat',m.odr_item_pdt_cat_cat,'dia',m.odr_item_pdt_dia,'length',m.odr_item_pdt_length,'thread',m.odr_item_pdt_thread,'asm_cat',m.d_cat,'asm_dia',m.d_dia,'asm_length',m.d_len,'asm_thread',m.d_th),1)) qtyperctn ";
        Sql_str += "              FROM  PRODUCTS D,CATEGORIES A,ORDERITEMS I,listitems M,ORDERS O,PACKINGS K ";
        Sql_str += "              WHERE k.pac_no BETWEEN 'JH11120812' AND 'JH11120812' ";
        Sql_str += "                AND M.PAC_ID = K.PAC_ID ";
        Sql_str += "                AND D.CAT_CAT =M.ODR_ITEM_PDT_CAT_CAT ";
        Sql_str += "                AND D.DIA=M.ODR_ITEM_PDT_DIA  ";
        Sql_str += "                AND D.LENGTH=M.ODR_ITEM_PDT_LENGTH  ";
        Sql_str += "                AND D.THREAD=M.ODR_ITEM_PDT_THREAD  ";
        Sql_str += "                AND O.PO_NO=M.ODR_ITEM_ODR_PO_NO  ";
        Sql_str += "                AND I.ODR_PO_NO=M.ODR_ITEM_ODR_PO_NO  ";
        Sql_str += "                AND I.PDT_CAT_CAT=M.ODR_ITEM_PDT_CAT_CAT  ";
        Sql_str += "                AND I.PDT_DIA=M.ODR_ITEM_PDT_DIA  ";
        Sql_str += "                AND I.PDT_LENGTH=M.ODR_ITEM_PDT_LENGTH  ";
        Sql_str += "                AND I.PDT_THREAD=M.ODR_ITEM_PDT_THREAD  ";
        Sql_str += "                AND I.PDT_FIN_FIN_ID =M.ODR_ITEM_PDT_FIN_FIN_ID ";
        Sql_str += "                AND A.CAT=D.CAT_CAT  ";
        Sql_str += "              GROUP BY  K.PAC_NO ";
        Sql_str += "            ,M.ODR_ITEM_ODR_PO_NO ";
        Sql_str += "            ,M.JOB_NO ";
        Sql_str += "            ,M.ODR_ITEM_PDT_CAT_CAT ";
        Sql_str += "            ,M.ODR_ITEM_PDT_DIA        ";
        Sql_str += "            ,M.ODR_ITEM_PDT_LENGTH     ";
        Sql_str += "            ,M.ODR_ITEM_PDT_THREAD     ";
        Sql_str += "            ,M.ODR_ITEM_PDT_FIN_FIN_ID ";
        Sql_str += "            ,M.PAC_ID ";
        Sql_str += "            ,D.WEIGHT ";
        Sql_str += "            ,A.DESCRPT_E ";
        Sql_str += "            ,D.PDT_SIZE ";
        Sql_str += "            ,D.STNDRD_WT,D.WEIGHT ";
        Sql_str += "            ,O.CO_CO_ID,M.QTYPERCTN,I.PACK_CODE, I.CUST_PRT_NO ";
        Sql_str += "            ,I.PRICE ";
        Sql_str += "            ,k.port ,i.smlqty ";
        Sql_str += "            ,k.ship_mark3 ";
        Sql_str += "            ,k.port_mix ";
        Sql_str += "            ,O.MIC,I.SEQUENCE ";
        Sql_str += "            ,O.PO_NO ";
        Sql_str += "            ,o.TARRIFF_CODE ";
        Sql_str += "            ,sf_get_jit_temp_po(M.JOB_NO) ";
        Sql_str += "            ,O.CST_PRT_NO,O.UNIT ";
        Sql_str += "            ,M.TCTN ,M.PK_QTY ";
        Sql_str += "            ,m.d_cat,m.d_dia,m.d_len,m.d_th,ctn2,ctn1,plt2,plt1 ";
        Sql_str += "    ) ";
        Sql_str += "    GROUP BY pac_no,job_no,odr_item_odr_po_no,odr_item_pdt_cat_cat,odr_item_pdt_dia,odr_item_pdt_length ";
        Sql_str += "           ,odr_item_pdt_thread,odr_item_pdt_fin_fin_id,tctn,pk_qty,pac_id,weight,descrpt_e,pdt_size ";
        Sql_str += "           ,stndrd_wt,weight,co_co_id,pack_code,qtyperctn,price,cust_prt_no,port,pk_shpmrk3,port_mix ";
        Sql_str += "           ,mic,po_no,tarriff_code,cst_prt_no,unit,pk_plt,SEQUENCE,smlqty,ctn2,ctn1,plt2,plt1 ";
        Sql_str += "       )  A , ";
        Sql_str += "      ( ";
        Sql_str += "      SELECT  LISTITEMS.LOT_NO,  ";
        Sql_str += "        LISTITEMS.HEAT_NO, SUM(LISTITEMS.PK_QTY)/SF_GET_ASSEMB_NO(LISTITEMS.ODR_ITEM_PDT_CAT_CAT, LISTITEMS.ODR_ITEM_PDT_DIA, LISTITEMS.ODR_ITEM_PDT_LENGTH, LISTITEMS.ODR_ITEM_PDT_THREAD)  lot_pk_qty, LISTITEMS.PAC_ID,  ";
        Sql_str += "        LISTITEMS.ODR_ITEM_ODR_PO_NO, LISTITEMS.ODR_ITEM_PDT_CAT_CAT, LISTITEMS.ODR_ITEM_PDT_DIA,  ";
        Sql_str += "        LISTITEMS.ODR_ITEM_PDT_LENGTH, LISTITEMS.ODR_ITEM_PDT_THREAD,  ";
        Sql_str += "        LISTITEMS.ODR_ITEM_PDT_FIN_FIN_ID, LISTITEMS.CTRL_LOT_NO, ";
        Sql_str += "        LISTITEMS.QTYPERCTN,LISTITEMS.PLT1,LISTITEMS.CTN1  ";
        Sql_str += "        FROM  LISTITEMS ";
        Sql_str += "        WHERE LISTITEMS.PAC_ID= 'SHP123515' ";
        Sql_str += "        GROUP BY LISTITEMS.LOT_NO, LISTITEMS.HEAT_NO, LISTITEMS.PAC_ID,  ";
        Sql_str += "        LISTITEMS.ODR_ITEM_ODR_PO_NO, LISTITEMS.ODR_ITEM_PDT_CAT_CAT, LISTITEMS.ODR_ITEM_PDT_DIA,  ";
        Sql_str += "        LISTITEMS.ODR_ITEM_PDT_LENGTH, LISTITEMS.ODR_ITEM_PDT_THREAD,  ";
        Sql_str += "        LISTITEMS.ODR_ITEM_PDT_FIN_FIN_ID, LISTITEMS.CTRL_LOT_NO, ";
        Sql_str += "        LISTITEMS.QTYPERCTN,LISTITEMS.LOT_NO,LISTITEMS.HEAT_NO,LISTITEMS.PLT1,LISTITEMS.CTN1 ";
        Sql_str += "        order by  LISTITEMS.PAC_ID,  ";
        Sql_str += "        LISTITEMS.ODR_ITEM_ODR_PO_NO, LISTITEMS.ODR_ITEM_PDT_CAT_CAT, LISTITEMS.ODR_ITEM_PDT_DIA,  ";
        Sql_str += "        LISTITEMS.ODR_ITEM_PDT_LENGTH, LISTITEMS.ODR_ITEM_PDT_THREAD,  ";
        Sql_str += "        LISTITEMS.ODR_ITEM_PDT_FIN_FIN_ID ";
        Sql_str += "      ) B1   ";
        Sql_str += " WHERE A.ODR_ITEM_ODR_PO_NO=B1.ODR_ITEM_ODR_PO_NO ";
        Sql_str += " AND A.ODR_ITEM_PDT_CAT_CAT=B1.ODR_ITEM_PDT_CAT_CAT ";
        Sql_str += " AND A.ODR_ITEM_PDT_DIA=B1.ODR_ITEM_PDT_DIA ";
        Sql_str += " AND A.ODR_ITEM_PDT_LENGTH=B1.ODR_ITEM_PDT_LENGTH ";
        Sql_str += " AND A.ODR_ITEM_PDT_THREAD=B1.ODR_ITEM_PDT_THREAD ";
        Sql_str += " AND A.ODR_ITEM_PDT_FIN_FIN_ID=B1.ODR_ITEM_PDT_FIN_FIN_ID ";
        Sql_str += " AND A.QTYPERCTN=B1.QTYPERCTN ";
        Sql_str += " AND A.PLT1 = B1.PLT1 ";
        Sql_str += " AND A.CTN1 = B1.CTN1 ";
        Sql_str += " GROUP BY B1.LOT_NO,B1.HEAT_NO ,A.PAC_NO,A.DESCRPT_E,A.PDT_SIZE   ";
        try
        {
            //message.Text = "";
            SDS_PackingList.SelectCommand = Sql_str;
            //ReportViewer1.LocalReport.ReportPath = "report/R_BBI_TO_SHIP2.rdlc";
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("R_ds_packlist", SDS_PackingList));
            ReportViewer1.LocalReport.Refresh();

            //Warning[] warnings;
            //string[] streamids;
            //string mimeType;
            //string encoding;
            //string extension;

            //byte[] bytes = ReportViewer1.LocalReport.Render(
            // "Excel", null, out mimeType, out encoding, out extension,
            //  out streamids, out warnings);

            //Response.Clear();
            //Response.AddHeader("Content-Disposition", "attachment; filename=Anetwt.xls");
            //Response.AddHeader("Content-Length", bytes.Length.ToString());
            //Response.ContentType = "application/octet-stream";
            //Response.OutputStream.Write(bytes, 0, bytes.Length);

            save_log("Query-A棟產能查詢");
        }
        catch (Exception ex)
        {
            message.Text = ex.ToString();
            save_log(ex.ToString());
        }
    }

    protected void save_log(string v_mess)
    {
        //取得程式名稱
        string filename = System.IO.Path.GetFileName(Request.PhysicalPath).Replace(".aspx", "");

        //紀錄LOG
        string strClientIP = Context.Request.ServerVariables["REMOTE_ADDR"];
        tsconn.save_log("Guest", filename, strClientIP, v_mess);
    }
}