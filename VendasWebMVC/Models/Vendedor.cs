using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VendasWebMVC.Models
{
    public class Vendedor
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "{0} obrigatório!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0} deve conter entre {2} e {1} caracteres!")]
        public string Nome { get; set; }

        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} obrigatório!")]
        [EmailAddress(ErrorMessage = "{0} inválido!")]
        public string Email { get; set; }

        //Conotação DataAnnotations
        //Essa anotação será exibida na View com DisplayNameFor no lugar do nome da propriedade
        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "{0} obrigatório!")]
        //Tipo de dado a ser exibido no Display
        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }

        [Display(Name = "Salário Base")]
        [Required(ErrorMessage = "{0} obrigatório!")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Range(100.00, 10000.00, ErrorMessage = "{0} deve ser entre {1} e {2}!")]
        public double SalarioBase { get; set; }


        public Departamento Departamento { get; set; }
        public int DepartamentoId { get; set; }
        public ICollection<Venda> Vendas { get; set; } = new List<Venda>();

        public Vendedor()
        {
        }

        public Vendedor(int iD, string nome, string email, DateTime nascimento, double salarioBase, Departamento departamento)
        {
            ID = iD;
            Nome = nome;
            Email = email;
            Nascimento = nascimento;
            SalarioBase = salarioBase;
            Departamento = departamento;
        }

        public void InserirVenda(Venda venda)
        {
            Vendas.Add(venda);
        }

        public void RemoverVenda(Venda venda)
        {
            Vendas.Remove(venda);
        }

        public double TotalVendas(DateTime inicio, DateTime fim)
        {
            return Vendas.Where(v => v.Data >= inicio && v.Data <= fim).Sum(v => v.Valor);
        }

    }
}
