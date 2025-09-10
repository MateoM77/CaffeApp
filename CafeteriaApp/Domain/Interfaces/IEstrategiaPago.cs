using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Domain.Interfaces
{
    public interface IEstrategiaPago
    {
        string NombreMetodo { get; }
        bool ProcesarPago(decimal monto, out string mensaje);
        bool RequiereInformacionAdicional();
        Dictionary<string, string> ObtenerInformacionPago();
    }
}
