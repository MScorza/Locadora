using System;
using Locadora.Models;
using Locadora.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _clienteRepositorio;

        public ClienteController(IClienteRepository clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        [HttpGet]
        public IActionResult ListarClientes()
        {
            return Ok(_clienteRepositorio.Listar());
        }

        [HttpPost]
        public IActionResult IncluirCliente([FromBody]Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest("Cliente = Null");
            }

            Cliente cli = _clienteRepositorio.Obter(cliente.Cpf);
            if (cli != null)
            {
                return BadRequest("Cliente já Cadastrado!");
            }

            _clienteRepositorio.Incluir(cliente);
            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult AtualizarCliente(Guid id, [FromBody]Cliente cliente)
        {
            var cli = _clienteRepositorio.Obter(id);
            if (cli == null)
            {
                return NotFound();
            }

            cli.Cpf = cliente.Cpf;
            cli.Nome = cliente.Nome;
            cli.Endereco = cliente.Endereco;
            _clienteRepositorio.Atualizar(cli);

            return Ok(cli);
        }

        [HttpGet("{id}")]
        public IActionResult ObterCliente(Guid id)
        {
            return Ok(_clienteRepositorio.Obter(id));
        }

        [HttpDelete("{id}")]
        public IActionResult AlterarStatus(Guid id)
        {
            var cli = _clienteRepositorio.Obter(id);
            if (cli == null)
            {
                return NotFound();
            }
            _clienteRepositorio.AlterarStatus(id, "I");
            return Ok(cli);
        }
    }
}
