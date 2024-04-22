using FrontEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontEnd.Helpers.Interfaces
{
    public interface ITicketHelper
    {
        Task<List<TicketViewModel>> GetTickets();
        Task<TicketViewModel> GetTicket(int id);
        Task AddTicket(TicketViewModel ticket);
        Task UpdateTicket(TicketViewModel ticket);
        Task DeleteTicket(int id);
    }
}
