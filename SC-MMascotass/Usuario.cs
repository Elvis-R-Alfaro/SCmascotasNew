using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Agregar los namespaces de conexion con SQL server
using System.Data.SqlClient;
using System.Configuration;

namespace SC_MMascotass
{
    class Usuario
    {
        //Variable miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["SC_MMascotass.Properties.Settings.MascotasConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Propiedades
        public int Id { get; set; }

        public string NombreCompleto { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        public bool Estado { get; set; }

        //Constructores
        public Usuario() { }

        public Usuario(string nombreCompleto, string username, string password, bool estado) 
        {
            NombreCompleto = nombreCompleto;
            Username = username;
            Password = password;
            Estado = estado;
        }

        //Metodos

        /// <summary>
        /// Verifica si las credenciales de inicio de sesion son correctas.
        /// </summary>
        /// <param name="username">El nombre del usuario</param>
        /// <returns>Los datos del usuario</returns>
        public Usuario BuscarUsuario(string username)
        {
            //Crear ibjeto que almacena la información de los resultados
            Usuario usuario = new Usuario();

            try
            {
                //Crear comando SQL
                SqlCommand sqlCommand = new SqlCommand("BuscarUsuario", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@username", username);

                sqlConnection.Open();

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        //Obtener los valores del usuarios si la consulta retorna valores
                        usuario.Id = Convert.ToInt32(rdr["IdUsuario"]);
                        usuario.NombreCompleto = rdr["Nombre"].ToString();
                        usuario.Username = rdr["Usuario"].ToString();
                        usuario.Password = rdr["Clave"].ToString();
                        usuario.Estado = Convert.ToBoolean(rdr["Estado"]);
                    }

                }
                //retornar el usuario con los valores
                return usuario;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally {
                //Cerrar la seccion
                sqlConnection.Close();
            }
        }

        public List<Usuario> MostrarUsuario()
        {
            //Iniciamos la lista vacia de categorias
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                //Crear comando SQL
                SqlCommand sqlCommand = new SqlCommand("MostrarUsuario", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establcer la coneccion
                sqlConnection.Open();

                //Obtener los datos de las categorias
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        usuarios.Add(new Usuario{ 
                            Id = Convert.ToInt32(rdr["IdUsuario"]),
                            NombreCompleto = rdr["Nombre"].ToString(), 
                            Username = rdr["Usuario"].ToString(),
                            Password = rdr["Clave"].ToString(),
                            Estado = Convert.ToBoolean(rdr["Estado"])
                        });
                    }
                }
                return usuarios;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                //Cerrar la conexion
                sqlConnection.Close();
            }
        }

    }
}












