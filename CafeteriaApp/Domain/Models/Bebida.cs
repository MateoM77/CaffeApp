using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Domain.Models
{
    public class Bebida : Producto
    {
        public string Tamano { get; set; } // Pequeño, Mediano, Grande
        public bool EsFria { get; set; }

        public Bebida(string id, string nombre, string descripcion, decimal precio, string tamano, bool esFria)
            : base(id, nombre, descripcion, precio)
        {
            Tamano = tamano;
            EsFria = esFria;
        }

        public override string ObtenerCategoria()
        {
            return "Bebida";
        }

        public override string ToString()
        {
            var tipo = EsFria ? "Fría" : "Caliente";
            return $"{base.ToString()} - {Tamano} ({tipo})";
        }
    }
}
