using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Globalization;

public partial class modulos_tarimaembarque_componentes_tarimaembarque : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public struct info_acta
    {
        public string clave_;
        public string id_operador_;
        public string cajas_;
        public int cajas_totales_;
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string tarimaembarque(string tarima)
    {
        List<info_acta> info = new List<info_acta>();

        string sql = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        int c = 0;
        DataSet ds = new DataSet();
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(sql);
        com.Connection = con;
        com.CommandType = CommandType.Text;
        com.CommandText = string.Format(@"
        SELECT 
        CLAVE, 
        ID_OPERADOR,
        COUNT(CLAVE) AS CAJAS 
        FROM Validar_Embarque
        WHERE tarima=('{0}') 
        GROUP BY
        CLAVE,
        ID_OPERADOR", tarima);
        SqlDataAdapter adp = new SqlDataAdapter(com);
        con.Open();
        com.ExecuteNonQuery();
        con.Close();
        adp.Fill(ds, "ds");
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string clave = ds.Tables[0].Rows[i]["clave"].ToString();
                string id_operador = ds.Tables[0].Rows[i]["id_operador"].ToString();
                string cajas = ds.Tables[0].Rows[i]["cajas"].ToString();
                string m4 = ConfigurationManager.ConnectionStrings["pnet8prodConnectionString"].ConnectionString;

                DataSet ds_m = new DataSet();
                SqlCommand com_m = new SqlCommand();
                SqlConnection con_m = new SqlConnection(m4);
                com_m.Connection = con_m;
                com_m.CommandType = CommandType.Text;
                com_m.CommandText = string.Format(@"select 
                STD_N_FIRST_NAME as nombre, STD_N_FAM_NAME_1 as apellido, STD_N_MAIDEN_NAME as apellido2
                from STD_PERSON where STD_ID_PERSON=('{0}')", id_operador);
                SqlDataAdapter adp_m = new SqlDataAdapter(com_m);
                con_m.Open();
                com_m.ExecuteNonQuery();
                con_m.Close();
                adp_m.Fill(ds_m, "ds_m");
                if (ds_m.Tables[0].Rows.Count > 0)
                {
                    string nombre = ds_m.Tables[0].Rows[0]["nombre"].ToString() + " " + ds_m.Tables[0].Rows[0]["apellido"].ToString();
                    
                    c = c + Convert.ToInt32(cajas);
                    info.Add(new info_acta()
                    {
                        clave_ = clave,
                        id_operador_ = id_operador + " " + nombre,
                        cajas_ = cajas,
                        cajas_totales_ = c
                    });
                }
            }
        }
        return new JavaScriptSerializer().Serialize(info);
    }

}
    