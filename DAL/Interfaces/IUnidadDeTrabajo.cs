namespace DAL.Interfaces
{
    public interface IUnidadDeTrabajo : IDisposable
    {
        IUsuariosDAL UsuariosDAL { get; }
        IRolesDAL RolesDAL { get; }
        ITicketsDAL TicketsDAL { get; } // Asegúrate de que el nombre sea correcto
        IDepartamentosDAL DepartamentosDAL { get; }
        IAuditoriaDAL AuditoriaDAL { get; }

        Task<bool> CompleteAsync(); // Método asincrónico
        bool Complete(); // Método sincrónico
    }
}


