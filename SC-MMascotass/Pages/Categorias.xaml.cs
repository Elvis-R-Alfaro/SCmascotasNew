using SC_MMascotass.Pages;
using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class Categorias : UserControl
    {
        private Categoria categoria = new Categoria();
        private List<Categoria> categorias;


        public Categorias()
        {
            InitializeComponent();

            ObtenerCategorias();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Formularios.FormCategorias categoria = new Formularios.FormCategorias(false);
            categoria.Show();
        }
        
        private void ObtenerCategorias()
        {
            categorias = categoria.MonstrarCategorias();
            dgClientes.SelectedValuePath = "Id";
            dgClientes.ItemsSource = categorias;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientes.SelectedValue == null)
                MessageBox.Show("Por favor selecciona una categoría");
            else
            {
                Formularios.FormCategorias.ides = Convert.ToInt32(dgClientes.SelectedValue);
                Formularios.FormCategorias categoria = new Formularios.FormCategorias(true);
                categoria.Show();
            }
                       
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            if (dgClientes.SelectedValue == null)
                MessageBox.Show("Por favor, seleccione una categoría de la lista");
            else
            {
                //Monstrar mensaje de confirmacion
                MessageBoxResult result = MessageBox.Show("¿Deseas eliminar la categoria?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    //Eliminar la categoria
                    categoria.EliminarCategoria(Convert.ToInt32(dgClientes.SelectedValue));
                    ObtenerCategorias();
                }
                    
            }         

        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            ObtenerCategorias();
        }
    }
}
