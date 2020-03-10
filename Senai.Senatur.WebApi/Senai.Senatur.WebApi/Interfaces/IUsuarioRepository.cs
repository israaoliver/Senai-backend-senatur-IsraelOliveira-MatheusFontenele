using Senai.Senatur.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Senatur.WebApi.Interfaces
{
    interface IUsuarioRepository
    {

        List<Usuarios> Listar();

        Usuarios BuscarPorId(int id);

        void Deletar(int id);

        void Cadastrar(Usuarios CadastrarUsuario);

        void Atualizar(int id, Usuarios UsuariosAtualizados);

        Usuarios BuscarPorEmailSenha(string email, string senha);
    }
}
