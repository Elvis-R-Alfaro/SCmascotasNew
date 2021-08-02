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
    public class Mascota
    {
        //Variable Miembro
        private static SqlConnection sqlConnection = database.Conexion.ObtenerConexion();

        private Cliente cliente = new Cliente();
        //Propiedades

        public int IdMascota { get; set; }
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public string NombreMascota { get; set; }
        public int IdRaza { get; set; }
        public string NombreRaza { get; set; }
        public string Altura { get; set; }
        public string RangoPeso { get; set; }
        public string EsperanzaVida { get; set; }
        public string ActividadFisica { get; set; }
        public string TipoDePelo { get; set; }
        public string Sexo { get; set; }
        public int IdEspecie { get; set; }
        public string Descripcion { get; set; }
        public string Familia { get; set; }
        public DateTime Fecha { get; set; }

        //Constructor
        public Mascota() { }
        public Mascota(int idmascota, int idcliente, string nombremascota, string cliente,string sexo, int idraza,  string nombreraza, string altura, string rangopeso, string esperanzavida, string actividadfisica, string tipodepelo, int idespecie, string descripcion, string familia, DateTime fecha)
        {
            IdMascota = idmascota;
            IdCliente = idcliente;
            NombreMascota = nombremascota;
            Cliente = cliente;
            IdRaza = idraza;
            NombreRaza = nombreraza;
            Altura = altura;
            RangoPeso = rangopeso;
            EsperanzaVida = esperanzavida;
            ActividadFisica = actividadfisica;
            TipoDePelo = tipodepelo;
            Sexo = sexo;
            IdEspecie = idespecie;
            Descripcion = descripcion;
            Familia = familia;
            Fecha = fecha;
        }

        //Metodos

        
        static public List<string> GetData()
        {
            List<string> data = new List<string>();

            data.Add("Comida para perros");
            data.Add("Comida para Gatos");
            data.Add("Juguetes");
            data.Add("Jarabes");
            data.Add("Pastillas");
            data.Add("Vitaminas");
            data.Add("Correas");
            data.Add("Camas");
            data.Add("Vacunas");
            data.Add("En latados");
            data.Add("Tazones");
            data.Add("Servicio");

            return data;
        }
    }
}
