using System;
using System.Linq;
using Locadora.Models;
using System.Collections.Generic;

namespace Locadora.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly List<Cliente> _storage;

        public ClienteRepository()
        {
            _storage = new List<Cliente>();
        }

        public void Incluir(Cliente cliente)
        {
            _storage.Add(cliente);
        }

        public void Atualizar(Cliente cliente)
        {
            var index = _storage.FindIndex(0, x => x.Id == cliente.Id);
            if (index != -1)
            {
                _storage[index] = cliente;
            }
        }

        public IEnumerable<Cliente> Listar()
        {
            return _storage;
        }

        public Cliente Obter(Guid id)
        {
            return _storage.FirstOrDefault(c => c.Id == id);
        }

        public Cliente Obter(string cpf)
        {
            return _storage.FirstOrDefault(c => c.Cpf == cpf);
        }

        public void AlterarStatus(Guid Id, string novoStatus)
        {
            Cliente cliente = Obter(Id);
            cliente.Status = novoStatus;
            Atualizar(cliente);
        }
    }
}
