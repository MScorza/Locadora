using System;
using Locadora.Models;
using Locadora.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.Controllers
{
    [Route("api/[controller]")]
    public class FilmeController : Controller
    {
        private readonly IFilmeRepository _filmeRepositorio;

        public FilmeController(IFilmeRepository fimeRepositorio)
        {
            _filmeRepositorio = fimeRepositorio;
        }

        [HttpGet]
        public IActionResult ListarFilmes()
        {
            return Ok(_filmeRepositorio.Listar());
        }

        [HttpPost]
        public IActionResult IncluirFilme([FromBody]Filme filme)
        {
            if (filme == null)
            {
                return BadRequest("Filme = Null");
            }

            Filme movie = _filmeRepositorio.Obter(filme.Id);
            if (movie == null) //o id indicado não existe - novo filme
            {
                _filmeRepositorio.Incluir(filme);
            }
            else // o id indicado ja existe: soma 1 nas cópias disponíveis
            {
                _filmeRepositorio.AtualizarCopiasDisponiveis(filme.Id, "N");
            }

            return Ok();
        }

        [HttpPut("{Id}")]
        public IActionResult AtualizarFilme(string id, [FromBody]Filme filme)
        {
            var movie = _filmeRepositorio.Obter(id);
            if (movie == null)
            {
                return NotFound();
            }

            movie.Titulo = filme.Titulo;
            movie.AtrizAtorPrincipal = filme.AtrizAtorPrincipal;
            movie.Diretor = filme.Diretor;
            movie.CopiasDisponiveis = filme.CopiasDisponiveis;
            _filmeRepositorio.Atualizar(movie);

            return Ok(movie);
        }

        [HttpGet("{Id}")]
        public IActionResult ObterFilme(string id)
        {
            return Ok(_filmeRepositorio.Obter(id));
        }

        [HttpDelete("{Id}")]
        public IActionResult RemoverFilme(string id)
        {
            var filme = _filmeRepositorio.Obter(id);
            if (filme == null)
            {
                return NotFound();
            }
            _filmeRepositorio.Excluir(id);

            return Ok(filme);
        }

        public int ObterCopiasDisponiveis(string id)
        {
            var filme = _filmeRepositorio.Obter(id);
            return filme.CopiasDisponiveis;
        }

        public Filme Obter(string id)
        {
            return _filmeRepositorio.Obter(id);
        }
    }
}
