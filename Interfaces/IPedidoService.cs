using WebNegocio.Models.Details;

namespace WebNegocio.Interfaces.Pedidos
{

    public interface IPedidoService : IGenericService<Pedido>
    {
        // Método para obtener todos los pedidos de un cliente
        Task<List<Pedido>?> GetResumenesPorCliente(int? idCliente);

        Task<List<Pedido>> GetPedidosPorEstadoAsync(string estado);
        Task UpdatePedidoAsync(Pedido pedido);

        // Nuevo método para ajustar el stock de productos al crear o actualizar un pedido
        Task AjustarStockAlCrearPedido(Pedido nuevoPedido);

        Task ActualizarStockProducto(int productoId, int cantidadRestar);


    }
}
