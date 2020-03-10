using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.Senatur.WebApi.Domains;
using Senai.Senatur.WebApi.Interfaces;
using Senai.Senatur.WebApi.Repositories;
using Senai.Senatur.WebApi.ViewModels;

namespace Senai.Senatur.WebApi.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        public LoginController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Metodo que gera um token para logar o usuario
        /// </summary>
        /// <param name="lg">lg é login o objeto que vem com o email é senha do usuario</param>
        /// <returns>Retorna o token gerado</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult Login(LoginViewModel lg)
        {
            var user = _usuarioRepository.Autenticando(lg);

            if (user == null)
            {
                return NotFound("E-mail ou senha inválidos");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, user.IdTipoUsuario.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("senatur-key-autenticacao"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Gera o token
            var token = new JwtSecurityToken(
                issuer: "Senatur.WebApi",                // emissor do token
                audience: "Senatur.WebApi",              // destinatário do token
                claims: claims,                          // dados definidos acima
                expires: DateTime.Now.AddMinutes(30),    // tempo de expiração
                signingCredentials: creds                // credenciais do token
            );


            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });

        }

    }
}