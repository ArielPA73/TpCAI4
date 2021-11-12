using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_CAI
{
    class Tarifa
    {
        public Tarifa()
        {

        }

        public static double CalcularPrecio(SolicitudServicio solicitud)
        {
            double precio = 0;

            if (solicitud.LocalidadOrigen == solicitud.LocalidadDestino)
            {
                precio = CalcularPrecioLocal(solicitud);
            }

            if (solicitud.LocalidadOrigen != solicitud.LocalidadDestino && solicitud.ProvinciaOrigen == solicitud.ProvinciaDestino)
            {
                precio = CalcularPrecioProvincial(solicitud);
            }

            if (solicitud.ProvinciaOrigen != solicitud.ProvinciaDestino && solicitud.EsInternacional == "No")
            {
                precio = CalcularPrecioNacional(solicitud);
            }

            if (solicitud.EsInternacional == "Si")
            {
                precio = CalcularPrecioInternacional(solicitud);
            }

            if (solicitud.EsUrgente == "Si")
            {
                precio = CalcularPrecioEnvíoUrgente(precio);
            }

            if (solicitud.EntregaEnPuerta == "Si")
            {
                precio = CalcularPrecioEnvioRetiroPuerta(precio);
            }

            if (solicitud.RetiroEnPuerta == "Si")
            {
                precio = CalcularPrecioEnvioRetiroPuerta(precio);
            }

            return precio;
        }

        private static double CalcularPrecioLocal(SolicitudServicio solicitud)
        {
            float precio = 0;

            if (solicitud.PesoKg == 0.5)
            {
                precio = 5;
            }

            if (solicitud.PesoKg > 0.5 && solicitud.PesoKg <= 10)
            {
                precio = 10;
            }

            if (solicitud.PesoKg > 10 && solicitud.PesoKg <= 20)
            {
                precio = 20;
            }

            if (solicitud.PesoKg > 20 && solicitud.PesoKg <= 30)
            {
                precio = 30;
            }

            return precio;
        }

        private static double CalcularPrecioProvincial(SolicitudServicio solicitud)
        {
            float precio = 0;

            if (solicitud.PesoKg == 0.5)
            {
                precio = 25;
            }

            if (solicitud.PesoKg > 0.5 && solicitud.PesoKg <= 10)
            {
                precio = 50;
            }

            if (solicitud.PesoKg > 10 && solicitud.PesoKg <= 20)
            {
                precio = 100;
            }

            if (solicitud.PesoKg > 20 && solicitud.PesoKg <= 30)
            {
                precio = 150;
            }

            return precio;
        }

        private static double CalcularPrecioNacional(SolicitudServicio solicitud)
        {
            float precio = 0;

            if (solicitud.PesoKg == 0.5)
            {
                precio = 50;
            }

            if (solicitud.PesoKg > 0.5 && solicitud.PesoKg <= 10)
            {
                precio = 100;
            }

            if (solicitud.PesoKg > 10 && solicitud.PesoKg <= 20)
            {
                precio = 200;
            }

            if (solicitud.PesoKg > 20 && solicitud.PesoKg <= 30)
            {
                precio = 300;
            }

            return precio;
        }

        private static double CalcularPrecioInternacional(SolicitudServicio solicitud)
        {
            float precio = 0;

            if (solicitud.PesoKg == 0.5)
            {
                precio = 100;
            }

            if (solicitud.PesoKg > 0.5 && solicitud.PesoKg <= 10)
            {
                precio = 200;
            }

            if (solicitud.PesoKg > 10 && solicitud.PesoKg <= 20)
            {
                precio = 400;
            }

            if (solicitud.PesoKg > 20 && solicitud.PesoKg <= 30)
            {
                precio = 600;
            }

            return precio;
        }

        private static double CalcularPrecioEnvíoUrgente(double precio)
        {
            precio = precio * 1.10;

            return precio;
        }

        private static double CalcularPrecioEnvioRetiroPuerta(double precio)
        {
            precio = precio + 100;
            return precio;
        }
    }
}
