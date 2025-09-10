using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CafeteriaApp.Domain.Interfaces;

namespace CafeteriaApp.Infrastructure.Pagos
{
    public class PagoTarjeta : IEstrategiaPago
    {
        public string NombreMetodo => "Tarjeta";
        private string ultimosDigitos;

        public bool ProcesarPago(decimal monto, out string mensaje)
        {
            Console.Write("Ingrese los últimos 4 dígitos de la tarjeta: ");
            ultimosDigitos = Console.ReadLine();

            if (!string.IsNullOrEmpty(ultimosDigitos) && ultimosDigitos.Length == 4)
            {
                Console.WriteLine("Procesando pago con tarjeta...");
                System.Threading.Thread.Sleep(1500); // Simular procesamiento

                // Simulación: 95% de éxito
                Random rand = new Random();
                if (rand.Next(100) < 95)
                {
                    mensaje = $"Pago aprobado. Tarjeta terminada en {ultimosDigitos}";
                    return true;
                }
                else
                {
                    mensaje = "Pago rechazado por el banco";
                    return false;
                }
            }

            mensaje = "Información de tarjeta inválida";
            return false;
        }

        public bool RequiereInformacionAdicional() => true;

        public Dictionary<string, string> ObtenerInformacionPago()
        {
            return new Dictionary<string, string>
            {
                { "UltimosDigitos", ultimosDigitos },
                { "TipoTarjeta", "Crédito/Débito" }
            };
        }
    }
}
