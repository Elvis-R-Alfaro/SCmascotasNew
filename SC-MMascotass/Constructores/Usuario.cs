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
    public class Usuario
    {
        //Variable miembro
        private SqlConnection sqlConnection = database.Conexion.ObtenerConexion();

        //Propiedades
        public  int Id { get; set; }

        public string NombreCompleto { get; set; }
        public static string NombreCompletoGlobal { get; set; }
        public static string CargoGlobal { get; set; }
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

     
        //public List<Usuario> MostrarUsuario()
        //{
        //    //Iniciamos la lista vacia de categorias
        //    List<Usuario> usuarios = new List<Usuario>();

        //    try
        //    {
        //        //Crear comando SQL
        //        SqlCommand sqlCommand = new SqlCommand("MostrarUsuario", sqlConnection);
        //        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

        //        //Establcer la coneccion
        //        sqlConnection.Open();

        //        //Obtener los datos de las categorias
        //        using (SqlDataReader rdr = sqlCommand.ExecuteReader())
        //        {
        //            while (rdr.Read())
        //            {
        //                usuarios.Add(new Usuario{ 
        //                    Id = Convert.ToInt32(rdr["IdUsuario"]),
        //                    NombreCompleto = rdr["Nombre"].ToString(), 
        //                    Username = rdr["Usuario"].ToString(),
        //                    Password = rdr["Clave"].ToString(),
        //                    Estado = Convert.ToBoolean(rdr["Estado"])
        //                });
        //            }
        //        }
        //        return usuarios;
        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }
        //    finally
        //    {
        //        //Cerrar la conexion
        //        sqlConnection.Close();
        //    }
        //}

    }
}












