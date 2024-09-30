using Microsoft.JSInterop;

namespace WebNegocio.Services
{
    public class SweetAlert
    {
        private readonly IJSRuntime _jsRuntime;

        public SweetAlert(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        // instanciamos ShowAlert
        public async Task ShowAlert(string tipo, string titulo, string mensaje)
        {
            await _jsRuntime.InvokeVoidAsync("showSwal", tipo, titulo, mensaje);
        }

        // Método para mostrar la alerta de éxito
        public async Task ShowSuccess(string message)
        {
            await _jsRuntime.InvokeVoidAsync("Swal.fire", new { icon = "success", title = "Éxito", text = message });
        }

        // instanciamos Confirmation
        public async Task<bool> Confirmation(string titulo, string mensaje, TipoIconSweetAlert tipoIconSweetAlert)
        {
            return await _jsRuntime.InvokeAsync<bool>("SweetAlertHelper.showDeleteConfirmation", titulo, mensaje, tipoIconSweetAlert.ToString());
        }
    }
    // eneumeramos los tipos de alerta según el icon
    public enum TipoIconSweetAlert
    {
        question, warning, success, info
    }
}
