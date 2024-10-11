using Microsoft.Extensions.Options;
using System.Text.Json;
using WebNegocio.Interfaces;
using WebNegocio.Models.Commos;

namespace WebNegocio.Services
{
    public class ClienteService : GenericService<Cliente>, IClienteService
    {
       

        public ClienteService(HttpClient client) : base(client)
        {
        }


        public async Task<List<Cliente>?> GetClienteAsync()
        {
            var response = await client.GetAsync($"{_endpoint}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content?.ToString());
            }
            return JsonSerializer.Deserialize<List<Cliente>>(content, options); ;
        }
    }
}
