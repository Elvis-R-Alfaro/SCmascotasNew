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
using System.Windows.Shapes;

namespace SC_MMascotass.Pages.Formularios
{
    /// <summary>
    /// Interaction logic for FormDetalleExpediente.xaml
    /// </summary>
    public partial class FormDetalleExpediente : Window
    {
        public static int IdDetalle;

        public static string mascota;

        private Constructores.Expediente expediente = new Constructores.Expediente();

        public FormDetalleExpediente(bool visible)
        {
            InitializeComponent();

            if (visible)
            {
                expediente = Constructores.Procedimientos.BuscarDetalleExpedienteId(IdDetalle);
                txtProducto.Text = expediente.Producto;
                txtSintomas.Text = expediente.Sintomas;
                txtPatologia.Text = expediente.Patologia;
                txtTratamiento.Text = expediente.TratamientoRecomendado;
                txtMascota.Text = mascota;

                btnAceptar.Visibility = Visibility.Visible;
                btnRegresaar.Visibility = Visibility.Visible;
                btnGuardar.Visibility = Visibility.Hidden;
                btnCancelar.Visibility = Visibility.Hidden;
            }
        }

        private void ObtenerValores()
        {

        }

        private void Limpiar()
        {
            btnAceptar.Visibility = Visibility.Hidden;
            btnRegresaar.Visibility = Visibility.Hidden;
            btnGuardar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRegresaar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
