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
        public string lista_;
        public string tipo_;
    }


    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string relacionEmbarque(string relacion)
    {
        List<relacion_info> info = new List<relacion_info>();

        string sql = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        string ConnString_qad = "DRIVER=DataDirect 7.1 OpenEdge Wire Protocol; HOST = 10.10.90.153; DSN=QADProd; PORT=48750; DB=custom; UID=mfg; PWD=mfg123; DIL=Read Uncommitted";
        string domain = "200PARPA";

        DataSet ds = new DataSet();
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(sql);
        com.Connection = con;
        com.CommandType = CommandType.Text;
        com.CommandText = string.Format(@"select 
        factura, validar
        from embarque where
        embarque=('{0}') order by validar asc", relacion);
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
                string factura2 = factura.Replace("-", "/");

                DataSet ds_qad2 = new DataSet();
                OdbcCommand cmd_qad2 = new OdbcCommand();
                OdbcConnection conn_qad2 = new OdbcConnection(ConnString_qad);
                cmd_qad2.Connection = conn_qad2;
                cmd_qad2.CommandType = CommandType.Text;
                cmd_qad2.CommandText = string.Format(@"
                    SELECT 
                    PUB.""lsd-det"".""lsd-domain"", 
                    PUB.""lsd-det"".""lsd-doc"", 
                    PUB.""lsd-det"".""lsd-folio""              
                    FROM PUB.""lsd-det"" 
                    WHERE ""lsd-domain"" = ('{0}')
                    AND ""lsd-doc"" = ('{1}')
                    WITH (NOLOCK)
                    ", domain, factura2);
                OdbcDataAdapter da_qad2 = new OdbcDataAdapter(cmd_qad2);
                conn_qad2.Open();
                cmd_qad2.ExecuteNonQuery();
                conn_qad2.Close();
                da_qad2.Fill(ds_qad2, "lsd_det");
                if (ds_qad2.Tables[0].Rows.Count > 0)
                {
                    string lista = ds_qad2.Tables[0].Rows[0]["lsd-folio"].ToString();
                    info.Add(new relacion_info()
                    {
                        factura_ = factura.ToString(),
                        validar_ = validar.ToString(),
                        lista_ = lista.ToString(),
                        tipo_ = "Ya cargado"

                    });
                }
            }
        }
        else
        {
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

                    DataSet ds_qad2 = new DataSet();
                    OdbcCommand cmd_qad2 = new OdbcCommand();
                    OdbcConnection conn_qad2 = new OdbcConnection(ConnString_qad);
                    cmd_qad2.Connection = conn_qad2;
                    cmd_qad2.CommandType = CommandType.Text;
                    cmd_qad2.CommandText = string.Format(@"
                    SELECT 
                    PUB.""lsd-det"".""lsd-domain"", 
                    PUB.""lsd-det"".""lsd-doc"", 
                    PUB.""lsd-det"".""lsd-folio""              
                    FROM PUB.""lsd-det"" 
                    WHERE ""lsd-domain"" = ('{0}')
                    AND ""lsd-doc"" = ('{1}')
                    WITH (NOLOCK)
                    ", dominio, factura1);
                    OdbcDataAdapter da_qad2 = new OdbcDataAdapter(cmd_qad2);
                    conn_qad2.Open();
                    cmd_qad2.ExecuteNonQuery();
                    conn_qad2.Close();
                    da_qad2.Fill(ds_qad2, "lsd_det");
                    if (ds_qad2.Tables[0].Rows.Count > 0)
                    {
                        string lista = ds_qad2.Tables[0].Rows[0]["lsd-folio"].ToString();

                        SqlCommand cmd_insert2 = new SqlCommand();
                        SqlConnection conn_insert2 = new SqlConnection(sql);
                        cmd_insert2.Connection = conn_insert2;
                        cmd_insert2.CommandType = CommandType.Text;
                        cmd_insert2.CommandText = string.Format(@"INSERT INTO 
                        EMBARQUE_DET
                        (DOMINIO, EMBARQUE, LISTA, FACTURA)
                        VALUES
                        ('{0}', '{1}', '{2}', '{3}')",
                        dominio, embarque, lista, factura1);
                        conn_insert2.Open();
                        cmd_insert2.ExecuteNonQuery();
                        conn_insert2.Close();

                        info.Add(new relacion_info()
                        {
                            factura_ = factura2.ToString(),
                            validar_ = "0",
                            lista_ = lista.ToString(),
                            tipo_ = "Se cargo"

                        });
                    }
                    else
                    {
                        info.Add(new relacion_info()
                        {
                            factura_ = factura2.ToString(),
                            validar_ = "0",
                            lista_ = "",
                            tipo_ = "Se cargo"

                        });
                    }
            }

            }
            else
            {

            }
        }
        
        ////////////////////////////////////////////////////////////////////////////////
        return new JavaScriptSerializer().Serialize(info);
    }


    public struct info_factura
    {
        public string caso_;
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string facturas(string folio, string embarque)
    {
       List<info_factura> info = new List<info_factura>();

        string sql = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        
        string fecha = DateTime.Now.ToString("dd/MM/yyyy");

        DataSet ds = new DataSet();
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(sql);
        com.Connection = con;
        com.CommandType = CommandType.Text;
        com.CommandText = string.Format(@"select factura from embarque_det where lista=('{0}') and embarque=('{1}')", folio, embarque);
        SqlDataAdapter adp = new SqlDataAdapter(com);
        con.Open();
        com.ExecuteNonQuery();
        con.Close();
        adp.Fill(ds, "ds");
        if (ds.Tables[0].Rows.Count > 0)
        {
            string factura = ds.Tables[0].Rows[0]["factura"].ToString();

            DataSet ds2 = new DataSet();
            SqlCommand com2 = new SqlCommand();
            SqlConnection con2 = new SqlConnection(sql);
            com2.Connection = con2;
            com2.CommandType = CommandType.Text;
            com2.CommandText = string.Format(@"
            update embarque_det 
            set status=('{0}'), fecha=('{1}')
            where 
            lista=('{2}') and embarque=('{3}')", "1", fecha, folio, embarque);
            SqlDataAdapter adp2 = new SqlDataAdapter(com2);
            con2.Open();
            com2.ExecuteNonQuery();
            con2.Close();
            string factura2 = factura.Replace("/", "-");
            DataSet ds3 = new DataSet();
            SqlCommand com3 = new SqlCommand();
            SqlConnection con3 = new SqlConnection(sql);
            com3.Connection = con3;
            com3.CommandType = CommandType.Text;
            com3.CommandText = string.Format(@"
            update embarque 
            set validar=('{0}'), fec_validar=('{1}')
            where 
            factura=('{2}') and embarque=('{3}')", "1", fecha, factura2, embarque);
            SqlDataAdapter adp3 = new SqlDataAdapter(com3);
            con3.Open();
            com3.ExecuteNonQuery();
            con3.Close();

            info.Add(new info_factura()
            {
                caso_ = "1"
            });

        }
        else
        {
            info.Add(new info_factura()
            {
                caso_ = "0"
            });
        }


        ////////////////////////////////////////////////////////////////////////////////
        return new JavaScriptSerializer().Serialize(info);
    }



    public struct info_cerrar
    {
        public string cerrado_;
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string cerrar(string embarque)
    {
        List<info_cerrar> info = new List<info_cerrar>();

        string sql = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

        string fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        DataSet ds = new DataSet();
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(sql);
        com.Connection = con;
        com.CommandType = CommandType.Text;
        com.CommandText = string.Format(@"select status from embarque_det where embarque=('{0}') and status='0'", embarque);
        SqlDataAdapter adp = new SqlDataAdapter(com);
        con.Open();
        com.ExecuteNonQuery();
        con.Close();
        adp.Fill(ds, "ds");
        if (ds.Tables[0].Rows.Count > 0)
        {

            info.Add(new info_cerrar()
            {
                cerrado_ = "1"
            });

        }
        else
        {
            DataSet ds2 = new DataSet();
            SqlCommand com2 = new SqlCommand();
            SqlConnection con2 = new SqlConnection(sql);
            com2.Connection = con2;
            com2.CommandType = CommandType.Text;
            com2.CommandText = string.Format(@"UPDATE embarque
            set validar = ('{0}'), fec_validar = ('{1}')
            WHERE embarque = ('{2}')", "1", fecha, embarque);
            SqlDataAdapter adp2 = new SqlDataAdapter(com2);
            con2.Open();
            com2.ExecuteNonQuery();
            con2.Close();

            info.Add(new info_cerrar()
            {
                cerrado_ = "0"
            });
        }


        ////////////////////////////////////////////////////////////////////////////////
        return new JavaScriptSerializer().Serialize(info);
    }

    /* UPDATE embarque
        set validar = ('{0}'), fec_validar = ('{1}')
        WHERE embarque = ('{2}')", */

}
 