using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Globalization;

public partial class modulos_actanacimiento_componentes_actanacimiento : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public struct info_acta
    {
        public string clave_;
        public string descripcion_;
        public string tarima_;
        public string codigo_;
        public string ayudante_;
        public string operador_;
        public string maquina_;
        public string turno_;
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string actanacimiento(string acta)
    {
        List<info_acta> info = new List<info_acta>();

        string sql = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        string m4 = ConfigurationManager.ConnectionStrings["pnet8prodConnectionString"].ConnectionString;

        DataSet ds = new DataSet();
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(sql);
        com.Connection = con;
        com.CommandType = CommandType.Text;
        com.CommandText = string.Format(@"select 
        CLAVE_PRODUCTO,
        DESCRIPCION,
        TARIMA,
        CODIGO_PRODUCTO,
        ID_AYUDANTE,
        ID_OPERADOR,
        ID_MAQUINA,
        ID_TURNO 
        from PRODUCCION_SELLADO 
        where ACTA_NACIMIENTO=('{0}')", acta);
        SqlDataAdapter adp = new SqlDataAdapter(com);
        con.Open();
        com.ExecuteNonQuery();
        con.Close();
        adp.Fill(ds, "ds");
        if (ds.Tables[0].Rows.Count > 0)
        {
            string clave = ds.Tables[0].Rows[0]["CLAVE_PRODUCTO"].ToString();
            string descripcion = ds.Tables[0].Rows[0]["DESCRIPCION"].ToString();
            string tarima = ds.Tables[0].Rows[0]["TARIMA"].ToString();
            string codigo = ds.Tables[0].Rows[0]["CODIGO_PRODUCTO"].ToString();
            string ayudante = ds.Tables[0].Rows[0]["ID_AYUDANTE"].ToString();
            string operador = ds.Tables[0].Rows[0]["ID_OPERADOR"].ToString();
            string maquina = ds.Tables[0].Rows[0]["ID_MAQUINA"].ToString();
            string turno = ds.Tables[0].Rows[0]["ID_TURNO"].ToString();


            DataSet ds_m = new DataSet();
            SqlCommand com_m = new SqlCommand();
            SqlConnection con_m = new SqlConnection(m4);
            com_m.Connection = con_m;
            com_m.CommandType = CommandType.Text;
            com_m.CommandText = string.Format(@"select 
                    STD_N_FIRST_NAME as nombre, STD_N_FAM_NAME_1 as apellido, STD_N_MAIDEN_NAME as apellido2
                    from STD_PERSON where STD_ID_PERSON=('{0}')", operador);
            SqlDataAdapter adp_m = new SqlDataAdapter(com_m);
            con_m.Open();
            com_m.ExecuteNonQuery();
            con_m.Close();
            adp_m.Fill(ds_m, "ds_m");
            if (ds_m.Tables[0].Rows.Count > 0)
            {
                string nombre = ds_m.Tables[0].Rows[0]["nombre"].ToString() + " " + ds_m.Tables[0].Rows[0]["apellido"].ToString() + " " + ds_m.Tables[0].Rows[0]["apellido2"].ToString();

                DataSet ds_m2 = new DataSet();
                SqlCommand com_m2 = new SqlCommand();
                SqlConnection con_m2 = new SqlConnection(m4);
                com_m2.Connection = con_m2;
                com_m2.CommandType = CommandType.Text;
                com_m2.CommandText = string.Format(@"select 
                    STD_N_FIRST_NAME as nombre, STD_N_FAM_NAME_1 as apellido, STD_N_MAIDEN_NAME as apellido2
                    from STD_PERSON where STD_ID_PERSON=('{0}')", ayudante);
                SqlDataAdapter adp_m2 = new SqlDataAdapter(com_m2);
                con_m2.Open();
                com_m2.ExecuteNonQuery();
                con_m2.Close();
                adp_m2.Fill(ds_m2, "ds_m");
                if (ds_m2.Tables[0].Rows.Count > 0)
                {
                    string nombre_ayudante = ds_m2.Tables[0].Rows[0]["nombre"].ToString() + " " + ds_m.Tables[0].Rows[0]["apellido"].ToString() + " " + ds_m.Tables[0].Rows[0]["apellido2"].ToString();

                    info.Add(new info_acta()
                    {
                        clave_ = clave,
                        descripcion_ = descripcion,
                        tarima_ = tarima,
                        codigo_ = codigo,
                        ayudante_ = ayudante + " " + nombre_ayudante,
                        operador_ = operador + " " + nombre,
                        maquina_ = maquina,
                        turno_ = turno,
                    });
                }
                else
                {
                    info.Add(new info_acta()
                    {
                        clave_ = clave,
                        descripcion_ = descripcion,
                        tarima_ = tarima,
                        codigo_ = codigo,
                        ayudante_ = ayudante,
                        operador_ = operador,
                        maquina_ = maquina,
                        turno_ = turno,
                    });
                }
            }
            else
            {
                DataSet ds_m2 = new DataSet();
                SqlCommand com_m2 = new SqlCommand();
                SqlConnection con_m2 = new SqlConnection(m4);
                com_m2.Connection = con_m2;
                com_m2.CommandType = CommandType.Text;
                com_m2.CommandText = string.Format(@"select 
                    STD_N_FIRST_NAME as nombre, STD_N_FAM_NAME_1 as apellido, STD_N_MAIDEN_NAME as apellido2
                    from STD_PERSON where STD_ID_PERSON=('{0}')", ayudante);
                SqlDataAdapter adp_m2 = new SqlDataAdapter(com_m2);
                con_m2.Open();
                com_m2.ExecuteNonQuery();
                con_m2.Close();
                adp_m2.Fill(ds_m2, "ds_m");
                if (ds_m2.Tables[0].Rows.Count > 0)
                {
                    string nombre_ayudante = ds_m2.Tables[0].Rows[0]["nombre"].ToString() + " " + ds_m.Tables[0].Rows[0]["apellido"].ToString() + " " + ds_m.Tables[0].Rows[0]["apellido2"].ToString();

                    info.Add(new info_acta()
                    {
                        clave_ = clave,
                        descripcion_ = descripcion,
                        tarima_ = tarima,
                        codigo_ = codigo,
                        ayudante_ = ayudante + " " + nombre_ayudante,
                        operador_ = operador,
                        maquina_ = maquina,
                        turno_ = turno,
                    });
                }
                else
                {
                    info.Add(new info_acta()
                    {
                        clave_ = clave,
                        descripcion_ = descripcion,
                        tarima_ = tarima,
                        codigo_ = codigo,
                        ayudante_ = ayudante,
                        operador_ = operador,
                        maquina_ = maquina,
                        turno_ = turno,
                    });
                }
            }

        }
        else
        {
            DataSet ds2 = new DataSet();
            SqlCommand com2 = new SqlCommand();
            SqlConnection con2 = new SqlConnection(sql);
            com2.Connection = con2;
            com2.CommandType = CommandType.Text;
            com2.CommandText = string.Format(@"select 
            CLAVE_PRODUCTO,
            DESCRIPCION,
            TARIMA,
            CODIGO_PRODUCTO,
            ID_AYUDANTE,
            ID_OPERADOR,
            ID_MAQUINA,
            ID_TURNO 
            from PRODUCCION
            where ACTA_NACIMIENTO=('{0}')", acta);
            SqlDataAdapter adp2 = new SqlDataAdapter(com2);
            con2.Open();
            com2.ExecuteNonQuery();
            con2.Close();
            adp2.Fill(ds2, "d2s");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                string clave = ds2.Tables[0].Rows[0]["CLAVE_PRODUCTO"].ToString();
                string descripcion = ds2.Tables[0].Rows[0]["DESCRIPCION"].ToString();
                string tarima = ds2.Tables[0].Rows[0]["TARIMA"].ToString();
                string codigo = ds2.Tables[0].Rows[0]["CODIGO_PRODUCTO"].ToString();
                string ayudante = ds2.Tables[0].Rows[0]["ID_AYUDANTE"].ToString();
                string operador = ds2.Tables[0].Rows[0]["ID_OPERADOR"].ToString();
                string maquina = ds2.Tables[0].Rows[0]["ID_MAQUINA"].ToString();
                string turno = ds2.Tables[0].Rows[0]["ID_TURNO"].ToString();

                DataSet ds_m = new DataSet();
                SqlCommand com_m = new SqlCommand();
                SqlConnection con_m = new SqlConnection(m4);
                com_m.Connection = con_m;
                com_m.CommandType = CommandType.Text;
                com_m.CommandText = string.Format(@"select 
                    STD_N_FIRST_NAME as nombre, STD_N_FAM_NAME_1 as apellido, STD_N_MAIDEN_NAME as apellido2
                    from STD_PERSON where STD_ID_PERSON=('{0}')", operador);
                SqlDataAdapter adp_m = new SqlDataAdapter(com_m);
                con_m.Open();
                com_m.ExecuteNonQuery();
                con_m.Close();
                adp_m.Fill(ds_m, "ds_m");
                if (ds_m.Tables[0].Rows.Count > 0)
                {
                    string nombre = ds_m.Tables[0].Rows[0]["nombre"].ToString() + " " + ds_m.Tables[0].Rows[0]["apellido"].ToString() + " " + ds_m.Tables[0].Rows[0]["apellido2"].ToString();

                    DataSet ds_m2 = new DataSet();
                    SqlCommand com_m2 = new SqlCommand();
                    SqlConnection con_m2 = new SqlConnection(m4);
                    com_m2.Connection = con_m2;
                    com_m2.CommandType = CommandType.Text;
                    com_m2.CommandText = string.Format(@"select 
                    STD_N_FIRST_NAME as nombre, STD_N_FAM_NAME_1 as apellido, STD_N_MAIDEN_NAME as apellido2
                    from STD_PERSON where STD_ID_PERSON=('{0}')", ayudante);
                    SqlDataAdapter adp_m2 = new SqlDataAdapter(com_m2);
                    con_m2.Open();
                    com_m2.ExecuteNonQuery();
                    con_m2.Close();
                    adp_m2.Fill(ds_m2, "ds_m");
                    if (ds_m2.Tables[0].Rows.Count > 0)
                    {
                        string nombre_ayudante = ds_m2.Tables[0].Rows[0]["nombre"].ToString() + " " + ds_m.Tables[0].Rows[0]["apellido"].ToString() + " " + ds_m.Tables[0].Rows[0]["apellido2"].ToString();

                        info.Add(new info_acta()
                        {
                            clave_ = clave,
                            descripcion_ = descripcion,
                            tarima_ = tarima,
                            codigo_ = codigo,
                            ayudante_ = ayudante + " " + nombre_ayudante,
                            operador_ = operador + " " + nombre,
                            maquina_ = maquina,
                            turno_ = turno,
                        });
                    }
                    else
                    {
                        info.Add(new info_acta()
                        {
                            clave_ = clave,
                            descripcion_ = descripcion,
                            tarima_ = tarima,
                            codigo_ = codigo,
                            ayudante_ = ayudante,
                            operador_ = operador,
                            maquina_ = maquina,
                            turno_ = turno,
                        });
                    }
                }
                else
                {
                    DataSet ds_m2 = new DataSet();
                    SqlCommand com_m2 = new SqlCommand();
                    SqlConnection con_m2 = new SqlConnection(m4);
                    com_m2.Connection = con_m2;
                    com_m2.CommandType = CommandType.Text;
                    com_m2.CommandText = string.Format(@"select 
                    STD_N_FIRST_NAME as nombre, STD_N_FAM_NAME_1 as apellido, STD_N_MAIDEN_NAME as apellido2
                    from STD_PERSON where STD_ID_PERSON=('{0}')", ayudante);
                    SqlDataAdapter adp_m2 = new SqlDataAdapter(com_m2);
                    con_m2.Open();
                    com_m2.ExecuteNonQuery();
                    con_m2.Close();
                    adp_m2.Fill(ds_m2, "ds_m");
                    if (ds_m2.Tables[0].Rows.Count > 0)
                    {
                        string nombre_ayudante = ds_m2.Tables[0].Rows[0]["nombre"].ToString() + " " + ds_m.Tables[0].Rows[0]["apellido"].ToString() + " " + ds_m.Tables[0].Rows[0]["apellido2"].ToString();

                        info.Add(new info_acta()
                        {
                            clave_ = clave,
                            descripcion_ = descripcion,
                            tarima_ = tarima,
                            codigo_ = codigo,
                            ayudante_ = ayudante + " " + nombre_ayudante,
                            operador_ = operador,
                            maquina_ = maquina,
                            turno_ = turno,
                        });
                    }
                    else
                    {
                        info.Add(new info_acta()
                        {
                            clave_ = clave,
                            descripcion_ = descripcion,
                            tarima_ = tarima,
                            codigo_ = codigo,
                            ayudante_ = ayudante,
                            operador_ = operador,
                            maquina_ = maquina,
                            turno_ = turno,
                        });
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////
        return new JavaScriptSerializer().Serialize(info);
    }

}