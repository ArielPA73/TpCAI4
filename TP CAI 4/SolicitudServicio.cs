using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_CAI
{
    class SolicitudServicio
    {
        const string nombreArchivo = "RegistroSolicitudes.txt";
        public SolicitudServicio()
        {
            if (File.Exists(nombreArchivo))
            {
                using (var reader = new StreamReader(nombreArchivo))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var solicitud = new SolicitudServicio(linea);
                        RegistroSolicitudes.Add(solicitud.IDsolicitud, solicitud);
                    }
                }
            }
        }

        public SolicitudServicio(string linea)
        {
            var datos = linea.Split(';');
            IDsolicitud = int.Parse(datos[0]);
            IDcliente = int.Parse(datos[1]);
            Tiposolicitud = datos[2];
            PesoKg = double.Parse(datos[3]);
            DomicilioEntrega = datos[4];
            Receptor = datos[5];
            EstadoSolicitud = datos[6];
            PrecioAFacturar = float.Parse(datos[7]);
            LocalidadOrigen = datos[8];
            ProvinciaOrigen = datos[9];
            LocalidadDestino = datos[10];
            ProvinciaDestino = datos[11];
            PaisDestino = datos[12];
            EsUrgente = datos[13];
            RetiroEnPuerta = datos[14];
            EntregaEnPuerta = datos[15];
            EsInternacional = datos[16];
        }

        public int IDsolicitud { get; set; }
        public int IDcliente { get; set; }
        public string Tiposolicitud { get; set; }
        public double PesoKg { get; set; }
        public string DomicilioEntrega { get; set; }
        public string Receptor { get; set; }
        public string EstadoSolicitud { get; set; }
        public double PrecioAFacturar { get; set; }
        public string LocalidadOrigen { get; set; }
        public string ProvinciaOrigen { get; set; }
        public string LocalidadDestino { get; set; }
        public string ProvinciaDestino { get; set; }
        public string PaisDestino { get; set; }
        public string EsUrgente { get; set; }
        public string RetiroEnPuerta { get; set; }
        public string EntregaEnPuerta { get; set; }
        public string EsInternacional { get; set; }

        private static readonly Dictionary<int, SolicitudServicio> RegistroSolicitudes = new Dictionary<int, SolicitudServicio>();

        public string Titulo
        {
            get
            {
                return $"{IDsolicitud} - {IDcliente}";
            }
        }

        public string ObtenerLineaDatos()
        {
            return $"{IDsolicitud};{IDcliente};{Tiposolicitud};{PesoKg};{DomicilioEntrega};{Receptor};{EstadoSolicitud};{PrecioAFacturar};{LocalidadOrigen};{ProvinciaOrigen};{LocalidadDestino};{ProvinciaDestino};{PaisDestino};{EsUrgente};{RetiroEnPuerta};{EntregaEnPuerta};{EsInternacional}";
        }

        public static SolicitudServicio IngresarSolicitud(int idcliente)
        {
            var solicitud = new SolicitudServicio();
            bool salir = false;
            solicitud.IDcliente = idcliente;
            //solicitud.IDsolicitud = GenerarIDsolicitud();
            solicitud.IDsolicitud = 1224;


            do
            {
                Console.WriteLine("Nueva solicitud de servicio." + Environment.NewLine);
                Console.WriteLine("Seleccione el tipo de servicio:");
                Console.WriteLine("1- Correspondencia (hasta 500 gramos).");
                Console.WriteLine("2- Encomienda.");
                Console.WriteLine("9- Volver al menú principal.");

                Console.WriteLine(Environment.NewLine + "Ingrese una opción y presione [Enter]");
                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        solicitud.Tiposolicitud = "Correspondencia";
                        solicitud.PesoKg = 0.5;
                        salir = true;
                        break;

                    case "2":
                        solicitud.Tiposolicitud = "Encomienda";
                        bool flag = false;
                        do
                        {
                            Console.WriteLine("Ingrese el peso de la encomienda (en Kg).");
                            var ingreso = Console.ReadLine();

                            if (!double.TryParse(ingreso, out var pesoEncomienda))
                            {
                                Console.WriteLine("No ha ingresado un peso válido. Por favor, intente nuevamente." + Environment.NewLine);
                                continue;
                            }

                            if (pesoEncomienda < 0.5)
                            {
                                Console.WriteLine("El peso de la encomienda no puede ser menor a 0.5Kg." + Environment.NewLine);
                                continue;
                            }

                            if (pesoEncomienda > 30)
                            {
                                Console.WriteLine("El peso de la encomienda no puede ser mayor a 30Kg." + Environment.NewLine);
                                continue;
                            }

                            solicitud.PesoKg = pesoEncomienda;

                        } while (flag);

                        salir = true;
                        break;

                    case "9":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine(Environment.NewLine + "No ha ingresado una opción del menú. Por favor, intente de nuevo." + Environment.NewLine);
                        break;
                }
            } while (!salir);

            solicitud.LocalidadOrigen = Ingreso("Ingrese la localidad de origen del envío.");
            solicitud.ProvinciaOrigen = Ingreso("Ingrese la provincia de origen del envío.");
            solicitud.DomicilioEntrega = Ingreso("Ingrese el domicilio de destino.");
            solicitud.LocalidadDestino = Ingreso("Ingrese la localidad de destino.");
            solicitud.EsInternacional = TarifaDiferencial("¿El envío es internacional? S/N");

            if (solicitud.EsInternacional == "Si")
            {
                solicitud.PaisDestino = Ingreso("Ingrese el país de destino.");
                solicitud.ProvinciaDestino = null;
            }
            else
            {
                solicitud.ProvinciaDestino = Ingreso("Ingrese la provincia de destino.");
                solicitud.PaisDestino = "Argentina";
            }

            solicitud.Receptor = Ingreso("Ingrese el nombre y apellido del receptor del envío.");
            solicitud.EsUrgente = TarifaDiferencial("¿Desea que el envío sea con servicio urgente? S/N (El mismo cuenta con una tarifa adicional.)");
            solicitud.RetiroEnPuerta = TarifaDiferencial("¿Desea que el servicio incluya retiro en puerta? S/N (El mismo cuenta con una tarifa adicional).");
            solicitud.EntregaEnPuerta = TarifaDiferencial("¿Desea que el servicio incluya entrega en puerta? S/N (El mismo cuenta con una tarifa adicional).");
            solicitud.PrecioAFacturar = Tarifa.CalcularPrecio(solicitud);
            solicitud.EstadoSolicitud = "Recibida";

            GrabarSolicitudes();

            //var idcliente = solicitud.IDcliente;
            //var cuenta = Cliente.SeleccionarCliente(idcliente);
            //var idcuenta = cuenta.IDcuenta;
            //var Idcuenta = CuentaCorriente.SeleccionarCuentaDebito(idcuenta);
            //var monto = solicitud.PrecioAFacturar;
            //Idcuenta.Modificar(monto);

            return solicitud;
        }

        public static SolicitudServicio SeleccionarSolicitud()
        {
            var modelo = SolicitudServicio.CrearModeloBusqueda();

            foreach (var solicitud in RegistroSolicitudes.Values)
            {
                if (solicitud.CoincideCon(modelo))
                {
                    return solicitud;
                }
            }

            Console.WriteLine("No se ha encontrado una solicitud que coincida con los criterios indicados." + Environment.NewLine);
            return null;
        }

        public void MostrarEstadoSolicitud()
        {
            Console.WriteLine($"ID solicitud: {IDsolicitud}");
            Console.WriteLine($"Estado: {EstadoSolicitud}");
        }

        public void MostrarSolicitud()
        {
            Console.WriteLine($"ID solicitud: {IDsolicitud}");
            Console.WriteLine($"Cliente: {IDcliente}");
            Console.WriteLine(Tiposolicitud);
            Console.WriteLine($"Domicilio entrega: {DomicilioEntrega}");
            Console.WriteLine($"Localidad destino: {LocalidadDestino}");

            if (EsInternacional == "No")
            {
                Console.WriteLine($"Provincia destino: {ProvinciaDestino}");
            }

            if (EsInternacional == "Si")
            {
                Console.WriteLine($"País destino: {PaisDestino}");
            }

        }

        private static string Ingreso(string titulo)
        {
            bool flag = false;
            string ingreso;
            do
            {
                Console.WriteLine(titulo);
                ingreso = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("Debe ingresar un valor." + Environment.NewLine);
                    continue;
                }
                else
                {
                    flag = true;
                }

            } while (!flag);

            return ingreso;
        }

        private static string TarifaDiferencial(string titulo)
        {
            bool flag = false;
            string ingreso = "";

            do
            {
                Console.WriteLine(titulo);
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.S)
                {
                    ingreso = "Si";
                    flag = true;
                }
                else if (key.Key == ConsoleKey.N)
                {
                    ingreso = "No";
                    flag = true;
                }
                else
                {
                    Console.WriteLine("Usted ingresó un valor incorrecto, intente de nuevo." + Environment.NewLine);
                }
            } while (!flag);

            return ingreso;
        }

        public static void AgregarSolicitud(SolicitudServicio solicitud)
        {
            RegistroSolicitudes.Add(solicitud.IDsolicitud, solicitud);
        }

        public static SolicitudServicio CrearModeloBusqueda()
        {
            var modelo = new SolicitudServicio();
            bool flag = false;
            do
            {
                Console.WriteLine("Ingrese el número de la solicitud a buscar.");
                var solicitudABuscar = Console.ReadLine();

                if (!int.TryParse(solicitudABuscar, out var salida))
                {
                    Console.WriteLine("Usted ingreso un valor incorrecto. Intente nuevamente." + Environment.NewLine);
                    continue;
                }
                else
                {
                    modelo.IDsolicitud = salida;
                    flag = true;
                }

            } while (flag == false);

            return modelo;
        }

        public bool CoincideCon(SolicitudServicio modelo)
        {
            if (IDsolicitud != modelo.IDsolicitud)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static void GrabarSolicitudes()
        {
            using (var writer = new StreamWriter(nombreArchivo, append: false))
            {
                foreach (var solicitud in RegistroSolicitudes.Values)
                {
                    var linea = solicitud.ObtenerLineaDatos();
                    writer.WriteLine(linea);
                }
            }
        }

        private static int GenerarIDsolicitud()
        {
            Random r = new Random();

            bool flag = true;
            var idsolicitud = 0;

            do
            {
                idsolicitud = r.Next(10000, 99999);
                bool existe = SolicitudServicio.Existe(idsolicitud);

                if (existe)
                {
                    flag = false;
                }
            } while (!flag);

            return idsolicitud;
        }

        public static bool Existe(int idsolicitud)
        {
            return RegistroSolicitudes.ContainsKey(idsolicitud);
        }
    }


}
