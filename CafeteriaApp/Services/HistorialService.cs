using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CafeteriaApp.Domain.Models;
using CafeteriaApp.Infrastructure.Persistence;
using CafeteriaApp.Infrastructure.Logging;

namespace CafeteriaApp.Services
{
    public class HistorialService
    {
        private readonly JsonRepository _repository;
        private readonly FileLogger _logger;
        private readonly List<Pedido> _pedidosMemoria;

        public HistorialService(JsonRepository repository, FileLogger logger)
        {
            _repository = repository;
            _logger = logger;
            _pedidosMemoria = new List<Pedido>();
        }

        public void GuardarPedido(Pedido pedido)
        {
            _pedidosMemoria.Add(pedido);
            _repository.Guardar(pedido);
            _logger.LogOperacion("Guardar Pedido", $"ID: {pedido.Id}");
        }

        public List<Pedido> ObtenerPedidosDelDia()
        {
            var hoy = DateTime.Today;
            return _repository.ObtenerTodos()
                .Where(p => p.FechaCreacion.Date == hoy)
                .OrderByDescending(p => p.FechaCreacion)
                .ToList();
        }

        public List<Pedido> ObtenerTodosPedidos()
        {
            return _repository.ObtenerTodos()
                .OrderByDescending(p => p.FechaCreacion)
                .ToList();
        }

        public Pedido ObtenerPedidoPorId(string id)
        {
            return _repository.ObtenerPorId(id);
        }

        public decimal ObtenerTotalVentasDelDia()
        {
            return ObtenerPedidosDelDia()
                .Where(p => p.Estado == EstadoPedido.Pagado)
                .Sum(p => p.Total);
        }

        public Dictionary<string, int> ObtenerProductosMasVendidos()
        {
            var productos = new Dictionary<string, int>();

            foreach (var pedido in ObtenerPedidosDelDia().Where(p => p.Estado == EstadoPedido.Pagado))
            {
                foreach (var linea in pedido.Lineas)
                {
                    if (productos.ContainsKey(linea.Producto.Nombre))
                    {
                        productos[linea.Producto.Nombre] += linea.Cantidad;
                    }
                    else
                    {
                        productos[linea.Producto.Nombre] = linea.Cantidad;
                    }
                }
            }

            return productos;
        }
    }
}