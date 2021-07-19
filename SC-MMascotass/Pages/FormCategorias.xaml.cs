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
    public partial class FormCategorias : Window
    {
        private Categoria categoria = new Categoria();

        //Variable de id
        public static int ides;
        public FormCategorias(bool visible)
        {
            InitializeComponent();
            MonstrarBotones(visible);

            //Validacion de carga de datos
            if (visible)
            {
                categoria = categoria.BuscarCategoria(ides);
                txtCategoria.Text = categoria.NombreCategoria;
            }
               
        }

        private bool VerificarValores()
        {
            if (string.IsNullOrWhiteSpace(txtCategoria.Text))
            {
                MessageBox.Show("¡Ingrese el Nombre de la Categoría!");
                return false;
            }
            return true;
        }

        private void ObtenerValoresFormulario()
        {
            categoria.NombreCategoria = txtCategoria.Text;
            categoria.Id = Convert.ToInt32(ides);
        }

        //Monstrar Ocultal botones
        private void MonstrarBotones(bool visibles)
        {
            if (visibles)
            {
                spNuevaCategoria.Visibility = Visibility.Hidden;
                spButton1.Visibility = Visibility.Hidden;
                spEditarCategoria.Visibility = Visibility.Visible;
                spButton2.Visibility = Visibility.Visible;
            }
            else
            {
                spNuevaCategoria.Visibility = Visibility.Visible;
                spButton1.Visibility = Visibility.Visible;
                spEditarCategoria.Visibility = Visibility.Hidden;
                spButton2.Visibility = Visibility.Hidden;
            }
        }
      
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            if (VerificarValores())
            {
                try
                {
                    //Obtener los valores para la categoria
                    categoria.NombreCategoria = txtCategoria.Text;

                    //Insertar los datos de la categoria
                    categoria.CrearCategoria(categoria);

                    //Mensaje de inserccion exito
                    MessageBox.Show("Datos insertados correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar la categoria....");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    txtCategoria.Text=string.Empty;
                }
            }
        }

        private void btnRestablecer_Click(object sender, RoutedEventArgs e)
        {
            txtCategoria.Text = string.Empty;
        }

        private void bntAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValores())
            {
                try
                {
                    //Obtener los valores para la categoria desde el formulario
                    ObtenerValoresFormulario();

                    //Actualizar los valores en la base de datos
                    categoria.EditarCategoria(categoria);

                    //Mensaje de actualizacion realizada
                    MessageBox.Show("Datos Modificados correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);

                    //Limpiar formulario
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al momento de actualizar la categoria....");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
