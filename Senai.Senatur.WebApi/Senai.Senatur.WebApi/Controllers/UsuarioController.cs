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

namespace Senai.Senatur.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Lista todos os Usuarios
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_usuarioRepository.Listar());
        }

        /// <summary>
        /// Retorna um usuario especifico
        /// </summary>
        /// <param name="id">busca o usuario especifico</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var usuario = _usuarioRepository.BuscarPorId(id);

            if(usuario == null)
            {
                return NotFound("usuario não existe");
            }

            return Ok(usuario);
        }

        /// <summary>
        /// Deleta um usuario
        /// </summary>
        /// <param name="id">Id para identificar e deletar um usuario</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var usuario = _usuarioRepository.BuscarPorId(id);

            if(usuario == null)
            {
                return NotFound("usuario não existe");
            }

            _usuarioRepository.Deletar(id);

            return Ok();
        }

        /// <summary>
        /// Cadastra um novo usuario
        /// </summary>
        /// <param name="novoUsuario">Passa as informações do novo usuario</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult Cadastrar(Usuarios novoUsuario)
        {
            _usuarioRepository.Cadastrar(novoUsuario);

            return StatusCode(201);
        }

        /// <summary>
        /// Atualiza os Usuarios
        /// </summary>
        /// <param name="id">Especifica qual vai atualizar</param>
        /// <param name="usuarioAtualizado">Informações que vão ser atualizadas</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Usuarios usuarioAtualizado)
        {
            var usuario = _usuarioRepository.BuscarPorId(id);

            if(usuario == null)
            {
                return NotFound("usuario não encontrado");
            }

            _usuarioRepository.Atualizar(id, usuarioAtualizado);

            return Ok();
        }

    }
}