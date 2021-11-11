using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_CAI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;

            do
            {
                Console.WriteLine("MENÚ PRINCIPAL");
                Console.WriteLine("--------------");

                Console.WriteLine("1- Solicitar servicio de correspondencia o encomienda.");
                Console.WriteLine("2- Consultar el estado de un servicio.");
                Console.WriteLine("3- Consultar el estado de cuenta.");
                Console.WriteLine("4- Finalizar.");

                Console.WriteLine(Environment.NewLine + "Ingrese una opción y presione [Enter]");
                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        SolicitarServicio();
                        break;

                    case "2":
                        ConsultarEstadoServicio();
                        break;

                    case "3":
                        ConsultarEstadoCuenta();
                        break;

                    case "4":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine(Environment.NewLine + "No ha ingresado una opción del menú. Por favor, intente de nuevo." + Environment.NewLine);
                        break;
                }

            } while (!salir);
        }

        private static void SolicitarServicio()
        {
            var solicitud = SolicitudServicio.IngresarSolicitud();
        }

        private static void ConsultarEstadoServicio()
        {
            var solicitud = SolicitudServicio.SeleccionarSolicitud();
            solicitud.MostrarSolicitud();
            bool flag = false;

            do
            {
                Console.WriteLine($"La solicitud seleccionada es: {solicitud.Titulo}, ¿está usted seguro? (S/N)");
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.S)
                {
                    SolicitudServicio.MostrarEstado(solicitud);
                    flag = true;
                }
                else if (key.Key == ConsoleKey.N)
                {
                    Console.WriteLine("Operación cancelada.");
                    flag = true;
                }
                else
                {
                    Console.WriteLine("Usted ingresó un valor incorrecto, intente de nuevo." + Environment.NewLine);
                }

            } while (flag == false);
        }

        private static void ConsultarEstadoCuenta()
        {
            var cuenta = CuentaCorriente.SeleccionarCuenta();
            cuenta.MostrarCuenta();
            bool flag = false;

            do
            {
                Console.WriteLine($"La cuenta seleccionada es: {cuenta.Titulo}, ¿está usted seguro? (S/N)");
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.S)
                {
                    CuentaCorriente.MostrarEstado(cuenta);
                    flag = true;
                }
                else if (key.Key == ConsoleKey.N)
                {
                    Console.WriteLine("Operación cancelada.");
                    flag = true;
                }
                else
                {
                    Console.WriteLine("Usted ingresó un valor incorrecto, intente de nuevo." + Environment.NewLine);
                }

            } while (flag == false);
        }
    }
}

