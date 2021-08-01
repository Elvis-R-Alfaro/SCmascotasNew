using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Agregar los namespaces
using System.Data.SqlClient;
using System.Configuration;

namespace SC_MMascotass
{
    class Categoria
    {
        //Variable Miembro
        static private SqlConnection sqlConnection = database.Conexion.ObtenerConexion();

        //Propiedades

        public int Id { get; set; }
        public string NombreCategoria { get; set; }

        //Constructor
        public Categoria() { }
        public Categoria(int id, string nombrecategoria)
        {
            Id = id;
            NombreCategoria = nombrecategoria;
        }

        //Metodos

        /// <summary>
        /// Inserta una Categoria
        /// </summary>
        /// <param name="categoria">La informacion de la categoria</param>
        public void CrearCategoria(Categoria categoria)
        {
            try
            {
                //Query de insertar
                string query = @"INSERT INTO Veterinaria.Categoria (NombreCategoria) 
                            VALUES(@nombrecategoria)";

                //Establecer la conexion
                sqlConnection.Open();

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@nombrecategoria", categoria.NombreCategoria);

                //ejecutar el comando insertado
                sqlCommand.ExecuteNonQuery();
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

        /// <summary>
        /// Monstrar todas las categorias
        /// </summary>
        /// <returns>Listado de Categorias</returns>
         public List<Categoria> MonstrarCategorias()
        {
            //Iniciamos la lista vacia de categorias
            List<Categoria> categorias = new List<Categoria>();

            try
            {
                //Query de seleccion
                string query = @"SELECT IdCategoria, NombreCategoria
                                FROM Veterinaria.Categoria";

                //Establcer la coneccion
                sqlConnection.Open();

                //Crear el comando sql
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Obtener los datos de las categorias
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        categorias.Add(new Categoria { Id = Convert.ToInt32(rdr["IdCategoria"]), NombreCategoria = rdr["NombreCategoria"].ToString() });
                    }
                }
                return categorias;
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

        /// <summary>
        /// Obtiene una categoria
        /// </summary>
        /// <param name="id"></param>
        /// <returns>retorna la categoria </returns>
        public Categoria BuscarCategoria(int id)
        {
            Categoria laCategoria = new Categoria();

            try
            {
                //Query busqueda
                string query = @"SELECT * From Veterinaria.Categoria
                                WHERE IdCategoria = @id";

                //Establecer la coneccion
                sqlConnection.Open();

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@id", id);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        laCategoria.Id = Convert.ToInt32(rdr["IdCategoria"]);
                        laCategoria.NombreCategoria = rdr["NombreCategoria"].ToString();
                    }
                }

                return laCategoria;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                //Cerrar la conexio
                sqlConnection.Close();
            }
        }
        /// <summary>
        /// busca una categoria
        /// </summary>
        /// <param name="NombreCategoria"></param>
        /// <returns></returns>
        public Categoria BuscarCategoriasId(string NombreCategoria)
        {
            Categoria laCategoria = new Categoria();

            try
            {
                //Query busqueda
                string query = @"SELECT * From Veterinaria.Categoria
                                WHERE NombreCategoria = @NombreCategoria";

                //Establecer la coneccion
                sqlConnection.Open();

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@NombreCategoria", NombreCategoria);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        laCategoria.Id = Convert.ToInt32(rdr["IdCategoria"]);
                        laCategoria.NombreCategoria = rdr["NombreCategoria"].ToString();
                    }
                }

                return laCategoria;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                //Cerrar la conexio
                sqlConnection.Close();
            }
        }
        /// <summary>
        /// modifica una categoria
        /// </summary>
        /// <param name="categoria"></param>
        public void EditarCategoria(Categoria categoria)
        {
            try
            {
                //Query de actualizacion
                string query = @"UPDATE Veterinaria.Categoria
                                SET NombreCategoria = @nombrecategoria
                                WHERE IdCategoria = @id";

                //Strablecer la conexion
                sqlConnection.Open();

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Establecer los valores de los parametros
                sqlCommand.Parameters.AddWithValue("@id", categoria.Id);
                sqlCommand.Parameters.AddWithValue("@nombrecategoria", categoria.NombreCategoria);

                //Ejecutar el comando de actualizar
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //Cerrar conexcion
                sqlConnection.Close();
            }
        }
        /// <summary>
        /// Elimina una Categoria
        /// </summary>
        /// <param name="id"></param>
        public void EliminarCategoria(int id)
        {
            try
            {
                //Query de eliminar
                string query = @"DELETE FROM Veterinaria.Categoria
                                WHERE IdCategoria = @id";

                //Establecer la conexion SQL
                sqlConnection.Open();

                //Establecer el valor del parametro
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@id", id);

                //Ejecutar el comando de eliminacion
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //CErrar conexion
                sqlConnection.Close();
            }
        }
        /// <summary>
        /// Muestra las mascotasa en el list
        /// </summary>
        /// <returns></returns>
        static public List<string> MonstrarMascotas22()
        {
            //Iniciamos la lista vacia de categorias
            List<string> data = new List<string>();

            try
            {
                //Query de seleccion
                string query = @"SELECT *
                                FROM Veterinaria.Categoria";

                //Establcer la coneccion
                sqlConnection.Open();

                //Crear el comando sql
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Obtener los datos de las categorias
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        data.Add(rdr["NombreCategoria"].ToString());
                        data.Add(rdr["IdCategoria"].ToString());
                    }
                }

                return data;
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

        static public List<string> GetData()
        {
            List<string> data = new List<string>();

            data.Add("Comida para perros");
            data.Add("Comida para Gatos");
            data.Add("Juguetes");
            data.Add("Jarabes");
            data.Add("Pastillas");
            data.Add("Vitaminas");
            data.Add("Correas");
            data.Add("Camas");
            data.Add("Vacunas");
            data.Add("En latados");
            data.Add("Tazones");
            data.Add("Servicio");

            return data;
        }
    }
}
