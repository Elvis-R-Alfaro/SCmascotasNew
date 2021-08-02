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
    public partial class ReporteVencimientoxFecha : Form
    {
        public ReporteVencimientoxFecha()
        {
            InitializeComponent();
        }

        public int Mes { get; set; }
        public int Anio { get; set; }
        private void ReporteVencimientoxFecha_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetPrincipal.VencimientoxFecha' Puede moverla o quitarla según sea necesario.
            this.VencimientoxFechaTableAdapter.Fill(this.DataSetPrincipal.VencimientoxFecha,Anio, Mes);

            this.reportViewer1.RefreshReport();
        }
    }
}
