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
    /// Interaction logic for FormRecuperarClave.xaml
    /// </summary>
    public partial class FormRecuperarClave : Window
    {
        public FormRecuperarClave()
        {
            InitializeComponent();
        }

        private void btnRecuperar_Click(object sender, RoutedEventArgs e)
        {
            Constructores.Procedimientos.EvtNuevaContrasena(txtCorreoElectronico.Text);
        }
    }
}
