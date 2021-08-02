using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public partial class FormCliente : Window
    {
        private Cliente cliente = new Cliente();

        //Variable de id
        public static int ides;
        bool vali;
        public FormCliente(bool visible)
        {
            InitializeComponent();
            MonstrarBotones(visible);

            //ErrorLog eLog = new ErrorLog("/eLogs");
            //eLog.Add("Test");

            //Validacion de cargar datos
            if (visible)
            {
                cliente = cliente.BuscarCliente(ides);
                txtNombre.Text = cliente.Nombre_Cliente;
                txtTelefono.Text = cliente.Teléfono;
                txtCorreo.Text = cliente.Correo;
                txtDireccion.Text = cliente.Direccion;
            }
        }

        private bool VerificarValores()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("¡Ingrese el Nombre del Cliente!");
                return false;
            }

            
            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("¡Ingrese el Teléfono Corectamente!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("¡Ingrese la Dirección Corectamente!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("¡Ingrese el Correo Corectamente!");
                return false;
            }

            if (vali)
            {
                MessageBox.Show("¡El Teléfono debe ser numerico!");
                return false;
            }
            return true;
        }


        //Obtener datos del formulario
        private void ObtenerValoresFormulario()
        {
            cliente.Nombre_Cliente = txtNombre.Text;
            cliente.Teléfono = txtTelefono.Text;
            cliente.ID = Convert.ToInt32(ides);
            cliente.Correo = txtCorreo.Text;
            cliente.Direccion = txtDireccion.Text;
        }

        //Vibilidad de los botones
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
            if ( Constructores.Validaciones.Vacios_Cliente(txtNombre.Text, txtTelefono.Text, txtCorreo.Text,txtDireccion.Text,vali))
            {
                try
                {
                    //caste
                    ObtenerValoresFormulario();

                    //caste
                    ObtenerValoresFormulario();
                    //Obtener los valores para el cliente
                    ObtenerValoresFormulario();
                    //Caste



                    ///ELVIS
                    //////ELVIS
                    //Insertar los datos de el cliente
                    cliente.CrearCliente(cliente);

                    //Mensaje de inserccion exito
                    MessageBox.Show("Datos insertados correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    Limpiar();
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Revise que los Campos esten Escritos Correctamente");
                    Console.WriteLine(ex.Message);

                }


            }
            else
            {
                MessageBox.Show(Constructores.Validaciones.Mensaje);
            }

        }

        //Metodo Limpiar
        private void Limpiar()
        {
            txtNombre.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtDireccion.Text = string.Empty;
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
                    //Obtener los valores para el cliente desde el formulario
                    ObtenerValoresFormulario();

                    //Actualizar los valores en la base de datos
                    cliente.EditarCliente(cliente);

                    //Mensaje de actualizacion realizada
                    MessageBox.Show("Datos modificados correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);

                    //Limpiar formulario
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Revise que los Campos esten Escritos Correctamente");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            cliente = cliente.BuscarCliente(ides);
            txtNombre.Text = cliente.Nombre_Cliente;
            txtTelefono.Text = cliente.Teléfono;
            txtCorreo.Text = cliente.Correo;
            txtDireccion.Text = cliente.Direccion;
        }

        private void txtTelefono_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                vali = false;
            else
                vali = true;
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
