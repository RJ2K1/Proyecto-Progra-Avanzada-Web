using System;

namespace DAL.Interfaces
{
    public interface IUnidadDeTrabajo : IDisposable
    {
        IUsuariosDAL UsuariosDAL { get; }
        IRolesDAL RolesDAL { get; }

        IAuditoriaDAL AuditoriaDAL { get; }
        bool Complete();
    }
}