using Newtonsoft.Json;
using Shared.Models;
using System.Net.Http.Json;

namespace ClientApp.Services
{
    public class ApiCaller : IApiCaller
    {
        public HttpClient Client { get; set; } = new()
        {
            BaseAddress = new Uri("https://localhost:7267/api/")
        };

        public async Task<List<TicketModel>> GetTickets()
        {
            var response = await Client.GetAsync("ticket");


            if (response.IsSuccessStatusCode)
            {
                string TicketsJson = await response.Content.ReadAsStringAsync();

                List<TicketModel>? tickets = JsonConvert.DeserializeObject<List<TicketModel>>(TicketsJson);
                tickets = tickets.OrderByDescending(x => x.Id).ToList();
                if (tickets != null)
                {
                    return tickets;
                }

                throw new JsonException();
            }

            throw new HttpRequestException();
        }
        public async Task<TicketModel> GetTicket(int id)
        {
            var response = await Client.GetAsync($"ticket/{id}");

            if (response.IsSuccessStatusCode)
            {
                string TicketJson = await response.Content.ReadAsStringAsync();
                TicketModel? ticket = JsonConvert.DeserializeObject<TicketModel>(TicketJson);

                if (ticket != null)
                {
                    return ticket;
                }

                throw new JsonException();
            }
            throw new HttpRequestException();
        }

        public async Task PostTicket(TicketApiModel ticketApiModel)
        {
            await Client.PostAsJsonAsync("ticket", ticketApiModel);
        }

        public async Task<List<ResponseModel>> GetResponse(int id)
        {
            var response = await Client.GetAsync($"response/{id}");

            if (response.IsSuccessStatusCode)
            {
                string responseJson = await response.Content.ReadAsStringAsync();
                List<ResponseModel>? responses = JsonConvert.DeserializeObject<List<ResponseModel>>(responseJson);

                if (responses != null)
                {
                    return responses;
                }

                throw new JsonException();
            }
            throw new HttpRequestException();
        }
    }
}
