using Microsoft.JSInterop;
using System.Text.Json;

namespace WebNegocio.Services.Login
{
    public class FirebaseAuthService
    {
        private readonly IJSRuntime _jsRuntime;
        private const string UserIdKey = "firebaseUserId";
        public event Action OnChangeLogin;

        public FirebaseAuthService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<(string userId, string message)> RegisterUser(string email, string password, string nombre, string apellido)
        {
            try
            {
                // La respuesta ahora incluye userId y message
                var result = await _jsRuntime.InvokeAsync<JsonElement>("firebaseAuth.registerWithEmailPassword", email, password, nombre, apellido);
                string userId = result.GetProperty("userId").GetString();
                string message = result.GetProperty("message").GetString();

                // No guardamos el userId porque necesitamos verificación de email
                return (userId, message);
            }
            catch (JSException jsEx)
            {
                return (null, $"Error: {jsEx.Message}");
            }
            catch (Exception ex)
            {
                return (null, $"Error: {ex.Message}");
            }
        }
        public async Task LogoutAsync()
        {
            // Llama a la función signOut del SDK de Firebase desde JavaScript.
            await _jsRuntime.InvokeVoidAsync("firebaseAuth.signOut");
        }

        public async Task<string> SignInWithEmailPassword(string email, string password)
        {
            try
            {
                var userCredential = await _jsRuntime.InvokeAsync<JsonElement>("firebaseAuth.signInWithEmailPassword", email, password);
                var userId = userCredential.GetProperty("userId").GetString();
                var displayName = userCredential.GetProperty("displayName").GetString();

                if (!string.IsNullOrEmpty(userId))
                {
                    await _jsRuntime.InvokeVoidAsync("localStorageHelper.setItem", UserIdKey, userId);
                    await _jsRuntime.InvokeVoidAsync("localStorageHelper.setItem", "userDisplayName", displayName);
                    OnChangeLogin?.Invoke();
                }
                return userId;
            }
            catch (JSException jsEx)
            {
                if (jsEx.Message.Contains("email-not-verified"))
                {
                    return "Error: Por favor verifica tu email antes de iniciar sesión";
                }
                return $"Error: {jsEx.Message}";
            }
        }

        public async Task<bool> ResendVerificationEmail(string email, string password)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("firebaseAuth.resendVerificationEmail", email, password);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task SignOut()
        {
            await _jsRuntime.InvokeVoidAsync("firebaseAuth.signOut");
            await _jsRuntime.InvokeVoidAsync("localStorageHelper.removeItem", UserIdKey);
            OnChangeLogin?.Invoke();
        }

        public async Task<string> GetUserId()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorageHelper.getItem", UserIdKey);
        }

        public async Task<bool> IsUserAuthenticated()
        {
            var userId = await GetUserId();
            return !string.IsNullOrEmpty(userId);
        }
    }
}
