using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//Agregar los namespaces
using System.Data.SqlClient;
using System.Configuration;


namespace SC_MMascotass.Pages.Formularios
{
    /// <summary>
    /// Lógica de interacción para FormEspecie.xaml
    /// </summary>
    public partial class FormEspecies : Window
    {
        private Mascota mascota = new Mascota();
        private List<Mascota> mascotas;

        private SqlConnection sqlConnection = database.Conexion.ObtenerConexion();
        public FormEspecies(bool visible)
        {
            InitializeComponent();
            ObtenerMascotas();
        }

        private void ObtenerMascotas()
        {
            mascotas = Constructores.Procedimientos.MostrarEspecies();
            dgClientes.SelectedValuePath = "IdEspecie";
            dgClientes.ItemsSource = mascotas;
        }

        private bool VerificarValores()
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("¡Ingrese la Descripción de la especie!");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtFamilia.Text))
            {
                MessageBox.Show("¡Ingrese la Familia de la especie!");
                return false;
            }
            return true;
        }
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValores())
            {
                try
                {
                    //Obtener los valores para la mascota
                    mascota.Descripcion = txtDescripcion.Text;
                    mascota.Familia = txtFamilia.Text;

                    //Ejecutamos
                    Constructores.Procedimientos.CrearEspecie(mascota);

                    //Mensaje de inserccion exito
                    MessageBox.Show("Datos Insertados Correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    ObtenerMascotas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar la mascota....");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Limpiar();

                }
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientes.SelectedValue == null)
                MessageBox.Show("Por favor selecciona una especie de la lista");
            else
            {
                spButton1.Visibility = Visibility.Hidden;
                spButton2.Visibility = Visibility.Visible;
                try
                {
                    //Query busqueda
                    string query = @"SELECT *
                                    FROM     Veterinaria.Especie
                                    WHERE  IdEspecie = @IdEspecie";

                    //Establecer la coneccion
                    sqlConnection.Open();

                    //Crear el comando SQL
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                    //Establecer el valor del parametro
                    sqlCommand.Parameters.AddWithValue("@IdEspecie", Convert.ToInt32(dgClientes.SelectedValue));

                    using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            txtDescripcion.Text = rdr["Descripcion"].ToString();
                            txtFamilia.Text = rdr["Familia"].ToString();
                        }
                    }

                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    //Cerrar la conexio
                    sqlConnection.Close();
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgClientes.SelectedValue == null)
                    MessageBox.Show("Por favor, seleccione una especie de la lista");
                else
                {
                    //Monstrar mensjae de confirmacion
                    MessageBoxResult result = MessageBox.Show("¿Deseas eliminar la especie?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        //Eliminar la mascotas
                        Constructores.Procedimientos.EliminarEspecie(Convert.ToInt32(dgClientes.SelectedValue));
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al eliminar la mascota...");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //Actualizar el listbox de mascotas
                ObtenerMascotas();
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValores())
            {
                try
                {
                    //Query de actualizacion
                    string query = @"UPDATE Veterinaria.Especie
                                   SET Descripcion = @Descripcion
                                      ,Familia = @Familia
                                 WHERE IdEspecie = @IdEspecie";

                    //Strablecer la conexion
                    sqlConnection.Open();

                    //Crear el comando SQL
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                    //Establecer los valores de los parametros
                    sqlCommand.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);
                    sqlCommand.Parameters.AddWithValue("@Familia", txtFamilia.Text);
                    sqlCommand.Parameters.AddWithValue("@IdEspecie", Convert.ToInt32(dgClientes.SelectedValue));

                    //Ejecutar el comando de actualizar
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception es)
                {
                    throw es;
                }
                finally
                {
                    //Cerrar conexcion
                    sqlConnection.Close();
                    MessageBox.Show("La especie se han editado correctamente");
                    Limpiar();
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            txtDescripcion.Text = "";
            txtFamilia.Text = "";
            ObtenerMascotas();
            spButton1.Visibility = Visibility.Visible;
            spButton2.Visibility = Visibility.Hidden;
        }
    }
}
