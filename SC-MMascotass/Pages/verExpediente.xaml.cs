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

namespace SC_MMascotass.Pages
{
    /// <summary>
    /// Interaction logic for verExpediente.xaml
    /// </summary>
    public partial class verExpediente : Window
    {
        private Constructores.Expediente expediente = new Constructores.Expediente();
        private List<Constructores.Expediente> expedientes;

        public static int id;
        public verExpediente()
        {
            InitializeComponent();
            ObtenerValores();


        }

        private void ObtenerValores()
        {
            expedientes = Constructores.Procedimientos.BuscarExpediente(id);
            dgDetalle.SelectedValuePath = "IdDetalle";
            dgDetalle.ItemsSource = expedientes;
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            //Formularios.FormDetalleExpediente.mascota = txtNombreMascota.Text;
            Formularios.FormDetalleExpediente detalleExpediente = new Formularios.FormDetalleExpediente();

            detalleExpediente.txtMascota.Text = txtNombreMascota.Text;
            detalleExpediente.Show();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
