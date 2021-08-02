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
//Agregar los namespaces
using System.Data.SqlClient;
using System.Configuration;

namespace SC_MMascotass.Pages.Formularios
{
    public partial class FormMascotas : Window
    {
        private Mascota mascota = new Mascota();
        private Cliente cliente = new Cliente();

        private SqlConnection sqlConnection = database.Conexion.ObtenerConexion();

        //Variable de id
        public static int ides;
        public FormMascotas(bool visible)
        {
            InitializeComponent();
            CargarRazasCombo();

            //Muestra u oculta los botones
            MonstrarBotones(visible);

            //Oculta el auto completar al iniiar el fomrulario
            var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            border.Visibility = System.Windows.Visibility.Collapsed;

            //VAlidacion de cargar los datos
            if (visible)
            {
                mascota = Constructores.Procedimientos.BuscarMascota(ides);

                txtAliasMascota.Text = mascota.NombreMascota;
                cmbraza.Text = mascota.NombreRaza;
                cmbraza.SelectedValuePath = mascota.IdRaza.ToString();
                cmbsexo.Text = mascota.Sexo;
                dtpFechaNacimiento.Text = mascota.Fecha.ToString();

                cliente = Constructores.Procedimientos.BuscarCliente(mascota.IdCliente);
                txtAuCliente.Text = cliente.Nombre_Cliente;
            }

        }

        private void CargarRazasCombo()
        {
            try
            {
                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand("CargarRazasCombo", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Abrir conexion
                sqlConnection.Open();

                SqlDataReader dr = sqlCommand.ExecuteReader();
                while (dr.Read())
                {
                    cmbraza.Items.Add(dr["NombreRaza"].ToString());
                    cmbraza.SelectedValuePath = dr["IdRaza"].ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //Cerrar la conexion
                sqlConnection.Close();
            }
        }


        //Autocompletar TextBox
        private void txtAuCliente_KeyUp(object sender, KeyEventArgs e)
        {
            bool found = false;
            var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            var data = Constructores.Procedimientos.MonstrarMascotas22();


            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear   
                resultStack.Children.Clear();
                border.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                border.Visibility = System.Windows.Visibility.Visible;
            }

            // Clear the list   
            resultStack.Children.Clear();

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
                resultStack.Children.Add(new TextBlock() { Text = "No se ha encontrado" });
            }
        }

        private void addItem(string text)
        {
            TextBlock block = new TextBlock();
            Label id = new Label();


            // Add the text   
            block.Text = text;

            // A little style...   
            block.Margin = new Thickness(2, 3, 2, 3);
            block.Cursor = Cursors.Hand;

            // Mouse events   
            block.MouseLeftButtonUp += (sender, e) =>
            {
                var border = (resultStack.Parent as ScrollViewer).Parent as Border;
                txtAuCliente.Text = (sender as TextBlock).Text;
                cliente = Constructores.Procedimientos.BuscarClientID(txtAuCliente.Text);
                border.Visibility = System.Windows.Visibility.Collapsed;
                ObtenerValoresFormulario();
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
            resultStack.Children.Add(block);
        }

        private void btnNuevoCliente_Click(object sender, RoutedEventArgs e)
        {
            // Mostrar el formulario de menú principal
            FormCliente cliente = new FormCliente(false);
            cliente.Show();
        }

        private bool VerificarValores()
        {
            if (string.IsNullOrWhiteSpace(txtAuCliente.Text))
            {
                MessageBox.Show("¡Ingrese el Nombre del Cliente!");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtAliasMascota.Text))
            {
                MessageBox.Show("¡Ingrese el Nombre de la Mascota!");
                return false;
            }
            //else if (string.IsNullOrWhiteSpace(txtEspecie.Text))
            //{
            //    MessageBox.Show("¡Ingrese la especie de la Mascota!");
            //    return false;
            //}
            //else if (string.IsNullOrWhiteSpace(txtRaza.Text))
            //{
            //    MessageBox.Show("¡Ingrese la raza de la Mascota!");
            //    return false;
            //}


            return true;
        }

        //Obtener los valores del fomrulario en variables
        private void ObtenerValoresFormulario()
        {
            mascota = Constructores.Procedimientos.BuscarMascota(ides);
            cliente = Constructores.Procedimientos.BuscarCliente(ides);

            cliente = Constructores.Procedimientos.BuscarClientID(txtAuCliente.Text);
            mascota.IdCliente = cliente.ID;
            cliente.Nombre_Cliente = txtAuCliente.Text;
            mascota.NombreMascota = txtAliasMascota.Text;
            mascota.Sexo = cmbsexo.SelectionBoxItem.ToString();
            mascota.IdRaza = Convert.ToInt32(cmbraza.SelectedValuePath);
            mascota.Fecha = dtpFechaNacimiento.SelectedDate.Value;

        }
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValores())
            {
                try
                {
                    //Obtener los valores para la mascota
                    ObtenerValoresFormulario();

                    Constructores.Procedimientos.CrearMascota(mascota);

                    //Mensaje de inserccion exito
                    MessageBox.Show("Datos Insertados Correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar la mascota....");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Limpiar();
                    this.Close();
                }
            }
        }

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

        private void Limpiar()
        {
            txtAliasMascota.Text = string.Empty;
            txtAuCliente.Text = string.Empty;
            cmbsexo.Text = "";
            cmbraza.Text = "";
        }
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValores())
            {
                try
                {
                    //Obtener los valores para la mascota
                    ObtenerValoresFormulario();

                    //Insertar los datos de la mascota
                    Constructores.Procedimientos.EditarMascota(mascota);

                    //Mensaje de inserccion exito
                    MessageBox.Show("Datos Modificados Correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar la mascota....");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Limpiar();
                    this.Close();
                }
            }
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnRestablecer_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void btnNuevaRaza_Click(object sender, RoutedEventArgs e)
        {
            FormRazas raza = new FormRazas(false);
            raza.Show();
        }
    }
}
