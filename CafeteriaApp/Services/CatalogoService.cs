using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CafeteriaApp.Domain.Models;

namespace CafeteriaApp.Services
{
    public class CatalogoService
    {
        private List<Producto> _productos;

        public CatalogoService()
        {
            InicializarCatalogo();
        }

        private void InicializarCatalogo()
        {
            _productos = new List<Producto>
            {
                // Bebidas
                new Bebida("B001", "Café Americano", "Café negro clásico", 2.50m, "Mediano", false),
                new Bebida("B002", "Cappuccino", "Espresso con leche espumada", 3.50m, "Mediano", false),
                new Bebida("B003", "Latte", "Café con leche suave", 3.75m, "Grande", false),
                new Bebida("B004", "Café Helado", "Café frío con hielo", 3.00m, "Grande", true),
                new Bebida("B005", "Té Verde", "Té verde natural", 2.00m, "Mediano", false),
                new Bebida("B006", "Chocolate Caliente", "Chocolate cremoso", 3.25m, "Mediano", false),
                new Bebida("B007", "Jugo de Naranja", "Jugo natural recién exprimido", 3.50m, "Grande", true),
                new Bebida("B008", "Limonada", "Limonada fresca", 2.75m, "Grande", true),
                
                // Comidas
                new Comida("C001", "Croissant", "Croissant de mantequilla", 2.50m, "Desayuno", false),
                new Comida("C002", "Sandwich de Jamón y Queso", "Sandwich clásico", 5.50m, "Almuerzo", true),
                new Comida("C003", "Ensalada César", "Ensalada fresca con pollo", 7.00m, "Almuerzo", false),
                new Comida("C004", "Muffin de Arándanos", "Muffin casero", 3.00m, "Postre", false),
                new Comida("C005", "Bagel con Queso Crema", "Bagel tostado", 4.00m, "Desayuno", true),
                new Comida("C006", "Brownie", "Brownie de chocolate", 3.50m, "Postre", false),
                new Comida("C007", "Wrap de Pollo", "Wrap saludable", 6.50m, "Almuerzo", false),
                new Comida("C008", "Galletas de Avena", "Pack de 3 galletas", 2.25m, "Snack", false)
            };
        }

        public List<Producto> ObtenerTodos()
        {
            return _productos.Where(p => p.Disponible).ToList();
        }

        public List<Producto> ObtenerPorCategoria(string categoria)
        {
            return _productos.Where(p => p.Disponible && p.ObtenerCategoria() == categoria).ToList();
        }

        public Producto ObtenerPorId(string id)
        {
            return _productos.FirstOrDefault(p => p.Id == id && p.Disponible);
        }

        public List<Bebida> ObtenerBebidas()
        {
            return _productos.OfType<Bebida>().Where(b => b.Disponible).ToList();
        }

        public List<Comida> ObtenerComidas()
        {
            return _productos.OfType<Comida>().Where(c => c.Disponible).ToList();
        }
    }
}
