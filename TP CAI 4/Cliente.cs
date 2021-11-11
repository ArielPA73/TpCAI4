using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_CAI
{
    class Cliente
    {
        public Cliente()
        {

        }

        public int IDcliente { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string direccion { get; set; }
        public int numtelefono { get; set; }

        //cliente corporativo va en clase aparte?
    }
}
