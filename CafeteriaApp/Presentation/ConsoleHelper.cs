using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Presentation
{
    public static class ConsoleHelper
    {
        public static void MostrarTitulo(string titulo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  {titulo.PadRight(58)}║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void MostrarOpcion(int numero, string descripcion)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"  [{numero}] ");
            Console.ResetColor();
            Console.WriteLine(descripcion);
        }

        public static void MostrarMensajeExito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✓ {mensaje}");
            Console.ResetColor();
        }

        public static void MostrarMensajeError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"✗ {mensaje}");
            Console.ResetColor();
        }

        public static void MostrarMensajeAdvertencia(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⚠ {mensaje}");
            Console.ResetColor();
        }

        public static void MostrarSeparador()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("────────────────────────────────────────────────────────────");
            Console.ResetColor();
        }

        public static int LeerOpcion(string prompt = "Seleccione una opción: ")
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(prompt);
            Console.ResetColor();

            if (int.TryParse(Console.ReadLine(), out int opcion))
            {
                return opcion;
            }
            return -1;
        }

        public static string LeerTexto(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(prompt);
            Console.ResetColor();
            return Console.ReadLine();
        }

        public static void PausarConsola()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        public static bool ConfirmarAccion(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{mensaje} (S/N): ");
            Console.ResetColor();

            var respuesta = Console.ReadLine()?.ToUpper();
            return respuesta == "S" || respuesta == "SI";
        }
    }
}
