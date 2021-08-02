﻿using System;
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
        
        #region LOGIN

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
                        usuario.NombreCompleto = rdr["Usuario"].ToString();
                        usuario.Username = rdr["username"].ToString();
                        usuario.Password = rdr["password"].ToString();
                        usuario.Estado = Convert.ToBoolean(rdr["estado"]);
                    }

                }
                //retornar el usuario con los valores
                return usuario;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un error al buscar el cliente...");
                throw e;
            }
            finally
            {
                //Cerrar la seccion
                sqlConnection.Close();
            }
        }
        #endregion

        #region CLIENTES


        /// <summary>
        /// Inserta una Categoria
        /// </summary>
        /// <param name="cliente">La informacion del cliente</param>
        public static void CrearCliente(Cliente cliente)
        {
            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Clientes", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@NombreCliente", cliente.Nombre_Cliente);
                sqlCommand.Parameters.AddWithValue("@Telefono", cliente.Teléfono);
                sqlCommand.Parameters.AddWithValue("@Correo", cliente.Correo);
                sqlCommand.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                sqlCommand.Parameters.AddWithValue("@Accion", "Insertar");

                //Establecer la conexion
                sqlConnection.Open();

                //ejecutar el comando insertado
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Revise que los Campos esten Escritos Correctamente");
                Console.WriteLine(e.Message);
            }
            finally
            {
                //Cerrar la conexion
                sqlConnection.Close();
            }
        }

        public static Cliente BuscarClientID(string NombreCliente)
        {

            Cliente elCliente = new Cliente();

            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Clientes", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@NombreCliente", NombreCliente);
                sqlCommand.Parameters.AddWithValue("@Accion", "BuscarClientID");

                //Establecer la coneccion
                sqlConnection.Open();

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        elCliente.ID = Convert.ToInt32(rdr["IdCliente"]);
                        elCliente.Nombre_Cliente = rdr["NombreCliente"].ToString();
                        elCliente.Correo = rdr["Correo"].ToString();
                        elCliente.Direccion = rdr["Direccion"].ToString();
                        elCliente.Teléfono = rdr["Telefono"].ToString();

                    }
                }

                return elCliente;
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
        /// Monstrar todas las categorias
        /// </summary>
        /// <returns>Listado de Categorias</returns>
        public static List<Cliente> MonstrarCliente()
        {
            //Iniciamos la lista vacia de categorias
            List<Cliente> clientes = new List<Cliente>();

            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Clientes", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@Accion", "MonstrarCliente");

                //Establcer la coneccion
                sqlConnection.Open();

                //Obtener los datos de las categorias
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        clientes.Add(new Cliente { ID = Convert.ToInt32(rdr["IdCliente"]),
                            Nombre_Cliente = rdr["NombreCliente"].ToString(),
                            Direccion = rdr["Direccion"].ToString(),
                            Correo = rdr["Correo"].ToString(),
                            Teléfono = rdr["Telefono"].ToString() });
                    }
                }
                return clientes;
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
        public static Cliente BuscarCliente(int IdCliente)
        {
            Cliente elCliente = new Cliente();

            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Clientes", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@IdCliente", IdCliente);
                sqlCommand.Parameters.AddWithValue("@Accion", "BuscarCliente");

                //Establecer la coneccion
                sqlConnection.Open();

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        elCliente.ID = Convert.ToInt32(rdr["IdCliente"]);
                        elCliente.Nombre_Cliente = rdr["NombreCliente"].ToString();
                        elCliente.Correo = rdr["Correo"].ToString();
                        elCliente.Direccion = rdr["Direccion"].ToString();
                        elCliente.Teléfono = rdr["Telefono"].ToString();

                    }
                }

                return elCliente;
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

        public static void EditarCliente(Cliente cliente)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Clientes", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los parametros
                sqlCommand.Parameters.AddWithValue("@IdCliente", cliente.ID);
                sqlCommand.Parameters.AddWithValue("@NombreCliente", cliente.Nombre_Cliente);
                sqlCommand.Parameters.AddWithValue("@Telefono", cliente.Teléfono);
                sqlCommand.Parameters.AddWithValue("@Correo", cliente.Correo);
                sqlCommand.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                sqlCommand.Parameters.AddWithValue("@Accion", "Editar");

                //Strablecer la conexion
                sqlConnection.Open();

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

        public static void EliminarCliente(int IdCliente)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Clientes", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@IdCliente", IdCliente);
                sqlCommand.Parameters.AddWithValue("@Accion", "Eliminar");

                //Establecer la conexion SQL
                sqlConnection.Open();

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

        #endregion

        #region MASCOTAS
        ///

        /// <summary>
        /// Inserta una Mascota
        /// </summary>
        public static void CrearMascota(Mascota mascota)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Mascotas", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@IdCliente", mascota.IdCliente);
                sqlCommand.Parameters.AddWithValue("@NombreMascota", mascota.NombreMascota);
                sqlCommand.Parameters.AddWithValue("@Sexo", mascota.Sexo);
                sqlCommand.Parameters.AddWithValue("@IdRaza", mascota.IdRaza);
                sqlCommand.Parameters.AddWithValue("@FechaDeNacimiento", mascota.Fecha);
                sqlCommand.Parameters.AddWithValue("@Accion", "CrearMascota");

                //Establecer la conexion
                sqlConnection.Open();

                //ejecutar el comando insertado
                sqlCommand.ExecuteNonQuery();

                MensajeProcedimiento = "Datos Insertados Correctamente";

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
        /// Mostrar todas las categorias
        /// </summary>
        /// <returns>Listado de Categorias</returns>
        public static List<Mascota> MonstrarMascotas()
        {
            //Iniciamos la lista vacia de categorias
            List<Mascota> mascotas = new List<Mascota>();

            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Mascotas", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@Accion", "MonstrarMascotas");

                //Establcer la coneccion
                sqlConnection.Open();

                //Obtener los datos de las categorias
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        mascotas.Add(new Mascota
                        {
                            IdMascota = Convert.ToInt32(rdr["IdMascota"]),
                            IdCliente = Convert.ToInt32(rdr["IdCliente"]),
                            IdRaza = Convert.ToInt32(rdr["IdRaza"]),
                            NombreMascota = rdr["NombreMascota"].ToString(),
                            Sexo = rdr["Sexo"].ToString(),
                            Fecha = (DateTime)rdr["FechaDeNacimiento"]
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
        public static List<string> MonstrarMascotas22()
        {
            //Iniciamos la lista vacia de categorias
            List<string> data = new List<string>();

            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Mascotas", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@Accion", "MonstrarMascotas22");

                //Establcer la coneccion
                sqlConnection.Open();

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

        public static List<string> MonstrarMascotas23()
        {
            //Iniciamos la lista vacia de categorias
            List<string> data = new List<string>();

            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Mascotas", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@Accion", "MonstrarMascotas23");

                //Establcer la coneccion
                sqlConnection.Open();

                //Obtener los datos de las categorias
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        data.Add(rdr["NombreMascota"].ToString());
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
        public static Mascota BuscarMascota(int id)
        {
            Mascota laMascota = new Mascota();

            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Mascotas", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@IdMascota", id);
                sqlCommand.Parameters.AddWithValue("@Accion", "BuscarMascota");

                //Establecer la coneccion
                sqlConnection.Open();

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        laMascota.IdMascota = Convert.ToInt32(rdr["IdMascota"]);
                        laMascota.IdCliente = Convert.ToInt32(rdr["IdCliente"]);
                        laMascota.IdRaza = Convert.ToInt32(rdr["IdRaza"]);

                        laMascota.NombreMascota = rdr["NombreMascota"].ToString();
                        laMascota.Sexo = rdr["Sexo"].ToString();
                        laMascota.Fecha = (DateTime)rdr["FechaDeNacimiento"];
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

        public static Mascota BuscarMascotaNombre(string AliasMascota)
        {
            Mascota laMascota = new Mascota();

            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Mascotas", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@NombreMascota", AliasMascota);
                sqlCommand.Parameters.AddWithValue("@Accion", "BuscarMascotaNombre");

                //Establecer la coneccion
                sqlConnection.Open();

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
        public static void EditarMascota(Mascota mascota)
        {
            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Mascotas", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los parametros
                sqlCommand.Parameters.AddWithValue("@IdMascota", mascota.IdMascota);
                sqlCommand.Parameters.AddWithValue("@IdCliente", mascota.IdCliente);
                sqlCommand.Parameters.AddWithValue("@NombreMascota", mascota.NombreMascota);
                sqlCommand.Parameters.AddWithValue("@Sexo", mascota.Sexo);
                sqlCommand.Parameters.AddWithValue("@IdRaza", mascota.IdRaza);
                sqlCommand.Parameters.AddWithValue("@FechaDeNacimiento", mascota.Fecha);

                sqlCommand.Parameters.AddWithValue("@Accion", "EditarMascota");

                //Strablecer la conexion
                sqlConnection.Open();

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

        public static void EliminarMascota(int id)
        {
            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Mascotas", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@IdMascota", id);
                sqlCommand.Parameters.AddWithValue("@Accion", "EliminarMascota");

                //Establecer la conexion SQL
                sqlConnection.Open();

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

        #endregion

        #region Razas

        public static void CrearRaza(Mascota mascota)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Razas", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Abrir conexion
                sqlConnection.Open();

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@IdEspecie", mascota.IdEspecie);
                sqlCommand.Parameters.AddWithValue("@NombreRaza", mascota.NombreRaza);
                sqlCommand.Parameters.AddWithValue("@Altura", mascota.Altura);
                sqlCommand.Parameters.AddWithValue("@RangoPeso", mascota.RangoPeso);
                sqlCommand.Parameters.AddWithValue("@EsperanzaVida", mascota.EsperanzaVida);
                sqlCommand.Parameters.AddWithValue("@ActividadFisica", mascota.ActividadFisica);
                sqlCommand.Parameters.AddWithValue("@TipoDePelo", mascota.TipoDePelo);
                sqlCommand.Parameters.AddWithValue("@Accion", "CrearRaza");

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

        public static void EditarRaza(Mascota mascota)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Razas", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Abrir conexion
                sqlConnection.Open();

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@IdRaza", mascota.IdRaza);
                sqlCommand.Parameters.AddWithValue("@IdEspecie", mascota.IdEspecie);
                sqlCommand.Parameters.AddWithValue("@NombreRaza", mascota.NombreRaza);
                sqlCommand.Parameters.AddWithValue("@Altura", mascota.Altura);
                sqlCommand.Parameters.AddWithValue("@RangoPeso", mascota.RangoPeso);
                sqlCommand.Parameters.AddWithValue("@EsperanzaVida", mascota.EsperanzaVida);
                sqlCommand.Parameters.AddWithValue("@ActividadFisica", mascota.ActividadFisica);
                sqlCommand.Parameters.AddWithValue("@TipoDePelo", mascota.TipoDePelo);
                sqlCommand.Parameters.AddWithValue("@Accion", "EditarRaza");

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

        public static void EliminarRaza(int id)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Razas", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Abrir conexion
                sqlConnection.Open();

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@IdRaza", id);
                sqlCommand.Parameters.AddWithValue("@Accion", "EliminarRaza");

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



        public static List<Mascota> MostrarRaza()
        {
            //Iniciamos la lista vacia de categorias
            List<Mascota> mascotas = new List<Mascota>();

            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Razas", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@Accion", "MostrarRaza");

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
                            IdRaza = Convert.ToInt32(rdr["IdRaza"]),
                            NombreRaza = rdr["NombreRaza"].ToString(),
                            Altura = rdr["Altura"].ToString(),
                            RangoPeso = rdr["RangoPeso"].ToString(),
                            EsperanzaVida = rdr["EsperanzaVida"].ToString(),
                            ActividadFisica = rdr["ActividadFisica"].ToString(),
                            TipoDePelo = rdr["TipoDePelo"].ToString(),
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

        #endregion

        #region Especies
        public static void CrearEspecie(Mascota mascota)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Especies", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Abrir conexion
                sqlConnection.Open();

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@Descripcion", mascota.Descripcion);
                sqlCommand.Parameters.AddWithValue("@Familia", mascota.Familia);
                sqlCommand.Parameters.AddWithValue("@Accion", "CrearEspecie");

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

        public static void EditarEspecie(Mascota mascota)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Especies", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Abrir conexion
                sqlConnection.Open();

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@Descripcion", mascota.Descripcion);
                sqlCommand.Parameters.AddWithValue("@Familia", mascota.Familia);
                sqlCommand.Parameters.AddWithValue("@IdEspecie", mascota.IdEspecie);
                sqlCommand.Parameters.AddWithValue("@Accion", "EditarEspecie");

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

        public static void EliminarEspecie(int id)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Especies", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Abrir conexion
                sqlConnection.Open();

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@IdEspecie", id);
                sqlCommand.Parameters.AddWithValue("@Accion", "EliminarEspecie");

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



        public static List<Mascota> MostrarEspecies()
        {
            //Iniciamos la lista vacia de categorias
            List<Mascota> mascotas = new List<Mascota>();

            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Especies", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@Accion", "MostrarEspecies");

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


        #endregion

        #region Categorias
        ///

        /// <summary>
        /// Inserta una Categoria
        /// </summary>
        /// <param name="categoria">La informacion de la categoria</param>
        public static void CrearCategoria(Categoria categoria)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Categorias", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@NombreCategoria", categoria.NombreCategoria);
                sqlCommand.Parameters.AddWithValue("@Accion", "CrearCategoria");

                //Establecer la conexion
                sqlConnection.Open();

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
        public static List<Categoria> MonstrarCategorias()
        {
            //Iniciamos la lista vacia de categorias
            List<Categoria> categorias = new List<Categoria>();

            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Categorias", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@Accion", "MonstrarCategorias");

                //Establcer la coneccion
                sqlConnection.Open();

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
        public static Categoria BuscarCategoria(int id)
        {
            Categoria laCategoria = new Categoria();

            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Categorias", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@IdCategoria", id);
                sqlCommand.Parameters.AddWithValue("@Accion", "BuscarCategoria");

                //Establecer la coneccion
                sqlConnection.Open();

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
        public static Categoria BuscarCategoriasId(string NombreCategoria)
        {
            Categoria laCategoria = new Categoria();

            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Categorias", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@NombreCategoria", NombreCategoria);
                sqlCommand.Parameters.AddWithValue("@Accion", "BuscarCategoriasId");

                //Establecer la coneccion
                sqlConnection.Open();

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
        public static void EditarCategoria(Categoria categoria)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Categorias", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los parametros
                sqlCommand.Parameters.AddWithValue("@IdCategoria", categoria.Id);
                sqlCommand.Parameters.AddWithValue("@NombreCategoria", categoria.NombreCategoria);
                sqlCommand.Parameters.AddWithValue("@Accion", "EditarCategoria");

                //Strablecer la conexion
                sqlConnection.Open();

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
        public static void EliminarCategoria(int id)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Categorias", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@IdCategoria", id);
                sqlCommand.Parameters.AddWithValue("@Accion", "EliminarCategoria");

                //Establecer la conexion SQL
                sqlConnection.Open();

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
        public static List<string> MonstrarCategorias22()
        {
            //Iniciamos la lista vacia de categorias
            List<string> data = new List<string>();

            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Categorias", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@Accion", "MonstrarCategorias22");

                //Establcer la coneccion
                sqlConnection.Open();

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
        #endregion

        #region Expediente

        public static void CrearExpediente(Expediente expediente)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("CrearExpediente", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@idMascota", expediente.IdMascota);
                sqlCommand.Parameters.AddWithValue("@fechaRegistro", expediente.FechaRegistro);

                //Establecer la conexion
                sqlConnection.Open();

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

        #endregion

        #region INVENTARIO

        /// <summary>
        /// Inserta un Producto
        /// </summary>
        /// <param name="producto">La informacion del producto</param>
        public static void CrearProducto(InventarioC producto)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Inventario", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@IdProveedor", producto.IdProveedor);
                sqlCommand.Parameters.AddWithValue("@NombreProducto", producto.Descripcion);
                sqlCommand.Parameters.AddWithValue("@PrecioCosto", producto.PrecioCosto);
                sqlCommand.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                sqlCommand.Parameters.AddWithValue("@Stock", producto.Stock);
                sqlCommand.Parameters.AddWithValue("@Accion", "CrearProducto");

                //Establecer la conexion
                sqlConnection.Open();

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
        public static List<InventarioC> MonstrarInventario()
        {
            //Iniciamos la lista vacia de categorias
            List<InventarioC> inventarios = new List<InventarioC>();

            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Inventario", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@Accion", "MonstrarInventario");

                //Establcer la coneccion
                sqlConnection.Open();

                //Obtener los datos de las categorias
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        inventarios.Add(new InventarioC
                        {
                            Id = Convert.ToInt32(rdr["IdProducto"]),
                            IdCategoria = Convert.ToInt32(rdr["IdCategoria"]),
                            IdProveedor = Convert.ToInt32(rdr["IdProveedor"]),
                            Descripcion = rdr["NombreProducto"].ToString(),
                            Categoria = rdr["NombreCategoria"].ToString(),
                            PrecioCosto = Convert.ToDouble(rdr["PrecioCosto"]),
                            PrecioVenta = Convert.ToDouble(rdr["PrecioVenta"]),
                            Stock = Convert.ToInt32(rdr["Stock"])
                        });
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
        public static InventarioC BuscarProducto(int id)
        {
            InventarioC elProducto = new InventarioC();

            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Inventario", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@IdProducto", id);
                sqlCommand.Parameters.AddWithValue("@Accion", "BuscarProducto");

                //Establecer la coneccion
                sqlConnection.Open();

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        elProducto.Id = Convert.ToInt32(rdr["IdProducto"]);
                        elProducto.IdProveedor = Convert.ToInt32(rdr["IdProveedor"]);
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
        public static void EditarProducto(InventarioC producto)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Inventario", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los parametros
                sqlCommand.Parameters.AddWithValue("@id", producto.Id);
                sqlCommand.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria);
                sqlCommand.Parameters.AddWithValue("@IdProveedor", producto.IdProveedor);
                sqlCommand.Parameters.AddWithValue("@NombreProducto", producto.Descripcion);
                sqlCommand.Parameters.AddWithValue("@PrecioCosto", producto.PrecioCosto);
                sqlCommand.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                sqlCommand.Parameters.AddWithValue("@Stock", producto.Stock);
                sqlCommand.Parameters.AddWithValue("@Accion", "EditarProducto");

                //Strablecer la conexion
                sqlConnection.Open();

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

        public static void EliminarRegistro(int id)
        {
            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Inventario", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@IdProducto", id);
                sqlCommand.Parameters.AddWithValue("@Accion", "EliminarRegistro");

                //Establecer la conexion SQL
                sqlConnection.Open();

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

        #endregion

        #region VACUNAS
        ///

        //VACUNACIONES
        public static void CrearVacuna(vacunaciones producto)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("CrearVacuna", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@IdMascota", producto.IdMascota);
                sqlCommand.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                sqlCommand.Parameters.AddWithValue("@Fecha", producto.Fecha);

                //Establecer la conexion
                sqlConnection.Open();

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

        //HISTORIAL VACUNACIONES
        public static List<HistorialVacunacion> MonstrarRegistroVacunas(string nombre)
        {
            //Iniciamos la lista vacia de categorias
            List<HistorialVacunacion> historialVacunacions = new List<HistorialVacunacion>();

            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("MonstrarRegistroVacunas", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@nombre", nombre);

                //Establcer la coneccion
                sqlConnection.Open();

                //Obtener los datos de las categorias
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        historialVacunacions.Add(new HistorialVacunacion
                        {
                            IdHistorialVacunacion = Convert.ToInt32(rdr["IdHistorialVacunacion"]),
                            Mascota = rdr["AliasMascota"].ToString(),
                            Cliente = rdr["NombreCliente"].ToString(),
                            Producto = rdr["NombreProducto"].ToString(),
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

        #endregion

    }
}