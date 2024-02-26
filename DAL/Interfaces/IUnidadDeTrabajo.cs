using System;

namespace DAL.Interfaces
{
    public interface IUnidadDeTrabajo : IDisposable
    {
        IUsuariosDAL UsuariosDAL { get; }
        IRolesDAL RolesDAL { get; }
        IDepartamentosDAL DepartamentosDAL { get; }
        bool Complete();
    }
}