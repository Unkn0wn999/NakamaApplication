using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NakamaApplication
{
    public static class ConexionBD
    {
        public static SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = NAKAMA; Integrated Security = True; Encrypt=False");
        public static void AbrirConexion()
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
        }

        public static void CerrarConexion()
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
        }

    }
}
