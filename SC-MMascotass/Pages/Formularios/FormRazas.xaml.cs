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
        private SqlConnection sqlConnection = database.Conexion.ObtenerConexion();
        private Mascota mascota = new Mascota();
        private List<Mascota> mascotas;
        public FormRazas(bool visible)
        {
            InitializeComponent();
            CargarEspeciesCombo();
            ObtenerRazas();
        }

        private void ObtenerRazas()
        {
            mascotas = Constructores.Procedimientos.MostrarRaza();
            dgClientes.SelectedValuePath = "IdRaza";
            dgClientes.ItemsSource = mascotas;
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
                    //Obtener los valores para la mascota
                    mascota.IdEspecie = Convert.ToInt32(cmbesoecie.SelectedValuePath);
                    mascota.NombreRaza = txtNombreRaza.Text;
                    mascota.Altura = cmbaltura.SelectionBoxItem.ToString();
                    mascota.RangoPeso = cmbPesoIdeal.SelectionBoxItem.ToString();
                    mascota.EsperanzaVida = cmbEsperanzaVida.SelectionBoxItem.ToString();
                    mascota.ActividadFisica = cmbActividadFisica.SelectionBoxItem.ToString();
                    mascota.TipoDePelo = txtTipoPelo.Text;

                    //Ejecutamos
                    Constructores.Procedimientos.CrearRaza(mascota);

                    //Mensaje de inserccion exito
                    MessageBox.Show("Datos Insertados Correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    ObtenerRazas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar la raza....");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Limpiar();

                }
            }
        }

        private void Limpiar()
        {
            txtNombreRaza.Text = "";
            txtTipoPelo.Text = "";
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

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientes.SelectedValue == null)
                MessageBox.Show("Por favor selecciona una especie de la lista");
            else
            {
                //spButton1.Visibility = Visibility.Hidden;
                //spButton2.Visibility = Visibility.Visible;
                try
                {
                    //Query busqueda
                    string query = @"SELECT Veterinaria.Raza.*, Veterinaria.Especie.Descripcion
                        FROM     Veterinaria.Especie INNER JOIN
                  Veterinaria.Raza ON Veterinaria.Especie.IdEspecie = Veterinaria.Raza.IdEspecie
				  WHERE  (Veterinaria.Raza.IdRaza = @IdRaza)";

                    //Establecer la coneccion
                    sqlConnection.Open();

                    //Crear el comando SQL
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                    //Establecer el valor del parametro
                    sqlCommand.Parameters.AddWithValue("@IdRaza", Convert.ToInt32(dgClientes.SelectedValue));

                    using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            txtNombreRaza.Text = rdr["NombreRaza"].ToString();
                            cmbaltura.Text = rdr["Altura"].ToString();
                            cmbActividadFisica.Text = rdr["ActividadFisica"].ToString();
                            cmbPesoIdeal.Text = rdr["RangoPeso"].ToString();
                            cmbesoecie.Text = rdr["Descripcion"].ToString();
                            cmbEsperanzaVida.Text = rdr["EsperanzaVida"].ToString();
                            txtTipoPelo.Text = rdr["TipoDePelo"].ToString();
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

        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
