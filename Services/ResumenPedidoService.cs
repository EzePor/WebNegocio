
using System.Text.Json;
using WebNegocio.Models.Details;
using WebNegocio.Interfaces;
using WebNegocio.Interfaces.resumenPedido;



namespace WebNegocio.Services
{
    public class ResumenPedidoService : GenericService<ResumenPedido>, IResumenPedidoService
    {
        public ResumenPedidoService(HttpClient client) : base(client)
        {
        }

       

        public async Task<List<ResumenPedido>> GetResumenesPorCliente(int? idCliente)
        {
            if (idCliente == null)
                throw new ArgumentNullException(nameof(idCliente));

            var response = await client.GetAsync($"{_endpoint}/cliente/{idCliente}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content?.ToString());
            }
            return JsonSerializer.Deserialize<List<ResumenPedido>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

    }
}
