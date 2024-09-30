using System.ComponentModel.DataAnnotations;

namespace WebNegocio.Models.Commos
{
    public class Empleado
    {

        public int id { get; set; }
        [Required(ErrorMessage = "El apellido y el nombre  debe cargarse obligatoriamente")]
        public string apellidoNombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El dni debe cargarse obligatoriamente")]

        public string dni { get; set; } = string.Empty;

        public bool eliminado { get; set; } = false;

        public override string ToString()
        {
            return apellidoNombre;
        }
    }
}
