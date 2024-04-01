using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface ITicketHelper
    {

        Task<List<TicketViewModel>> GetTickets();

        Task<TicketViewModel> GetTicket(int id);

        Task<TicketViewModel> AddTicket(TicketViewModel ticket);

        Task<bool> DeleteTicket(int id);

        Task<TicketViewModel> UpdateTicket(TicketViewModel ticket);
    }
}
    
