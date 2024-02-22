using Shared.Models;

namespace ClientApp.Services
{
    public interface IApiCaller
    {
        public HttpClient Client { get; set; }
        public Task<List<TicketModel>> GetTickets();

        public Task PostTicket(TicketApiModel ticketApiModel);
    }
}
