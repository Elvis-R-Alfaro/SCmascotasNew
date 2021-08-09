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
    /// Lógica de interacción para FormProveedores.xaml
    /// </summary>
    public partial class FormProveedores : Window
    {
        private InventarioC proveedor = new InventarioC();
        private List<InventarioC> proveedores;
        public FormProveedores()
        {
            InitializeComponent();
            ObtenerProveedor();
        }

        private void ObtenerProveedor()
        {
            proveedores = Constructores.Procedimientos.MostrarProveedor();
            dgClientes.SelectedValuePath = "IdProveedor";
            dgClientes.ItemsSource = proveedores;
        }

        private bool VerificarValores()
        {
            //if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            //{
            //    MessageBox.Show("¡Ingrese la Descripción de la especie!");
            //    return false;
            //}
            //else if (string.IsNullOrWhiteSpace(txtFamilia.Text))
            //{
            //    MessageBox.Show("¡Ingrese la Familia de la especie!");
            //    return false;
            //}
            return true;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValores())
            {
                //Obtener los valores para la mascota
                proveedor.NombreProveedor = txtnombreProveedor.Text;
                proveedor.Compania = txtcompañia.Text;
                proveedor.Telefono = txttelefono.Text;
                proveedor.Correo = txtcorreo.Text;
                proveedor.DirreccionEmpresa = txtdireccion.Text;

                //Ejecutamos
                Constructores.Procedimientos.CrearProveedor(proveedor);

                ObtenerProveedor();
                Limpiar();
            }
        }

        private void Limpiar()
        {
            txtcompañia.Clear();
            txtcorreo.Clear();
            txtdireccion.Clear();
            txtnombreProveedor.Clear();
            txttelefono.Clear();

            spButton1.Visibility = Visibility.Visible;
            spButton2.Visibility = Visibility.Hidden;
            ObtenerProveedor();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientes.SelectedValue == null)
                MessageBox.Show("Por favor selecciona un proveedor de la lista");
            else
            {
                spButton1.Visibility = Visibility.Hidden;
                spButton2.Visibility = Visibility.Visible;
                proveedor = Constructores.Procedimientos.CargarDatosEditarProveedor(Convert.ToInt32(dgClientes.SelectedValue));
                txtnombreProveedor.Text = proveedor.NombreProveedor;
                txtcompañia.Text = proveedor.Compania;
                txttelefono.Text = proveedor.Telefono;
                txtcorreo.Text = proveedor.Correo;
                txtdireccion.Text = proveedor.DirreccionEmpresa;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientes.SelectedValue == null)
                MessageBox.Show("Por favor, seleccione un proveedor de la lista");
            else
            {
                //Monstrar mensjae de confirmacion
                MessageBoxResult result = MessageBox.Show("¿Deseas eliminar el proveedor?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    //Eliminar la mascotas
                    Constructores.Procedimientos.EliminarProveedor(Convert.ToInt32(dgClientes.SelectedValue));
                }
                //Actualizar el listbox de mascotas
                ObtenerProveedor();
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValores())
            {
                //Obtener los valores para la mascota
                proveedor.NombreProveedor = txtnombreProveedor.Text;
                proveedor.Compania = txtcompañia.Text;
                proveedor.Telefono = txttelefono.Text;
                proveedor.Correo = txtcorreo.Text;
                proveedor.DirreccionEmpresa = txtdireccion.Text;
                proveedor.IdProveedor = Convert.ToInt32(dgClientes.SelectedValue);

                //Ejecutamos
                Constructores.Procedimientos.EditarProveerdor(proveedor);

                ObtenerProveedor();
                Limpiar();
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
    }
}
