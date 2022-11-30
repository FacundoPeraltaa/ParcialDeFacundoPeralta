using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcialDeYagoTorres.Entidades;
namespace ParcialDeYagoTorres.Datos
{
   public class RepositorioDeCubos
   {
        private List<ortoedro> listaOrtoedros;
        private bool hayCambios = false;

        public RepositorioDeOrtoedros()
        {
            listaOrtoedros = new List<ortoedro>();
            listaOrtoedros = ManejadorDeArchivo.LeerArchivo();
        }

        public void Agregar(ortoedro ortoedro)
        {
            listaOrtoedros.Add(ortoedro);
            hayCambios = true;
        }

        public void Borrar(ortoedro ortoedro)
        {
            listaOrtoedros.Remove(ortoedro);
            hayCambios = true;
        }

        public void Editar(ortoedro ortoedro)
        {
            hayCambios = true;
        }

        public List<ortoedro> GetLista()
        {
            return listaOrtoedros;
        }

        public int GetCantidad()
        {
            return listaOrtoedros.Count;
        }

        public void GuardarEnArchivo()
        {
            if (hayCambios)
            {
                ManejadorDeArchivo.GuardarEnArchivo(listaOrtoedros);
            }
        }


        public List<ortoedro> GetListaOrdenadaAsc()
        {
            return listaOrtoedros.OrderBy(e => e.Arista).ToList();
        }

        public List<ortoedro> GetListaOrdenadaDesc()
        {
            return listaOrtoedros.OrderByDescending(e => e.Arista).ToList();
        }

        public List<ortoedro> FiltrarPorTrazo(Trazo trazoFiltrar)
        {
            return listaOrtoedros.Where(e => e.Trazo == trazoFiltrar).ToList();
        }



    }
}
