using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebNegocio;
using WebNegocio.Interfaces;
using WebNegocio.Interfaces.Pedidos;
using WebNegocio.Interfaces.resumenPedido;
using WebNegocio.Models.Commos;
using WebNegocio.Models.Details;
using WebNegocio.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Logging.SetMinimumLevel(LogLevel.Debug);
var urlApi = builder.Configuration.GetValue<string>("urlApi");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(urlApi) });

builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

builder.Services.AddScoped<GenericService<Producto>>();
builder.Services.AddScoped<GenericService<Impresion>>();
builder.Services.AddScoped<GenericService<Cliente>>();
builder.Services.AddScoped<GenericService<ModoPago>>();
builder.Services.AddScoped<GenericService<Empleado>>();
builder.Services.AddScoped<GenericService<DetalleProducto>>();
builder.Services.AddScoped<GenericService<DetalleImpresion>>();
builder.Services.AddScoped<GenericService<Pedido>>();
builder.Services.AddScoped<GenericService<ResumenPedido>>();
builder.Services.AddScoped<IResumenPedidoService, ResumenPedidoService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IModoPagoService, ModoPagoService>();

builder.Services.AddScoped<IGenericService<Cliente>, GenericService<Cliente>>();
builder.Services.AddScoped<IGenericService<ModoPago>, GenericService<ModoPago>>();

builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ModoPagoService>();

builder.Services.AddScoped<SweetAlertService>();








builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
