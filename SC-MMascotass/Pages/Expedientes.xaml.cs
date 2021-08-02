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

namespace SC_MMascotass.Pages
{
    /// <summary>
    /// Interaction logic for Expedientes.xaml
    /// </summary>
    public partial class Expedientes : UserControl
    {
        public Expedientes()
        {
            InitializeComponent();
        }

        private void btnNuevoExpediente_Click(object sender, RoutedEventArgs e)
        {
            Formularios.FormExpediente expediente = new Formularios.FormExpediente();
            expediente.Show();
        }

        private void btnVerExpediente_Click(object sender, RoutedEventArgs e)
        {
            Formularios.FormDetalleExpediente detalleExpediente = new Formularios.FormDetalleExpediente();
            detalleExpediente.Show();
        }
    }
}
