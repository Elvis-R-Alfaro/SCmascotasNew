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
    /// Lógica de interacción para FormCategoriasN.xaml
    /// </summary>
    public partial class FormCategoriasN : Window
    {
        private Categoria categoria = new Categoria();
        private List<Categoria> categorias;
        public FormCategoriasN()
        {
            InitializeComponent();
            ObtenerCategorias();
        }

        private void ObtenerCategorias()
        {
            categorias = Constructores.Procedimientos.MostrarCategorias();
            dgClientes.SelectedValuePath = "Id";
            dgClientes.ItemsSource = categorias;
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
                categoria.NombreCategoria = txtCategoria.Text;

                //Ejecutamos
                Constructores.Procedimientos.CrearCategoria(categoria);

                ObtenerCategorias();
                Limpiar();
            }
        }

        private void Limpiar()
        {
            txtCategoria.Clear();

            spButton1.Visibility = Visibility.Visible;
            spButton2.Visibility = Visibility.Hidden;
            ObtenerCategorias();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientes.SelectedValue == null)
                MessageBox.Show("Por favor selecciona un categoria de la lista");
            else
            {
                spButton1.Visibility = Visibility.Hidden;
                spButton2.Visibility = Visibility.Visible;
                categoria = Constructores.Procedimientos.BuscarCategoria(Convert.ToInt32(dgClientes.SelectedValue));
                txtCategoria.Text = categoria.NombreCategoria;                
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientes.SelectedValue == null)
                MessageBox.Show("Por favor, seleccione una categoria de la lista");
            else
            {
                //Monstrar mensjae de confirmacion
                MessageBoxResult result = MessageBox.Show("¿Deseas eliminar la categoría?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    //Eliminar la mascotas
                    Constructores.Procedimientos.EliminarCategoria(Convert.ToInt32(dgClientes.SelectedValue));
                }
                //Actualizar el listbox de mascotas
                ObtenerCategorias();
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
                categoria.NombreCategoria = txtCategoria.Text;
                categoria.Id = Convert.ToInt32(dgClientes.SelectedValue);

                //Ejecutamos
                Constructores.Procedimientos.EditarCategoria(categoria);

                ObtenerCategorias();
                Limpiar();
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
    }
}
