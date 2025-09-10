using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

using CafeteriaApp.Domain.Interfaces;
using CafeteriaApp.Domain.Models;

namespace CafeteriaApp.Infrastructure.Persistence
{
    public class JsonRepository : IRepositorio<Pedido>
    {
        private readonly string _filePath;
        private List<Pedido> _pedidos;

        public JsonRepository(string filePath)
        {
            _filePath = filePath;
            _pedidos = new List<Pedido>();
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string json = File.ReadAllText(_filePath);
                    _pedidos = JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar datos: {ex.Message}");
                _pedidos = new List<Pedido>();
            }
        }

        private void GuardarDatos()
        {
            try
            {
                string directorio = Path.GetDirectoryName(_filePath);
                if (!Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(_pedidos, options);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar datos: {ex.Message}");
            }
        }

        public void Guardar(Pedido entidad)
        {
            _pedidos.Add(entidad);
            GuardarDatos();
        }

        public Pedido ObtenerPorId(string id)
        {
            return _pedidos.FirstOrDefault(p => p.Id == id);
        }

        public List<Pedido> ObtenerTodos()
        {
            return new List<Pedido>(_pedidos);
        }

        public void Actualizar(Pedido entidad)
        {
            var index = _pedidos.FindIndex(p => p.Id == entidad.Id);
            if (index != -1)
            {
                _pedidos[index] = entidad;
                GuardarDatos();
            }
        }
    }
}
