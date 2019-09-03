using System;
using System.ComponentModel.DataAnnotations;
using VendasWebMVC.Models.Enums;

namespace VendasWebMVC.Models
{
    public class Venda
    {
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Valor { get; set; }
        public VendaStatus Status { get; set; }
        public Vendedor Vendedor { get; set; }

        public Venda()
        {
        }

        public Venda(int iD, DateTime data, double valor, VendaStatus status, Vendedor vendedor)
        {
            ID = iD;
            Data = data;
            Valor = valor;
            Status = status;
            Vendedor = vendedor;
        }
    }
}
