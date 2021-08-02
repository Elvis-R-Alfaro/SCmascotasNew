using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Windows.Input;
namespace SC_MMascotass.Constructores
{
    class Validaciones
    {
        public static string Mensaje;

        //Validaciones elemento seleccionado del datagrid
        public static bool dg_seleccionado(object seleccion, string objeto)
        {
            if (seleccion == null)
            {
                Mensaje = "Por favor selecciona " + objeto + " de la lista";
                return false;
            }
            return true;
        }


        public static bool Vacios_Login(string Usuario, string contra)
        {
            if (ValoresVacios(Usuario))
            {
                Mensaje = "¡Ingrese el usuario!";
                return false;
            }
            else if (ValoresVacios(contra))
            {
                Mensaje = "¡Ingrese la contraseña!";
                return false;
            }

            return true;
        }


        //Validacion de números
        public void SoloNumeros(TextCompositionEventArgs e)
        {
            int character = Convert.ToInt32(Convert.ToChar(e.Text));
            if (character >= 48 && character <= 57)
                e.Handled = false;
            else
                e.Handled = true;
        }

        //validar correo
        public bool ValidarEmail(string email)
        {
            Regex regex = new Regex(@" [\w-\.]+@([\w-]+\•)+[\w-1{2,4)$]");
            return regex.IsMatch(email);
        }

        //Validar letras
        public static void SoloLetras(TextCompositionEventArgs e)
        {
            int character = Convert.ToInt32(Convert.ToChar(e.Text));
            if ((character >= 65 && character <= 90) || (character >= 97 && character <= 122))
                e.Handled = false;
            else
                e.Handled = true;
        }


        //ValoresVacios
        public static bool ValoresVacios(string Campo)
        {
            if (string.IsNullOrWhiteSpace(Campo))
            {
                return false;
            }

            return true;
        }

        //Verificar numero

        public bool VerificarNumero(string numero)
        {
            bool sino = false;

            if(ValoresVacios(numero))
            {
                if (numero.Length == 8)
                    sino = true;
            }
            
            return sino;
        }

        public bool VerificarFecha(DateTime fecha)
        {
            DateTime hoy = DateTime.Today;
            bool sino = false;
            int anio = fecha.Year;
            if(anio>1990 && anio< hoy.Year -1)
            {
                sino = true;
            }
            return sino;
        }


        //Validaciones Vacios Cliente

        public static bool Vacios_Cliente(string Nombre, string Telefono, string correo,string direccion,bool vali)
        {

            if (ValoresVacios(Nombre))
            {
                Mensaje = "¡Ingrese el Nombre del Cliente!";
                return false;
            }

            else if (ValoresVacios(Telefono))
            {
                Mensaje = "¡Ingrese el teléfono del cliente!";
                return false;
            }

            else if (ValoresVacios(correo))
            {
                Mensaje = "¡Ingrese el correo del cliente!";
                return false;
            }
            else if (ValoresVacios(direccion))
            {
                Mensaje = "¡Ingrese la dirección del cliente";
                return false;
            }

            return true;
        }

        //Validaciones Vacio Mascotas

        public static bool Vacios_Mascota(string AuCliente, string sexo, string AliasMascota, string Especie, string Raza, string ColorPelo)
        {
            if (ValoresVacios(AuCliente))
            {
                Mensaje = "¡Ingrese el nombre del cliente!";
                return false;
            }
            else if (ValoresVacios(AliasMascota))
            {
                Mensaje = "¡Ingrese el Nombre de la Mascota!";
                return false;
            }else if (ValoresVacios(sexo))
            {
                Mensaje = "¡Ingrese el sexo de la mascota!";
                return false;
            }
            else if (ValoresVacios(Especie))
            {
                Mensaje = "¡Ingrese la especie de la mascota!";
                return false;
            }
            else if (ValoresVacios(Raza))
            {
                Mensaje = "¡Ingrese la raza de la mascota!";
                return false;
            }
            else if (ValoresVacios(ColorPelo))
            {
                Mensaje = "¡Ingrese el color de pelo de la mascota!";
                return false;
            }

            return true;
        }


        //Validaciones Vacio Categoria

        public static bool Vacios_Categoria(string Categoria)
        {
            if (ValoresVacios(Categoria))
            {
                Mensaje = "¡Ingrese el nombre de la categoría!";
                return false;
            }

            return true;
        }


        //Validaciones Vacios Inventario
        public static bool Vacios_Inventario(string AuCategoria, string AuProveedor, string Descripcion, string Stock, string PrecioCosto, string PrecioVenta)
        {
            if (ValoresVacios(AuCategoria))
            {
                Mensaje = "¡Ingrese el nombre de la Categoría!";
                return false;
            } if (ValoresVacios(AuProveedor))
            {
                Mensaje = "¡Ingrese el nombre de la Categoría!";
                return false;
            }
            if (ValoresVacios(Descripcion))
            {
                Mensaje = "¡Ingrese la descripción del producto!";
                return false;
            }
            if (ValoresVacios(Stock))
            {
                Mensaje = "¡Ingrese stock para el producto!";
                return false;
            }
            if (ValoresVacios(PrecioCosto))
            {
                Mensaje = "¡Ingrese el costo!";
                return false;
            }
            if (ValoresVacios(PrecioVenta))
            {
                Mensaje = "¡Ingrese el precio de venta!";
                return false;
            }
            return true;
        }

        //Validacion especies
        public static bool Vacios_Especie(string descripcion,string familia)
        {
            if (ValoresVacios(descripcion))
            {
                Mensaje = "¡Ingrese la especie!";
                return false;
            }
            if (ValoresVacios(familia))
            {
                Mensaje = "¡Ingrese la familia de la especie!";
                return false;
            }
            return true;
        }
        
        //Validacion raza
        public static bool Vacios_Raza(string especie,string raza, string altura, string peso, string esperanzaVida, string actividad, string tipoPelo)
        {
            if (ValoresVacios(especie))
            {
                Mensaje = "¡Ingrese la especie!";
                return false;
            }
            if (ValoresVacios(raza))
            {
                Mensaje = "¡Ingrese la raza!";
                return false;
            }
            if (ValoresVacios(altura))
            {
                Mensaje = "¡Ingrese la altura!";
                return false;
            }
            if (ValoresVacios(peso))
            {
                Mensaje = "¡Ingrese el peso!";
                return false;
            }
            if (ValoresVacios(esperanzaVida))
            {
                Mensaje = "¡Ingrese la esperanza de vida!";
                return false;
            }
            if (ValoresVacios(actividad))
            {
                Mensaje = "¡Ingrese la actividad fisica!";
                return false;
            }
            if (ValoresVacios(tipoPelo))
            {
                Mensaje = "¡Ingrese el tipo de pelo!";
                return false;
            }
            return true;
        }

        //Validacion vencidos
        public static bool Vacios_Vencidos(string producto,string retornable, string cantidad)
        {
            if (ValoresVacios(producto))
            {
                Mensaje = "¡Ingrese el producto!";
                return false;
            }

            if (ValoresVacios(retornable))
            {
                Mensaje = "¡Ingrese si es retornable o no!";
                return false;
            }
            if (ValoresVacios(cantidad))
            {
                Mensaje = "¡Ingrese la cantidad con la misma fecha de vencimiento!";
                return false;
            }
            return true;
        }

    }

}
