using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloRevista;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloCategoria
{
    public class Categoria : EntidadeBase
    {
        private readonly string nome;
        private readonly int diasEmprestimo;

        public Revista[] revistas;

        public string Nome => nome;

        public int DiasEmprestimo => diasEmprestimo;

        public Categoria(string nome, int diasEmprestimo)
        {
            this.nome = nome;
            this.diasEmprestimo = diasEmprestimo;
        }

        public override ResultadoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (diasEmprestimo < 1)
                erros.Add("Um empréstimo precisa ser feito por pelo menos um dia!");

            if (string.IsNullOrEmpty(nome))
                erros.Add("Por favor insira um nome válido para a categoria!");

            return new ResultadoValidacao(erros);
        }
    }
}
