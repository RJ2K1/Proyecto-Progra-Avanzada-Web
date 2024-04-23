namespace DAL.Interfaces
{
    public interface IUnidadDeTrabajo : IDisposable
    {
        IUsuariosDAL UsuariosDAL { get; }
        IRolesDAL RolesDAL { get; }
        ITicketsDAL TicketsDAL { get; }
        IDepartamentosDAL DepartamentosDAL { get; }
        IAuditoriaDAL AuditoriaDAL { get; }

        Task<bool> CompleteAsync();
        bool Complete();
    }
}


