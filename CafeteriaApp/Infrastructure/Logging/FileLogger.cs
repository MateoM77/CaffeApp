using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CafeteriaApp.Infrastructure.Logging
{
    public class FileLogger
    {
        private readonly string _logPath;

        public FileLogger(string logPath)
        {
            _logPath = logPath;
            EnsureLogDirectory();
        }

        private void EnsureLogDirectory()
        {
            string directory = Path.GetDirectoryName(_logPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public void Log(string mensaje)
        {
            try
            {
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {mensaje}";
                File.AppendAllText(_logPath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al escribir log: {ex.Message}");
            }
        }

        public void LogError(string mensaje, Exception ex = null)
        {
            string errorMessage = ex != null ? $"{mensaje}: {ex.Message}" : mensaje;
            Log($"ERROR: {errorMessage}");
        }

        public void LogOperacion(string operacion, string detalles = "")
        {
            string mensaje = string.IsNullOrEmpty(detalles)
                ? $"OPERACIÓN: {operacion}"
                : $"OPERACIÓN: {operacion} - {detalles}";
            Log(mensaje);
        }
    }
}
