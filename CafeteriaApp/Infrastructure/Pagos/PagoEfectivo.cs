using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CafeteriaApp.Domain.Interfaces;

namespace CafeteriaApp.Infrastructure.Pagos
{
    public class PagoEfectivo : IEstrategiaPago
    {
        public string NombreMetodo => "Efectivo";
        private decimal montoRecibido;

        public bool ProcesarPago(decimal monto, out string mensaje)
        {
            Console.Write($"Total a pagar: ${monto:F2}. Ingrese monto recibido: $");

            if (decimal.TryParse(Console.ReadLine(), out montoRecibido))
            {
                if (montoRecibido >= monto)
                {
                    decimal cambio = montoRecibido - monto;
                    mensaje = $"Pago exitoso. Cambio: ${cambio:F2}";
                    return true;
                }
                else
                {
                    mensaje = $"Monto insuficiente. Faltan: ${(monto - montoRecibido):F2}";
                    return false;
                }
            }

            mensaje = "Monto inválido";
            return false;
        }

        public bool RequiereInformacionAdicional() => false;

        public Dictionary<string, string> ObtenerInformacionPago()
        {
            return new Dictionary<string, string>
            {
                { "MontoRecibido", montoRecibido.ToString("F2") }
            };
        }
    }
}
