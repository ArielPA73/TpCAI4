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
        public SolicitudServicio()
        {

        }

        public int IDsolicitud { get; set; } //FALTA
        public int IDcliente { get; set; } //FALTA
        public string Tiposolicitud { get; set; }
        public double PesoKg { get; set; }
        public string DomicilioEntrega { get; set; }
        public string Receptor { get; set; }
        public string EstadoSolicitud { get; set; }
        public float PrecioAFacturar { get; set; }
        public string LocalidadOrigen { get; set; } //HACER
        public string ProvinciaOrigen { get; set; } //HACER
        public string RegiónOrigen { get; set; } //HACER
        public string LocalidadDestino { get; set; } //HACER
        public string ProvinciaDestino { get; set; } //HACER
        public string RegiónDestino { get; set; } //HACER

        public string Titulo
        {
            get
            {
                return $"{IDsolicitud} - {IDcliente}";
            }
        }

        public static SolicitudServicio IngresarSolicitud()
        {
            var solicitud = new SolicitudServicio();
            bool salir = false;

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
                        solicitud.DomicilioEntrega = Ingreso("Ingrese el domicilio de destino.");
                        solicitud.Receptor = Ingreso("Ingrese el nombre y apellido del receptor del envío.");
                        solicitud.PrecioAFacturar = CalcularPrecio();
                        solicitud.Tiposolicitud = "Correspondencia";
                        solicitud.PesoKg = 0.5;
                        solicitud.EstadoSolicitud = "Recibida";
                        salir = true;
                        break;

                    case "2":
                        solicitud.DomicilioEntrega = Ingreso("Ingrese el domicilio de destino.");
                        solicitud.Receptor = Ingreso("Ingrese el nombre y apellido del receptor del envío.");
                        solicitud.PrecioAFacturar = CalcularPrecio();
                        solicitud.Tiposolicitud = "Encomienda";
                        solicitud.EstadoSolicitud = "Recibida";
                        bool flag = false;
                        do
                        {
                            Console.WriteLine("Ingrese el peso de la encomienda (en Kg).");
                            var ingreso = Console.ReadLine();

                            if (!double.TryParse(ingreso, out var pesoEncomienda))
                            {
                                Console.WriteLine("No ha ingresado un peso válido. Por favor, intente nuevamente.");
                                continue;
                            }

                            if (pesoEncomienda < 0.5)
                            {
                                Console.WriteLine("El peso de la encomienda no puede ser menor a 0,5Kg");
                                continue;
                            }

                            solicitud.PesoKg = pesoEncomienda;

                        } while (!flag);

                        solicitud.PrecioAFacturar = CalcularPrecio();
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



            return solicitud;
        }

        public void ConsultaEstado()
        {
            //HACER
            throw new NotImplementedException();
        }

        public static SolicitudServicio SeleccionarSolicitud()
        {
            //HACER: pedir datos de la solicitud, buscarla en el archivo y devolver
            throw new NotImplementedException();
        }

        public static void MostrarEstado(SolicitudServicio solicitud)
        {
            //HACER: ver sintaxis en el final de la clase 5.2
            throw new NotImplementedException();
        }

        public void MostrarSolicitud()
        {
            //HACER: ver sintaxis en el final de la clase 5.2
            throw new NotImplementedException();
        }

        private static float CalcularPrecio()
        {
            //HACER
            float precio = 0;
            return precio;
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
                    Console.WriteLine("Debe ingresar un valor.");
                    continue;
                }
                else
                {
                    flag = true;
                }

            } while (!flag);

            return ingreso;
        }
    }


}
