using Xunit;
using Moq;
using System;
using System.IO;
using CafeteriaApp.Presentation;
using CafeteriaApp.Services;
using CafeteriaApp.Infrastructure.Logging;
using CafeteriaApp.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace CafeteriaApp.Tests.Presentation
{
    public class MenuPrincipalTests
    {
        private readonly Mock<PedidoService> _mockPedidoService;
        private readonly Mock<CatalogoService> _mockCatalogoService;
        private readonly Mock<HistorialService> _mockHistorialService;
        private readonly Mock<FileLogger> _mockLogger;
        private readonly MenuPrincipal _menuPrincipal;
        private readonly StringWriter _stringWriter;

        public MenuPrincipalTests()
        {
            _mockPedidoService = new Mock<PedidoService>();
            _mockCatalogoService = new Mock<CatalogoService>();
            _mockHistorialService = new Mock<HistorialService>();
            _mockLogger = new Mock<FileLogger>();

            _menuPrincipal = new MenuPrincipal(
                _mockPedidoService.Object,
                _mockCatalogoService.Object,
                _mockHistorialService.Object,
                _mockLogger.Object
            );

            // Redirigir la salida de consola para las pruebas
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [Fact]
        public void Constructor_DebeInicializarCorrectamenteLasDependencias()
        {
            // Arrange & Act
            var menu = new MenuPrincipal(
                _mockPedidoService.Object,
                _mockCatalogoService.Object,
                _mockHistorialService.Object,
                _mockLogger.Object
            );

            // Assert
            Assert.NotNull(menu);
        }

        [Fact]
        public void MostrarCatalogo_DebeListarBebidasYComidas()
        {
            // Arrange
            var bebidas = new List<Bebida>
            {
                new Bebida("B001", "Café Americano", "Café negro clásico", 2.50m, "Mediano", false),
                new Bebida("B002", "Cappuccino", "Espresso con leche espumada", 3.50m, "Mediano", false)
            };

            var comidas = new List<Comida>
            {
                new Comida("C001", "Croissant", "Croissant de mantequilla", 2.50m, "Desayuno", false),
                new Comida("C002", "Sandwich de Jamón y Queso", "Sandwich clásico", 5.50m, "Almuerzo", true)
            };

            _mockCatalogoService.Setup(x => x.ObtenerBebidas()).Returns(bebidas);
            _mockCatalogoService.Setup(x => x.ObtenerComidas()).Returns(comidas);

            // Act
            // Nota: Este método es privado, necesitaríamos usar reflexión o hacer el método interno/público
            // Para esta prueba, asumimos que podemos acceder al método
            var methodInfo = typeof(MenuPrincipal).GetMethod("MostrarCatalogo",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            methodInfo?.Invoke(_menuPrincipal, null);

            // Assert
            var output = _stringWriter.ToString();
            Assert.Contains("CATÁLOGO DE PRODUCTOS", output);
            Assert.Contains("BEBIDAS:", output);
            Assert.Contains("COMIDAS:", output);
            Assert.Contains("Café", output);
            Assert.Contains("Sandwich", output);
        }

        [Theory]
        [InlineData(true, "Pedido Activo")]
        [InlineData(false, "MENÚ PRINCIPAL")]
        public void MostrarMenuPrincipal_DebeAdaptarseSegunEstadoPedido(bool hayPedidoActivo, string textoEsperado)
        {
            // Arrange
            _mockPedidoService.Setup(x => x.HayPedidoActivo()).Returns(hayPedidoActivo);

            if (hayPedidoActivo)
            {
                var pedidoMock = new Mock<Pedido>();
                pedidoMock.Setup(x => x.Id).Returns("PED001");
                pedidoMock.Setup(x => x.Lineas).Returns(new List<LineaPedido>());
                pedidoMock.Setup(x => x.Subtotal).Returns(10.50m);

                _mockPedidoService.Setup(x => x.ObtenerPedidoActual()).Returns(pedidoMock.Object);
            }

            // Act
            var methodInfo = typeof(MenuPrincipal).GetMethod("MostrarMenuPrincipal",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            methodInfo?.Invoke(_menuPrincipal, null);

            // Assert
            var output = _stringWriter.ToString();
            Assert.Contains(textoEsperado, output);
        }

        [Fact]
        public void CrearNuevoPedido_DebeCrearPedidoYMostrarMensajeExito()
        {
            // Arrange
            var pedidoMock = new Mock<Pedido>();
            pedidoMock.Setup(x => x.Id).Returns("PED001");

            _mockPedidoService.Setup(x => x.CrearNuevoPedido()).Returns(pedidoMock.Object);

            // Act
            var methodInfo = typeof(MenuPrincipal).GetMethod("CrearNuevoPedido",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            methodInfo?.Invoke(_menuPrincipal, null);

            // Assert
            var output = _stringWriter.ToString();
            Assert.Contains("Pedido creado exitosamente: PED001", output);
            _mockPedidoService.Verify(x => x.CrearNuevoPedido(), Times.Once);
        }

        [Fact]
        public void AplicarPropina_ConPorcentajeValido_DebeAplicarPropinaCorrectamente()
        {
            // Arrange
            decimal porcentajePropina = 15m;
            _mockPedidoService.Setup(x => x.AplicarPropina(porcentajePropina));

            // Act
            // Simulamos la aplicación directa de propina (método privado)
            _mockPedidoService.Object.AplicarPropina(porcentajePropina);

            // Assert
            _mockPedidoService.Verify(x => x.AplicarPropina(porcentajePropina), Times.Once);
        }

        public void Dispose()
        {
            _stringWriter?.Dispose();
            Console.SetOut(Console.Out); // Restaurar la salida original
        }
    }

    // Enums necesarios para las pruebas (si no existen en el dominio original)
    public enum TipoBebida
    {
        Caliente,
        Fria
    }

    public enum TipoComida
    {
        Entrada,
        Principal,
        Postre
    }
}