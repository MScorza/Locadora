using System;
using Locadora.Models;
using System.Collections.Generic;

namespace Locadora.Repositories
{
    public interface IFilmeRepository
    {
        void Incluir(Filme filme);

        void Atualizar(Filme filme);

        IEnumerable<Filme> Listar();

        Filme Obter(string id);

        void Excluir(string id);

        /// <summary>
        /// Atualiza a quantidade de copias disponiveis do filme para locacao
        /// </summary>
        /// <param name="Id">Identificador Unico baseado no IMDB</param>
        /// <param name="operacao">L = Locacao --> Diminui em 1 a quantidade disponivel
        ///                        D = Devolução --> Acrescenta 1 à quantidade disponível
        /// </param>
        void AtualizarCopiasDisponiveis(string id, string operacao);
    }
}
