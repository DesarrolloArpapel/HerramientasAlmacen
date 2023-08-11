using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Globalization;

public partial class modulos_listaSurtimiento_listasurtimiento : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public struct lista_info
    {
        public string almacenista_;
        public string id_almacenista_;
        public string cajas_;
        public string factura_;
        public string cajas2_;
    }



    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string lista_surtimiento(string folio)
    {
        //string meta4 = ConfigurationManager.ConnectionStrings["pnet8prodConnectionString"].ConnectionString;
        List<lista_info> info = new List<lista_info>();

        string sql = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

        DataSet ds = new DataSet();
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(sql);
        com.Connection = con;
        com.CommandType = CommandType.Text;
        com.CommandText = string.Format(@"select 
        id_operador, count(folio) as cajas
        from validar_embarque where
        folio=('{0}') and id_operador is not null group by id_operador", folio);
        SqlDataAdapter adp = new SqlDataAdapter(com);
        con.Open();
        com.ExecuteNonQuery();
        con.Close();
        adp.Fill(ds, "ds");
        if(ds.Tables[0].Rows.Count > 0)
        {
            
            for (int i = 0; i< ds.Tables[0].Rows.Count; i++)
            {
                string id = ds.Tables[0].Rows[i]["id_operador"].ToString();
                string cajas = ds.Tables[0].Rows[i]["cajas"].ToString();
                DataSet ds2 = new DataSet();
                SqlCommand com2 = new SqlCommand();
                SqlConnection con2 = new SqlConnection(sql);
                com2.Connection = con2;
                com2.CommandType = CommandType.Text;
                com2.CommandText = string.Format(@"select 
                count(folio) as cajas, factura
                from validar_embarque where
                folio=('{0}') group by factura", folio);
                SqlDataAdapter adp2 = new SqlDataAdapter(com2);
                con2.Open();
                com2.ExecuteNonQuery();
                con2.Close();
                adp2.Fill(ds2, "ds");
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    string factura = ds2.Tables[0].Rows[0]["factura"].ToString();
                    string cajas2 = ds2.Tables[0].Rows[0]["cajas"].ToString();

                    string m4 = ConfigurationManager.ConnectionStrings["pnet8prodConnectionString"].ConnectionString;

                    DataSet ds_m = new DataSet();
                    SqlCommand com_m = new SqlCommand();
                    SqlConnection con_m = new SqlConnection(m4);
                    com_m.Connection = con_m;
                    com_m.CommandType = CommandType.Text;
                    com_m.CommandText = string.Format(@"select 
                    STD_N_FIRST_NAME as nombre, STD_N_FAM_NAME_1 as apellido, STD_N_MAIDEN_NAME as apellido2
                    from STD_PERSON where STD_ID_PERSON=('{0}')", id);
                    SqlDataAdapter adp_m = new SqlDataAdapter(com_m);
                    con_m.Open();
                    com_m.ExecuteNonQuery();
                    con_m.Close();
                    adp_m.Fill(ds_m, "ds_m");
                    if(ds_m.Tables[0].Rows.Count > 0)
                    {
                        string nombre = ds_m.Tables[0].Rows[0]["nombre"].ToString() +" "+ ds_m.Tables[0].Rows[0]["apellido"].ToString() +" "+ ds_m.Tables[0].Rows[0]["apellido2"].ToString();


                        info.Add(new lista_info()
                        {
                            almacenista_ = nombre.ToString(),
                            id_almacenista_ = id.ToString(),
                            cajas_ = cajas.ToString(),
                            factura_ = factura.ToString(),
                            cajas2_ = cajas2.ToString()
                        });


                    }
                    else
                    {
                        info.Add(new lista_info()
                        {
                            id_almacenista_ = id.ToString(),
                            cajas_ = cajas.ToString(),
                            factura_ = factura.ToString(),
                            cajas2_ = cajas2.ToString()
                        });
                    }



                }

            }
        }
        ////////////////////////////////////////////////////////////////////////////////
        return new JavaScriptSerializer().Serialize(info);
    }



    public struct info
    {
        public string factura_;
        public string cajas_;
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string lista(string folio)
    {
        //string meta4 = ConfigurationManager.ConnectionStrings["pnet8prodConnectionString"].ConnectionString;
        List<info> info = new List<info>();

        string sql = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

        DataSet ds = new DataSet();
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(sql);
        com.Connection = con;
        com.CommandType = CommandType.Text;
        com.CommandText = string.Format(@"select 
        count(folio) as cajas, factura
        from validar_embarque where
        folio=('{0}') group by factura", folio);
        SqlDataAdapter adp = new SqlDataAdapter(com);
        con.Open();
        com.ExecuteNonQuery();
        con.Close();
        adp.Fill(ds, "ds");
        if (ds.Tables[0].Rows.Count > 0)
        {

                string factura = ds.Tables[0].Rows[0]["factura"].ToString();
                string cajas = ds.Tables[0].Rows[0]["cajas"].ToString();


                info.Add(new info()
                {
                    factura_ = factura.ToString(),
                    cajas_ = cajas.ToString()
                });
            
        }


        ////////////////////////////////////////////////////////////////////////////////
        return new JavaScriptSerializer().Serialize(info);
    }


}