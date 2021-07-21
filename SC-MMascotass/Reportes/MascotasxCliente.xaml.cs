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
    /// Lógica de interacción para MascotasxCliente.xaml
    /// </summary>
    public partial class MascotasxCliente : UserControl
    {

        private static string connectionString = ConfigurationManager.ConnectionStrings["SC_MMascotass.Properties.Settings.MascotasConnectionString"].ConnectionString;
        private static SqlConnection sqlConnection = new SqlConnection(connectionString);

        public MascotasxCliente()
        {
            InitializeComponent();
            ComboCargarClientes();
        }

        private void ComboCargarClientes()
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("MonstrarCliente", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Abrir conexion
                sqlConnection.Open();

                SqlDataReader dr = sqlCommand.ExecuteReader();
                while (dr.Read())
                {
                    cmbCliente.Items.Add(dr["NombreCliente"].ToString());
                    cmbCliente.SelectedValuePath = dr["IdCliente"].ToString();
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
            if (cmbCliente.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Seleccione un cliente");
                return;
            }
            ReporteMascotasxCliente Mostrar = new ReporteMascotasxCliente();
            Mostrar.Cliente = cmbCliente.SelectionBoxItem.ToString();

            Mostrar.ShowDialog();
        }
    }
}
