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
    /// Lógica de interacción para Inicio.xaml
    /// </summary>
    public partial class Inicio : UserControl
    {
        public Inicio()
        {
            InitializeComponent();
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
                MessageBox.Show("Archivo no encontrado en la Ruta: " + targetURL);
                MessageBox.Show("Archivo no encontrado");
            }
        }
    }
}
