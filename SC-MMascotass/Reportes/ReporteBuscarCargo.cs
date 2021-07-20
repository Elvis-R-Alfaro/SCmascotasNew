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

        private void ReporteBuscarCargo_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            //this.reportViewer2.RefreshReport();
        }
    }
}
