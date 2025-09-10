using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CafeteriaApp.Services;
using CafeteriaApp.Infrastructure.Logging;
using CafeteriaApp.Domain.Models;

namespace CafeteriaApp.Presentation
{
    public class MenuPrincipal
    {
        private readonly PedidoService _pedidoService;
        private readonly CatalogoService _catalogoService;
        private readonly HistorialService _historialService;
        private readonly FileLogger _logger;
        private bool _continuar = true;

        public MenuPrincipal(PedidoService pedidoService, CatalogoService catalogoService,
                           HistorialService historialService, FileLogger logger)
        {
            _pedidoService = pedidoService;
            _catalogoService = catalogoService;
            _historialService = historialService;
            _logger = logger;
        }

        public void Mostrar()
        {
            while (_continuar)
            {
                try
                {
                    MostrarMenuPrincipal();
                    ProcesarOpcionPrincipal();
                }
                catch (Exception ex)
                {
                    ConsoleHelper.MostrarMensajeError($"Error: {ex.Message}");
                    _logger.LogError("Error en menú principal", ex);
                    ConsoleHelper.PausarConsola();
                }
            }
        }

        private void MostrarMenuPrincipal()
        {
            ConsoleHelper.MostrarTitulo("SISTEMA DE GESTIÓN - CAFETERÍA");

            if (_pedidoService.HayPedidoActivo())
            {
                var pedido = _pedidoService.ObtenerPedidoActual();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  Pedido Activo: {pedido.Id}");
                Console.WriteLine($"  Items: {pedido.Lineas.Count} | Subtotal: ${pedido.Subtotal:F2}");
                Console.ResetColor();
                ConsoleHelper.MostrarSeparador();
            }

            Console.WriteLine("  MENÚ PRINCIPAL");
            ConsoleHelper.MostrarSeparador();
            ConsoleHelper.MostrarOpcion(1, "Gestión de Pedidos");
            ConsoleHelper.MostrarOpcion(2, "Ver Catálogo");
            ConsoleHelper.MostrarOpcion(3, "Historial y Reportes");
            ConsoleHelper.MostrarOpcion(4, "Administración");
            ConsoleHelper.MostrarOpcion(0, "Salir");
            Console.WriteLine();
        }

        private void ProcesarOpcionPrincipal()
        {
            int opcion = ConsoleHelper.LeerOpcion();

            switch (opcion)
            {
                case 1:
                    MostrarMenuPedidos();
                    break;
                case 2:
                    MostrarCatalogo();
                    break;
                case 3:
                    MostrarMenuHistorial();
                    break;
                case 4:
                    MostrarMenuAdministracion();
                    break;
                case 0:
                    if (ConsoleHelper.ConfirmarAccion("¿Está seguro que desea salir?"))
                    {
                        _continuar = false;
                        ConsoleHelper.MostrarMensajeExito("Gracias por usar el sistema. ¡Hasta pronto!");
                    }
                    break;
                default:
                    ConsoleHelper.MostrarMensajeError("Opción no válida");
                    ConsoleHelper.PausarConsola();
                    break;
            }
        }

