using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Services.Interfaces
{
    public interface ITicketsService
    {
        Task<bool> add(TicketModel ticket);
        Task<bool> delete(int ind);
        Task<TicketModel> getById(int id);

        Task<List<TicketModel>> GetTickts();

        Task<bool> Update(TicketModel ticket);

    }
}
