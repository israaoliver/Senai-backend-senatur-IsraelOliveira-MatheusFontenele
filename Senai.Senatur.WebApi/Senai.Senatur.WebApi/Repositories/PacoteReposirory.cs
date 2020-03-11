using Senai.Senatur.WebApi.Domains;
using Senai.Senatur.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Senatur.WebApi.Repositories
{
    public class PacoteRepository : IPacoteRepository
    {
        SenaturContext ctx = new SenaturContext();

        public void Atualizar(Pacotes pct)
        {
            var PacoteDB = ctx.Pacotes.Find(pct.IdPacote);


            if (pct.NomePacote != null)
            {
                PacoteDB.NomePacote = pct.NomePacote;
            }
            if(pct.Descricao != null)
            {
                PacoteDB.Descricao = pct.Descricao;
            }
            if(pct.DataIda != null)
            {
                PacoteDB.DataIda = pct.DataIda;
            }
            if(pct.DataVolta != null)
            {
                PacoteDB.DataVolta = pct.DataVolta;
            }
            if(pct.Valor != 0)
            {
                PacoteDB.Valor = pct.Valor;
            }
            if(pct.NomeCidade != null)
            {
                PacoteDB.NomeCidade = pct.NomeCidade;
            }

            ctx.Pacotes.Update(PacoteDB);
            ctx.SaveChanges();


        }

        public Pacotes BuscarId(int id)
        {

            return ctx.Pacotes.FirstOrDefault(p => p.IdPacote == id);

        }

        public void Cadastrar(Pacotes pct)
        {
            ctx.Pacotes.Add(pct);
            ctx.SaveChanges();
        }

        public List<Pacotes> Listar()
        {
            return ctx.Pacotes.ToList();
        }

        public IEnumerable<Pacotes> PacotesAtivos()
        {
            return ctx.Pacotes.Where(p => p.Ativo == true);
        }

        public IEnumerable<Pacotes> PacotesInativos()
        {
            return ctx.Pacotes.Where(p => p.Ativo == false);
        }

        public IEnumerable<Pacotes> PorCidade(string cidade)
        {
            return ctx.Pacotes.Where(p => p.NomeCidade == cidade);
        }

        public IEnumerable<Pacotes> PorPreco(bool ordem)
        {

            return ctx.Pacotes.OrderBy(p => p.Valor);
        }

    }
}