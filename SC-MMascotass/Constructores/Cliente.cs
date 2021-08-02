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
    public class Cliente
    {
        //Variable Miembro
        private SqlConnection sqlConnection = database.Conexion.ObtenerConexion();

        //Propiedades
        public int ID { get; set; }
        public string Nombre_Cliente { get; set; }
        public string Teléfono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }

        //Constructor
        public Cliente() { }
        public Cliente(int idcliente, string nombrecliente, string telefono, string correo, string direccion)
        {
            ID = idcliente;
            Nombre_Cliente = nombrecliente;
            Teléfono = telefono;
            Correo = correo;
            Direccion = direccion;
        }
        
        //static public List<string> GetData()
        //{
        //    List<string> data = new List<string>();

        //    data.Add("Comida para perros");
        //    data.Add("Comida para Gatos");
        //    data.Add("Juguetes");
        //    data.Add("Jarabes");
        //    data.Add("Pastillas");
        //    data.Add("Vitaminas");
        //    data.Add("Correas");
        //    data.Add("Camas");
        //    data.Add("Vacunas");
        //    data.Add("En latados");
        //    data.Add("Tazones");
        //    data.Add("Servicio");

        //    return data;
        //}
    }
}

