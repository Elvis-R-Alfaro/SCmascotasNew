using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public partial class FormInventario : Window
    {
        private InventarioC inventario = new InventarioC();
        private Categoria categoria = new Categoria();
        private SqlConnection sqlConnection = database.Conexion.ObtenerConexion();

        //Variable de id
        public static int ides;
        public FormInventario(bool visible)
        {
            InitializeComponent();
            CargarCombobox();

            //Monstrar botones visibles/invisibles
            MonstrarBotones(visible);

            //Esconde el autocompleta al iniciar el formulario
            var border = (autoCompleteCategorias.Parent as ScrollViewer).Parent as Border;
            border.Visibility = System.Windows.Visibility.Collapsed;

            //Validacion de cargar datos
            if (visible)
            { 
                inventario = Constructores.Procedimientos.BuscarProducto(ides);
                categoria = Constructores.Procedimientos.BuscarCategoria(inventario.IdCategoria);

                txtAuCategoria.Text = categoria.NombreCategoria;
                txtproducto.Text = inventario.Descripcion;
                txtPrecioCosto.Text = inventario.PrecioCosto.ToString();
                txtPrecioVenta.Text = inventario.PrecioVenta.ToString();
                txtStock.Text = inventario.Stock.ToString();
                cmbproveedor.Text = inventario.NombreProveedor;
                cmbproveedor.SelectedValuePath = inventario.IdProveedor.ToString();
            }

        }

        private void CargarCombobox()
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("Inventario", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Enviar Parametros
                sqlCommand.Parameters.AddWithValue("@Accion", "CargarProveedorCombo");

                //Abrir conexion
                sqlConnection.Open();

                SqlDataReader dr = sqlCommand.ExecuteReader();
                while (dr.Read())
                {
                    cmbproveedor.Items.Add(dr["NombreProveedor"].ToString());
                    cmbproveedor.SelectedValuePath = dr["IdProveedor"].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar los Proveedores");
            }
            finally
            {
                //Cerrar la conexion
                sqlConnection.Close();
            }
        }

        private bool VerificarValores()
        {
            if (string.IsNullOrWhiteSpace(txtAuCategoria.Text))
            {
                MessageBox.Show("¡Ingrese el Nombre de la Categoría!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtproducto.Text))
            {
                MessageBox.Show("¡Ingrese el Producto!");
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
            categoria = Constructores.Procedimientos.BuscarCategoriasId(txtAuCategoria.Text);

            inventario.IdCategoria = categoria.Id;
            inventario.Descripcion = txtproducto.Text;
            inventario.Stock = Convert.ToInt32(txtStock.Text);
            inventario.PrecioCosto = Convert.ToDouble(txtPrecioCosto.Text);
            inventario.PrecioVenta = Convert.ToDouble(txtPrecioVenta.Text);
            inventario.IdProveedor = Convert.ToInt32(cmbproveedor.SelectedValuePath);
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
            var data = Constructores.Procedimientos.MonstrarCategorias22();

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
            txtproducto.Text = string.Empty;
            txtPrecioCosto.Text = string.Empty;
            txtPrecioVenta.Text = string.Empty;
            txtStock.Text = "";
            cmbproveedor.Text = "";
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValores())
            {                
                //Obtener los valores para el inventario
                categoria = Constructores.Procedimientos.BuscarCategoriasId(txtAuCategoria.Text);

                inventario.IdCategoria = categoria.Id;
                inventario.Descripcion = txtproducto.Text;
                inventario.IdProveedor = Convert.ToInt32(cmbproveedor.SelectedValuePath);
                inventario.Stock = Convert.ToInt32(txtStock.Text);
                inventario.PrecioCosto = Convert.ToDouble(txtPrecioCosto.Text);
                inventario.PrecioVenta = Convert.ToDouble(txtPrecioVenta.Text);

                //Insertar los datos del inventario
                Constructores.Procedimientos.CrearProducto(inventario);                
                
                Limpiar();
                this.Close();
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
                
                //Obtener los valores para el inventario desde el formulario
                ObtenerValoresFormulario();

                //Actualizar los valores en la base de datos
                Constructores.Procedimientos.EditarProducto(inventario);                

                //Limpiar formulario
                Limpiar();
                this.Close();
                
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

        private void btnnuevoProveedor_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
