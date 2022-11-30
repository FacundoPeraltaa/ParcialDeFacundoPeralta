using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcialDeYagoTorres.Entidades;
namespace ParcialDeYagoTorres.Datos
{

        public static class ManejadorDeArchivo
        {
            private static string archivo = "Ortoedro.txt";

            public static void GuardarEnArchivo(List<ortoedro> lista)
            {
                using (var escritor = new StreamWriter(archivo))
                {
                    foreach (var ortoedro in lista)
                    {
                        string linea = ConstuirLinea(ortoedro);
                        escritor.WriteLine(linea);
                    }
                }
            }

            private static string ConstuirLinea(ortoedro ortoedro)
            {
                return $"{ortoedro.Arista}|{(int)ortoedro.Relleno}|{(int)ortoedro.Trazo}";
            }

            public static List<ortoedro> LeerDelArchivo()
            {
                List<ortoedro> lista = new List<ortoedro>();
                using (var lector = new StreamReader(archivo))
                {
                    while (!lector.EndOfStream)
                    {
                        string linea = lector.ReadLine();
                        ortoedro ortoedro = CrearOrtoedro(linea);
                        lista.Add(ortoedro);
                    }
                }

                return lista;
            }

            private static ortoedro CrearOrtoedro(string linea)
            {
                var campos = linea.Split('|');
                ortoedro ortoedro= new ortoedro()
                {
                    Arista = int.Parse(campos[0]),
                    Relleno = (Relleno)int.Parse(campos[1]),
                    Trazo = (Trazo)int.Parse(campos[2])
                };
                return ortoedro;
            }
            public static List<ortoedro> LeerArchivo()
            {
                List<ortoedro> lista = new List<ortoedro>();
                using (var lector = new StreamReader(archivo))
                {
                    while (!lector.EndOfStream)
                    {
                        string linea = lector.ReadLine();
                    ortoedro ortoedro = CrearOrtoedro(linea);
                        
                        lista.Add(ortoedro);
                    }
                }

                return lista;
            }
        }



    
}
