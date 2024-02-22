using Newtonsoft.Json;
using Shared.Models;
using System.Net.Http.Json;

namespace ClientApp.Services
{
    public class ApiCaller : IApiCaller
    {
        public HttpClient Client { get; set; } = new()
        {
            BaseAddress = new Uri("https://localhost:7267/api")
        };

        public async Task<List<TicketModel>> GetTickets()
        {
            var response = await Client.GetAsync("Ticket");


            if (response.IsSuccessStatusCode)
            {
                string TicketsJson = await response.Content.ReadAsStringAsync();

                List<TicketModel>? tickets = JsonConvert.DeserializeObject<List<TicketModel>>(TicketsJson);

                if (tickets != null)
                {
                    return tickets;
                }

                throw new JsonException();
            }

            throw new HttpRequestException();
        }

        public async Task PostTicket(TicketApiModel ticketApiModel)
        {
            await Client.PostAsJsonAsync("Ticket", ticketApiModel);
        }

    }
}
