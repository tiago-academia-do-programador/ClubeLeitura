using ClubeLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeLeitura.ConsoleApp.ModuloReserva;
using System;

namespace ClubeLeitura.ConsoleApp.ModuloAmigo
{
    public class Amigo
    {
        public int numero;

        public string nome;
        public string nomeResponsavel;
        public string telefone;
        public string endereco;

        public Emprestimo[] historicoEmprestimos = new Emprestimo[10];
        public Reserva[] historicoReservas = new Reserva[10];
        public Multa multa;

        public void RegistrarEmprestimo(Emprestimo emprestimo)
        {
            historicoEmprestimos[ObtemPosicaoVazia()] = emprestimo;
        }

        public void RegistrarReserva(Reserva reserva)
        {
            historicoReservas[ObtemPosicaoReservasVazia()] = reserva;
        }

        public bool TemReservaEmAberto()
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

        public bool TemEmprestimoEmAberto()
        {
            bool temEmprestimoEmAberto = false;

            foreach (Emprestimo emprestimo in historicoEmprestimos)
            {
                if (emprestimo != null && emprestimo.estaAberto)
                {
                    temEmprestimoEmAberto = true;
                    break;
                }
            }
            return temEmprestimoEmAberto;
        }

        private int ObtemPosicaoVazia()
        {
            for (int i = 0; i < historicoEmprestimos.Length; i++)
            {
                if (historicoEmprestimos[i] == null)
                    return i;
            }

            return -1;
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

        public void RegistrarMulta(decimal valorMulta)
        {
            Multa novaMulta = new Multa();

            novaMulta.valor = valorMulta;

            multa = novaMulta;
        }

        public void PagarMulta()
        {
            if (multa != null)
                multa = null;
        }

        public bool TemMultaEmAberto()
        {
            if (multa == null)
                return false;

            return true;
        }
    }
}