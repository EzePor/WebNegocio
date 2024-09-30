using WebNegocio.Models.Details;

namespace WebNegocio.Interfaces.resumenPedido
{
    public interface IResumenPedidoService : IGenericService<ResumenPedido>
    {
        // Método para obtener todos los pedidos de un cliente
        Task<List<ResumenPedido>?> GetResumenesPorCliente(int? idCliente);
    }
}
