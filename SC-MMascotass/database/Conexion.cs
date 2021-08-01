using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SC_MMascotass.Properties;
using System.Configuration;

namespace SC_MMascotass.database
{
    public class Conexion
    {
        public static string ObtenerString()
        {
            return Settings.Default.scmascotas2ConnectionString;
        }
        public static SqlConnection ObtenerConexion()
        {
            SqlConnection sqlConnection = new SqlConnection(ObtenerString());

            return sqlConnection;
        }
    }
}
