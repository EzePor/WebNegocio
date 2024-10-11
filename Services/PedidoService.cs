using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using WebNegocio.Interfaces.Pedidos;
using WebNegocio.Models.Commos;
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


        public async Task<List<Pedido>> GetPedidosPorEstadoAsync(string estado)
        {
            var response = await client.GetAsync($"pedidos/estado/{estado}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Pedido>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }


        public async Task UpdatePedidoAsync(Pedido pedido)
        {
            var response = await client.PutAsJsonAsync($"pedidos/{pedido.id}", pedido);
            response.EnsureSuccessStatusCode();
        }

        // *** Nuevo método para ajustar el stock de productos al crear o actualizar un pedido ***
        public async Task AjustarStockAlCrearPedido(Pedido nuevoPedido)
        {
            foreach (var detalleProducto in nuevoPedido.DetallesProducto)
            {
                try
                {
                    Console.WriteLine($"Reduciendo stock para producto ID: {detalleProducto.ProductoId}, cantidad: {detalleProducto.cantidad}");

                    // Disminuir el stock del producto
                    await ActualizarStockProducto(detalleProducto.ProductoId, detalleProducto.cantidad);

                    Console.WriteLine($"Stock reducido para producto ID: {detalleProducto.ProductoId}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al ajustar el stock del producto ID: {detalleProducto.ProductoId}, Error: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task ActualizarStockProducto(int productoId, int cantidadRestar)
        {
            var response = await client.PutAsJsonAsync($"productos/{productoId}/reducirStock", cantidadRestar);

            // Verificar si la respuesta fue exitosa
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error al reducir el stock del producto ID: {productoId}, Mensaje: {errorMessage}");
                throw new ApplicationException($"Error al reducir el stock del producto ID: {productoId}, Mensaje: {errorMessage}");
            }

            Console.WriteLine($"Stock reducido correctamente para producto ID: {productoId}");
        }


        public async Task<string> AddAsync(Pedido nuevoPedido)
        {
            try
            {
                // Validación de stock
                foreach (var detalleProducto in nuevoPedido.DetallesProducto)
                {
                    var productoResponse = await client.GetAsync($"productos/{detalleProducto.ProductoId}");
                    if (!productoResponse.IsSuccessStatusCode)
                    {
                        return "Error al obtener el producto.";
                    }

                    var producto = await productoResponse.Content.ReadFromJsonAsync<Producto>();
                    if (producto.stock < detalleProducto.cantidad)
                    {
                        return $"Stock insuficiente para el producto '{producto.nombre}'. Stock disponible: {producto.stock}";
                    }
                }

                // Ajustar el stock de los productos
                await AjustarStockAlCrearPedido(nuevoPedido);

                // Guardar el pedido en la base de datos
                var response = await client.PostAsJsonAsync(_endpoint, nuevoPedido);
                if (response.IsSuccessStatusCode)
                {
                    return "Success";  // Retorna "Success" si el pedido se guarda correctamente
                }

                return "Error al guardar el pedido.";
            }
            catch (Exception ex)
            {
                // Captura cualquier excepción y retorna el mensaje de error
                return $"Error: {ex.Message}";
            }
        }

    }

}

