using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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




        #region Login
        public static bool Vacios_Login(string Usuario, string contra)
        {
            if (string.IsNullOrWhiteSpace(Usuario))
            {
                Mensaje = "¡Ingrese el usuario!";
                return false;
            }
            else if (string.IsNullOrWhiteSpace(contra))
            {
                Mensaje = "¡Ingrese la contraseña!";
                return false;
            }

            return true;
        }

        public static bool ValoresVacios(string Texto)
        {
            if (string.IsNullOrWhiteSpace(Texto))
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Clientes
        //Validaciones Vacios Cliente

        public static bool Vacios_Cliente(string Nombre, string Telefono, bool vali)
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                Mensaje = "¡Ingrese el Nombre del Cliente!";
                return false;
            }

            else if (string.IsNullOrWhiteSpace(Telefono))
            {
                Mensaje = "¡Ingrese el Teléfono Corectamente!";
                return false;
            }

            else if (vali)
            {
                Mensaje = "¡El Teléfono debe ser numerico!";
                return false;
            }

            return true;
        }

        #endregion

        #region Mascotas
        //Validaciones Vacio Mascotas

        public static bool Vacios_Mascota(string AuCliente, string AliasMascota, string Especie, string Raza, string ColorPelo)
        {
            if (string.IsNullOrWhiteSpace(AuCliente))
            {
                Mensaje = "¡Ingrese el Nombre del Cliente!";
                return false;
            }
            else if (string.IsNullOrWhiteSpace(AliasMascota))
            {
                Mensaje = "¡Ingrese el Nombre de la Mascota!";
                return false;
            }
            else if (string.IsNullOrWhiteSpace(Especie))
            {
                Mensaje = "¡Ingrese la especie de la Mascota!";
                return false;
            }
            else if (string.IsNullOrWhiteSpace(Raza))
            {
                Mensaje = "¡Ingrese la raza de la Mascota!";
                return false;
            }
            else if (string.IsNullOrWhiteSpace(ColorPelo))
            {
                Mensaje = "¡Ingrese el Color de Pelo de la mascota!";
                return false;
            }

            return true;
        }

        #endregion

        #region Categorias
        //Validaciones Vacio Categoria

        public static bool Vacios_Categoria(string Categoria)
        {
            if (string.IsNullOrWhiteSpace(Categoria))
            {
                Mensaje = "¡Ingrese el Nombre de la Categoría!";
                return false;
            }

            return true;
        }

        #endregion

        #region Inventarios
        //Validaciones Vacios Inventario
        public static bool Vacios_Inventario(string AuCategoria, string Descripcion, string Stock, string PrecioCosto, string PrecioVenta)
        {
            if (string.IsNullOrWhiteSpace(AuCategoria))
            {
                Mensaje = "¡Ingrese el Nombre de la Categoría!";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Descripcion))
            {
                Mensaje = "¡Ingrese la Descripción del producto!";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Stock))
            {
                Mensaje = "¡Ingrese Stock para el producto!";
                return false;
            }
            if (string.IsNullOrWhiteSpace(PrecioCosto))
            {
                Mensaje = "¡Ingrese el costo!";
                return false;
            }
            if (string.IsNullOrWhiteSpace(PrecioVenta))
            {
                Mensaje = "¡Ingrese el precio de venta!";
                return false;
            }
            return true;
        }
        #endregion

        #region Vacunaciones
        //Validaciones Vacio HistorialVacunacion

        public static bool Vacios_Vacunas(string AuCliente, object cmb)
        {
            if (string.IsNullOrWhiteSpace(AuCliente))
            {
                Mensaje = "¡Ingrese el Nombre de la Mascota!";
                return false;
            }
            else if (cmb.Equals(-1))
            {
                Mensaje = "Por favor seleccione una vacuna";
                return false;
            }

            return true;
        }
        #endregion
    }

}
