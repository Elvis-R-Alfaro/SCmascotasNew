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
    /// Interaction logic for Inventario.xaml
    /// </summary>
    public partial class Inventario : UserControl
    {
        private InventarioC inventario = new InventarioC();
        private List<InventarioC> inventarios;
        public Inventario()
        {
            InitializeComponent();
            ObtenerInventario();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            // Mostrar el formulario de ingreso de productos
            Formularios.FormInventario inventario = new Formularios.FormInventario(false);
            inventario.Show();
        }

        private void ObtenerInventario()
        {
            inventarios = Constructores.Procedimientos.MostrarInventario();
            dgInventario.SelectedValuePath = "Id";
            dgInventario.ItemsSource = inventarios;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dgInventario.SelectedValue == null)
                MessageBox.Show("Por favor selecciona un producto");
            else
            {
                Formularios.FormInventario.ides = Convert.ToInt32(dgInventario.SelectedValue);
                Formularios.FormInventario inventario = new Formularios.FormInventario(true);
                inventario.Show();
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgInventario.SelectedValue == null)
                    MessageBox.Show("Por favor, seleccione un producto de la lista");
                else
                {
                    //Monstrar mensjae de confirmacion
                    MessageBoxResult result = MessageBox.Show("¿Deseas eliminar el producto?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        //Eliminar el inventario
                        Constructores.Procedimientos.EliminarProducto(Convert.ToInt32(dgInventario.SelectedValue));
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al eliminar la producto...");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //Actualizar el listbox de inventario
                ObtenerInventario();
            }
        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            ObtenerInventario();
        }
    }
}
