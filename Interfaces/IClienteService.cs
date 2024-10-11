using WebNegocio.Models.Commos;

namespace WebNegocio.Interfaces
{
    public interface IClienteService : IGenericService<Cliente>
    {
        public Task<List<Cliente>?> GetClienteAsync();
    }
}
