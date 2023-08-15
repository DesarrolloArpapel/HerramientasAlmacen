using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Globalization;


public partial class modulos_embarque_embarqueporsuper_embarqueporsuper : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public struct relacion_info
    {
        public string factura_;
        public string validar_;
        public string fecha_inicio_;
        public string fecha_fin_;
    }


    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string relacionEmbarque(string relacion)
    {
        //string meta4 = ConfigurationManager.ConnectionStrings["pnet8prodConnectionString"].ConnectionString;
        List<relacion_info> info = new List<relacion_info>();

        string sql = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

        DataSet ds = new DataSet();
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(sql);
        com.Connection = con;
        com.CommandType = CommandType.Text;
        com.CommandText = string.Format(@"select 
        factura, validar, fecha_inicio, fecha_fin
        from embarque where
        embarque=('{0}')", relacion);
        SqlDataAdapter adp = new SqlDataAdapter(com);
        con.Open();
        com.ExecuteNonQuery();
        con.Close();
        adp.Fill(ds, "ds");
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string factura = ds.Tables[0].Rows[i]["factura"].ToString();
                string validar = ds.Tables[0].Rows[i]["validar"].ToString();
                string fecha_inicio = ds.Tables[0].Rows[i]["fecha_inicio"].ToString();
                string fecha_fin = ds.Tables[0].Rows[i]["fecha_fin"].ToString();


                info.Add(new relacion_info()
                {
                    factura_ = factura.ToString(),
                    validar_ = validar.ToString(),
                    fecha_inicio_ = fecha_inicio.ToString(),
                    fecha_fin_ = fecha_fin.ToString()
                });
            }
        }
        else
        {
            string ConnString_qad = "DRIVER=DataDirect 7.1 OpenEdge Wire Protocol; HOST = 10.10.90.153; DSN=QADProd; PORT=48750; DB=custom; UID=mfg; PWD=mfg123; DIL=Read Uncommitted";

            string domain = "200PARPA";

            DataSet ds_qad = new DataSet();
            OdbcCommand cmd_qad = new OdbcCommand();
            OdbcConnection conn_qad = new OdbcConnection(ConnString_qad);
            cmd_qad.Connection = conn_qad;
            cmd_qad.CommandType = CommandType.Text;
            cmd_qad.CommandText = string.Format(@"
                SELECT 
                PUB.""vjd-det"".""vjd-domain"", 
                PUB.""vjd-det"".""vjd-nbr"", 
                PUB.""vjd-det"".""vjd-inv-nbr"",
                PUB.""vjd-det"".""vjd--chr02""
                FROM PUB.""vjd-det"" 
                WHERE ""vjd-nbr""= ('{0}')
                AND ""vjd-domain""= ('{1}')
                WITH (NOLOCK)
                ", relacion, domain);
            OdbcDataAdapter da_qad = new OdbcDataAdapter(cmd_qad);
            conn_qad.Open();
            cmd_qad.ExecuteNonQuery();
            conn_qad.Close();
            da_qad.Fill(ds_qad, "VJD-DET");
            if (ds_qad.Tables[0].Rows.Count > 0)
            {
              for (int i = 0; i < ds_qad.Tables[0].Rows.Count; i++)
              {
                    string factura1 = ds_qad.Tables[0].Rows[i]["VJD-INV-NBR"].ToString();
                    string factura2 = factura1.Replace("/", "-");
                    string dominio = ds_qad.Tables[0].Rows[i]["VJD-DOMAIN"].ToString();
                    string embarque = ds_qad.Tables[0].Rows[i]["VJD-NBR"].ToString();
                    string factura = factura2;
                     
                    SqlCommand cmd_insert = new SqlCommand();
                    SqlConnection conn_insert = new SqlConnection(sql);
                    cmd_insert.Connection = conn_insert;
                    cmd_insert.CommandType = CommandType.Text;
                    cmd_insert.CommandText = string.Format(@"
                    INSERT INTO EMBARQUE(DOMINIO, EMBARQUE, FACTURA)
                    VALUES('{0}', '{1}', '{2}')", dominio, embarque, factura);
                    conn_insert.Open();
                    cmd_insert.ExecuteNonQuery();
                    conn_insert.Close();

                    DataSet ds_qad_ = new DataSet();
                    OdbcCommand cmd_qad_ = new OdbcCommand();
                    OdbcConnection conn_qad_ = new OdbcConnection(ConnString_qad);
                    cmd_qad_.Connection = conn_qad_;
                    cmd_qad_.CommandType = CommandType.Text;
                    cmd_qad_.CommandText = string.Format(@"
                    SELECT 
                    PUB.""lsd-det"".""lsd-domain"", 
                    PUB.""lsd-det"".""lsd-doc"", 
                    PUB.""lsd-det"".""lsd-folio""              
                    FROM PUB.""lsd-det"" 
                    WHERE ""lsd-domain""= ('{0}')
                    AND ""lsd-doc"" IN ({1})
                    WITH (NOLOCK)
                    ", dominio, factura1);
                    OdbcDataAdapter da_qad_ = new OdbcDataAdapter(cmd_qad_);
                    conn_qad_.Open();
                    cmd_qad_.ExecuteNonQuery();
                    conn_qad_.Close();
                    da_qad_.Fill(ds_qad_, "lsd_det");
                    if (ds_qad_.Tables[0].Rows.Count > 0)
                    {
                        string lista = ds_qad_.Tables[0].Rows[0]["lsd-folio"].ToString();

                        SqlCommand cmd_insert2 = new SqlCommand();
                        SqlConnection conn_insert2 = new SqlConnection(sql);
                        cmd_insert2.Connection = conn_insert2;
                        cmd_insert2.CommandType = CommandType.Text;
                        cmd_insert2.CommandText = string.Format(@"INSERT INTO 
                        EMBARQUE_DET
                        (DOMINIO, EMBARQUE, LISTA, FACTURA)
                        VALUES
                        ('{0}', '{1}', '{2}', '{3}')",
                        dominio, embarque, lista, factura);
                        conn_insert2.Open();
                        cmd_insert2.ExecuteNonQuery();
                        conn_insert2.Close();
                    }

                    info.Add(new relacion_info()
                    {
                        factura_ = factura2.ToString(),
                        validar_ = "0",
                        fecha_inicio_ = "",
                        fecha_fin_ = ""
                    });
            }

            }
            else
            {

            }
           

        }
        
        ////////////////////////////////////////////////////////////////////////////////
        return new JavaScriptSerializer().Serialize(info);
    }


}