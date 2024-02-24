using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnidadDeTrabajo: IDisposable
    {
        public interface IUnidadDeTrabajo : IDisposable
        {
            IUsuariosDAL _usuariosDAL { get; }
            ITicketsDAL _ticketsDAL { get; }

            bool Complete();
        }
    }
}
