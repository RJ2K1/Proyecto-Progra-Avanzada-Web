using System;

namespace DAL.Interfaces
{
    public interface IUnidadDeTrabajo : IDisposable
    {
        IUsuariosDAL UsuariosDAL { get; }
        bool Complete();
    }
}
