using Microsoft.EntityFrameworkCore;
using Senai.Senatur.WebApi.Domains;
using Senai.Senatur.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Senatur.WebApi.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        SenaturContext ctx = new SenaturContext();

        public TiposUsuario BuscarPorId(int id)
        {
            return ctx.TiposUsuario.FirstOrDefault(e => e.IdTipoUsuario == id);
        }

        public List<TiposUsuario> Listar()
        {
            return ctx.TiposUsuario.ToList();
        }
    }
}
