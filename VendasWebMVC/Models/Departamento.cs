namespace VendasWebMVC.Models
{
    public class Departamento
    {
        public int ID { get; set; }
        public string Nome { get; set; }

        public Departamento()
        {
        }

        public Departamento(int iD, string nome)
        {
            ID = iD;
            Nome = nome;
        }
    }
}
