using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC_MMascotass.Reportes
{
    public partial class BucarPersona : Form
    {
        public BucarPersona()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ReporteBuscarCargo Mostrar = new ReporteBuscarCargo();
            Mostrar.Cargo = cmbcargo.SelectedItem.ToString();
            MessageBox.Show(cmbcargo.SelectedItem.ToString());

            Mostrar.ShowDialog();
        }
    }
}
