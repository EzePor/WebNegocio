using Microsoft.Extensions.Options;
using System.Text.Json;
using WebNegocio.Interfaces;
using WebNegocio.Models.Commos;
using WebNegocio.Pages.Commons.Clientes;

namespace WebNegocio.Services
{
    public class ModoPagoService : GenericService<ModoPago>, IModoPagoService
    {
        public ModoPagoService(HttpClient client) : base(client)
        {
        }


        public async Task<List<ModoPago>?> GetModosPagoAsync()
        {
            var response = await client.GetAsync($"{_endpoint}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content?.ToString());
            }
            return JsonSerializer.Deserialize<List<ModoPago>>(content, options); ;
        }
    }
}
