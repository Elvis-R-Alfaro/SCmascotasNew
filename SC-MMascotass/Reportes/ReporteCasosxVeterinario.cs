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
    public partial class ReporteCasosxVeterinario : Form
    {
        public ReporteCasosxVeterinario()
        {
            InitializeComponent();
        }

        public int Mes { get; set; }
        public int Anio { get; set; }

        private void ReporteCasosxVeterinario_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetPrincipal.CasosxVeterinario' Puede moverla o quitarla según sea necesario.
            this.CasosxVeterinarioTableAdapter.Fill(this.DataSetPrincipal.CasosxVeterinario,Anio, Mes);

            this.reportViewer1.RefreshReport();
        }
    }
}
