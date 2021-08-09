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
    /// Interaction logic for FormCompras.xaml
    /// </summary>
    public partial class FormCompras : Window
    {
        private InventarioC inventario = new InventarioC();
        private List<InventarioC> inventarios;
        public FormCompras()
        {
            InitializeComponent();
            ObtenerInventario();
            DeshabilitarStockNum(false);
            txtNum.Text = _numValue.ToString();
        }

        private int _numValue = 0;


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
            if (NumValue >0) {
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

        private void ObtenerInventario()
        {
            inventarios = Constructores.Procedimientos.MostrarInventario();
            dgCompras.SelectedValuePath = "Id";
            dgCompras.ItemsSource = inventarios;
        }

        private void dgCompras_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                inventario.Id = Convert.ToInt32(dgCompras.SelectedValue);
                object row = dgCompras.SelectedItem;
                int columnIndex = dgCompras.Columns.Single(c => c.Header.Equals("Producto")).DisplayIndex;
                String cardName = (dgCompras.SelectedCells[columnIndex].Column.GetCellContent(row) as TextBlock).Text;

                object row2 = dgCompras.SelectedItem;
                int columnIndex2 = dgCompras.Columns.Single(c => c.Header.Equals("Stock")).DisplayIndex;
                String cardName2 = (dgCompras.SelectedCells[columnIndex2].Column.GetCellContent(row2) as TextBlock).Text;
                txtDescripcion.Text = cardName;
                txtNum.Text = cardName2;
                DeshabilitarStockNum(true);
            }
            catch
            {

            }
        }

        private void ObtenerInventarioLike()
        {
            inventarios = Constructores.Procedimientos.BuscarProductoLike(txtCompras.Text);
            dgCompras.SelectedValuePath = "Id";
            dgCompras.ItemsSource = inventarios;
        }


        //Obtener los datos del formulario
        private void ObtenerValoresFormulario()
        {
            inventario.Descripcion = txtDescripcion.Text;
            inventario.Stock = Convert.ToInt32(txtNum.Text);
        }

        private void LimpiarForm()
        {
            txtCompras.Clear();
            txtDescripcion.Clear();
            txtNum.Text = "0";
        }

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

        private void txtCompras_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObtenerInventarioLike();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarForm();
            ObtenerInventario();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            ObtenerValoresFormulario();
            Constructores.Procedimientos.EditarStock(inventario);
            if (txtCompras.Text == "")
                ObtenerInventario();
            else
                ObtenerInventarioLike();
            txtDescripcion.Clear();
            txtNum.Text = "0";
        }
    }
}
