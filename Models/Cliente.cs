using System;

namespace Locadora.Models
{
    public class Cliente
    {
        public Cliente()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public string Cpf { get; set; }

        public string Nome { get; set; }

        public string Endereco { get; set; }

        public string Status { get; set; }
    }
}
