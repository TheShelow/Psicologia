﻿namespace Entities
{
    public class Cidade
    {
        public int ID { get; set; }
        public string NomeCidade { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }
    }
}