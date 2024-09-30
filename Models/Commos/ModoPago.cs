using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebNegocio.Models.Commos
{
    public class ModoPago
    {
        public int id { get; set; }
        [Required(ErrorMessage = "El modo de pago debe cargarse obligatoriamente")]
        public string nombre { get; set; } = string.Empty;
        public decimal ajuste { get; set; }

        [NotMapped]
        public string ajusteF
        {
            get { return ajuste.ToString("0.00"); }
            set
            {
                if (decimal.TryParse(value, out var parsedValue))
                {
                    ajuste = Math.Round(parsedValue, 2);
                }
            }
        }

        public bool eliminado { get; set; } = false;

        public override string ToString()
        {
            return nombre;
        }
    }
}
