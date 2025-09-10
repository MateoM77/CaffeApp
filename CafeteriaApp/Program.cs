using System;
using CafeteriaApp.Presentation;
using CafeteriaApp.Services;
using CafeteriaApp.Infrastructure.Logging;
using CafeteriaApp.Infrastructure.Persistence;

namespace CafeteriaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Sistema de Gestión - Cafetería";

            // Inicializar servicios
            var logger = new FileLogger("logs/app.log");
            var repository = new JsonRepository("data/pedidos.json");
            var catalogoService = new CatalogoService();
            var historialService = new HistorialService(repository, logger);
            var pagoService = new PagoService(logger);
            var pedidoService = new PedidoService(catalogoService, pagoService, historialService, logger);

            // Iniciar menú principal
            var menu = new MenuPrincipal(pedidoService, catalogoService, historialService, logger);

            logger.Log("Aplicación iniciada");
            menu.Mostrar();
            logger.Log("Aplicación finalizada");
        }
    }
}