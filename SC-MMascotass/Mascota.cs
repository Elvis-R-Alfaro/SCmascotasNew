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
    class Mascota
    {
        //Variable Miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["SC_MMascotass.Properties.Settings.MascotasConnectionString"].ConnectionString;
        private static SqlConnection sqlConnection = new SqlConnection(connectionString);

        private Cliente cliente = new Cliente();
        //Propiedades

        public int IdMascota { get; set; }
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public string NombreMascota { get; set; }
        public int IdRaza { get; set; }
        public string NombreRaza { get; set; }
        public string Sexo { get; set; }
        public int IdEspecie { get; set; }
        public string Descripcion { get; set; }
        public string Familia { get; set; }
        public DateTime Fecha { get; set; }

        //Constructor
        public Mascota() { }
        public Mascota(int idmascota, int idcliente, string nombremascota, string cliente,string sexo, int idraza,  string nombreraza, int idespecie, string descripcion, string familia, DateTime fecha)
        {
            IdMascota = idmascota;
            IdCliente = idcliente;
            NombreMascota = nombremascota;
            Cliente = cliente;
            IdRaza = idraza;
            NombreRaza = nombreraza;
            Sexo = sexo;
            IdEspecie = idespecie;
            Descripcion = descripcion;
            Familia = familia;
            Fecha = fecha;
        }

        //Metodos

        /// <summary>
        /// Inserta una Mascota
        /// </summary>
        /// <param name="categoria">La informacion de la categoria</param>
        public void CrearMascota(Mascota mascota)
        {
            try
            {
                //Query de insertar
                string query = @"INSERT INTO Veterinaria.Mascota
                                (IdCliente,AliasMascota,Especie
                                ,Raza,ColorPelo,Fecha)
                            VALUES
                                (@IdCliente,@AliasMascota,@Especie
                                ,@Raza,@ColorPelo,@Fecha)";

                //Establecer la conexion
                sqlConnection.Open();

                

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@IdCliente", mascota.IdCliente);
                sqlCommand.Parameters.AddWithValue("@AliasMascota", mascota.NombreMascota);
                sqlCommand.Parameters.AddWithValue("@Raza", mascota.NombreRaza);
                sqlCommand.Parameters.AddWithValue("@Fecha", mascota.Fecha);

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

        public void CrearEspecie(Mascota mascota)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("CrearEspecie", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Abrir conexion
                sqlConnection.Open();

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@Descripcion", mascota.Descripcion);
                sqlCommand.Parameters.AddWithValue("@Familia", mascota.Familia);

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

        public void EditarEspecie(Mascota mascota)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("EditarEspecie", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Abrir conexion
                sqlConnection.Open();

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@Descripcion", mascota.Descripcion);
                sqlCommand.Parameters.AddWithValue("@Familia", mascota.Familia);
                sqlCommand.Parameters.AddWithValue("@IdEspecie", mascota.IdEspecie);

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

        public void EliminarEspecie(int id)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("EliminarEspecie", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Abrir conexion
                sqlConnection.Open();

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@IdEspecie", id);

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
        public List<Mascota> MostrarMascotas()
        {
            //Iniciamos la lista vacia de categorias
            List<Mascota> mascotas = new List<Mascota>();

            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("MostrarMascotas", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Abrir conexion
                sqlConnection.Open();

                //Obtener los datos de las categorias
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        mascotas.Add(new Mascota { IdMascota = Convert.ToInt32(rdr["IdMascota"]),
                            IdCliente = Convert.ToInt32(rdr["IdCliente"]),
                            IdRaza = Convert.ToInt32(rdr["IdRaza"]),
                            NombreMascota = rdr["NombreMascota"].ToString(),
                            Cliente = rdr["NombreCliente"].ToString(),
                            NombreRaza = rdr["NombreRaza"].ToString(),
                            Sexo = rdr["Sexo"].ToString(),
                            Fecha = (DateTime)rdr["FechaDeNacimiento"] });
                    }
                }
                return mascotas;
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

        public List<Mascota> MostrarEspecies()
        {
            //Iniciamos la lista vacia de categorias
            List<Mascota> mascotas = new List<Mascota>();

            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("MostrarEspecies", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Abrir conexion
                sqlConnection.Open();

                //Obtener los datos de las categorias
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        mascotas.Add(new Mascota
                        {
                            IdEspecie = Convert.ToInt32(rdr["IdEspecie"]),
                            Descripcion = rdr["Descripcion"].ToString(),
                            Familia = rdr["Familia"].ToString(),
                        });
                    }
                }
                return mascotas;
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
        /// muestra una mascota
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
                                FROM Veterinaria.Cliente";

                //Establcer la coneccion
                sqlConnection.Open();

                //Crear el comando sql
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Obtener los datos de las categorias
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        data.Add(rdr["NombreCliente"].ToString());
                        data.Add(rdr["IdCliente"].ToString());
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

        static public List<string> MonstrarMascotas23()
        {
            //Iniciamos la lista vacia de categorias
            List<string> data = new List<string>();

            try
            {
                //Query de seleccion
                string query = @"SELECT *
                                FROM Veterinaria.Mascota";

                //Establcer la coneccion
                sqlConnection.Open();

                //Crear el comando sql
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Obtener los datos de las categorias
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        data.Add(rdr["AliasMascota"].ToString());
                        data.Add(rdr["IdMascota"].ToString());
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

        /// <summary>
        /// Obtiene una categoria
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Mascota BuscarMascota(int id)
        {
            Mascota laMascota = new Mascota();

            try
            {
                //Query busqueda
                string query = @"SELECT * From Veterinaria.Mascota
                                WHERE IdMascota = @IdMascota";

                //Establecer la coneccion
                sqlConnection.Open();

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@IdMascota", id);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        laMascota.IdMascota = Convert.ToInt32(rdr["IdMascota"]);
                        laMascota.IdCliente = Convert.ToInt32(rdr["IdCliente"]);

                        laMascota.NombreMascota = rdr["AliasMascota"].ToString();
                        laMascota.NombreRaza = rdr["Raza"].ToString();
                        laMascota.Fecha = (DateTime)rdr["Fecha"];
                    }
                }

                return laMascota;
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

        public Mascota BuscarMascotaNombre(string AliasMascota)
        {
            Mascota laMascota = new Mascota();

            try
            {
                //Query busqueda
                string query = @"SELECT * From Veterinaria.Mascota
                                WHERE AliasMascota = @AliasMascota";

                //Establecer la coneccion
                sqlConnection.Open();

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@AliasMascota", AliasMascota);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        laMascota.IdMascota = Convert.ToInt32(rdr["IdMascota"]);

                    }
                }

                return laMascota;
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
        /// edita una mascota existente
        /// </summary>
        /// <param name="mascota"></param>
        public void EditarMascota(Mascota mascota)
        {
            try
            {
                //Query de actualizacion
                string query = @"UPDATE Veterinaria.Mascota
                                SET IdCliente = @IdCliente,
                                    AliasMascota=  @AliasMascota,
                                    Especie = @Especie,
                                    Raza = @Raza,
                                    ColorPelo = @ColorPelo,
                                    Fecha = @Fecha                                  
                                WHERE IdMascota = @IdMascota";

                //Strablecer la conexion
                sqlConnection.Open();

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Establecer los valores de los parametros
                sqlCommand.Parameters.AddWithValue("@IdMascota", mascota.IdMascota);
                sqlCommand.Parameters.AddWithValue("@IdCliente", mascota.IdCliente);
                sqlCommand.Parameters.AddWithValue("@AliasMascota", mascota.NombreMascota);
                sqlCommand.Parameters.AddWithValue("@Raza", mascota.NombreRaza);
                sqlCommand.Parameters.AddWithValue("@Fecha", mascota.Fecha);

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
        /// elimina una mascota existente
        /// </summary>
        /// <param name="id"></param>

        public void EliminarMascota(int id)
        {
            try
            {
                //Query de eliminar
                string query = @"DELETE FROM Veterinaria.Mascota
                                WHERE IdMascota = @IdMascota";

                //Establecer la conexion SQL
                sqlConnection.Open();

                //Establecer el valor del parametro
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@IdMascota", id);

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
