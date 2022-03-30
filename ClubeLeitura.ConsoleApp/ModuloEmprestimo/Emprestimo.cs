using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloRevista;
using System;

namespace ClubeLeitura.ConsoleApp.ModuloEmprestimo
{
    public class Emprestimo
    {
        public int numero;

        public Amigo amigo;
        public Revista revista;
        public DateTime dataEmprestimo;
        public DateTime dataDevolucao;

        public bool estaAberto;

        public void Abrir()
        {
            if (!estaAberto)
            {
                estaAberto = true;
                dataEmprestimo = DateTime.Today;
                dataDevolucao = dataEmprestimo.AddDays(revista.categoria.diasEmprestimo);
            }
        }

        public void Fechar()
        {
            if (estaAberto)
            {
                estaAberto = false;

                DateTime dataRealEmprestimo = DateTime.Today;

                bool devolucaoAtrasada = dataRealEmprestimo > dataDevolucao;

                if (devolucaoAtrasada)
                {
                    int diasAtrasados = (dataRealEmprestimo - dataDevolucao).Days;

                    decimal valorMulta = 10 * diasAtrasados;

                    amigo.RegistrarMulta(valorMulta);
                }
            }
        }

    }
}
