using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CafeteriaApp.Domain.Interfaces;
using CafeteriaApp.Infrastructure.Pagos;
using CafeteriaApp.Infrastructure.Logging;

namespace CafeteriaApp.Services
{
    public class PagoService
    {
        private readonly Dictionary<string, IEstrategiaPago> _estrategias;
        private readonly FileLogger _logger;

        public PagoService(FileLogger logger)
        {
            _logger = logger;
            _estrategias = new Dictionary<string, IEstrategiaPago>
            {
                { "efectivo", new PagoEfectivo() },
                { "tarjeta", new PagoTarjeta() }
            };
        }

        public bool ProcesarPago(decimal monto, string metodoPago)
        {
            var metodoLower = metodoPago.ToLower();

            if (!_estrategias.ContainsKey(metodoLower))
            {
                _logger.LogError($"Método de pago no válido: {metodoPago}");
                return false;
            }

            var estrategia = _estrategias[metodoLower];
            bool resultado = estrategia.ProcesarPago(monto, out string mensaje);

            Console.WriteLine(mensaje);

            if (resultado)
            {
                _logger.LogOperacion("Pago Exitoso", $"Método: {metodoPago}, Monto: ${monto:F2}");
            }
            else
            {
                _logger.LogError($"Pago Fallido: {mensaje}");
            }

            return resultado;
        }

        public List<string> ObtenerMetodosPago()
        {
            return new List<string>(_estrategias.Keys);
        }
    }
}
