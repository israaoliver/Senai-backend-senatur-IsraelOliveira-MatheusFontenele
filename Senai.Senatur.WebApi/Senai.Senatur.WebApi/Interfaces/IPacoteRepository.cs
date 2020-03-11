using Senai.Senatur.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Senatur.WebApi.Interfaces
{
    interface IPacoteRepository
    {
        List<Pacotes> Listar();

        Pacotes BuscarId(int id);

        void Atualizar(Pacotes pct);

        void Cadastrar(Pacotes pct);

        IEnumerable<Pacotes> PacotesAtivos();

        IEnumerable<Pacotes> PacotesInativos();

        IEnumerable<Pacotes> PorCidade(string cidade);

        IEnumerable<Pacotes> PorPreco(string ordem);
    }
}