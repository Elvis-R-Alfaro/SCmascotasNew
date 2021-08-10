using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Agregar los namespaces
using System.Data.SqlClient;
using System.Configuration;

namespace SC_MMascotass
{
    public class InventarioC
    {
        //Propiedades
        public int IdCategoria { get; set; }            
        public string Descripcion { get; set; }
        public int Id { get; set; }
        public string Categoria { get; set; }
        public int Stock { get; set; }
        public int Cantidad { get; set; }
        public double PrecioCosto { get; set; }
        public double PrecioVenta { get; set; }

        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string Compania { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string DirreccionEmpresa { get; set; }

        //Constructor
        public InventarioC() { }
        public InventarioC(int idCategoria, int idproveedor, string descripcion,string categoria, int id, int stock, int cantidad, double precioCosto, double precioVenta, string nombreproveedor, string compania, string telefono, string correo, string direccionempresa)
        {
            IdCategoria = idCategoria;
            Descripcion = descripcion;
            Categoria = categoria;
            Id = id;
            Stock = stock;
            Cantidad = cantidad;

            PrecioCosto = precioCosto;
            PrecioVenta = precioVenta;

            IdProveedor = idproveedor;
            NombreProveedor = nombreproveedor;
            Compania = compania;
            Telefono = telefono;
            Correo = correo;
            DirreccionEmpresa = direccionempresa;
        }

    }
}
