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


namespace SC_MMascotass
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class IniciarSesion : Window
    {
        //Objeto de tipo usuario para implementar su funcionalidad
        private Usuario usuario = new Usuario();

        public IniciarSesion()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Implementar la busqueda del usuario desde la clase usuario
                Usuario elUsuario = usuario.BuscarUsuario(txtUsername.Text);

                //Verificar si el usuario existe 
                if (elUsuario.Username == null)
                {
                    MessageBox.Show("El usuario o contraseña no es correcta");
                }
                else {
                    //Verificar si la contraseña coincide con la de la base
                    if (elUsuario.Password == pwbPassword.Password && elUsuario.Estado)
                    {
                        MenuPrincipal menu = new MenuPrincipal();
                        menu.Show();
                        this.Close();
                    }
                    else if (!elUsuario.Estado)
                    {
                        MessageBox.Show("El usuario se encuentra inactivo, comuniquese con el personal de IT");
                    }else
                    {
                        MessageBox.Show("El usuario o contraseña no es correcta");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al momento de realizar la consulta...");
                Console.WriteLine(ex.Message);
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
