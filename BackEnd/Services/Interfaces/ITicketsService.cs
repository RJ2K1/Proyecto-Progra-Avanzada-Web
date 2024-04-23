using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Services.Interfaces
{
    public interface ITicketsService
    {
        Task<bool> Add(TicketModel ticket);
        Task<bool> Delete(int id);
        Task<TicketModel> GetById(int id);
        Task<List<TicketModel>> GetTickets();
        Task<bool> Update(TicketModel ticket);
    }
}
