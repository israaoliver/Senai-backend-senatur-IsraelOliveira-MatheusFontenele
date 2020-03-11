using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Senatur.WebApi.Domains;
using Senai.Senatur.WebApi.Interfaces;
using Senai.Senatur.WebApi.Repositories;

namespace Senai.Senatur.WebApi.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PacoteController : ControllerBase
    {

        private IPacoteRepository _pacoteRepository;

        public PacoteController()
        {
            _pacoteRepository = new PacoteRepository();
        }


        /// <summary>
        /// Lista todos os Pacotes
        /// </summary>
        /// <returns>Uma lista de pacotes e um status code 200 - Ok</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_pacoteRepository.Listar());
        }

        /// <summary>
        /// Busca um pacote pelo id informado
        /// </summary>
        /// <param name="id">Id do pacote que sera buscado</param>
        /// <returns>Retorna um objeto do pacote</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet ("{id}")]
        public IActionResult BuscarId(int id)
        {
            var pacote = _pacoteRepository.BuscarId(id);
            if (pacote != null)
            {
                return Ok(pacote);
            }

            return NotFound("Nunhum Pacote encontrado com esse id");
        }

        /// <summary>
        /// Cadastra um novo pacote, pode ser cadastrado apenas uma informação
        /// </summary>
        /// <remarks>
        /// Sample response:
        /// 
        ///      {
        ///        "NomePacote": "Name",
        ///        "Descricao" : "description",
        ///        "DataIda" : "2001/12/31",
        ///        "DataVolta" : "2001/12/31",
        ///        "Valor" : 0 ,
        ///        "Ativo" : true ,
        ///        "NomeCidade" : "Name"
        ///        }
        ///     
        ///</remarks>
        /// <param name="pacote">Objeto que sera cadastrado</param>
        /// <returns>o proprio objeto</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Cadastrar(Pacotes pacote)
        {
            _pacoteRepository.Cadastrar(pacote);

            return Created("http://localhost:5000/api/Jogos", pacote);
        }

        /// <summary>
        /// Cadastra um novo pacote
        /// </summary>
        /// <remarks>
        /// Sample response:
        /// 
        ///      {
        ///        "IdPacote" : 0,
        ///        "NomePacote": "Name",
        ///        "Descricao" : "description",
        ///        "DataIda" : "2001/12/31",
        ///        "DataVolta" : "2001/12/31",
        ///        "Valor" : 0 ,
        ///        "Ativo" : 1 ,
        ///        "NomeCidade" : "Name"
        ///        }
        ///     
        ///</remarks>
        /// <param name="pacote">Objeto que sera atualizado</param>
        /// <returns>o proprio objeto</returns>
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "1")]
        [HttpPut]
        public IActionResult Atualizar(Pacotes pacote)
        {
            var pacoteFind = _pacoteRepository.BuscarId(pacote.IdPacote);

            if(pacoteFind == null)
            {
                return NotFound("Pacote não encontrado");
            }
            _pacoteRepository.Atualizar(pacote);

            return Accepted();
        }

        /// <summary>
        /// Busca um pacotes que estão ativos
        /// </summary>
        /// <returns>Retorna os pacotes que estão ativos</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Ativos")]
        public IActionResult PacotesAtivos()
        {

            return Ok(_pacoteRepository.PacotesAtivos());

        }

        /// <summary>
        /// Busca um pacotes que estão inativos
        /// </summary>
        /// <returns>Retorna os pacotes que estão inativos</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Inativos")]
        public IActionResult PacotesInativos()
        {

            return Ok(_pacoteRepository.PacotesInativos());

        }


        /// <summary>
        /// Lista todos os Pacotes que possuam a mesma cidade
        /// </summary>
        /// <returns>Uma lista de pacotes que possuam a cidade informada</returns>
        /// <param name="cidade">Cidade a qual ele vai procurar</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("Cidade/{cidade}")]
        public IActionResult BuscarCidade(string cidade)
        {
            var listPacotes = _pacoteRepository.PorCidade(cidade);

            if(listPacotes == null)
            {
                return NotFound("Nenhuma pacote com essa Cidade!");
            }

            return Ok(listPacotes);
        }


        /// <summary>
        /// Lista os pacotes por ordem de preço
        /// </summary>
        /// <param name="ordem">O parametro é boleano escreva True para crecente e False para Decrecente</param>
        /// <returns>Retor</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Ordem/{ordem}")]
        public IActionResult Ordenar(bool ordem)
        {
            if((ordem == true) || (ordem == false))
            {
                return Ok(_pacoteRepository.PorPreco(ordem));
            }

            return BadRequest("Escreva True para ordem crecente ou False para ordem decrecente");
        }


    }
}