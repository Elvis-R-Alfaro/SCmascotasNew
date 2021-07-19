﻿using System;
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
    /// Interaction logic for Mascotas.xaml
    /// </summary>
    public partial class Mascotas : UserControl
    {
        private Mascota mascota = new Mascota();
        private List<Mascota> mascotas;
        public Mascotas()
        {
            InitializeComponent();
            ObtenerMascotas();
        }



        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            // Mostrar el formulario de menú principal
            FormMascotas mascota = new FormMascotas(false);
            mascota.Show();

        }

        private void ObtenerMascotas()
        {
            mascotas = mascota.MostrarMascotas();
            dgClientes.SelectedValuePath = "IdMascota";
            dgClientes.ItemsSource = mascotas;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientes.SelectedValue == null)
                MessageBox.Show("Por favor selecciona una mascota de la lista");
            else
            {
                FormMascotas.ides = Convert.ToInt32(dgClientes.SelectedValue);
                FormMascotas mascota = new FormMascotas(true);
                mascota.Show();
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgClientes.SelectedValue == null)
                    MessageBox.Show("Por favor, seleccione una mascota de la lista");
                else
                {
                    //Monstrar mensjae de confirmacion
                    MessageBoxResult result = MessageBox.Show("¿Deseas eliminar la mascota?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        //Eliminar la mascotas
                        mascota.EliminarMascota(Convert.ToInt32(dgClientes.SelectedValue));
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al eliminar la mascota...");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //Actualizar el listbox de mascotas
                ObtenerMascotas();
            }
        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            ObtenerMascotas();
        }
    }
}
