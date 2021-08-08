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
                SqlCommand sqlCommand = new SqlCommand("Razas", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Enviar Parametros
                sqlCommand.Parameters.AddWithValue("@Accion", "CargarEspeciesCombo");

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
                MessageBox.Show("Error al cargar las Especies");
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

                ObtenerRazas();
                Limpiar();
            }
        }

        private void Limpiar()
        {
            spButton1.Visibility = Visibility.Visible;
            spButton2.Visibility = Visibility.Hidden;
            txtNombreRaza.Text = "";
            txtTipoPelo.Text = "";
            cmbActividadFisica.Text = "";
            cmbaltura.Text = "";
            cmbesoecie.Text = "";
            cmbPesoIdeal.Text = "";
            cmbEsperanzaVida.Text = "";
        }

        private void btnRestablecer_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValores())
            {

                //Obtener los valores para la mascota
                mascota.IdEspecie = Convert.ToInt32(cmbesoecie.SelectedValuePath);
                mascota.NombreRaza = txtNombreRaza.Text;
                mascota.Altura = cmbaltura.SelectionBoxItem.ToString();
                mascota.RangoPeso = cmbPesoIdeal.SelectionBoxItem.ToString();
                mascota.EsperanzaVida = cmbEsperanzaVida.SelectionBoxItem.ToString();
                mascota.ActividadFisica = cmbActividadFisica.SelectionBoxItem.ToString();
                mascota.TipoDePelo = txtTipoPelo.Text;

                mascota.IdRaza = Convert.ToInt32(dgClientes.SelectedValue);

                //Ejecutamos
                Constructores.Procedimientos.EditarRaza(mascota);

                ObtenerRazas();
                Limpiar();
            }
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                spButton1.Visibility = Visibility.Hidden;
                spButton2.Visibility = Visibility.Visible;
                mascota = Constructores.Procedimientos.CargarDatosEditarRazas(Convert.ToInt32(dgClientes.SelectedValue));
                txtNombreRaza.Text = mascota.NombreRaza;
                cmbaltura.Text = mascota.Altura;
                cmbActividadFisica.Text = mascota.ActividadFisica;
                cmbPesoIdeal.Text =mascota.RangoPeso;
                cmbesoecie.Text = mascota.Descripcion;
                cmbEsperanzaVida.Text = mascota.EsperanzaVida;
                txtTipoPelo.Text = mascota.TipoDePelo;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            if (dgClientes.SelectedValue == null)
                MessageBox.Show("Por favor, seleccione una especie de la lista");
            else
            {
                //Monstrar mensjae de confirmacion
                MessageBoxResult result = MessageBox.Show("¿Deseas eliminar la raza?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    //Eliminar la mascotas
                    Constructores.Procedimientos.EliminarRaza(Convert.ToInt32(dgClientes.SelectedValue));
                }
            }
            //Actualizar el listbox de mascotas
            ObtenerRazas();
            Limpiar();

        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();   
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
    }
}
