using System;
using Locadora.Models;
using System.Collections.Generic;

namespace Locadora.Repositories
{
    public interface ILocacaoRepository
    {
        void Alugar(Locacao locacao);

        void Devolver(Locacao locacao);

        IEnumerable<Locacao> Listar();

        Locacao Obter(Guid id);
    }
}
