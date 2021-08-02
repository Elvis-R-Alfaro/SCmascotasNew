using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Agregar los namespaces
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;

namespace SC_MMascotass.Constructores
{
    public class Procedimientos
    {
        //Conexion
        private static SqlConnection sqlConnection = database.Conexion.ObtenerConexion();

        public static string MensajeProcedimiento;
        //LOGIN

        /// <summary>
        /// Verifica si las credenciales de inicio de sesion son correctas.
        /// </summary>
        /// <param name="username">El nombre del usuario</param>
        /// <returns>Los datos del usuario</returns>
        public static Usuario BuscarUsuario(string username)
        {
            //Crear ibjeto que almacena la información de los resultados
            Usuario usuario = new Usuario();

            try
            {
                //Crear comando SQL
                SqlCommand sqlCommand = new SqlCommand("Clientes", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@Usuario", username);
                sqlCommand.Parameters.AddWithValue("@Accion", "BuscarUsuario");

                sqlConnection.Open();

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        //Obtener los valores del usuarios si la consulta retorna valores
                        usuario.Id = Convert.ToInt32(rdr["IdUsuario"]);
                        usuario.NombreCompleto = rdr["Usuario"].ToString();
                        usuario.Password = rdr["Clave"].ToString();
                        usuario.Estado = Convert.ToBoolean(rdr["Estado"]);
                    }

                }
                //retornar el usuario con los valores
                return usuario;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un error...");
                throw e;
            }
            finally
            {
                //Cerrar la seccion
                sqlConnection.Close();
            }
        }

    }
}
