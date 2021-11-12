using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_CAI
{
    class CuentaCorriente
    {
        const string nombreArchivo = "RegistroCuentas.txt";
        public CuentaCorriente()
        {
            if (File.Exists(nombreArchivo))
            {
                using (var reader = new StreamReader(nombreArchivo))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var cuenta = new CuentaCorriente(linea);
                        RegistroCuentas.Add(cuenta.IDcuenta, cuenta);
                    }
                }
            }
        }

        public CuentaCorriente(string linea)
        {
            var datos = linea.Split(';');
            IDcuenta = int.Parse(datos[0]);
            IDcliente = int.Parse(datos[1]);
            Credito = double.Parse(datos[2]);
            Deuda = double.Parse(datos[3]);
        }

        public int IDcuenta { get; set; }
        public int IDcliente { get; set; }
        public double Credito { get; set; }
        public double Deuda { get; set; }

        private static readonly Dictionary<int, CuentaCorriente> RegistroCuentas = new Dictionary<int, CuentaCorriente>();

        public string ObtenerLineaDatos()
        {
            return $"{IDcuenta};{IDcliente};{Credito};{Deuda}";
        }

        public string Titulo
        {
            get
            {
                return $"{IDcuenta} - {IDcliente}";
            }
        }

        public static CuentaCorriente SeleccionarCuenta()
        {
            var modelo = CuentaCorriente.CrearModeloBusqueda();

            foreach (var cuenta in RegistroCuentas.Values)
            {
                if (cuenta.Compara(modelo))
                {
                    return cuenta;
                }
            }

            Console.WriteLine("No se ha encontrado una solicitud que coincida con los criterios indicados." + Environment.NewLine);
            return null;
        }

        public void MostrarEstadoCuenta()
        {
            Console.WriteLine($"ID cuenta: {IDcuenta}");
            Console.WriteLine($"Crédito: {Credito}");
            Console.WriteLine($"Deuda: {Deuda}");
        }

        public void MostrarCuenta()
        {
            Console.WriteLine($"ID cuenta: {IDcuenta}");
            Console.WriteLine($"Cliente: {IDcliente}");
        }

        public static CuentaCorriente CrearModeloBusqueda()
        {
            var modelo = new CuentaCorriente();
            bool flag = false;
            do
            {
                Console.WriteLine("Ingrese el número de la cuenta a buscar.");
                var cuentaABuscar = Console.ReadLine();

                if (!int.TryParse(cuentaABuscar, out var salida))
                {
                    Console.WriteLine("Usted ingreso un valor incorrecto. Intente nuevamente." + Environment.NewLine);
                    continue;
                }
                else
                {
                    modelo.IDcuenta = salida;
                    flag = true;
                }

            } while (flag == false);

            return modelo;
        }

        public bool Compara(CuentaCorriente modelo)
        {
            if (IDcuenta != modelo.IDcuenta)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void Debitar(SolicitudServicio solicitud)
        {
            var monto = solicitud.PrecioAFacturar;
            var idcliente = solicitud.IDcliente;
            var cliente = Cliente.SeleccionarCliente(idcliente);
            var idcuenta = cliente.IDcuenta;
            var cuenta = CuentaCorriente.SeleccionarCuentaDebito(idcuenta);

            cuenta.Deuda = cuenta.Deuda + monto;

            GrabarCuentas();
        }

        public static CuentaCorriente SeleccionarCuentaDebito(int idcuenta)
        {
            var modelo = CuentaCorriente.CrearModeloBusqueda(idcuenta);

            foreach (var cuenta in RegistroCuentas.Values)
            {
                if (cuenta.Comparativa(modelo))
                {
                    return cuenta;
                }
            }

            Console.WriteLine("No se ha encontrado una solicitud que coincida con los criterios indicados." + Environment.NewLine);
            return null;
        }

        private static void GrabarCuentas()
        {
            using (var writer = new StreamWriter(nombreArchivo, append: false))
            {
                foreach (var cuenta in RegistroCuentas.Values)
                {
                    var linea = cuenta.ObtenerLineaDatos();
                    writer.WriteLine(linea);
                }
            }
        }

        public static CuentaCorriente CrearModeloBusqueda(int idcuenta)
        {
            var modelo = new CuentaCorriente();
            modelo.IDcliente = idcuenta;

            return modelo;
        }

        public bool Comparativa(CuentaCorriente modelo)
        {
            if (IDcuenta != modelo.IDcuenta)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Modificar(double monto)
        {
            Deuda = Deuda + monto;
        }
    }
}
