# CafeteriaApp

# CafeteriaApp

Sistema de Gestión para Cafetería desarrollado en C#.

## Descripción

CafeteriaApp es una aplicación de consola para la gestión de pedidos en una cafetería. Permite registrar productos, gestionar pedidos, procesar pagos y generar reportes de ventas y productos más vendidos. Utiliza persistencia en archivos JSON y registro de operaciones en archivos de log.

## Características principales

- Menú interactivo en consola para realizar pedidos.
- Gestión de productos (bebidas, alimentos, etc.).
- Procesamiento de pagos (efectivo y tarjeta) usando el patrón estrategia.
- Registro y consulta de historial de ventas.
- Generación de reportes de ventas diarias y productos más vendidos.
- Propinas configurables en los pedidos.
- Persistencia de datos en archivos JSON (`data/pedidos.json`).
- Logging de operaciones en archivo (`logs/app.log`).

## Requisitos

- .NET 6.0 o superior
- Sistema operativo Windows, Linux o macOS

## Instalación

1. Clona el repositorio:
   ```bash
   git clone https://github.com/MateoM77/CaffeApp.git
   ```
2. Accede al directorio del proyecto:
   ```bash
   cd CaffeApp/CafeteriaApp
   ```
3. Restaura los paquetes y compila:
   ```bash
   dotnet restore
   dotnet build
   ```

## Ejecución

Desde la terminal, ejecuta:
```bash
dotnet run
```
Se mostrará el menú principal del sistema de cafetería.

## Uso

- Agrega productos al pedido seleccionando del catálogo.
- Aplica propina si lo deseas.
- Elige método de pago (efectivo o tarjeta).
- Consulta el historial de ventas y productos más vendidos.

## Estructura del proyecto

- `CafeteriaApp/Domain`: Modelos y interfaces base.
- `CafeteriaApp/Services`: Lógica de negocio (PedidoService, HistorialService, PagoService).
- `CafeteriaApp/Infrastructure`: Persistencia y procesamiento de pagos.
- `CafeteriaApp/Presentation`: Menú de usuario y presentación.

## Contribuciones

Se aceptan sugerencias y mejoras. Puedes abrir issues o pull requests.

## Licencia

Este proyecto no especifica licencia. Contacta al autor para más detalles.

## Autor

[MateoM77](https://github.com/MateoM77)
