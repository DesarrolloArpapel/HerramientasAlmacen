using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Globalization;

public partial class modulos_tarimalistasurtimiento_componentes_tarimalistasuertimientoCom : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public struct info_tarima
    {
        public string tarima_;
        public string cajas_;
        public string cajas_qad_;
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string tarima(string folio)
    {
        List<info_tarima> info = new List<info_tarima>();
        string dominio = "200PARPA";
        string qad = "DRIVER=DataDirect 7.1 OpenEdge Wire Protocol; HOST = 10.10.90.153; DSN=QADProd; PORT=48750; DB=custom; UID=mfg; PWD=mfg123; DIL=READ";
        string sql = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
 
        DataSet ds = new DataSet();
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(sql);
        com.Connection = con;
        com.CommandType = CommandType.Text;
        com.CommandText = string.Format(@"
        select 
        tarima, 
        COUNT(tarima) as cajas
        from Validar_Embarque 
        where folio=('{0}') group by tarima", folio);
        SqlDataAdapter adp = new SqlDataAdapter(com);
        con.Open();
        com.ExecuteNonQuery();
        con.Close();
        adp.Fill(ds, "ds");
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                string tarima = ds.Tables[0].Rows[i]["tarima"].ToString();
                string cajas = ds.Tables[0].Rows[i]["cajas"].ToString();
              

                     DataSet odbc = new DataSet();
                     OdbcCommand como = new OdbcCommand();
                     OdbcConnection cono = new OdbcConnection(qad);
                     como.Connection = cono;
                     como.CommandType = CommandType.Text;
                     como.CommandText = string.Format(@"
                     SELECT 
                     count(""cbd-cja"") as cajas
                     FROM PUB.""cbd-det""
			         WHERE 
                     ""cbd-tarima"" = ('{0}') and ""cbd-domain"" = ('{1}') WITH (NOLOCK)"
                     , tarima, dominio
                     );
                     OdbcDataAdapter adpo = new OdbcDataAdapter(como);
                     cono.Open();
                     como.ExecuteNonQuery();
                     cono.Close();
                     adpo.Fill(odbc, "odbc");
                if(odbc.Tables[0].Rows.Count > 0)
                {
                    string cajas_qad = odbc.Tables[0].Rows[0]["cajas"].ToString();
                    info.Add(new info_tarima()
                    {
                     tarima_ = tarima,
                     cajas_ = cajas,
                     cajas_qad_ = cajas_qad,
                    });
                }
                else
                {
                    info.Add(new info_tarima()
                    {
                        tarima_ = tarima,
                        cajas_ = cajas,
                        cajas_qad_ = "0"
                    });
                } 

            

            }
        }
        return new JavaScriptSerializer().Serialize(info);
    }

}