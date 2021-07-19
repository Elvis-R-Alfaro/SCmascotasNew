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
    class InventarioC
    {
        //Variable Miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["SC_MMascotass.Properties.Settings.MascotasConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Propiedades
        public int IdCategoria { get; set; }        
        public string Descripcion { get; set; }
        public int Id { get; set; }
        public string Categoria { get; set; }
        public int Stock { get; set; }
        public double PrecioCosto { get; set; }
        public double PrecioVenta { get; set; }

        //Constructor
        public InventarioC() { }
        public InventarioC(int idCategoria, string descripcion,string categoria, int id, int stock, double precioCosto, double precioVenta)
        {
            IdCategoria = idCategoria;
            Descripcion = descripcion;
            Categoria = categoria;
            Id = id;
            Stock = stock;
            PrecioCosto = precioCosto;
            PrecioVenta = precioVenta;
            
        }

        //Metodos

        /// <summary>
        /// Inserta un Producto
        /// </summary>
        /// <param name="producto">La informacion del producto</param>
        public void CrearProducto(InventarioC producto)
        {
            try
            {
                //Query de insertar
                string query = @"INSERT INTO Veterinaria.Inventario (IdCategoria,NombreProducto,PrecioCosto,PrecioVenta,Stock) 
                            VALUES(@idCategoria,@nombreProducto,@precioCosto,@precioVenta,@stock)";

                //Establecer la conexion
                sqlConnection.Open();

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@idCategoria", producto.IdCategoria);

                sqlCommand.Parameters.AddWithValue("@nombreProducto", producto.Descripcion);

                sqlCommand.Parameters.AddWithValue("@precioCosto", producto.PrecioCosto);

                sqlCommand.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);

                sqlCommand.Parameters.AddWithValue("@stock", producto.Stock);

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
        /// Monstrar todos los productos
        /// </summary>
        /// <returns>Listado de Productos</returns>
        public List<InventarioC> MonstrarInventario()
        {
            //Iniciamos la lista vacia de categorias
            List<InventarioC> inventarios = new List<InventarioC>();

            try
            {
                //Query de seleccion
                string query = @"SELECT Veterinaria.Inventario.IdProducto, Veterinaria.Inventario.IdCategoria, Veterinaria.Inventario.NombreProducto, Veterinaria.Inventario.PrecioCosto, Veterinaria.Inventario.PrecioVenta, Veterinaria.Inventario.Stock, 
                  Veterinaria.Categoria.NombreCategoria FROM Veterinaria.Inventario inner join Veterinaria.categoria on Veterinaria.Inventario.IdCategoria = Veterinaria.Categoria.IdCategoria";

                //Establcer la coneccion
                sqlConnection.Open();

                //Crear el comando sql
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Obtener los datos de las categorias
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        inventarios.Add(new InventarioC { Id = Convert.ToInt32(rdr["IdProducto"]), 
                            IdCategoria = Convert.ToInt32(rdr["IdCategoria"]), 
                            Descripcion = rdr["NombreProducto"].ToString(), 
                            Categoria = rdr["NombreCategoria"].ToString(),
                            PrecioCosto = Convert.ToDouble(rdr["PrecioCosto"]), 
                            PrecioVenta = Convert.ToDouble(rdr["PrecioVenta"]), 
                            Stock = Convert.ToInt32(rdr["Stock"]) } );
                    }
                }
                return inventarios;
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
        /// Obtiene productos de inventario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public InventarioC BuscarProducto(int id)
        {
            InventarioC elProducto = new InventarioC();

            try
            {
                //Query busqueda
                string query = @"SELECT * From Veterinaria.Inventario
                                WHERE IdProducto = @id";

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
                        elProducto.Id = Convert.ToInt32(rdr["IdProducto"]);
                        elProducto.Descripcion = rdr["NombreProducto"].ToString();
                        elProducto.IdCategoria = Convert.ToInt32(rdr["IdCategoria"]);
                        elProducto.PrecioCosto = Convert.ToDouble(rdr["PrecioCosto"]);
                        elProducto.PrecioVenta = Convert.ToDouble(rdr["PrecioVenta"]);
                        elProducto.Stock = Convert.ToInt32(rdr["Stock"]);
                    }
                }

                return elProducto;
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
        /// edita un producto existente
        /// </summary>
        /// <param name="producto"></param>
        public void EditarProducto(InventarioC producto)
        {
            try
            {
                //Query de actualizacion

                string query = @"UPDATE Veterinaria.Inventario
                                SET IdCategoria = @idCategoria,
                                NombreProducto = @nombreProducto,
                                PrecioCosto = @precioCosto,
                                PrecioVenta = @precioVenta,
                                Stock = @stock
                                WHERE IdProducto = @id";

                //Strablecer la conexion
                sqlConnection.Open();

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Establecer los valores de los parametros
                sqlCommand.Parameters.AddWithValue("@id", producto.Id);

                sqlCommand.Parameters.AddWithValue("@idCategoria", producto.IdCategoria);

                sqlCommand.Parameters.AddWithValue("@nombreProducto", producto.Descripcion);

                sqlCommand.Parameters.AddWithValue("@precioCosto", producto.PrecioCosto);

                sqlCommand.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);

                sqlCommand.Parameters.AddWithValue("@stock", producto.Stock);

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
        /// elimina un registro existente
        /// </summary>
        /// <param name="id"></param>

        public void EliminarRegistro(int id)
        {
            try
            {
                //Query de eliminar
                string query = @"DELETE FROM Veterinaria.Inventario
                                WHERE IdProducto = @id";

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

    }
}
