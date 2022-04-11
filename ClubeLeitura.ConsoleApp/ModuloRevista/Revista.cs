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

        public List<Emprestimo> historicoEmprestimos = new List<Emprestimo>();
        public List<Reserva> historicoReservas = new List<Reserva>();

        public string Colecao => colecao;

        public int Edicao => edicao;

        public int Ano => ano;

        public Revista(string colecao, int edicao, int ano, Caixa caixaSelecionada, Categoria categoriaSelecionada)
        {
            this.colecao = colecao;
            this.edicao = edicao;
            this.ano = ano;

            caixa = caixaSelecionada;
            categoria = categoriaSelecionada;
        }

        public override string ToString()
        {
            return "Número: " + numero + Environment.NewLine +
                "Categoria: " + categoria.Nome + Environment.NewLine +
                "Coleção: " + Colecao + Environment.NewLine +
                "Edição: " + Edicao + Environment.NewLine +
                "Ano: " + Ano + Environment.NewLine +
                "Caixa que está guardada: " + caixa.Cor + Environment.NewLine;
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
             historicoReservas.Add(reserva);
        }

        public void RegistrarEmprestimo(Emprestimo emprestimo)
        {
            historicoEmprestimos.Add(emprestimo);
        }

        public bool EstaReservada()
        {
            bool temReservaEmAberto = false;

            foreach (Reserva reserva in historicoReservas)
            {
                if (reserva != null && reserva.EstaAberta)
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
    }
}