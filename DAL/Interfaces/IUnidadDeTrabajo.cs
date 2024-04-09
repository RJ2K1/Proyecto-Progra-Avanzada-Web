namespace DAL.Interfaces
{
    public interface IUnidadDeTrabajo : IDisposable
    {
        IUsuariosDAL UsuariosDAL { get; }
        IRolesDAL RolesDAL { get; }
        ITicketsDAL TicketsDAL { get; } // Aseg�rate de que el nombre sea correcto
        IDepartamentosDAL DepartamentosDAL { get; }
        IAuditoriaDAL AuditoriaDAL { get; }

        Task<bool> CompleteAsync(); // M�todo asincr�nico
        bool Complete(); // M�todo sincr�nico
    }
}


