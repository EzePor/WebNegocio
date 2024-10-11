using WebNegocio.Models.Commos;

namespace WebNegocio.Interfaces
{
    public interface IModoPagoService : IGenericService<ModoPago>
    {
        public Task<List<ModoPago>?> GetModosPagoAsync();
    }
}
