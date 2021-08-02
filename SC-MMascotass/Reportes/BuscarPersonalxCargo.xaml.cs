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
    /// Lógica de interacción para BuscarPersonalxCargo.xaml
    /// </summary>
    public partial class BuscarPersonalxCargo : UserControl
    {
        public BuscarPersonalxCargo()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cmbcargos.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Seleccione un cargo");
                return;
            }
            ReporteBuscarCargo Mostrar = new ReporteBuscarCargo();
            Mostrar.Cargo = cmbcargos.SelectionBoxItem.ToString();

            Mostrar.ShowDialog();
        }
    }
}
