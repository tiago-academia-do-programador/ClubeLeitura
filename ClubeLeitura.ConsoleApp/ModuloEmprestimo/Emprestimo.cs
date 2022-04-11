using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloRevista;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloEmprestimo
{
    public class Emprestimo : EntidadeBase
    {
        public Amigo amigo;
        public Revista revista;
        private DateTime dataEmprestimo;
        private DateTime dataDevolucao;
        private bool estaAberto;
        public bool EstaAberto { get => estaAberto; }
        public DateTime DataEmprestimo { get => dataEmprestimo; }
        public string Status { get => EstaAberto ? "Aberto" : "Fechado"; }

        public Emprestimo(Amigo amigo, Revista revista)
        {
            this.amigo = amigo;
            this.revista = revista;

            Abrir();
        }

        public override string ToString()
        {
            return "Número: " + numero + Environment.NewLine +
                "Revista emprestada: " + revista.Colecao + Environment.NewLine +
                "Nome do Amigo: " + amigo.Nome + Environment.NewLine +
                "Data do empréstimo: " + DataEmprestimo.ToShortDateString() + Environment.NewLine +
                "Status do empréstimo: " + Status + Environment.NewLine;
        }

        public void Abrir()
        {
            if (!estaAberto)
            {
                estaAberto = true;
                dataEmprestimo = DateTime.Today;
                dataDevolucao = dataEmprestimo.AddDays(revista.categoria.DiasEmprestimo);

                amigo.RegistrarEmprestimo(this);
                revista.RegistrarEmprestimo(this);
            }
        }

        public void Fechar()
        {
            if (estaAberto)
            {
                estaAberto = false;

                DateTime dataRealDevolucao = DateTime.Today;

                bool devolucaoAtrasada = dataRealDevolucao > dataDevolucao;

                if (devolucaoAtrasada)
                {
                    int diasAtrasados = (dataRealDevolucao - dataDevolucao).Days;

                    decimal valorMulta = 10 * diasAtrasados;

                    amigo.RegistrarMulta(valorMulta);
                }
            }
        }

        public override ResultadoValidacao Validar()
        {
            return new ResultadoValidacao(new List<string>());
        }
    }
}
