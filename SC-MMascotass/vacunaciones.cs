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
    class vacunaciones
    {
        //Variable Miembro
        private SqlConnection sqlConnection = database.Conexion.ObtenerConexion();

        //Propiedades
        public int IdHistorialVacunacion { get; set; }
        public int IdMascota { get; set; }
        public int IdProducto { get; set; }
        public DateTime Fecha { get; set; }

        public vacunaciones() { }

        public vacunaciones(int idhistorialvacunacion, int idmascota, int idproducto, DateTime fecha)
        {
            IdHistorialVacunacion = idhistorialvacunacion;
            IdMascota = idmascota;
            IdProducto = idproducto;
            Fecha = fecha;

        }

        public void CrearVacuna(vacunaciones producto)
        {
            try
            {
                //Query de insertar
                string query = @"INSERT INTO Veterinaria.HistorialVacunacion(IdMascota, IdProducto, Fecha) VALUES (@IdMascota, @IdProducto, @Fecha)";



                //Establecer la conexion
                sqlConnection.Open();

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@IdMascota", producto.IdMascota);

                sqlCommand.Parameters.AddWithValue("@IdProducto", producto.IdProducto);

                sqlCommand.Parameters.AddWithValue("@Fecha", producto.Fecha);

                //ejecutar el comando insertado
                sqlCommand.ExecuteNonQuery();

                Console.WriteLine(producto.Fecha);
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
