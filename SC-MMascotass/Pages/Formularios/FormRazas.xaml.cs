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
    /// Lógica de interacción para FormRaza.xaml
    /// </summary>
    public partial class FormRazas : Window
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["SC_MMascotass.Properties.Settings.MascotasConnectionString"].ConnectionString;
        private static SqlConnection sqlConnection = new SqlConnection(connectionString);
        private Mascota mascota = new Mascota();
        public FormRazas(bool visible)
        {
            InitializeComponent();
            CargarEspeciesCombo();
        }

        private void CargarEspeciesCombo()
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("CargarEspeciesCombo", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Abrir conexion
                sqlConnection.Open();

                SqlDataReader dr = sqlCommand.ExecuteReader();
                while (dr.Read())
                {
                    cmbesoecie.Items.Add(dr["Descripcion"].ToString());
                    cmbesoecie.SelectedValuePath = dr["IdEspecie"].ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //Cerrar la conexion
                sqlConnection.Close();
            }
        }

        private bool VerificarValores()
        {
            if (cmbesoecie.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Por favor seleccione una especie");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtNombreRaza.Text))
            {
                MessageBox.Show("¡Ingrese el nombre de la raza!");
                return false;
            }
            else if (cmbaltura.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Por favor seleccione una altura");
                return false;
            }
            else if (cmbPesoIdeal.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Por favor seleccione un rango de peso");
                return false;
            }
            else if (cmbEsperanzaVida.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Por favor seleccione una esperanza de vida");
                return false;
            }

            else if (cmbActividadFisica.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Por favor seleccione una actividad fisica");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtTipoPelo.Text))
            {
                MessageBox.Show("¡Ingrese el tipo de pelo de la raza!");
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
                    //Query de insertar
                    string query = @"INSERT INTO Veterinaria.Raza
                                   (IdEspecie
                                   ,NombreRaza
                                   ,Altura
                                   ,RangoPeso
                                   ,EsperanzaVida
                                   ,ActividadFisica
                                   ,TipoDePelo)
                             VALUES
                                   ()";

                    //Establecer la conexion
                    sqlConnection.Open();



                    //Crear el comando SQL
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                    //Establecer los valores de los paramawtros
                    //sqlCommand.Parameters.AddWithValue("@IdCliente", mascota.IdCliente);
                    //sqlCommand.Parameters.AddWithValue("@AliasMascota", mascota.NombreMascota);
                    //sqlCommand.Parameters.AddWithValue("@Raza", mascota.NombreRaza);
                    //sqlCommand.Parameters.AddWithValue("@Fecha", mascota.Fecha);

                    //ejecutar el comando insertado
                    sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar la raza....");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    this.Close();

                }
            }
        }

        private void btnRestablecer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNuevaEspecie_Click(object sender, RoutedEventArgs e)
        {
            Formularios.FormEspecies especie = new Formularios.FormEspecies(false);
            especie.Show();
        }
    }
}
