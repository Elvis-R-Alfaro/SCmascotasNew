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
    /// Lógica de interacción para Usuarios.xaml
    /// </summary>
    public partial class Usuarios : UserControl
    {
        private Usuario usuario = new Usuario();
        private List<Usuario> usuarios;
        public Usuarios()
        {
            InitializeComponent();
            ObtenerUsuarios();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Formularios.FormUsuarios usuario = new Formularios.FormUsuarios(false);
            usuario.Show();
        }

        private void ObtenerUsuarios()
        {
            //usuarios = usuario.MostrarUsuario();
            //dgClientes.SelectedValuePath = "Id";
            //dgClientes.ItemsSource = usuarios;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
