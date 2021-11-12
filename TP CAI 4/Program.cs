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
            Console.Title = "Portal de Autogestión - Clientes Corporativos";
            //var usuario = new Cliente();
            //usuario.IDcliente = ValidarUsuario();

            bool salir = false;

            Console.WriteLine("Por favor, ingrese su número de cliente.");
            var ingreso = Convert.ToInt32(Console.ReadLine());

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
                        SolicitarServicio(ingreso);
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

        private static void SolicitarServicio(int idcliente)
        {
            var solicitud = SolicitudServicio.IngresarSolicitud(idcliente);
            SolicitudServicio.AgregarSolicitud(solicitud);
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
                    solicitud.MostrarEstadoSolicitud();
                    flag = true;
                }
                else if (key.Key == ConsoleKey.N)
                {
                    Console.WriteLine("Operación cancelada." + Environment.NewLine);
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
                    cuenta.MostrarEstadoCuenta();
                    flag = true;
                }
                else if (key.Key == ConsoleKey.N)
                {
                    Console.WriteLine("Operación cancelada." + Environment.NewLine);
                    flag = true;
                }
                else
                {
                    Console.WriteLine("Usted ingresó un valor incorrecto, intente de nuevo." + Environment.NewLine);
                }

            } while (flag == false);
        }

        private static int ValidarUsuario()
        {
            Console.WriteLine("Bienvenido al Portal de Clientes Corporativos." + Environment.NewLine);
            bool flag = false;
            int idcliente = 0;

            do
            {
                Console.WriteLine("Por favor, ingrese su número de cliente.");
                var ingreso = Console.ReadLine();

                if (!int.TryParse(ingreso, out var salida))
                {
                    Console.WriteLine("No ha ingresado un número de cliente válido. Intente nuevamente." + Environment.NewLine);
                    break;
                }

                bool existe = Cliente.ExisteID(salida);

                if (!existe)
                {
                    Console.WriteLine("El ID ingresado no pertenece a ningún cliente registrado. Intente nuevamente." + Environment.NewLine);
                    break;
                }

                if (existe)
                {
                    flag = true;
                }

                idcliente = salida;

            } while (!flag);

            return idcliente;
        }
    }
}
