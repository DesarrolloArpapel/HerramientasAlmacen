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
        folio=('{0}') group by id_operador", folio);
        SqlDataAdapter adp = new SqlDataAdapter(com);
        con.Open();
        com.ExecuteNonQuery();
        con.Close();
        adp.Fill(ds, "ds");
        if(ds.Tables[0].Rows.Count > 0)
        {
            for(int i = 0; i< ds.Tables[0].Rows.Count; i++)
            {
                string id = ds.Tables[0].Rows[i]["id_operador"].ToString();
                string cajas = ds.Tables[0].Rows[i]["cajas"].ToString();


                info.Add(new lista_info()
                {
                    id_almacenista_ = id.ToString(),
                    cajas_ = cajas.ToString()
                });
            }
        }

        
        ////////////////////////////////////////////////////////////////////////////////
        return new JavaScriptSerializer().Serialize(info);
    }


}