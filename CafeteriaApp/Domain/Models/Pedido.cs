using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Domain.Models
{
    public class Pedido
    {
        public string Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaPago { get; set; }
        public List<LineaPedido> Lineas { get; set; }
        public EstadoPedido Estado { get; set; }
        public decimal Subtotal => Lineas.Sum(l => l.Subtotal);
        public decimal Propina { get; set; }
        public decimal Total => Subtotal + Propina;
        public string MetodoPago { get; set; }

        public Pedido()
        {
            Id = GenerarId();
            FechaCreacion = DateTime.Now;
            Lineas = new List<LineaPedido>();
            Estado = EstadoPedido.Abierto;
            Propina = 0;
        }

        private string GenerarId()
        {
            return $"PED-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }

        public void AgregarLinea(Producto producto, int cantidad)
        {
            if (Estado != EstadoPedido.Abierto)
                throw new InvalidOperationException("No se puede modificar un pedido que no está abierto");

            var lineaExistente = Lineas.FirstOrDefault(l => l.Producto.Id == producto.Id);

            if (lineaExistente != null)
            {
                lineaExistente.Cantidad += cantidad;
            }
            else
            {
                Lineas.Add(new LineaPedido(producto, cantidad));
            }
        }

        public void EliminarLinea(string productoId)
        {
            if (Estado != EstadoPedido.Abierto)
                throw new InvalidOperationException("No se puede modificar un pedido que no está abierto");

            Lineas.RemoveAll(l => l.Producto.Id == productoId);
        }

        public void AplicarPropina(decimal porcentaje)
        {
            Propina = Subtotal * (porcentaje / 100);
        }

        public void MarcarComoPagado(string metodoPago)
        {
            Estado = EstadoPedido.Pagado;
            FechaPago = DateTime.Now;
            MetodoPago = metodoPago;
        }
    }
}
