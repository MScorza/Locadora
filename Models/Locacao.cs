using System;

namespace Locadora.Models
{
    public class Locacao
    {
        public Locacao()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public string IdFilme { get; set; }

        public string IdCliente { get; set; }

        public DateTime DataLocacao { get; set; }

        public DateTime DataDevolucao { get; set; }
    }
}
