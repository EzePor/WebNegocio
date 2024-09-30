using System.ComponentModel.DataAnnotations;
using WebNegocio.Enums;

namespace WebNegocio.Models.Commos
{
    public class Usuario
    {
        public int id { get; set; }
        [Required(ErrorMessage = "El nombre de usuario debe cargarse obligatoriamente")]
        public string user { get; set; } = string.Empty;
        [Required(ErrorMessage = "El email debe cargarse obligatoriamente")]
        public string email { get; set; } = string.Empty;
        [Required(ErrorMessage = "La contraseña debe cargarse obligatoriamente")]

        public string password { get; set; } = string.Empty;
        [Required(ErrorMessage = "El tipo de usuario debe cargarse obligatoriamente")]
        public TipoUsuarioEnum? TipoUsuario { get; set; }

        public bool eliminado { get; set; } = false;


        public override string ToString()
        {
            return $"{user}" ?? string.Empty;
        }
    }
}
