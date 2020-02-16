using System;
using Locadora.Models;
using Locadora.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.Controllers
{
    [Route("api/[controller]")]
    public class LocacaoController : Controller
    {
        private readonly ILocacaoRepository _locacaoRepositorio;

        public LocacaoController(ILocacaoRepository locacaoRepositorio)
        {
            _locacaoRepositorio = locacaoRepositorio;
        }

        [HttpGet]
        public IActionResult ListarLocacoes()
        {
            return Ok(_locacaoRepositorio.Listar());
        }

        [HttpPost]
        public IActionResult Alugar([FromBody]Locacao locacao)
        {
            FilmeRepository filmeRepository = new FilmeRepository();
            FilmeController filmeController = new FilmeController(filmeRepository);

            int copias = filmeController.ObterCopiasDisponiveis(locacao.IdFilme); 
            if (copias == 0)
            {
                return NotFound("Não há copia disponível para locação");
            }

            locacao.DataDevolucao = locacao.DataLocacao.AddDays(3);
            _locacaoRepositorio.Alugar(locacao);

            Filme filme = filmeController.Obter(locacao.IdFilme);
            filmeRepository.AtualizarCopiasDisponiveis(filme.Id, "L");
            return Ok(locacao);
        }

        [HttpPut("{Id}")]
        public IActionResult Devolver(Guid id, [FromBody]Locacao locacao)
        {
            var loc = _locacaoRepositorio.Obter(id);
            if (loc == null)
            {
                return NotFound();
            }

            DateTime dataPrevista = new DateTime(locacao.DataDevolucao.Year, locacao.DataDevolucao.Month, locacao.DataDevolucao.Day);
            DateTime dataEfetiva = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            loc.IdFilme = locacao.IdFilme;
            loc.IdCliente = locacao.IdCliente;
            loc.DataLocacao = locacao.DataLocacao;
            loc.DataDevolucao = DateTime.Now;
            _locacaoRepositorio.Devolver(loc);

            FilmeRepository filmeRepository = new FilmeRepository();
            Filme filme = filmeRepository.Obter(locacao.IdFilme);
            filmeRepository.AtualizarCopiasDisponiveis(filme.Id, "D");

            if (dataPrevista.CompareTo(dataEfetiva) > 0)
            {
                TimeSpan intervalo = dataPrevista - dataEfetiva;
                return Ok(String.Format("Devolução com atraso de {0} dias.", intervalo.Days));
            }
            return Ok();
        }

        [HttpGet("{Id}")]
        public IActionResult ObterLocacao(Guid id)
        {
            return Ok(_locacaoRepositorio.Obter(id));
        }
    }
}
