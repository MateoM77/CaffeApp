using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Domain.Models
{
    public class LineaPedido
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;

        public LineaPedido(Producto producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;
            PrecioUnitario = producto.Precio;
        }

        public override string ToString()
        {
            return $"{Cantidad}x {Producto.Nombre} @ ${PrecioUnitario:F2} = ${Subtotal:F2}";
        }
    }
}
