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
    class HistorialVacunacion
    {
        //Variable Miembro
        private SqlConnection sqlConnection = database.Conexion.ObtenerConexion();

        //Propiedades
        public int IdHistorialVacunacion { get; set; }

        public string Mascota { get; set; }
        public string Cliente { get; set; }
        public string Producto { get; set; }
        public DateTime Fecha { get; set; }

        public HistorialVacunacion() { }
        public HistorialVacunacion(int idhistorialvacunacion, string mascota, string cliente, string producto, DateTime fecha)
        {
            IdHistorialVacunacion = idhistorialvacunacion;
            Mascota = mascota;
            Cliente = cliente;
            Producto = producto;
            Fecha = fecha;

        }

        


        public List<HistorialVacunacion> MonstrarRegistro(string nombre)
        {
            //Iniciamos la lista vacia de categorias
            List<HistorialVacunacion> historialVacunacions = new List<HistorialVacunacion>();

            try
            {
                //Query de seleccion
                string query = @"SELECT Veterinaria.Mascota.AliasMascota, Veterinaria.Inventario.NombreProducto, Veterinaria.HistorialVacunacion.Fecha, Veterinaria.HistorialVacunacion.IdHistorialVacunacion, Veterinaria.Cliente.NombreCliente, 
                  Veterinaria.Cliente.IdCliente
                 FROM     Veterinaria.Mascota INNER JOIN
                  Veterinaria.HistorialVacunacion ON Veterinaria.Mascota.IdMascota = Veterinaria.HistorialVacunacion.IdMascota INNER JOIN
                  Veterinaria.Inventario ON Veterinaria.HistorialVacunacion.IdProducto = Veterinaria.Inventario.IdProducto INNER JOIN
                  Veterinaria.Categoria ON Veterinaria.Inventario.IdCategoria = Veterinaria.Categoria.IdCategoria INNER JOIN
                  Veterinaria.Cliente ON Veterinaria.Mascota.IdCliente = Veterinaria.Cliente.IdCliente
                        WHERE  Veterinaria.Mascota.AliasMascota = @nombre";

                //Establcer la coneccion
                sqlConnection.Open();

                //Crear el comando sql
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@nombre", nombre);

                //Obtener los datos de las categorias
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        historialVacunacions.Add(new HistorialVacunacion
                        {
                            IdHistorialVacunacion =Convert.ToInt32(rdr["IdHistorialVacunacion"]),
                            Mascota = rdr["AliasMascota"].ToString(),
                            Cliente = rdr["NombreCliente"].ToString(),
                            Producto =rdr["NombreProducto"].ToString(),
                            Fecha = (DateTime)rdr["Fecha"]
                        });
                    }
                }
                return historialVacunacions;
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
