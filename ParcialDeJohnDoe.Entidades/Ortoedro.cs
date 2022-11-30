using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ParcialDeYagoTorres.Entidades
{
   public class ortoedro
    {
        public int Arista { get; set; }
        public Relleno Relleno { get; set; }

        

        public ortoedro()
        {

        }

        public double GetVolumen() => 6 * Math.Pow(Arista,3);
        
        public double GetArea() => Math.Pow(Arista,3);

        public bool Validar() => Arista > 0;

    }
}
