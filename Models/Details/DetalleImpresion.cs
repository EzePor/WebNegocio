﻿using System.ComponentModel.DataAnnotations.Schema;
using WebNegocio.Models.Commos;

namespace WebNegocio.Models.Details
{
    public class DetalleImpresion
    {
        public int id { get; set; }
        public int PedidoId { get; set; }
        public Pedido? pedido { get; set; }
        public int ImpresionId { get; set; }
        public Impresion? impresion { get; set; }
        public int cantidad { get; set; }

        public decimal precioUnitario { get; set; }


        public decimal total
        {
            get
            {
                return cantidad * precioUnitario;
            }
        }

        [NotMapped]
        public string TotalF => total.ToString("0.00");


        public bool eliminado { get; set; } = false;

        public override string ToString()
        {
            return $"{impresion} - {cantidad}";
        }
    }
}
