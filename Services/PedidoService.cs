using System.Text.Json;
using WebNegocio.Interfaces.Pedidos;

using WebNegocio.Models.Details;

namespace WebNegocio.Services
{
    public class PedidoService : GenericService<Pedido>, IPedidoService
    {
        public PedidoService(HttpClient client) : base(client)
        {
        }



        public async Task<List<Pedido>> GetResumenesPorCliente(int? idCliente)
        {
            if (idCliente == null)
                throw new ArgumentNullException(nameof(idCliente));

            var response = await client.GetAsync($"{_endpoint}/cliente/{idCliente}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content?.ToString());
            }
            return JsonSerializer.Deserialize<List<Pedido>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

    }
}
