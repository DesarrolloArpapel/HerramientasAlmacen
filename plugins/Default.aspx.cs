using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Configuration;

public partial class plugins_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sql = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        string dominio = "200PARPA";
        string ConnString_qad = "DRIVER=DataDirect 7.1 OpenEdge Wire Protocol; HOST = 10.10.90.153; DSN=QADProd; PORT=48750; DB=custom; UID=mfg; PWD=mfg123; DIL=READ";

        DataSet ds = new DataSet();
        OdbcCommand com = new OdbcCommand();
        OdbcConnection con = new OdbcConnection(ConnString_qad);
        com.Connection = con;
        com.CommandType = CommandType.Text;
        com.CommandText = string.Format(@"
        SELECT ""XXSCC_acta"" FROM  
        PUB.""XXSCC_MSTR""
        WHERE ""XXSCC_TARIMA"" ='TT0322308' 
        AND ""XXSCC_DOMAIN""=('{0}')", dominio);
        OdbcDataAdapter adp = new OdbcDataAdapter(com);
        con.Open();
        com.ExecuteNonQuery();
        con.Close();
        adp.Fill(ds, "ds");
        if(ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string acta = ds.Tables[0].Rows[i]["ACTA_NACIMIENTO"].ToString();
                DataSet ds2 = new DataSet();
                SqlCommand com2 = new SqlCommand();
                SqlConnection con2 = new SqlConnection(sql);
                com2.Connection = con2;
                com2.CommandType = CommandType.Text;
                com2.CommandText = string.Format(@"
                select tarima from DET_SALIDA where acta=('{0}')", acta);
                SqlDataAdapter adp2 = new SqlDataAdapter(com2);
                con2.Open();
                com2.ExecuteNonQuery();
                con2.Close();
                adp2.Fill(ds2, "ds2");
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    string tarima = ds2.Tables[0].Rows[0]["Tarima"].ToString();
                    DataSet ds3 = new DataSet();
                    SqlCommand com3 = new SqlCommand();
                    SqlConnection con3 = new SqlConnection(sql);
                    com3.Connection = con3;
                    com3.CommandType = CommandType.Text;
                    com3.CommandText = string.Format(@"
                    update PRODUCCION set TARIMA=('{0}') where ACTA_NACIMIENTO=('{1}')", tarima, acta);
                    SqlDataAdapter adp3 = new SqlDataAdapter(com3);
                    con3.Open();
                    com3.ExecuteNonQuery();
                    con3.Close();
                    
                    DataSet ds_qad = new DataSet();
                    OdbcCommand cmd_qad = new OdbcCommand();
                    OdbcConnection conn_qad = new OdbcConnection(ConnString_qad);
                    cmd_qad.Connection = conn_qad;
                    cmd_qad.CommandType = CommandType.Text;
                    cmd_qad.CommandText = string.Format(@"UPDATE PUB.""XXSCC_MSTR""
                    SET ""XXSCC_TARIMA"" = ('{0}')
                    WHERE ""XXSCC_acta""= ('{1}')
                    AND ""XXSCC_DOMAIN"" = ('{2}') 
                    ",  tarima, acta, dominio);
                    OdbcDataAdapter da_qad = new OdbcDataAdapter(cmd_qad);

                    ////////////////////////////////////////////
   
                    /* try
                     {*/
                    conn_qad.Open();
                    /*     retValue = */
                    cmd_qad.ExecuteNonQuery();
                    /*    Label1.Text = Convert.ToString(retValue);
                    }
                    catch (Exception err)
                    {
                        Trace.Write(err.Message);
                    }
                    finally
                    {*/
                    conn_qad.Close();
                    /*   }*/

                }
                Label1.Text = "" + i +  " cajas";
            }
        }
        else
        {
            Label1.Text = "No encontre datos";
        }

    }



}