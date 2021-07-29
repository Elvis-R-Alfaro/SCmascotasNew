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
        }

        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {
            MenuPrincipal menu = new MenuPrincipal();
            menu.Show();
            this.Close();
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

        private void btnCategorias_Click(object sender, RoutedEventArgs e)
        {
            AgregarForm(new Pages.Categorias());
        }

        private void btnInventario_Click(object sender, RoutedEventArgs e)
        {
            AgregarForm(new Pages.Inventario());
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            IniciarSesion login = new IniciarSesion();
            login.Show();
            this.Close();
        }

        private void btnVacunacion_Click(object sender, RoutedEventArgs e)
        {
            AgregarForm(new Pages.Vacunas());
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

        private void btnayuda_Click(object sender, RoutedEventArgs e)
        {
            var file = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            string targetURL = @"file:///" + file.ToString() + @"Recursos\manual.html";

            try
            {
                System.Diagnostics.Process.Start(targetURL);
            }
            catch (Exception)
            {
               // MessageBox.Show("Archivo no encontrado en la Ruta: "+targetURL);
               MessageBox.Show("Archivo no encontrado");
            }
        }
    }
}
