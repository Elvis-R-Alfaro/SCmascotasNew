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

namespace SC_MMascotass.Pages
{
    /// <summary>
    /// Interaction logic for Expedientes.xaml
    /// </summary>
    public partial class Expedientes : UserControl
    {

        private Constructores.Expediente expediente = new Constructores.Expediente();
        private List<Constructores.Expediente> expedientes;
        public Expedientes()
        {
            InitializeComponent();
            ObtenerExpedientes();
        }

        private void ObtenerExpedientes()
        {
            expedientes = Constructores.Procedimientos.MostrarExpedientes();
            dgExpediente.SelectedValuePath = "IdExpediente";
            dgExpediente.ItemsSource = expedientes;
        }

        private void btnNuevoExpediente_Click(object sender, RoutedEventArgs e)
        {
            Formularios.FormExpediente expediente = new Formularios.FormExpediente();
            expediente.Show();
        }

        private void btnVerExpediente_Click(object sender, RoutedEventArgs e)
        {
            if (dgExpediente.SelectedValue == null)
                MessageBox.Show("Por favor selecciona un cliente de la lista");
            else
            {
               
                verExpediente.id = Convert.ToInt32(dgExpediente.SelectedValue);
                object row = dgExpediente.SelectedItem;
                int columnIndex = dgExpediente.Columns.Single(c => c.Header.Equals("Mascota")).DisplayIndex;
                String cardName = (dgExpediente.SelectedCells[columnIndex].Column.GetCellContent(row) as TextBlock).Text;

                object row2 = dgExpediente.SelectedItem;
                int columnIndex2 = dgExpediente.Columns.Single(c => c.Header.Equals("Cliente")).DisplayIndex;
                String cardName2 = (dgExpediente.SelectedCells[columnIndex2].Column.GetCellContent(row2) as TextBlock).Text;
                verExpediente veExpediente = new verExpediente();
                veExpediente.txtNombreMascota.Text = cardName;
                veExpediente.txtCliente.Text = cardName2;
                veExpediente.Show();
            }
            
        }

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }


        private void addItem(string text)
        {
            TextBlock block = new TextBlock();
            Label id = new Label();


            // Add the text   
            block.Text = text;

            // A little style...   
            block.Margin = new Thickness(2, 3, 2, 3);
            block.Cursor = Cursors.Hand;

            // Mouse events   
            block.MouseLeftButtonUp += (sender, e) =>
            {
                var border = (autoCompleteMascotas.Parent as ScrollViewer).Parent as Border;
                txtBuscarExpMascota.Text = (sender as TextBlock).Text;
                //cliente = cliente.BuscarClientID(txtAuCliente.Text);
                border.Visibility = System.Windows.Visibility.Collapsed;
                //ObtenerValoresFormulario();
            };

            block.MouseEnter += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.PeachPuff;
            };

            block.MouseLeave += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.Transparent;
            };

            // Add to the panel   
            autoCompleteMascotas.Children.Add(block);
        }

        private void txtBuscarExpMascota_KeyUp(object sender, KeyEventArgs e)
        {
            bool found = false;
            var border = (autoCompleteMascotas.Parent as ScrollViewer).Parent as Border;
            var data = Constructores.Procedimientos.MonstrarMascotas23();


            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear   
                autoCompleteMascotas.Children.Clear();
                border.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                border.Visibility = System.Windows.Visibility.Visible;
            }

            // Clear the list   
            autoCompleteMascotas.Children.Clear();

            // Add the result   
            foreach (var obj in data)
            {
                if (obj.ToLower().StartsWith(query.ToLower()))
                {
                    // The word starts with this... Autocomplete must work   
                    addItem(obj);
                    found = true;
                }
            }

            if (!found)
            {
                autoCompleteMascotas.Children.Add(new TextBlock() { Text = "No se ha encontrado" });
            }
        }

    }
}
