using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Domain.Models
{
    public class Comida : Producto
    {
        public string TipoComida { get; set; } // Desayuno, Almuerzo, Postre, Snack
        public bool RequiereCalentar { get; set; }

        public Comida(string id, string nombre, string descripcion, decimal precio, string tipoComida, bool requiereCalentar)
            : base(id, nombre, descripcion, precio)
        {
            TipoComida = tipoComida;
            RequiereCalentar = requiereCalentar;
        }

        public override string ObtenerCategoria()
        {
            return "Comida";
        }

        public override string ToString()
        {
            return $"{base.ToString()} - {TipoComida}";
        }
    }
}
