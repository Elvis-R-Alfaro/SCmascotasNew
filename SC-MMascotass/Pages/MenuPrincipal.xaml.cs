using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SC_MMascotass
{
    /// <summary>
    /// Interaction logic for MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        public MenuPrincipal()
        {
            InitializeComponent();
            ErrorLog error = new ErrorLog();
            error.Add(this, "test");
            AgregarForm(new Pages.Inicio());

            if (Usuario.GlobalClaveGen)
            {
                colapsarModulos();
            }

            if (Usuario.CargoGlobal != "Veterinario" || Usuario.CargoGlobal != "Administrador")
            {
                btnUsuarios.Visibility = Visibility.Collapsed;
            }
        }

        private void colapsarModulos()
        {
            btnInicio.Visibility = Visibility.Collapsed;
            btnInventario.Visibility = Visibility.Collapsed;
            btnMascotas.Visibility = Visibility.Collapsed;
            btnReportes.Visibility = Visibility.Collapsed;
            btnClientes.Visibility = Visibility.Collapsed;
            btnUsuarios.Visibility = Visibility.Collapsed;
            btnVacunacion.Visibility = Visibility.Collapsed;

            btnUsuRestablecerClave.Visibility = Visibility.Visible;
        }

        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {
            AgregarForm(new Pages.Inicio());
        }

        //Funcion Agregar el Form para cargar los formularios
        private void AgregarForm(UserControl control) 
        {
            this.spPrincipal.Children.Clear();
            this.spPrincipal.Children.Add(control);
        }



        private void btnClientes_Click(object sender, RoutedEventArgs e)
        {
            AgregarForm(new Pages.Clientes());
        }

        private void btnMascotas_Click(object sender, RoutedEventArgs e)
        {
            AgregarForm(new Pages.Mascotas());
        }

        //private void btnCategorias_Click(object sender, RoutedEventArgs e)
        //{
        //    AgregarForm(new Pages.Categorias());
        //}

        private void btnInventario_Click(object sender, RoutedEventArgs e)
        {
            AgregarForm(new Pages.Inventario());
        }

        public void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            IniciarSesion login = new IniciarSesion();
            login.Show();
            this.Close();
        }

        private void btnVacunacion_Click(object sender, RoutedEventArgs e)
        {
            AgregarForm(new Pages.Expedientes());
        }

        private void btnPersonal_Click(object sender, RoutedEventArgs e)
        {
            AgregarForm(new Pages.Personal());
        }

        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            AgregarForm(new Pages.Usuarios());
        }

        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {
            Reportes.Reportes menu = new Reportes.Reportes();
            menu.Show();
            this.Close();
        }

        private void btnUsuRestablecerClave_Click(object sender, RoutedEventArgs e)
        {
            AgregarForm(new Pages.Formularios.FormUsuRestablecerClave());
            
        }
    }
}
