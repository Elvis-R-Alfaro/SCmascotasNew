﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_MMascotass
{
    public class Carrito : IEquatable<Carrito>
    {
        //Propiedades
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public int Id { get; set; }
        public int Stock { get; set; }
        public int Cantidad { get; set; }
        public double PrecioCosto { get; set; }
        public double PrecioVenta { get; set; }
        public double CantidadPorPrecio { get; set; }


        //Constructor
        public Carrito() { }
        public Carrito(int idCategoria, int idproveedor, string descripcion, int id, int stock, int cantidad, double precioCosto, double precioVenta, double cantidadPorPrecio, string nombreproveedor, string compania, string telefono, string correo, string direccionempresa)
        {
            IdCategoria = idCategoria;
            Descripcion = descripcion;
            Id = id;
            Stock = stock;
            Cantidad = cantidad;

            PrecioCosto = precioCosto;
            PrecioVenta = precioVenta;
            CantidadPorPrecio = cantidadPorPrecio;

        }

        public bool Equals(Carrito other)
        {
            if (this.Id == other.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
