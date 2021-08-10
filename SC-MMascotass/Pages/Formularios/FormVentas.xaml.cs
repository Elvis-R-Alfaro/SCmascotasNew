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
    /// Interaction logic for FormVentas.xaml
    /// </summary>
    /// 
    public partial class FormVentas : Window
    {
        private InventarioC inventario = new InventarioC();
        private List<InventarioC> inventarios;

        List<Carrito> scarritos = new List<Carrito>();

        public FormVentas()
        {
            InitializeComponent();
            ObtenerInventario();
            DeshabilitarStockNum(false);
        }

        private int _numValue = 0;

        private int? idProdCarrito;

        #region BotonesDeAumentoStock
        public int NumValue
        {
            get { return _numValue; }
            set
            {
                _numValue = value;
                txtNum.Text = value.ToString();
            }
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            NumValue++;
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            if (NumValue > 0)
            {
                NumValue--;
            }
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNum == null)
            {
                return;
            }

            if (!int.TryParse(txtNum.Text, out _numValue))
                txtNum.Text = _numValue.ToString();
        }
        #endregion 

        private void DeshabilitarStockNum(bool activo)
        {
            cmdUp.IsEnabled = activo;
            cmdDown.IsEnabled = activo;
            if (!activo)
            {
                activo = true;
                txtNum.IsReadOnly = activo;
            }
            else
            {
                txtNum.IsReadOnly = activo;
            }
        }

        private void ObtenerInventario()
        {
            inventarios = Constructores.Procedimientos.MostrarInventario();
            dgCompras.SelectedValuePath = "Id";
            dgCompras.ItemsSource = inventarios;
        }

        public void carritos(int idprod, int cant, int idcat, string producto, double preciocosto, double precioventa, double cantPorPrecio, int nuevostock)
        {
            Carrito carrito2 = new Carrito();



            carrito2.Id = idprod;
            carrito2.Cantidad = cant;

            carrito2.Descripcion = producto;
            carrito2.PrecioCosto = preciocosto;
            carrito2.PrecioVenta = precioventa;
            carrito2.CantidadPorPrecio = cantPorPrecio;
            carrito2.Stock = nuevostock;

            scarritos.Add(carrito2);


        }

        private void dgCompras_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

            DeshabilitarStockNum(true);
            dgVentas.UnselectAllCells();
            btnEditar.Visibility = Visibility.Hidden;
            btnAgregar.Visibility = Visibility.Visible;
            btnRemover.Visibility = Visibility.Hidden;
            try
            {


                inventario.Id = Convert.ToInt32(dgCompras.SelectedValue);
                        

                idProdCarrito = Convert.ToInt32(dgCompras.SelectedValue);
                btnAgregar.IsEnabled = true;
                object row = dgCompras.SelectedItem;
                int columnIndex = dgCompras.Columns.Single(c => c.Header.Equals("Producto")).DisplayIndex;
                String cardName = (dgCompras.SelectedCells[columnIndex].Column.GetCellContent(row) as TextBlock).Text;

                txtDescripcion.Text = cardName;
            }
            catch
            {

            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            bool yaIncluido = scarritos.Any(inventario => inventario.Id == idProdCarrito);

            if (idProdCarrito == null)
            {
                MessageBox.Show("Por favor seleccione un producto","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }else if (yaIncluido)
            {
                MessageBox.Show("El producto ya se encuentra en el carrito", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var item = inventarios.Find(inventario => inventario.Id == idProdCarrito);


                if (Convert.ToInt32(txtNum.Text)<=0)
                {
                    MessageBox.Show("Ingrese la cantidad a llevar del producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int nuevoStrock = item.Stock - Convert.ToInt32(txtNum.Text);
                double cantidadPorPrecio = item.PrecioVenta * Convert.ToInt32(txtNum.Text);

                carritos(item.Id, Convert.ToInt32(txtNum.Text), item.IdCategoria, item.Descripcion, item.PrecioCosto, item.PrecioVenta, cantidadPorPrecio,nuevoStrock);

                txtDescripcion.Clear();
                txtNum.Text = "0";
                idProdCarrito = null;

                dgVentas.SelectedValuePath = "Id";
                dgVentas.ItemsSource = scarritos;
                dgVentas.Items.Refresh();
                CalcularTotal();
            }
        }

        private void CalcularTotal()
        {
            double subtotal = scarritos.Sum(carritos => carritos.CantidadPorPrecio);
            double impuesto = subtotal * 0.15;
            double neto = subtotal + impuesto;
            lblNeto.Content = neto.ToString("N");
            lblImpuesto.Content = impuesto.ToString("N");
            lblSubtotal.Content = subtotal.ToString("N");
        }

        private void btnRemover_Click(object sender, RoutedEventArgs e)
        {
            removeElementCarrito();
            txtDescripcion.Clear();
            txtNum.Text = "0";
            idProdCarrito = null;

            dgVentas.SelectedValuePath = "Id";
            dgVentas.ItemsSource = scarritos;
            dgVentas.Items.Refresh();
            btnAgregar.Visibility = Visibility.Hidden;
            btnEditar.Visibility = Visibility.Hidden;
            btnRemover.Visibility = Visibility.Hidden;
            CalcularTotal();
            dgVentas.UnselectAllCells();
        }

        private void dgVentas_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DeshabilitarStockNum(true);
            btnAgregar.Visibility = Visibility.Hidden;
            btnEditar.Visibility = Visibility.Visible;
            btnRemover.Visibility = Visibility.Visible;
            dgCompras.UnselectAllCells();
            try
            {
                inventario.Id = Convert.ToInt32(dgVentas.SelectedValue);


                idProdCarrito = Convert.ToInt32(dgVentas.SelectedValue);
                btnAgregar.IsEnabled = true;
                object row = dgVentas.SelectedItem;
                int columnIndex = dgVentas.Columns.Single(c => c.Header.Equals("Producto")).DisplayIndex;
                String cardName = (dgVentas.SelectedCells[columnIndex].Column.GetCellContent(row) as TextBlock).Text;

                int columnIndex2 = dgVentas.Columns.Single(c => c.Header.Equals("Cant")).DisplayIndex;
                String cardName2 = (dgVentas.SelectedCells[columnIndex2].Column.GetCellContent(row) as TextBlock).Text;

                txtDescripcion.Text = cardName;

                txtNum.Text = cardName2;
            }
            catch
            {

            }
        }

        private void removeElementCarrito()
        {
            var itemS = inventarios.Find(inventario => inventario.Id == idProdCarrito);
            var itemCarrito = scarritos.Find(Carrito => Carrito.Id == idProdCarrito);
            scarritos.Remove(itemCarrito);
            DeshabilitarStockNum(false);
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            removeElementCarrito();
            bool yaIncluido = scarritos.Any(inventario => inventario.Id == idProdCarrito);

            if (idProdCarrito == null)
            {
                MessageBox.Show("Por favor seleccione un producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (yaIncluido)
            {
                MessageBox.Show("El producto ya se encuentra en el carrito", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var item = inventarios.Find(inventario => inventario.Id == idProdCarrito);


                if (Convert.ToInt32(txtNum.Text) <= 0)
                {
                    MessageBox.Show("Ingrese la cantidad a llevar del producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int nuevoStrock = item.Stock - Convert.ToInt32(txtNum.Text);
                double cantidadPorPrecio = item.PrecioVenta * Convert.ToInt32(txtNum.Text);

                carritos(item.Id, Convert.ToInt32(txtNum.Text), item.IdCategoria, item.Descripcion, item.PrecioCosto, item.PrecioVenta, cantidadPorPrecio, nuevoStrock);

                txtDescripcion.Clear();
                txtNum.Text = "0";
                idProdCarrito = null;

                dgVentas.SelectedValuePath = "Id";
                dgVentas.ItemsSource = scarritos;
                dgVentas.Items.Refresh();
                btnAgregar.Visibility = Visibility.Hidden;
                btnEditar.Visibility = Visibility.Hidden;
                btnRemover.Visibility = Visibility.Hidden;
                CalcularTotal();
                DeshabilitarStockNum(false);
                dgVentas.UnselectAllCells();
            }
        }
        private void ObtenerInventarioLike()
        {
            inventarios = Constructores.Procedimientos.BuscarProductoLike(txtCompras.Text);
            dgCompras.SelectedValuePath = "Id";
            dgCompras.ItemsSource = inventarios;
        }

        private void txtCompras_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObtenerInventarioLike();
        }
    }
}
