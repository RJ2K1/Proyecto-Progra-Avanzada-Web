namespace DAL.Interfaces
{
    public interface IUnidadDeTrabajo : IDisposable
    {
        IUsuariosDAL UsuariosDAL { get; }
        IRolesDAL RolesDAL { get; }
        ITicketsDAL _ticketsDAL { get; } 
        IDepartamentosDAL DepartamentosDAL { get; }
        IAuditoriaDAL AuditoriaDAL { get; }
        
        bool Complete();
    }
}


