using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CafeteriaApp.Domain.Models;

namespace CafeteriaApp.Domain.Interfaces
{
    public interface IRepositorio<T>
    {
        void Guardar(T entidad);
        T ObtenerPorId(string id);
        List<T> ObtenerTodos();
        void Actualizar(T entidad);
    }
}
