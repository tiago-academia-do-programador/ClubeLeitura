using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloRevista;
using System;

namespace ClubeLeitura.ConsoleApp.ModuloEmprestimo
{
    public class Emprestimo : EntidadeBase, ITransacao
    {
        public Amigo amigo;
        public Revista revista;
        private DateTime dataEmprestimo;
        private DateTime dataDevolucao;

        public bool EstaAberto { get; set; }
        public DateTime DataEmprestimo { get => dataEmprestimo; }
        public string Status { get => EstaAberto ? "Aberto" : "Fechado"; }

        public Emprestimo(Amigo amigo, Revista revista)
        {
            this.amigo = amigo;
            this.revista = revista;
        }

        public void Abrir()
        {
            if (!EstaAberto)
            {
                EstaAberto = true;
                dataEmprestimo = DateTime.Today;
                dataDevolucao = dataEmprestimo.AddDays(revista.categoria.DiasEmprestimo);
            }
        }

        public void Fechar()
        {
            if (EstaAberto)
            {
                EstaAberto = false;

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

        public override string ToString()
        {
            return "Número: " + numero + Environment.NewLine +
                "Revista emprestada: " + revista.Colecao + Environment.NewLine +
                "Nome do Amigo: " + amigo.Nome + Environment.NewLine +
                "Data do empréstimo: " + DataEmprestimo.ToShortDateString() + Environment.NewLine +
                "Status do empréstimo: " + Status + Environment.NewLine;
        }

        public override ResultadoValidacao Validar()
        {
            throw new NotImplementedException();
        }
    }
}
