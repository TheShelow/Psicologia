﻿namespace Entities.EntityProjections
{
    public class FuncionarioModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public Endereco Endereco { get; set; }
        public string Cargo { get; set; }
    }
}