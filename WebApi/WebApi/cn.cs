using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace WebApi
{
    public static class cn
    {
        public static SqlConnection conectar()
        {
            
            SqlConnection sqlc = new SqlConnection(@"server=(local);Integrated security=SSPI;Database=web");
            sqlc.Open();            //SqlConnection.ClearAllPools();
            return sqlc;
        }
        public static DataSet ejecutar_select(string query)
        {
            SqlDataAdapter da = new SqlDataAdapter(query, conectar());
            DataSet ds = new DataSet();
            da.Fill(ds);
           conectar().Close();
            return ds;
        }
 
        public static Boolean ejecutar_comando(string comando)
        {
            SqlCommand cmd = new SqlCommand(comando, conectar());
            Boolean rta;
            try
            {
                cmd.ExecuteNonQuery();
                rta = true;
            }
            catch(Exception ex)
            {
                rta = false;
            }
            conectar().Close();
            return rta;
        }
     

    }
}