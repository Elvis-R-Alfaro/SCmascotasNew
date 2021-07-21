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
    public partial class ReporteMascotasxCliente : Form
    {
        public ReporteMascotasxCliente()
        {
            InitializeComponent();
        }

        public string Cliente { get; set; }

        private void ReporteMascotasxCliente_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetPrincipal.MascotasxCliente' Puede moverla o quitarla según sea necesario.
            this.MascotasxClienteTableAdapter.Fill(this.DataSetPrincipal.MascotasxCliente, Cliente);

            this.reportViewer1.RefreshReport();
        }
    }
}
