using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloCaixa;
using ClubeLeitura.ConsoleApp.ModuloCategoria;
using ClubeLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeLeitura.ConsoleApp.ModuloReserva;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloRevista
{
    public class Revista : EntidadeBase
    {
        private readonly string colecao;
        private readonly int edicao;
        private readonly int ano;
        public Caixa caixa;
        public Categoria categoria;

        public Emprestimo[] historicoEmprestimos = new Emprestimo[10];
        public Reserva[] historicoReservas = new Reserva[10];

        public string Colecao => colecao;

        public int Edicao => edicao;

        public int Ano => ano;

        public Revista(string colecao, int edicao, int ano)
        {
            this.colecao = colecao;
            this.edicao = edicao;
            this.ano = ano;
        }
        public override ResultadoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(Colecao))
                erros.Add("É necessário incluir uma coleção!");

            if (Edicao < 0)
                erros.Add("A edição de uma revista não pode ser menor que zero!");

            if (Ano < 0 || Ano > DateTime.Now.Year)
                erros.Add("O ano da revista precisa ser válido!");

            return new ResultadoValidacao(erros);
        }

        public void RegistrarReserva(Reserva reserva)
        {
            historicoReservas[ObtemPosicaoReservasVazia()] = reserva;
        }

        public void RegistrarEmprestimo(Emprestimo emprestimo)
        {
            historicoEmprestimos[ObtemPosicaoEmprestimosVazia()] = emprestimo;
        }

        public bool EstaReservada()
        {
            bool temReservaEmAberto = false;

            foreach (Reserva reserva in historicoReservas)
            {
                if (reserva != null && reserva.estaAberta)
                {
                    temReservaEmAberto = true;
                    break;
                }
            }

            return temReservaEmAberto;
        }

        public bool EstaEmprestada()
        {
            bool temEmprestimoEmAberto = false;

            foreach (Emprestimo emprestimo in historicoEmprestimos)
            {
                if (emprestimo != null && emprestimo.EstaAberto)
                {
                    temEmprestimoEmAberto = true;
                    break;
                }
            }
            return temEmprestimoEmAberto;
        }


        public int ObtemPosicaoReservasVazia()
        {
            for (int i = 0; i < historicoReservas.Length; i++)
            {
                if (historicoReservas[i] == null)
                    return i;
            }

            return -1;
        }

        public int ObtemPosicaoEmprestimosVazia()
        {
            for (int i = 0; i < historicoEmprestimos.Length; i++)
            {
                if (historicoEmprestimos[i] == null)
                    return i;
            }

            return -1;
        }
    }
}