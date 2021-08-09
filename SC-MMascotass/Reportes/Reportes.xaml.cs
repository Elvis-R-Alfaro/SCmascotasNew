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

namespace SC_MMascotass.Reportes
{
    /// <summary>
    /// Lógica de interacción para Reportes.xaml
    /// </summary>
    public partial class Reportes : Window
    {
        public Reportes()
        {
            InitializeComponent();
        }

        //Funcion Agregar el Form para cargar los formularios
        private void AgregarForm(UserControl control)
        {
            this.spPrincipal.Children.Clear();
            this.spPrincipal.Children.Add(control);
        }

        private void btnR1_Click(object sender, RoutedEventArgs e)
        {
            AgregarForm(new BuscarPersonalxCargo());
        }

        private void btnR2_Click(object sender, RoutedEventArgs e)
        {
            AgregarForm(new VacunaxMascota());
        }

        private void btnR3_Click(object sender, RoutedEventArgs e)
        {
            AgregarForm(new MascotasxCliente());
        }

        private void btnR4_Click(object sender, RoutedEventArgs e)
        {
            AgregarForm(new VencimientoxFecha());
        }

        private void btnR5_Click(object sender, RoutedEventArgs e)
        {
            AgregarForm(new CasosxVeterinario());
        }


        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MenuPrincipal menu = new MenuPrincipal();
            menu.Show();
            this.Close();
        }

        
    }
}
