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
//Agregar los namespaces
using System.Data.SqlClient;
using System.Configuration;

namespace SC_MMascotass.Reportes
{
    /// <summary>
    /// Lógica de interacción para VacunaxMascota.xaml
    /// </summary>
    public partial class VacunaxMascota : UserControl
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["SC_MMascotass.Properties.Settings.MascotasConnectionString"].ConnectionString;
        private static SqlConnection sqlConnection = new SqlConnection(connectionString);
        public VacunaxMascota()
        {
            InitializeComponent(); 
            ComboCargarClientes();
        }

        private void ComboCargarClientes()
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("MostrarMascotas", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Abrir conexion
                sqlConnection.Open();

                SqlDataReader dr = sqlCommand.ExecuteReader();
                while (dr.Read())
                {
                    cmbMascota.Items.Add(dr["NombreMascota"].ToString());
                    cmbMascota.SelectedValuePath = dr["IdMascota"].ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //Cerrar la conexion
                sqlConnection.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cmbMascota.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Seleccione una mascota");
                return;
            }
            else if(string.IsNullOrWhiteSpace(txtAnio.Text))
            {
                MessageBox.Show("Ingrese el año");
                return;
            }
            else if (Convert.ToInt32(txtAnio.Text) > 2021)
            {
                MessageBox.Show("La fecha no es valida");
                return;
            }
            InformeVacunaxMascota Mostrar = new InformeVacunaxMascota();
            Mostrar.NombreMascota = cmbMascota.SelectionBoxItem.ToString();
            Mostrar.Anio =Convert.ToInt32(txtAnio.Text);

            Mostrar.ShowDialog();
        }
    }
}