        private void MostrarMenuPedidos()
        {
            bool continuarSubmenu = true;

            while (continuarSubmenu)
            {
                ConsoleHelper.MostrarTitulo("GESTIÓN DE PEDIDOS");

                if (_pedidoService.HayPedidoActivo())
                {
                    MostrarDetallePedidoActual();
                    ConsoleHelper.MostrarSeparador();
                }

                Console.WriteLine("  OPCIONES DE PEDIDO");
                ConsoleHelper.MostrarSeparador();

                if (!_pedidoService.HayPedidoActivo())
                {
                    ConsoleHelper.MostrarOpcion(1, "Crear Nuevo Pedido");
                }
                else
                {
                    ConsoleHelper.MostrarOpcion(2, "Agregar Producto");
                    ConsoleHelper.MostrarOpcion(3, "Eliminar Producto");
                    ConsoleHelper.MostrarOpcion(4, "Aplicar Propina");
                    ConsoleHelper.MostrarOpcion(5, "Procesar Pago");
                    ConsoleHelper.MostrarOpcion(6, "Cancelar Pedido");
                }

                ConsoleHelper.MostrarOpcion(0, "Volver al Menú Principal");
                Console.WriteLine();

                int opcion = ConsoleHelper.LeerOpcion();

                try
                {
                    switch (opcion)
                    {
                        case 1:
                            if (!_pedidoService.HayPedidoActivo())
                                CrearNuevoPedido();
                            break;
                        case 2:
                            if (_pedidoService.HayPedidoActivo())
                                AgregarProductoAPedido();
                            break;
                        case 3:
                            if (_pedidoService.HayPedidoActivo())
                                EliminarProductoDePedido();
                            break;
                        case 4:
                            if (_pedidoService.HayPedidoActivo())
                                AplicarPropina();
                            break;
                        case 5:
                            if (_pedidoService.HayPedidoActivo())
                                ProcesarPago();
                            break;
                        case 6:
                            if (_pedidoService.HayPedidoActivo())
                                CancelarPedido();
                            break;
                        case 0:
                            continuarSubmenu = false;
                            break;
                        default:
                            ConsoleHelper.MostrarMensajeError("Opción no válida");
                            ConsoleHelper.PausarConsola();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ConsoleHelper.MostrarMensajeError($"Error: {ex.Message}");
                    _logger.LogError("Error en menú de pedidos", ex);
                    ConsoleHelper.PausarConsola();
                }
            }
        }

        private void MostrarDetallePedidoActual()
        {
            var pedido = _pedidoService.ObtenerPedidoActual();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  PEDIDO ACTUAL: {pedido.Id}");
            Console.ResetColor();
            Console.WriteLine($"  Estado: {pedido.Estado}");
            Console.WriteLine($"  Fecha: {pedido.FechaCreacion:dd/MM/yyyy HH:mm}");
            Console.WriteLine();

            if (pedido.Lineas.Any())
            {
                Console.WriteLine("  DETALLE:");
                foreach (var linea in pedido.Lineas)
                {
                    Console.WriteLine($"    • {linea}");
                }
                Console.WriteLine();
                Console.WriteLine($"  Subtotal: ${pedido.Subtotal:F2}");
                if (pedido.Propina > 0)
                {
                    Console.WriteLine($"  Propina: ${pedido.Propina:F2}");
                    Console.WriteLine($"  TOTAL: ${pedido.Total:F2}");
                }
            }
            else
            {
                ConsoleHelper.MostrarMensajeAdvertencia("  El pedido está vacío");
            }
        }

        private void CrearNuevoPedido()
        {
            var pedido = _pedidoService.CrearNuevoPedido();
            ConsoleHelper.MostrarMensajeExito($"Pedido creado exitosamente: {pedido.Id}");
            ConsoleHelper.PausarConsola();
        }

        private void AgregarProductoAPedido()
        {
            ConsoleHelper.MostrarTitulo("AGREGAR PRODUCTO AL PEDIDO");

            // Mostrar catálogo
            var productos = _catalogoService.ObtenerTodos();
            Console.WriteLine("  PRODUCTOS DISPONIBLES:");
            ConsoleHelper.MostrarSeparador();

            foreach (var producto in productos)
            {
                Console.WriteLine($"  [{producto.Id}] {producto} - {producto.Descripcion}");
            }

            ConsoleHelper.MostrarSeparador();

            string productoId = ConsoleHelper.LeerTexto("Ingrese el ID del producto: ");

            if (string.IsNullOrWhiteSpace(productoId))
            {
                ConsoleHelper.MostrarMensajeError("ID de producto no válido");
                ConsoleHelper.PausarConsola();
                return;
            }

            int cantidad = ConsoleHelper.LeerOpcion("Ingrese la cantidad: ");

            if (cantidad <= 0)
            {
                ConsoleHelper.MostrarMensajeError("La cantidad debe ser mayor a 0");
                ConsoleHelper.PausarConsola();
                return;
            }

            try
            {
                _pedidoService.AgregarProducto(productoId.ToUpper(), cantidad);
                ConsoleHelper.MostrarMensajeExito("Producto agregado exitosamente");
            }
            catch (Exception ex)
            {
                ConsoleHelper.MostrarMensajeError(ex.Message);
            }

            ConsoleHelper.PausarConsola();
        }

        private void EliminarProductoDePedido()
        {
            ConsoleHelper.MostrarTitulo("ELIMINAR PRODUCTO DEL PEDIDO");

            var pedido = _pedidoService.ObtenerPedidoActual();

            if (!pedido.Lineas.Any())
            {
                ConsoleHelper.MostrarMensajeAdvertencia("El pedido está vacío");
                ConsoleHelper.PausarConsola();
                return;
            }

            Console.WriteLine("  PRODUCTOS EN EL PEDIDO:");
            ConsoleHelper.MostrarSeparador();

            foreach (var linea in pedido.Lineas)
            {
                Console.WriteLine($"  [{linea.Producto.Id}] {linea}");
            }

            ConsoleHelper.MostrarSeparador();

            string productoId = ConsoleHelper.LeerTexto("Ingrese el ID del producto a eliminar: ");

            if (string.IsNullOrWhiteSpace(productoId))
            {
                ConsoleHelper.MostrarMensajeError("ID de producto no válido");
                ConsoleHelper.PausarConsola();
                return;
            }

            try
            {
                _pedidoService.EliminarProducto(productoId.ToUpper());
                ConsoleHelper.MostrarMensajeExito("Producto eliminado exitosamente");
            }
            catch (Exception ex)
            {
                ConsoleHelper.MostrarMensajeError(ex.Message);
            }

            ConsoleHelper.PausarConsola();
        }

        private void AplicarPropina()
        {
            ConsoleHelper.MostrarTitulo("APLICAR PROPINA");

            Console.WriteLine("  Opciones de propina:");
            ConsoleHelper.MostrarOpcion(1, "Sin propina (0%)");
            ConsoleHelper.MostrarOpcion(2, "10%");
            ConsoleHelper.MostrarOpcion(3, "15%");
            ConsoleHelper.MostrarOpcion(4, "20%");
            ConsoleHelper.MostrarOpcion(5, "Personalizada");

            int opcion = ConsoleHelper.LeerOpcion();
            decimal porcentaje = 0;

            switch (opcion)
            {
                case 1: porcentaje = 0; break;
                case 2: porcentaje = 10; break;
                case 3: porcentaje = 15; break;
                case 4: porcentaje = 20; break;
                case 5:
                    porcentaje = ConsoleHelper.LeerOpcion("Ingrese el porcentaje de propina: ");
                    break;
                default:
                    ConsoleHelper.MostrarMensajeError("Opción no válida");
                    ConsoleHelper.PausarConsola();
                    return;
            }

            _pedidoService.AplicarPropina(porcentaje);
            ConsoleHelper.MostrarMensajeExito($"Propina del {porcentaje}% aplicada");
            ConsoleHelper.PausarConsola();
        }

        private void ProcesarPago()
        {
            var pedido = _pedidoService.ObtenerPedidoActual();

            if (!pedido.Lineas.Any())
            {
                ConsoleHelper.MostrarMensajeError("No se puede procesar el pago de un pedido vacío");
                ConsoleHelper.PausarConsola();
                return;
            }

            ConsoleHelper.MostrarTitulo("PROCESAR PAGO");

            Console.WriteLine($"  Total a pagar: ${pedido.Total:F2}");
            ConsoleHelper.MostrarSeparador();

            Console.WriteLine("  Seleccione método de pago:");
            ConsoleHelper.MostrarOpcion(1, "Efectivo");
            ConsoleHelper.MostrarOpcion(2, "Tarjeta");
            ConsoleHelper.MostrarOpcion(0, "Cancelar");

            int opcion = ConsoleHelper.LeerOpcion();
            string metodoPago = "";

            switch (opcion)
            {
                case 1: metodoPago = "efectivo"; break;
                case 2: metodoPago = "tarjeta"; break;
                case 0: return;
                default:
                    ConsoleHelper.MostrarMensajeError("Opción no válida");
                    ConsoleHelper.PausarConsola();
                    return;
            }

            if (_pedidoService.ProcesarPago(metodoPago))
            {
                ConsoleHelper.MostrarMensajeExito("Pago procesado exitosamente");
                Console.WriteLine();
                Console.WriteLine("¿Desea imprimir el recibo? (S/N)");
                if (ConsoleHelper.ConfirmarAccion(""))
                {
                    ImprimirRecibo(pedido);
                }
            }
            else
            {
                ConsoleHelper.MostrarMensajeError("El pago no pudo ser procesado");
            }

            ConsoleHelper.PausarConsola();
        }

        private void ImprimirRecibo(Pedido pedido)
        {
            ConsoleHelper.MostrarTitulo("RECIBO DE COMPRA");
            Console.WriteLine($"  Cafetería - Recibo #{pedido.Id}");
            Console.WriteLine($"  Fecha: {pedido.FechaPago:dd/MM/yyyy HH:mm}");
            ConsoleHelper.MostrarSeparador();

            foreach (var linea in pedido.Lineas)
            {
                Console.WriteLine($"  {linea}");
            }

            ConsoleHelper.MostrarSeparador();
            Console.WriteLine($"  Subtotal: ${pedido.Subtotal:F2}");
            if (pedido.Propina > 0)
                Console.WriteLine($"  Propina: ${pedido.Propina:F2}");
            Console.WriteLine($"  TOTAL: ${pedido.Total:F2}");
            Console.WriteLine($"  Método de pago: {pedido.MetodoPago}");
            ConsoleHelper.MostrarSeparador();
            Console.WriteLine("  ¡Gracias por su compra!");
        }

        private void CancelarPedido()
        {
            if (ConsoleHelper.ConfirmarAccion("¿Está seguro que desea cancelar el pedido actual?"))
            {
                _pedidoService.CancelarPedido();
                ConsoleHelper.MostrarMensajeExito("Pedido cancelado");
            }
            ConsoleHelper.PausarConsola();
        }

        private void MostrarCatalogo()
        {
            ConsoleHelper.MostrarTitulo("CATÁLOGO DE PRODUCTOS");

            Console.WriteLine("  BEBIDAS:");
            ConsoleHelper.MostrarSeparador();
            foreach (var bebida in _catalogoService.ObtenerBebidas())
            {
                Console.WriteLine($"  [{bebida.Id}] {bebida}");
                Console.WriteLine($"       {bebida.Descripcion}");
            }

            Console.WriteLine();
            Console.WriteLine("  COMIDAS:");
            ConsoleHelper.MostrarSeparador();
            foreach (var comida in _catalogoService.ObtenerComidas())
            {
                Console.WriteLine($"  [{comida.Id}] {comida}");
                Console.WriteLine($"       {comida.Descripcion}");
            }

            ConsoleHelper.PausarConsola();
        }

        private void MostrarMenuHistorial()
        {
            ConsoleHelper.MostrarTitulo("HISTORIAL Y REPORTES");

            ConsoleHelper.MostrarOpcion(1, "Pedidos del día");
            ConsoleHelper.MostrarOpcion(2, "Todos los pedidos");
            ConsoleHelper.MostrarOpcion(3, "Total de ventas del día");
            ConsoleHelper.MostrarOpcion(4, "Productos más vendidos");
            ConsoleHelper.MostrarOpcion(0, "Volver");

            int opcion = ConsoleHelper.LeerOpcion();

            switch (opcion)
            {
                case 1:
                    MostrarPedidosDelDia();
                    break;
                case 2:
                    MostrarTodosPedidos();
                    break;
                case 3:
                    MostrarTotalVentas();
                    break;
                case 4:
                    MostrarProductosMasVendidos();
                    break;
            }
        }

        private void MostrarPedidosDelDia()
        {
            ConsoleHelper.MostrarTitulo("PEDIDOS DEL DÍA");

            var pedidos = _historialService.ObtenerPedidosDelDia();

            if (!pedidos.Any())
            {
                ConsoleHelper.MostrarMensajeAdvertencia("No hay pedidos registrados hoy");
            }
            else
            {
                foreach (var pedido in pedidos)
                {
                    Console.WriteLine($"  {pedido.Id} - {pedido.FechaCreacion:HH:mm} - ${pedido.Total:F2} - {pedido.Estado}");
                }

                Console.WriteLine();
                Console.WriteLine($"  Total de pedidos: {pedidos.Count}");
            }

            ConsoleHelper.PausarConsola();
        }

        private void MostrarTodosPedidos()
        {
            ConsoleHelper.MostrarTitulo("TODOS LOS PEDIDOS");

            var pedidos = _historialService.ObtenerTodosPedidos();

            if (!pedidos.Any())
            {
                ConsoleHelper.MostrarMensajeAdvertencia("No hay pedidos registrados");
            }
            else
            {
                foreach (var pedido in pedidos.Take(20)) // Mostrar últimos 20
                {
                    Console.WriteLine($"  {pedido.Id} - {pedido.FechaCreacion:dd/MM/yyyy HH:mm} - ${pedido.Total:F2} - {pedido.Estado}");
                }

                if (pedidos.Count > 20)
                {
                    Console.WriteLine($"  ... y {pedidos.Count - 20} pedidos más");
                }
            }

            ConsoleHelper.PausarConsola();
        }

        private void MostrarTotalVentas()
        {
            ConsoleHelper.MostrarTitulo("TOTAL DE VENTAS DEL DÍA");

            var total = _historialService.ObtenerTotalVentasDelDia();
            var pedidosDelDia = _historialService.ObtenerPedidosDelDia();

            Console.WriteLine($"  Total de ventas: ${total:F2}");
            Console.WriteLine($"  Número de pedidos: {pedidosDelDia.Count(p => p.Estado == EstadoPedido.Pagado)}");

            if (pedidosDelDia.Any())
            {
                var promedio = total / pedidosDelDia.Count(p => p.Estado == EstadoPedido.Pagado);
                Console.WriteLine($"  Ticket promedio: ${promedio:F2}");
            }

            ConsoleHelper.PausarConsola();
        }

        private void MostrarProductosMasVendidos()
        {
            ConsoleHelper.MostrarTitulo("PRODUCTOS MÁS VENDIDOS");

            var productos = _historialService.ObtenerProductosMasVendidos();

            if (!productos.Any())
            {
                ConsoleHelper.MostrarMensajeAdvertencia("No hay datos de ventas");
            }
            else
            {
                int posicion = 1;
                foreach (var producto in productos)
                {
                    Console.WriteLine($"  {posicion}. {producto.Key} - {producto.Value} unidades");
                    posicion++;
                }
            }

            ConsoleHelper.PausarConsola();
        }

        private void MostrarMenuAdministracion()
        {
            ConsoleHelper.MostrarTitulo("ADMINISTRACIÓN");

            Console.WriteLine("  Información del Sistema:");
            ConsoleHelper.MostrarSeparador();
            Console.WriteLine("  Versión: 1.0.0");
            Console.WriteLine("  Framework: .NET 8.0");
            Console.WriteLine("  Arquitectura: Consola - POO");
            Console.WriteLine();
            Console.WriteLine("  Rutas de archivos:");
            Console.WriteLine("  - Logs: logs/app.log");
            Console.WriteLine("  - Datos: data/pedidos.json");

            ConsoleHelper.PausarConsola();
        }
    }
}
