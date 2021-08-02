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
using System.Windows.Shapes;

namespace SC_MMascotass.Pages.Formularios
{
    /// <summary>
    /// Interaction logic for FormExpediente.xaml
    /// </summary>
    public partial class FormExpediente : Window
    {
        private Mascota mascota = new Mascota();
        private Constructores.Expediente expediente = new Constructores.Expediente();
        public FormExpediente()
        {
            InitializeComponent();
        }

        private void btnNuevoExpediente_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            expediente.IdMascota = mascota.IdMascota;
            expediente.FechaRegistro = DateTime.Today;

            Constructores.Procedimientos.CrearExpediente(expediente);
        }

        private void btnRestablecer_Click(object sender, RoutedEventArgs e)
        {

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
                var border = (autoCompleteMascotas.Parent as ScrollViewer).Parent as Border;
                txtexpediente.Text = (sender as TextBlock).Text;
                mascota = Constructores.Procedimientos.BuscarMascotaNombre(txtexpediente.Text);
                border.Visibility = System.Windows.Visibility.Collapsed;
                //ObtenerValoresFormulario();
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
            autoCompleteMascotas.Children.Add(block);
        }


        private void txtexpediente_KeyUp(object sender, KeyEventArgs e)
        {
            bool found = false;
            var border = (autoCompleteMascotas.Parent as ScrollViewer).Parent as Border;
            var data = Constructores.Procedimientos.MonstrarMascotas23();


            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear   
                autoCompleteMascotas.Children.Clear();
                border.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                border.Visibility = System.Windows.Visibility.Visible;
            }

            // Clear the list   
            autoCompleteMascotas.Children.Clear();

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
                autoCompleteMascotas.Children.Add(new TextBlock() { Text = "No se ha encontrado" });
            }
        }
    }
}
