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
    public class Categoria
    {

        //Propiedades

        public int Id { get; set; }
        public string NombreCategoria { get; set; }

        //Constructor
        public Categoria() { }
        public Categoria(int id, string nombrecategoria)
        {
            Id = id;
            NombreCategoria = nombrecategoria;
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
