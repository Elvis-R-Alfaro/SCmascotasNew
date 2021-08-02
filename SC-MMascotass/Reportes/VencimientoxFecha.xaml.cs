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

namespace SC_MMascotass.Reportes
{
    /// <summary>
    /// Lógica de interacción para VencimientoxFecha.xaml
    /// </summary>
    public partial class VencimientoxFecha : UserControl
    {
        public VencimientoxFecha()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMes.Text))
            {
                MessageBox.Show("Ingrese el mes");
                return;
            }
            else if (string.IsNullOrWhiteSpace(txtAnio.Text))
            {
                MessageBox.Show("Ingrese el año");
                return;
            }

            else if (Convert.ToInt32(txtMes.Text)>12 || Convert.ToInt32(txtMes.Text) <= 0)
            {
                MessageBox.Show("La fecha no es valida");
                return;
            }
            ReporteVencimientoxFecha Mostrar = new ReporteVencimientoxFecha();
            Mostrar.Mes = Convert.ToInt32(txtMes.Text);
            Mostrar.Anio = Convert.ToInt32(txtAnio.Text);

            Mostrar.ShowDialog();

        }
    }
}
