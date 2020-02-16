using System;

namespace Locadora.Models
{
    public class Filme
    {
        public string Id { get; set; }

        public string Titulo { get; set; }

        public string AtrizAtorPrincipal { get; set; }

        public string Diretor { get; set; }

        public int CopiasDisponiveis { get; set; }
    }
}
