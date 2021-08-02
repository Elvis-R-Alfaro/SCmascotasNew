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
        //Variable Miembro
        private static SqlConnection sqlConnection = database.Conexion.ObtenerConexion();


        public int IdExpediente { get; set; }
        public int IdMascota { get; set; }
        public string NombreMascota { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Expediente() { }

        public Expediente(int idMascota,string nombreMascota, DateTime fechaRegistro)
        {
            IdMascota = idMascota;
            NombreMascota = nombreMascota;
            FechaRegistro = fechaRegistro;
        }
    }
}
