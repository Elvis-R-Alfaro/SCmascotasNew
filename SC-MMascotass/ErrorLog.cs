using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Diagnostics;

namespace SC_MMascotass
{
    public class ErrorLog
    {


        private string Path = "/eLogs";


        public void Add(object obj,  /*Exception*/ string ex)
        {
            CreateDirectory();
            string nombre = GetNameFile();

            StreamWriter sw = new StreamWriter(Path + "/" + nombre, true);
            StackTrace stacktrace = new StackTrace();
            sw.WriteLine(obj.GetType().FullName + " " + DateTime.Now);
            sw.WriteLine(stacktrace.GetFrame(1).GetMethod().Name + " - " + ex/*.Message*/);
            sw.WriteLine("");
            sw.Close();

        }

        #region HELPER
        private string GetNameFile()
        {
            string nombre = "";

            nombre = "log_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".txt";

            return nombre;
        }

        private void CreateDirectory()
        {
            try
            {
                if (!Directory.Exists(Path))
                    Directory.CreateDirectory(Path);


            }
            catch (DirectoryNotFoundException ex)
            {
                throw new Exception(ex.Message);

            }
        }
        #endregion
    }
}