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
    public partial class InformeVacunaxMascota : Form
    {
        public InformeVacunaxMascota()
        {
            InitializeComponent();
        }

        public string NombreMascota { get; set; }

        public int Anio { get; set; }

        private void InformeVacunaxMascota_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetPrincipal.VacunaxMascota' Puede moverla o quitarla según sea necesario.
            this.VacunaxMascotaTableAdapter.Fill(this.DataSetPrincipal.VacunaxMascota, NombreMascota, Anio);

            this.reportViewer1.RefreshReport();
        }
    }
}
