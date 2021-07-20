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
    public partial class ReporteBuscarCargo : Form
    {
        public ReporteBuscarCargo()
        {
            InitializeComponent();
        }

        public string Cargo { get; set; }

        private void ReporteBuscarCargo_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetPrincipal.BuscarPersonalxCargo' Puede moverla o quitarla según sea necesario.
            this.BuscarPersonalxCargoTableAdapter.Fill(this.DataSetPrincipal.BuscarPersonalxCargo,Cargo);

            this.reportViewer1.RefreshReport();
        }
    }
}
