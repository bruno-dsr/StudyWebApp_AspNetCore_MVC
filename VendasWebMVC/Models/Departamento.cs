using System;
using System.Linq;
using System.Collections.Generic;

namespace VendasWebMVC.Models
{
    public class Departamento
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public ICollection<Vendedor> Vendedores { get; set; } = new List<Vendedor>();

        public Departamento()
        {
        }

        public Departamento(int iD, string nome)
        {
            ID = iD;
            Nome = nome;
        }

        public void InserirVendedor(Vendedor vendedor)
        {
            Vendedores.Add(vendedor);
        }

        public double TotalVendas(DateTime inicio, DateTime fim)
        {
            return Vendedores.Sum(vend => vend.TotalVendas(inicio, fim));
        }
    }
}
