using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Agregar los namespaces
using System.Data.SqlClient;
using System.Configuration;


namespace SC_MMascotass.Constructores
{
    public class Expediente
    {


        public int IdExpediente { get; set; }
        public int IdMascota { get; set; }
        public string NombreMascota { get; set; }
        public DateTime FechaRegistro { get; set; }


        public string NombreCliente { get; set; }

        public string UltimaVisita { get; set; }

        public int IdDetalle { get; set; }
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public string Patologia { get; set; }
        public string TratamientoRecomendado { get; set; }

        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Producto { get; set; }

        public DateTime FechaRegistroDeta { get; set; }



        public Expediente() { }

        public Expediente(int idExpediente,int idMascota,string nombreMascota, DateTime fechaRegistro, string nombreCliente, string ultimaVisita, int idDetalle, int idProducto, string descripcion, string patologia, string tratamiento,int idUsuario, DateTime fechaRegistroDeta, string usuario,string producto)
        {
            IdMascota = idMascota;
            NombreMascota = nombreMascota;
            FechaRegistro = fechaRegistro;
            IdExpediente = idExpediente;
            NombreCliente = nombreCliente;
            UltimaVisita = ultimaVisita;


            IdDetalle = idDetalle;
            IdProducto = idProducto;
            Descripcion = descripcion;
            Patologia = patologia;
            TratamientoRecomendado = tratamiento;
            IdUsuario = idUsuario;
            FechaRegistroDeta = fechaRegistroDeta;
            Usuario = usuario;

            Producto = producto;
        }



    }
}
