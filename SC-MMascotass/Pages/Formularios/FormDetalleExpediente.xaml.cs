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
using System.Windows.Shapes;

namespace SC_MMascotass.Pages.Formularios
{
    /// <summary>
    /// Interaction logic for FormDetalleExpediente.xaml
    /// </summary>
    public partial class FormDetalleExpediente : Window
    {
        public static int IdDetalle;
        private InventarioC producto = new InventarioC();
        public static string mascota;

        private Constructores.Expediente expediente = new Constructores.Expediente();

        public FormDetalleExpediente(bool visible)
        {
            InitializeComponent();
            expediente = Constructores.Procedimientos.BuscarDetalleExpedienteId(IdDetalle);
            txtMascota.Text = mascota;

            MonstrarBotones(visible);

            //Oculta el auto completar al iniiar el fomrulario
            var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            border.Visibility = System.Windows.Visibility.Collapsed;

            if (visible)
            {
                txtProducto.Text = expediente.Producto;
                txtdescripcion.Text = expediente.Descripcion;
                txtPatologia.Text = expediente.Patologia;
                txtTratamiento.Text = expediente.TratamientoRecomendado;                

                btnAceptar.Visibility = Visibility.Visible;
                btnRegresaar.Visibility = Visibility.Visible;
                btnGuardar.Visibility = Visibility.Hidden;
                btnCancelar.Visibility = Visibility.Hidden;
            }
        }

        private void ObtenerValores()
        {

        }

        private void Limpiar()
        {
            btnAceptar.Visibility = Visibility.Hidden;
            btnRegresaar.Visibility = Visibility.Hidden;
            btnGuardar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;

            txtPatologia.Clear();
            txtProducto.Clear();
            txtdescripcion.Clear();
            txtTratamiento.Clear();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValores())
            {
                //Obtener los valores para la mascota
                ObtenerValoresFormulario();

                Constructores.Procedimientos.CrearDetalle(expediente);
                if (Constructores.Procedimientos.error == 0)                
                    this.Close();                                
                
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValores())
            {
                //Obtener los valores para la mascota
                ObtenerValoresFormulario();

                //Insertar los datos de la mascota
                Constructores.Procedimientos.EditarDetalle(expediente);
                if (Constructores.Procedimientos.error == 0)
                {
                    Limpiar();
                    this.Close();
                }

            }
        }

        private void ObtenerValoresFormulario()
        {
            throw new NotImplementedException();
        }

        private bool VerificarValores()
        {
            if (string.IsNullOrWhiteSpace(txtdescripcion.Text))
            {
                MessageBox.Show("¡Ingrese el Nombre del Cliente!");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtMascota.Text))
            {
                MessageBox.Show("¡Ingrese el Nombre de la Mascota!");
                return false;
            }

            return true;
        }

        private void btnRegresaar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MonstrarBotones(bool visibles)
        {
            if (visibles)
            {
                spNuevaCategoria.Visibility = Visibility.Hidden;
                btnAceptar.Visibility = Visibility.Hidden;
                btnRegresaar.Visibility = Visibility.Hidden;
                btnGuardar.Visibility = Visibility.Visible;
                btnCancelar.Visibility = Visibility.Visible;
            }
            else
            {
                spNuevaCategoria.Visibility = Visibility.Visible;
                btnAceptar.Visibility = Visibility.Visible;
                btnRegresaar.Visibility = Visibility.Visible;
                btnGuardar.Visibility = Visibility.Hidden;
                btnCancelar.Visibility = Visibility.Hidden;
            }
        }

        private void txtProducto_KeyUp(object sender, KeyEventArgs e)
        {
            bool found = false;
            var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            var data = Constructores.Procedimientos.MonstrarMascotas22();


            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear   
                resultStack.Children.Clear();
                border.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                border.Visibility = System.Windows.Visibility.Visible;
            }

            // Clear the list   
            resultStack.Children.Clear();

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
                resultStack.Children.Add(new TextBlock() { Text = "No se ha encontrado" });
            }
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
                var border = (resultStack.Parent as ScrollViewer).Parent as Border;
                txtProducto.Text = (sender as TextBlock).Text;
                producto = Constructores.Procedimientos.BuscarProductoID(txtProducto.Text);
                border.Visibility = System.Windows.Visibility.Collapsed;
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
            resultStack.Children.Add(block);
        }
    }
}
