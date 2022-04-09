using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeLeitura.ConsoleApp.ModuloReserva;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloAmigo
{
    public class Amigo : EntidadeBase
    {
        private readonly string nome;
        private readonly string nomeResponsavel;
        private readonly string telefone;
        private readonly string endereco;
        public Multa Multa { get; set; }

        private readonly List<Emprestimo> historicoEmprestimos = new List<Emprestimo>();
        private readonly List<Reserva> historicoReservas = new List<Reserva>();

        public string Nome => nome;

        public string NomeResponsavel => nomeResponsavel;

        public string Telefone => telefone;

        public string Endereco => endereco;

        public Amigo(string nome, string nomeResponsavel, string telefone, string endereco)
        {
            this.nome = nome;
            this.nomeResponsavel = nomeResponsavel;
            this.telefone = telefone;
            this.endereco = endereco;
        }

        public override string ToString()
        {
            return "Número: " + numero + Environment.NewLine +
            "Nome: " + Nome + Environment.NewLine +
            "Nome do responsável: " + NomeResponsavel + Environment.NewLine +
            "Onde mora: " + Endereco + Environment.NewLine +
            "Multa: R$" + Multa.Valor + Environment.NewLine;
        }

        public void RegistrarEmprestimo(Emprestimo emprestimo)
        {
            historicoEmprestimos.Add(emprestimo);
        }

        public void RegistrarReserva(Reserva reserva)
        {
            historicoReservas.Add(reserva);
        }

        public bool TemReservaEmAberto()
        {
            bool temReservaEmAberto = false;

            foreach (Reserva reserva in historicoReservas)
            {
                if (reserva.EstaAberta)
                {
                    temReservaEmAberto = true;
                    break;
                }
            }

            return temReservaEmAberto;
        }

        public bool TemEmprestimoEmAberto()
        {
            bool temEmprestimoEmAberto = false;

            foreach (Emprestimo emprestimo in historicoEmprestimos)
            {
                if (emprestimo.EstaAberto)
                {
                    temEmprestimoEmAberto = true;
                    break;
                }
            }

            return temEmprestimoEmAberto;
        }

        public void RegistrarMulta(decimal valor)
        {
            Multa novaMulta = new Multa(valor);

            Multa = novaMulta;
        }

        public void PagarMulta()
        {
            if (Multa != null)
                Multa = null;
        }

        public bool TemMultaEmAberto()
        {
            if (Multa == null)
                return false;

            return true;
        }

        public override ResultadoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(nome))
                erros.Add("Um amigo precisa ter um nome válido!");

            if (string.IsNullOrEmpty(nomeResponsavel))
                erros.Add("Um amigo precisa ter um responsável!");

            if (telefone.Length < 9)
                erros.Add("Um amigo precisa ter um número de telefone com 9 digitos!");

            if (string.IsNullOrEmpty(endereco))
                erros.Add("Um amigo ter um endereço válido!");

            return new ResultadoValidacao(erros);
        }
    }
}