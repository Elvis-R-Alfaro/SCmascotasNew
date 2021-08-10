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
    /// Interaction logic for FormUsuRestablecerClave.xaml
    /// </summary>
    public partial class FormUsuRestablecerClave : UserControl
    {

       

        public FormUsuRestablecerClave()
        {
            InitializeComponent();
        }

        public bool cerrar;

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtClave.Password == txtConfirmarClave.Password)
            {
                cerrar=Constructores.Procedimientos.NuevaClave(txtConfirmarClave.Password.ToString(), Usuario.GlobalIdUsuario);
                if (cerrar)
                {
                    MenuPrincipal mp = new MenuPrincipal();
                    mp.btnCerrarSesion_Click(sender, e);
                    var window = Window.GetWindow(this);
                    window.Close();
                }
            }
            else
            {
                MessageBox.Show("No coninciden"); 
            }
        }
    }
}
