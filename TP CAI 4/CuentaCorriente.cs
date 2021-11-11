using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_CAI
{
    class CuentaCorriente
    {
        public CuentaCorriente()
        {

        }

        public int IDcuenta { get; set; }
        public int IDcliente { get; set; }
        public float credito { get; set; }
        public float deuda { get; set; }

        public string Titulo
        {
            get
            {
                return $"{IDcuenta} - {IDcliente}";
            }
        }

        public static CuentaCorriente SeleccionarCuenta()
        {
            //HACER: pedir datos de la solicitud, buscarla en el archivo y devolver
            throw new NotImplementedException();
        }

        public static void MostrarEstado(CuentaCorriente cuenta)
        {
            //HACER
            throw new NotImplementedException();
        }

        public void MostrarCuenta()
        {
            //HACER
            throw new NotImplementedException();
        }
    }
}
