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

## Evidencias ejecución

<img width="790" height="522" alt="{B81F7CB4-75E1-4622-B614-B60E9A3A2714}" src="https://github.com/user-attachments/assets/6d3a1e87-fa78-4475-b9ac-e08609d5d5d6" />

<img width="1000" height="309" alt="{7D1DFE57-6F42-4D29-8C32-476960F72654}" src="https://github.com/user-attachments/assets/381fa1a9-c336-4b1e-a598-f6b8ba8be78a" />

<img width="1043" height="414" alt="{3C0B6180-A825-4616-8081-242648DD53D0}" src="https://github.com/user-attachments/assets/84a93154-db7a-4b6d-9bb1-84bd85ea510b" />

<img width="1010" height="583" alt="{F9FABA2C-9C7E-466C-89F5-433630D38A27}" src="https://github.com/user-attachments/assets/2444eb21-7207-4765-bf6c-69cb56344478" />

<img width="1013" height="478" alt="{0528AD35-DA5F-4ECA-8E64-8F5222B3AEFB}" src="https://github.com/user-attachments/assets/eb6ac68e-7354-4ab2-bcf4-78874c865bc2" />


## Contribuciones

Se aceptan sugerencias y mejoras. Puedes abrir issues o pull requests.

## Licencia

Este proyecto no especifica licencia. Contacta al autor para más detalles.

## Autor

[MateoM77](https://github.com/MateoM77)
