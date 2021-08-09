using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Agregar los namespaces
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;

//Generar clave
using System.Net;
using System.Net.Mail;

namespace SC_MMascotass.Constructores
{
    public class Procedimientos
    {
        //Conexion
        private static SqlConnection sqlConnection = database.Conexion.ObtenerConexion();
        public static int error;
        
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
                SqlCommand sqlCommand = new SqlCommand("Usuarios", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@username", username);
                sqlCommand.Parameters.AddWithValue("@Accion", "BuscarUsuario");

                sqlConnection.Open();

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        //Obtener los valores del usuarios si la consulta retorna valores
                        usuario.IdUsuario = Convert.ToInt32(rdr["IdUsuario"]);
                        usuario.NombreCompleto = rdr["Nombre"].ToString();
                        usuario.Usename = rdr["Usuario"].ToString();
                        Usuario.NombreCompletoGlobal = usuario.NombreCompleto;
                        Usuario.GlobalClaveGen = Convert.ToBoolean(rdr["ClaveGen"]);
                        usuario.Clave = rdr["Clave"].ToString();
                        usuario.Estado = Convert.ToBoolean(rdr["Estado"]);
                    }

                }
                //retornar el usuario con los valores
                return usuario;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un error al buscar el usuario.");
                throw e;
            }
            finally
            {
                //Cerrar la seccion
                sqlConnection.Close();
            }
        }

        public static void EvtNuevaContrasena(string correo)
        {

            Usuario usuario = new Usuario();

            try
            {
                //Crear comando SQL
                SqlCommand sqlCommand = new SqlCommand("Usuarios", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@correo", correo);
                sqlCommand.Parameters.AddWithValue("@Accion", "ValidarCorreo");
                
                sqlConnection.Open();
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    if (rdr.Read())
                    {

                        usuario.IdUsuario = Convert.ToInt32(rdr["IdUsuario"]);
                        rdr.Close();
                        GenerarNuevaContrasena(correo, usuario.IdUsuario);

                    }
                    else
                    {
                        MessageBox.Show("Correo no encontrado");

                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                //Cerrar la seccion
                sqlConnection.Close();
            }
        }

        public static void GenerarNuevaContrasena(string email, int usuarioId)
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            int nuevaContrasena = rd.Next(10000000, 99999999);
             

            SqlCommand cmd = new SqlCommand("Usuarios", sqlConnection);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            bool CG = true;

            cmd.Parameters.AddWithValue("@correo", email);
            cmd.Parameters.AddWithValue("@clave", nuevaContrasena);
            cmd.Parameters.AddWithValue("@userid", usuarioId);
            cmd.Parameters.AddWithValue("@claveGen", CG);
            cmd.Parameters.AddWithValue("@Accion", "NuevaClave");
            try
            {
                int filasAfectadas = cmd.ExecuteNonQuery();
                if (filasAfectadas != 0)
                {
                    EnviarCorreoContrasena(nuevaContrasena, email);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //Cerrar la seccion
                sqlConnection.Close();
            }
        }

        public static void EnviarCorreoContrasena(int contrasenaNueva, string correo)
        {
            string contraseña = "#mimascota21";
            string mensaje = string.Empty;
            //Creando el correo electronico
            string destinatario = correo;
            string remitente = "mimascotasistema@gmail.com";
            string asunto = "Nueva contraseña Mi Mascota Sistema de Control";
            string cuerpoDelMesaje = "Su nueva contraseña es" + " " + Convert.ToString(contrasenaNueva);
            MailMessage ms = new MailMessage(remitente, destinatario, asunto, cuerpoDelMesaje);




            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("mimascotasistema@gmail.com", contraseña);

            try
            {
                Task.Run(() =>
                {
                    smtp.Send(ms);
                    ms.Dispose();
                    MessageBox.Show("Correo enviado, revisar su bandeja de entrada");
                }
                );

                MessageBox.Show("Esta tarea puede tardar unos segundos, por favor espere.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar correo electronico: " + ex.Message);
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

                //MensajeProcedimiento = "Datos Insertados Correctamente";

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
                            NombreRaza = rdr["NombreRaza"].ToString(),
                            Cliente = rdr["NombreCliente"].ToString(),
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
                        laMascota.NombreRaza = rdr["NombreRaza"].ToString();
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

                //Mensaje de inserccion exito
                MessageBox.Show("Datos Insertados Correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                error = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al momento de insertar la raza....");
                error++;
                Console.WriteLine(ex.Message);
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

                //Mensaje de inserccion exito
                MessageBox.Show("Datos Editados Correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                error = 0;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un error al momento de editar la raza....");
                error++;
                Console.WriteLine(e.Message);
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

                MessageBox.Show("Se ha eliminado la raza correctamente...");
                error = 0;
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un error al eliminar la raza...");
                error++;
                Console.WriteLine(e.Message);
            }
            finally
            {
                //Cerrar la conexion
                sqlConnection.Close();
            }
        }

        public static Mascota CargarDatosEditarRazas(int id)
        {
            Mascota mascota = new Mascota();
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Razas", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@IdRaza", id);
                sqlCommand.Parameters.AddWithValue("@Accion", "CargarDatosEditarRazas");

                //Establecer la coneccion
                sqlConnection.Open();

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        mascota.NombreRaza = rdr["NombreRaza"].ToString();
                        mascota.Altura = rdr["Altura"].ToString();
                        mascota.ActividadFisica = rdr["ActividadFisica"].ToString();
                        mascota.RangoPeso = rdr["RangoPeso"].ToString();
                        mascota.Descripcion = rdr["Descripcion"].ToString();
                        mascota.EsperanzaVida = rdr["EsperanzaVida"].ToString();
                        mascota.TipoDePelo = rdr["TipoDePelo"].ToString();
                    }
                }
                error = 0;
                return mascota;

            }
            catch (Exception e)
            {
                MessageBox.Show("Error al cargar los datos");
                Console.WriteLine(e.Message);
                error++;
                return mascota;
            }
            finally
            {
                //Cerrar la conexio
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
                            Descripcion = rdr["Descripcion"].ToString(),
                            Altura = rdr["Altura"].ToString(),
                            RangoPeso = rdr["RangoPeso"].ToString(),
                            EsperanzaVida = rdr["EsperanzaVida"].ToString(),
                            ActividadFisica = rdr["ActividadFisica"].ToString(),
                            TipoDePelo = rdr["TipoDePelo"].ToString(),
                        });
                    }
                }
                error = 0;
                return mascotas;
            }
            catch (Exception e)
            {
                error++;
                throw e;
            }
            finally
            {
                //Cerrar la conexion
                sqlConnection.Close();
            }
        }

        public static List<Mascota> CargarEspeciesCombo()
        {
            //Iniciamos la lista vacia de categorias
            List<Mascota> especies = new List<Mascota>();

            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Razas", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Enviar Parametros
                sqlCommand.Parameters.AddWithValue("@Accion", "CargarEspeciesCombo");

                //Abrir conexion
                sqlConnection.Open();

                //Obtener los datos de las categorias
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        especies.Add(new Mascota
                        {
                            IdEspecie = Convert.ToInt32(rdr["IdEspecie"]),
                            Descripcion = rdr["Descripcion"].ToString(),
                        });
                    }
                }
                error = 0;
                return especies;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("Error al cargar las Especies");
                error++;
                return especies;
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

                //Mensaje de inserccion exito
                MessageBox.Show("Datos Insertados Correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un error al momento de insertar la mascota....");
                Console.WriteLine(e.Message);
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

                MessageBox.Show("La especie se han editado correctamente");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al editar la especie");
                Console.WriteLine(e.Message);
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

                MessageBox.Show("Se ha eliminado la especie correctamente");
            }
            catch (Exception e)
            {

                MessageBox.Show("Ha ocurrido un error al eliminar la especie...");
                Console.WriteLine(e.Message);
            }
            finally
            {
                //Cerrar la conexion
                sqlConnection.Close();
            }
        }

        public static Mascota CargarDatosEditarEspecies(int id)
        {
            Mascota mascota = new Mascota();
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Especies", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@IdEspecie", id);
                sqlCommand.Parameters.AddWithValue("@Accion", "CargarDatosEditarEspecies");

                //Establecer la coneccion
                sqlConnection.Open();

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        mascota.Descripcion = rdr["Descripcion"].ToString();
                        mascota.Familia = rdr["Familia"].ToString();
                    }
                }
                return mascota;

            }
            catch (Exception e)
            {
                MessageBox.Show("Error al cargar los datos");
                Console.WriteLine(e.Message);
                return mascota;
            }
            finally
            {
                //Cerrar la conexio
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

                MessageBox.Show("Datos insertados correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un error al momento de insertar la categoria....");
                Console.WriteLine(e.Message);
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
        public static List<Categoria> MostrarCategorias()
        {
            //Iniciamos la lista vacia de categorias
            List<Categoria> categorias = new List<Categoria>();

            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Categorias", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@Accion", "MostrarCategorias");

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

                //Mensaje de actualizacion realizada
                MessageBox.Show("Datos Modificados correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al momento de editar la categoria....");
                Console.WriteLine(e.Message);
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
                MessageBox.Show("Ha ocurrido un error al eliminar la habitacion...");
                Console.WriteLine(e.Message);
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
        public static List<string> MostrarCategorias22()
        {
            //Iniciamos la lista vacia de categorias
            List<string> data = new List<string>();

            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Categorias", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@Accion", "MostrarCategorias22");

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
                SqlCommand sqlCommand = new SqlCommand("Expediente", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@idMascota", expediente.IdMascota);
                sqlCommand.Parameters.AddWithValue("@fechaRegistro", expediente.FechaRegistro);
                sqlCommand.Parameters.AddWithValue("@Accion", "Insertar");

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


        public static List<Expediente> MostrarExpedientes()
        {
            //Iniciamos la lista vacia de categorias
            List<Expediente> expedientes= new List<Expediente>();

            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Expediente", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@Accion", "MostrarExpediente");

                //Abrir conexion
                sqlConnection.Open();

                //Obtener los datos de las categorias
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        expedientes.Add(new Expediente
                        {
                            IdExpediente = Convert.ToInt32(rdr["IdExpediente"]),
                            NombreMascota = rdr["NombreMascota"].ToString(),
                            NombreCliente = rdr["NombreCliente"].ToString(),
                            UltimaVisita = rdr["UltimaVisita"].ToString()
                        });
                    }
                }
                return expedientes;
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


        public static List<Expediente> BuscarExpediente(int IdExpediente)
        {
            //Iniciamos la lista vacia de categorias
            List<Expediente> expedientes = new List<Expediente>();
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Expediente", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@idExpediente", IdExpediente);
                sqlCommand.Parameters.AddWithValue("@Accion", "BuscarExpedienteId");

                //Establecer la coneccion
                sqlConnection.Open();

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        expedientes.Add(new Expediente
                        {
                           IdDetalle = Convert.ToInt32(rdr["IdDetalle"]),
                            IdExpediente = Convert.ToInt32(rdr["IdExpediente"]),
                            IdProducto = Convert.ToInt32(rdr["IdProductoUtilizado"]),
                            Producto = rdr["NombreProducto"].ToString(),
                            Sintomas = rdr["Descripcion"].ToString(),
                            Patologia = rdr["Patologia"].ToString(),
                            TratamientoRecomendado = rdr["TratamientoRecomendado"].ToString(),
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"]),
                            FechaRegistroDeta = (DateTime)rdr["FechaDeRegistro"],
                            Usuario = rdr["Nombre"].ToString()
                    });
                        

                    }
                }

                return expedientes;
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


        public static Expediente BuscarDetalleExpedienteId(int IdDetalleExpediente)
        {
            Expediente elExpediente = new Expediente();
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Expediente", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@idExpediente", IdDetalleExpediente);
                sqlCommand.Parameters.AddWithValue("@Accion", "BuscarDetalleExpedienteId");

                //Establecer la coneccion
                sqlConnection.Open();

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        elExpediente.IdDetalle = Convert.ToInt32(rdr["IdDetalle"]);
                        elExpediente.IdExpediente = Convert.ToInt32(rdr["IdExpediente"]);
                        elExpediente.IdProducto = Convert.ToInt32(rdr["IdProductoUtilizado"]);
                        elExpediente.Producto = rdr["NombreProducto"].ToString();
                        elExpediente.Sintomas = rdr["Descripcion"].ToString();
                        elExpediente.Patologia = rdr["Patologia"].ToString();
                        elExpediente.TratamientoRecomendado = rdr["TratamientoRecomendado"].ToString();
                        elExpediente.IdUsuario = Convert.ToInt32(rdr["IdUsuario"]);
                        elExpediente.FechaRegistroDeta = (DateTime)rdr["FechaDeRegistro"];
                        elExpediente.Usuario = rdr["Nombre"].ToString();


                    }
                }

                return elExpediente;
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
                sqlCommand.Parameters.AddWithValue("@@IdCategoria", producto.IdCategoria);
                sqlCommand.Parameters.AddWithValue("@NombreProducto", producto.Descripcion);
                sqlCommand.Parameters.AddWithValue("@PrecioCosto", producto.PrecioCosto);
                sqlCommand.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                sqlCommand.Parameters.AddWithValue("@Stock", producto.Stock);
                sqlCommand.Parameters.AddWithValue("@Accion", "CrearProducto");

                //Establecer la conexion
                sqlConnection.Open();

                //ejecutar el comando insertado
                sqlCommand.ExecuteNonQuery();

                //Mensaje de inserccion exito
                MessageBox.Show("Datos Insertados Correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al momento de insertar los datos en el inventario....");
                Console.WriteLine(ex.Message);
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
        public static List<InventarioC> MostrarInventario()
        {
            //Iniciamos la lista vacia de categorias
            List<InventarioC> inventarios = new List<InventarioC>();

            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Inventario", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@Accion", "MostrarInventario");

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
                            NombreProveedor = rdr["NombreProveedor"].ToString(),
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

        public static List<InventarioC> CargarComboboxProveedores()
        {
            //Iniciamos la lista vacia de categorias
            List<InventarioC> inventarios = new List<InventarioC>();

            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Inventario", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Enviar Parametros
                sqlCommand.Parameters.AddWithValue("@Accion", "CargarProveedorCombo");

                //Abrir conexion
                sqlConnection.Open();

                //Obtener los datos de las categorias
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        inventarios.Add(new InventarioC
                        {
                            IdProveedor = Convert.ToInt32(rdr["IdProveedor"]),
                            NombreProveedor = rdr["NombreProveedor"].ToString(),
                        });
                    }
                }
                error = 0;
                return inventarios;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("Error al cargar los proveedores");
                error++;
                return inventarios;
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
                        elProducto.NombreProveedor = rdr["NombreProveedor"].ToString();
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
        /// Obtiene productos de inventario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<InventarioC> BuscarProductoLike(string producto)
        {
            List<InventarioC> inventarios = new List<InventarioC>();

            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Inventario", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@NombreProducto", producto);
                sqlCommand.Parameters.AddWithValue("@Accion", "BuscarProductoLike");

                //Establecer la coneccion
                sqlConnection.Open();

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        inventarios.Add(new InventarioC
                        {
                            Id = Convert.ToInt32(rdr["IdProducto"]),
                            IdCategoria = Convert.ToInt32(rdr["IdCategoria"]),
                            Descripcion = rdr["NombreProducto"].ToString(),
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
                sqlCommand.Parameters.AddWithValue("@IdProducto", producto.Id);
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

                //Mensaje de actualizacion realizada
                MessageBox.Show("Datos Modificado Correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al momento de actualizar el producto....");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //Cerrar conexcion
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// edita un producto existente
        /// </summary>
        /// <param name="producto"></param>
        public static void EditarStock(InventarioC producto)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Inventario", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los parametros
                sqlCommand.Parameters.AddWithValue("@IdProducto", producto.Id);
                sqlCommand.Parameters.AddWithValue("@Stock", producto.Stock);
                sqlCommand.Parameters.AddWithValue("@Accion", "EditarStock");

                //Strablecer la conexion
                sqlConnection.Open();

                //Ejecutar el comando de actualizar
                sqlCommand.ExecuteNonQuery();

                //Mensaje de actualizacion realizada
                MessageBox.Show("Datos Modificado Correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al momento de actualizar el producto....");
                Console.WriteLine(ex.Message);
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

        public static void EliminarProducto(int id)
        {
            try
            {

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Inventario", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@IdProducto", id);
                sqlCommand.Parameters.AddWithValue("@Accion", "EliminarProducto");

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

        #region Proveedores
        public static void CrearProveedor(InventarioC proveedor)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Proveedores", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Abrir conexion
                sqlConnection.Open();

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@NombreProveedor", proveedor.NombreProveedor);
                sqlCommand.Parameters.AddWithValue("@Compania", proveedor.Compania);
                sqlCommand.Parameters.AddWithValue("@Telefono", proveedor.Telefono);
                sqlCommand.Parameters.AddWithValue("@Correo", proveedor.Correo);
                sqlCommand.Parameters.AddWithValue("@Direccion", proveedor.DirreccionEmpresa);
                sqlCommand.Parameters.AddWithValue("@Accion", "CrearProveedor");

                //ejecutar el comando insertado
                sqlCommand.ExecuteNonQuery();

                //Mensaje de inserccion exito
                MessageBox.Show("Datos Insertados Correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un error al momento de insertar el proveedor....");
                Console.WriteLine(e.Message);
            }
            finally
            {
                //Cerrar la conexion
                sqlConnection.Close();
            }
        }

        public static void EditarProveerdor(InventarioC proveedor)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Proveedores", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Abrir conexion
                sqlConnection.Open();

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("IdProveedor", proveedor.IdProveedor);
                sqlCommand.Parameters.AddWithValue("@NombreProveedor", proveedor.NombreProveedor);
                sqlCommand.Parameters.AddWithValue("@Compania", proveedor.Compania);
                sqlCommand.Parameters.AddWithValue("@Telefono", proveedor.Telefono);
                sqlCommand.Parameters.AddWithValue("@Correo", proveedor.Correo);
                sqlCommand.Parameters.AddWithValue("@Direccion", proveedor.DirreccionEmpresa);
                sqlCommand.Parameters.AddWithValue("@Accion", "EditarProveedor");

                //ejecutar el comando insertado
                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("El proveedor se han editado correctamente");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al editar el proveedor");
                Console.WriteLine(e.Message);
            }
            finally
            {
                //Cerrar la conexion
                sqlConnection.Close();
            }
        }

        public static void EliminarProveedor(int id)
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Proveedores", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Abrir conexion
                sqlConnection.Open();

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@IdProveedor", id);
                sqlCommand.Parameters.AddWithValue("@Accion", "EliminarProveedor");

                //ejecutar el comando insertado
                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Se ha eliminado el proveedor correctamente");
            }
            catch (Exception e)
            {

                MessageBox.Show("Ha ocurrido un error al eliminar el proveedor...");
                Console.WriteLine(e.Message);
            }
            finally
            {
                //Cerrar la conexion
                sqlConnection.Close();
            }
        }

        public static InventarioC CargarDatosEditarProveedor(int id)
        {
            InventarioC proveedor = new InventarioC();
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Proveedores", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer el valor del parametro
                sqlCommand.Parameters.AddWithValue("@IdProveedor", id);
                sqlCommand.Parameters.AddWithValue("@Accion", "CargarDatosEditarProveedor");

                //Establecer la coneccion
                sqlConnection.Open();

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        proveedor.NombreProveedor = rdr["NombreProveedor"].ToString();
                        proveedor.Compania = rdr["Compania"].ToString();
                        proveedor.Telefono = rdr["Telefono"].ToString();
                        proveedor.Correo = rdr["Correo"].ToString();
                        proveedor.DirreccionEmpresa = rdr["DireccionEmpresa"].ToString();
                    }
                }
                return proveedor;

            }
            catch (Exception e)
            {
                MessageBox.Show("Error al cargar los datos");
                Console.WriteLine(e.Message);
                return proveedor;
            }
            finally
            {
                //Cerrar la conexio
                sqlConnection.Close();
            }
        }

        public static List<InventarioC> MostrarProveedor()
        {
            //Iniciamos la lista vacia de categorias
            List<InventarioC> proveedores = new List<InventarioC>();

            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Proveedores", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Establecer los valores de los paramawtros
                sqlCommand.Parameters.AddWithValue("@Accion", "MostrarProveedor");

                //Abrir conexion
                sqlConnection.Open();

                //Obtener los datos de las categorias
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        proveedores.Add(new InventarioC
                        {
                            IdProveedor = Convert.ToInt32(rdr["IdProveedor"]),
                            NombreProveedor = rdr["NombreProveedor"].ToString(),
                            Compania = rdr["Compania"].ToString(),
                            Telefono = rdr["Telefono"].ToString(),
                            Correo = rdr["Correo"].ToString(),
                            DirreccionEmpresa = rdr["DireccionEmpresa"].ToString(),
                        });
                    }
                }
                return proveedores;
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
