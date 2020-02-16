using System;
using System.Linq;
using Locadora.Models;
using System.Collections.Generic;

namespace Locadora.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly List<Filme> _storage;

        public FilmeRepository()
        {
            _storage = new List<Filme>();
        }

        public void Incluir(Filme filme)
        {
            var indice = _storage.FindIndex(0, f => f.Id == filme.Id);
            if (indice == -1) //novo id
            {
                _storage.Add(filme);
            }
            else // Nova cópia
            {
                AtualizarCopiasDisponiveis(filme.Id, "N");
            }
        }

        public void Atualizar(Filme filme)
        {
            var indice = _storage.FindIndex(0, f => f.Id == filme.Id);
            if (indice != -1)
            {
                _storage[indice] = filme;
            }
        }

        public IEnumerable<Filme> Listar()
        {
            return _storage;
        }

        public Filme Obter(string id)
        {
            return _storage.FirstOrDefault(f => f.Id == id);
        }

        public void Excluir(string id)
        {
            var indice = _storage.FindIndex(0, f => f.Id == id);
            var filme = _storage[indice];

            filme.CopiasDisponiveis = 0;

            _storage[indice] = filme;
        }

        public void AtualizarCopiasDisponiveis(string id, string operacao)
        {
            var indice = _storage.FindIndex(0, f => f.Id == id);
            var filme = _storage[indice];
            switch (operacao)
            {
                case "L": //locação
                    filme.CopiasDisponiveis--;
                    break;
                case "D": //devolução
                case "N": //nova cópia
                    filme.CopiasDisponiveis++;
                    break;
            }
            _storage[indice] = filme;
        }

        public int ObterCopiasDisponiveis(string id)
        {
            Listar();
            var indice = _storage.FindIndex(0, f => f.Id == id);
            var filme = _storage[indice];
            return filme.CopiasDisponiveis;
        }
    }
}
