using Microsoft.EntityFrameworkCore;
using Senai.Senatur.WebApi.Domains;
using Senai.Senatur.WebApi.Interfaces;
using Senai.Senatur.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Senatur.WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        SenaturContext ctx = new SenaturContext();
        public void Atualizar(int id, Usuarios UsuariosAtualizados)
        {
            throw new NotImplementedException();
        }

        public Usuarios BuscarPorEmailSenha(LoginViewModel login)
        {
            throw new NotImplementedException();
        }

        public Usuarios BuscarPorId(int id)
        {
            return ctx.Usuarios.Include(e => e.IdTipoUsuarioNavigation).FirstOrDefault(e => e.IdUsuario == id);
        }

        public void Cadastrar(Usuarios novoUsuario)
        {
            ctx.Usuarios.Add(novoUsuario);
        }

        public void Deletar(int id)
        {
            Usuarios usuarioBuscado = BuscarPorId(id);

            if(usuarioBuscado != null)
            {
                ctx.Usuarios.Remove(usuarioBuscado);
                ctx.SaveChanges();
            }
        }

        public List<Usuarios> Listar()
        {
            return ctx.Usuarios.Include(e => e.IdTipoUsuarioNavigation).ToList();
        }
    }
}
