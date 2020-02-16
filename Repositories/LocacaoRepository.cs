using System;
using System.Linq;
using Locadora.Models;
using System.Collections.Generic;

namespace Locadora.Repositories
{
    public class LocacaoRepository : ILocacaoRepository
    {
        private readonly List<Locacao> _storage;

        public LocacaoRepository()
        {
            _storage = new List<Locacao>();
        }

        public void Alugar(Locacao locacao)
        {
            _storage.Add(locacao);
        }

        public void Devolver(Locacao locacao)
        {
            var indice = _storage.FindIndex(0, l => l.Id == locacao.Id);
            _storage[indice].DataDevolucao = DateTime.Now;

            FilmeRepository filmeRepository = new FilmeRepository();
            Filme filme = filmeRepository.Obter(locacao.IdFilme);
            filmeRepository.AtualizarCopiasDisponiveis(filme.Id, "D");
        }

        public IEnumerable<Locacao> Listar()
        {
            return _storage;
        }

        public Locacao Obter(Guid id)
        {
            return _storage.FirstOrDefault(l => l.Id == id);
        }
    }
}
