using WebNegocio.Models.Details;

namespace WebNegocio.Interfaces.Pedidos
{

    public interface IPedidoService : IGenericService<Pedido>
    {
        // Método para obtener todos los pedidos de un cliente
        Task<List<Pedido>?> GetResumenesPorCliente(int? idCliente);
    }
}
