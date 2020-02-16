using System;
using Locadora.Models;
using System.Collections.Generic;

namespace Locadora.Repositories
{
    public interface IClienteRepository
    {
        void Incluir(Cliente cliente);

        void Atualizar(Cliente cliente);

        IEnumerable<Cliente> Listar();

        Cliente Obter(Guid id);

        Cliente Obter(string Cpf);

        void AlterarStatus(Guid Id, string novoStatus);
    }
}
