using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Senai.Senatur.WebApi.Domains
{
    public partial class Pacotes
    {
        public int IdPacote { get; set; }

        public string NomePacote { get; set; }

        public string Descricao { get; set; }

        public DateTime DataIda { get; set; }

        public DateTime DataVolta { get; set; }

        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O Ativo é Obrigatorio, true para o pacote estas ativo e false para o pacote Inativo")]
        public bool Ativo { get; set; }

        public string NomeCidade { get; set; }
    }
}
