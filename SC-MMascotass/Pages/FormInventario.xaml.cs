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

namespace SC_MMascotass.Pages
{
    public partial class FormInventario : Window
    {
        private InventarioC inventario = new InventarioC();
        private Categoria categoria = new Categoria();

        //Variable de id
        public static int ides;
        public FormInventario(bool visible)
        {
            InitializeComponent();

            //Monstrar botones visibles/invisibles
            MonstrarBotones(visible);

            //Esconde el autocompleta al iniciar el formulario
            var border = (autoCompleteCategorias.Parent as ScrollViewer).Parent as Border;
            border.Visibility = System.Windows.Visibility.Collapsed;

            //Validacion de cargar datos
            if (visible)
            {
                inventario = inventario.BuscarProducto(ides);
                categoria = categoria.BuscarCategoria(inventario.IdCategoria);

                txtAuCategoria.Text = categoria.NombreCategoria;
                txtDescripcion.Text = inventario.Descripcion;
                txtPrecioCosto.Text = inventario.PrecioCosto.ToString();
                txtPrecioVenta.Text = inventario.PrecioVenta.ToString();
                txtStock.Text = inventario.Stock.ToString();
                
            }

        }

        private bool VerificarValores()
        {
            if (string.IsNullOrWhiteSpace(txtAuCategoria.Text))
            {
                MessageBox.Show("¡Ingrese el Nombre de la Categoría!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("¡Ingrese la Descripción del producto!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtStock.Text))
            {
                MessageBox.Show("¡Ingrese Stock para el producto!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPrecioCosto.Text))
            {
                MessageBox.Show("¡Ingrese el costo!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPrecioVenta.Text))
            {
                MessageBox.Show("¡Ingrese el precio de venta!");
                return false;
            }
            return true;
        }

        //Obtener los datos del formulario
        private void ObtenerValoresFormulario()
        {
            inventario.Descripcion = txtDescripcion.Text;
            inventario.Stock = Convert.ToInt32(txtStock.Text);
            inventario.PrecioCosto = Convert.ToDouble(txtPrecioCosto.Text);
            inventario.PrecioVenta = Convert.ToDouble(txtPrecioVenta.Text);
        }

        //Funcion de ocultar botones
        private void MonstrarBotones(bool visibles)
        {
            if (visibles)
            {
                spEditar.Visibility = Visibility.Visible;
                spGuardar.Visibility = Visibility.Hidden;
            }
            else
            {
                spEditar.Visibility = Visibility.Hidden;
                spGuardar.Visibility = Visibility.Visible;
            }
        }

        //Metodo de Autocmpletar
        private void txtAuCategoria_KeyUp(object sender, KeyEventArgs e)
        {
            bool found = false;
            var border = (autoCompleteCategorias.Parent as ScrollViewer).Parent as Border;
            var data = Categoria.MonstrarMascotas22();

            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear   
                autoCompleteCategorias.Children.Clear();
                border.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                border.Visibility = System.Windows.Visibility.Visible;
            }

            // Clear the list   
            autoCompleteCategorias.Children.Clear();

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
                autoCompleteCategorias.Children.Add(new TextBlock() { Text = "No se ha encontrado" });
            }
        }

        private void addItem(string text)
        {
            TextBlock block = new TextBlock();

            // Add the text   
            block.Text = text;

            // A little style...   
            block.Margin = new Thickness(2, 3, 2, 3);
            block.Cursor = Cursors.Hand;

            // Mouse events   
            block.MouseLeftButtonUp += (sender, e) =>
            {
                var border = (autoCompleteCategorias.Parent as ScrollViewer).Parent as Border;
                border.Visibility = System.Windows.Visibility.Collapsed;
                txtAuCategoria.Text = (sender as TextBlock).Text;
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
            autoCompleteCategorias.Children.Add(block);
        }

        private void btnNuevoCliente_Click(object sender, RoutedEventArgs e)
        {
            // Mostrar el formulario de ingreso de categorias
            FormCategorias categorias = new FormCategorias(false);
            categorias.Show();
        }

        //Funcion limpiar
        private void Limpiar()
        {
            txtAuCategoria.Text = string.Empty;
            txtCodigo.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtPrecioCosto.Text = string.Empty;
            txtPrecioVenta.Text = string.Empty;
            txtStock.Text = string.Empty;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValores())
            {
                try
                {
                    //Obtener los valores para el inventario
                    categoria = categoria.BuscarCategoriasId(txtAuCategoria.Text);

                    inventario.IdCategoria = categoria.Id;
                    inventario.Descripcion = txtDescripcion.Text;
                    inventario.Stock = Convert.ToInt32(txtStock.Text);
                    inventario.PrecioCosto = Convert.ToDouble(txtPrecioCosto.Text);
                    inventario.PrecioVenta = Convert.ToDouble(txtPrecioVenta.Text);

                    //Insertar los datos del inventario
                    inventario.CrearProducto(inventario);

                    //Mensaje de inserccion exito
                    MessageBox.Show("Datos Insertados Correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar los datos en el inventario....");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Limpiar();
                    
                    //Faltan cosas
                }
            }
        }

        private void btnRestablecer_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValores())
            {
                try
                {
                    //Obtener los valores para el inventario desde el formulario
                    ObtenerValoresFormulario();

                    //Actualizar los valores en la base de datos
                    inventario.EditarProducto(inventario);

                    //Mensaje de actualizacion realizada
                    MessageBox.Show("Datos Modificado Correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);

                    //Limpiar formulario
                    Limpiar();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al momento de actualizar el producto....");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnRestablecer_Click_1(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
    }
}
