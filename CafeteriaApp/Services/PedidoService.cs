using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CafeteriaApp.Domain.Models;
using CafeteriaApp.Infrastructure.Logging;

namespace CafeteriaApp.Services
{
    public class PedidoService
    {
        private readonly CatalogoService _catalogoService;
        private readonly PagoService _pagoService;
        private readonly HistorialService _historialService;
        private readonly FileLogger _logger;
        private Pedido _pedidoActual;

        public PedidoService(CatalogoService catalogoService, PagoService pagoService,
                            HistorialService historialService, FileLogger logger)
        {
            _catalogoService = catalogoService;
            _pagoService = pagoService;
            _historialService = historialService;
            _logger = logger;
        }

        public Pedido CrearNuevoPedido()
        {
            _pedidoActual = new Pedido();
            _logger.LogOperacion("Crear Pedido", $"ID: {_pedidoActual.Id}");
            return _pedidoActual;
        }

        public Pedido ObtenerPedidoActual()
        {
            return _pedidoActual;
        }

        public bool HayPedidoActivo()
        {
            return _pedidoActual != null && _pedidoActual.Estado == EstadoPedido.Abierto;
        }

        public void AgregarProducto(string productoId, int cantidad)
        {
            if (_pedidoActual == null)
                throw new InvalidOperationException("No hay pedido activo");

            var producto = _catalogoService.ObtenerPorId(productoId);
            if (producto == null)
                throw new ArgumentException($"Producto con ID {productoId} no encontrado");

            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a 0");

            _pedidoActual.AgregarLinea(producto, cantidad);
            _logger.LogOperacion("Agregar Producto", $"Pedido: {_pedidoActual.Id}, Producto: {producto.Nombre}, Cantidad: {cantidad}");
        }

        public void EliminarProducto(string productoId)
        {
            if (_pedidoActual == null)
                throw new InvalidOperationException("No hay pedido activo");

            _pedidoActual.EliminarLinea(productoId);
            _logger.LogOperacion("Eliminar Producto", $"Pedido: {_pedidoActual.Id}, ProductoId: {productoId}");
        }

        public void AplicarPropina(decimal porcentaje)
        {
            if (_pedidoActual == null)
                throw new InvalidOperationException("No hay pedido activo");

            _pedidoActual.AplicarPropina(porcentaje);
            _logger.LogOperacion("Aplicar Propina", $"Pedido: {_pedidoActual.Id}, Porcentaje: {porcentaje}%");
        }

        public bool ProcesarPago(string metodoPago)
        {
            if (_pedidoActual == null || _pedidoActual.Lineas.Count == 0)
                throw new InvalidOperationException("No hay pedido activo o está vacío");

            var resultado = _pagoService.ProcesarPago(_pedidoActual.Total, metodoPago);

            if (resultado)
            {
                _pedidoActual.MarcarComoPagado(metodoPago);
                _historialService.GuardarPedido(_pedidoActual);
                _logger.LogOperacion("Procesar Pago", $"Pedido: {_pedidoActual.Id}, Método: {metodoPago}, Total: ${_pedidoActual.Total:F2}");
                _pedidoActual = null; // Limpiar pedido actual
            }

            return resultado;
        }

        public void CancelarPedido()
        {
            if (_pedidoActual != null)
            {
                _pedidoActual.Estado = EstadoPedido.Cancelado;
                _logger.LogOperacion("Cancelar Pedido", $"ID: {_pedidoActual.Id}");
                _pedidoActual = null;
            }
        }
    }
}
