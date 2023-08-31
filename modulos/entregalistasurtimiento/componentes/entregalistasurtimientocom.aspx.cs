using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Globalization;

public partial class modulos_entregalistasurtimiento_componentes_entregalistasurtimientocom : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public struct almacenista_info
    {
        public string almacenista_;
        public string id_almacenista_;
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string almacenistas()
    {
        string meta4 = ConfigurationManager.ConnectionStrings["pnet8prodConnectionString"].ConnectionString;
        List<almacenista_info> info = new List<almacenista_info>();

        //OPERADOR//
        DataSet ds = new DataSet();
        SqlCommand sql = new SqlCommand();
        SqlConnection con = new SqlConnection(meta4);
        sql.Connection = con;
        sql.CommandType = CommandType.Text;
        sql.CommandText = string.Format(@"
        SELECT PER.STD_ID_HR as ID, PE.SCO_GB_NAME as NOMBRE
        FROM STD_HR_PERIOD PER
        LEFT JOIN STD_PERSON PE ON (PE.STD_ID_PERSON = PER.STD_ID_HR)
        LEFT JOIN M4SCO_HR_ROLE ROL ON (ROL.SCO_ID_HR = PER.STD_ID_HR AND ROL.SCO_OR_HR_PER = PER.STD_OR_HR_PERIOD AND ROL.SCO_DT_END = PER.STD_DT_END)
        LEFT JOIN M4SAR_H_HR_C_COSTO CC ON (CC.SCO_ID_HR = ROL.SCO_ID_HR AND CC.SCO_OR_HR_ROLE = ROL.SCO_OR_HR_ROLE AND CC.DT_END = ROL.SCO_DT_END)
        LEFT JOIN M4SSP_CENTR_COSTO NCC ON (NCC.SSP_ID_CENT_COSTO = CC.SSP_ID_CENT_COSTO)
        WHERE PER.STD_DT_END = '01/01/4000' AND CC.SSP_ID_CENT_COSTO = '1255' order by PER.STD_ID_HR asc ");
        SqlDataAdapter da = new SqlDataAdapter(sql);
        con.Open();
        sql.ExecuteNonQuery();
        con.Close();
        da.Fill(ds, "ds");
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string almacenista_ = ds.Tables[0].Rows[i]["NOMBRE"].ToString();
                string id_almacenista = ds.Tables[0].Rows[i]["ID"].ToString();

                info.Add(new almacenista_info()
                {
                    almacenista_ = id_almacenista.ToString() + " " + almacenista_.ToString(),
                    id_almacenista_ = id_almacenista.ToString()
                });
            }

        }

        else
        {
            info.Add(new almacenista_info()
            {
                almacenista_ = "0"
            });
        }

        ////////////////////////////////////////////////////////////////////////////////
        return new JavaScriptSerializer().Serialize(info);
    }


    public struct info_entrega
    {
        public string almacenista_;
        public string id_almacenista_;
        public string existencia_;
        public string fecha_;
        public string factura_;
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string entregalista(string lista, string almacenista)
    {
        string meta4 = ConfigurationManager.ConnectionStrings["pnet8prodConnectionString"].ConnectionString;
        string sql_con = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        string qad = "DRIVER=DataDirect 7.1 OpenEdge Wire Protocol; HOST = 10.10.90.153; DSN=QADProd; PORT=48750; DB=custom; UID=mfg; PWD=mfg123; DIL=READ";

        List<info_entrega> info = new List<info_entrega>();

        string fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        string dominio = "200PARPA";

        DataSet ds = new DataSet();
        OdbcCommand com = new OdbcCommand();
        OdbcConnection con = new OdbcConnection(qad);
        com.Connection = con;
        com.CommandType = CommandType.Text;
        com.CommandText = string.Format(@"SELECT 
            ""cbd-inv-nbr"" FROM PUB.""cbd-det""
			WHERE ""cbd--chr02"" = ('{0}') and ""cbd-domain"" = ('{1}') group by ""cbd-inv-nbr"" WITH (NOLOCK) ", lista, dominio);
        OdbcDataAdapter adp = new OdbcDataAdapter(com);
        con.Open();
        com.ExecuteNonQuery();
        con.Close();
        adp.Fill(ds, "ds");
        if (ds.Tables[0].Rows.Count > 0)
        {
            string factura = ds.Tables[0].Rows[0]["cbd-inv-nbr"].ToString();
            DataSet ds2 = new DataSet();
            SqlCommand com2 = new SqlCommand();
            SqlConnection con2 = new SqlConnection(sql_con);
            com2.Connection = con2;
            com2.CommandType = CommandType.Text;
            com2.CommandText = string.Format(@"
            insert into 
            Listas_surtimiento 
            (Folio, Factura, Id_operador, Fecha_inicio)
             VALUES
            ('{0}','{1}','{2}','{3}')", lista, factura, almacenista, fecha);
            SqlDataAdapter adp2 = new SqlDataAdapter(com2);
            con2.Open();
            com2.ExecuteNonQuery();
            con2.Close();


            DataSet ds3 = new DataSet();
            SqlCommand sql3 = new SqlCommand();
            SqlConnection con3 = new SqlConnection(meta4);
            sql3.Connection = con3;
            sql3.CommandType = CommandType.Text;
            sql3.CommandText = string.Format(@"
        SELECT PER.STD_ID_HR as ID, PE.SCO_GB_NAME as NOMBRE
        FROM STD_HR_PERIOD PER
        LEFT JOIN STD_PERSON PE ON (PE.STD_ID_PERSON = PER.STD_ID_HR)
        LEFT JOIN M4SCO_HR_ROLE ROL ON (ROL.SCO_ID_HR = PER.STD_ID_HR AND ROL.SCO_OR_HR_PER = PER.STD_OR_HR_PERIOD AND ROL.SCO_DT_END = PER.STD_DT_END)
        LEFT JOIN M4SAR_H_HR_C_COSTO CC ON (CC.SCO_ID_HR = ROL.SCO_ID_HR AND CC.SCO_OR_HR_ROLE = ROL.SCO_OR_HR_ROLE AND CC.DT_END = ROL.SCO_DT_END)
        LEFT JOIN M4SSP_CENTR_COSTO NCC ON (NCC.SSP_ID_CENT_COSTO = CC.SSP_ID_CENT_COSTO)
        WHERE PER.STD_DT_END = '01/01/4000' AND CC.SSP_ID_CENT_COSTO = '1255' order by PER.STD_ID_HR asc ");
            SqlDataAdapter da3 = new SqlDataAdapter(sql3);
            con3.Open();
            sql3.ExecuteNonQuery();
            con3.Close();
            da3.Fill(ds3, "ds");
            if (ds3.Tables[0].Rows.Count > 0)
            {
              
                    string almacenista_ = ds3.Tables[0].Rows[0]["NOMBRE"].ToString();
                    string id_almacenista = ds3.Tables[0].Rows[0]["ID"].ToString();

                    info.Add(new info_entrega()
                    {
                        existencia_ = "1",
                        almacenista_ = id_almacenista.ToString() + " " + almacenista_.ToString(),
                        id_almacenista_ = id_almacenista.ToString(),
                        fecha_ = fecha.ToString(),
                        factura_ = factura.ToString()
                    });
            }

            else
            {
                info.Add(new info_entrega()
                {
                    existencia_ = "1",
                    almacenista_ = "0",
                    fecha_ = fecha.ToString(),
                    factura_ = factura.ToString()
                });
            }

         
        }
        else
        {
            info.Add(new info_entrega()
            {
                existencia_ = "0"
            });
        }

     
        ////////////////////////////////////////////////////////////////////////////////
        return new JavaScriptSerializer().Serialize(info);
    }

}