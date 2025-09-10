using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Domain.Models
{
    public abstract class Producto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Disponible { get; set; }

        protected Producto(string id, string nombre, string descripcion, decimal precio)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Disponible = true;
        }

        public abstract string ObtenerCategoria();

        public override string ToString()
        {
            return $"{Nombre} - ${Precio:F2}";
        }
    }
}
