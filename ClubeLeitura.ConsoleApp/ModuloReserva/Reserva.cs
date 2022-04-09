using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloRevista;
using System;

namespace ClubeLeitura.ConsoleApp.ModuloReserva
{
    public class Reserva : EntidadeBase
    {
        public Amigo amigo;
        public Revista revista;
        public DateTime dataInicialReserva;
        private bool estaAberta;

        public bool EstaAberta { get => estaAberta; }

        public string Status { get => EstaAberta ? "Aberta" : "Fechada"; }

        public Reserva(Amigo amigo, Revista revista)
        {
            this.amigo = amigo;
            this.revista = revista;
        }

        public void Abrir()
        {
            if (!estaAberta)
            {
                estaAberta = true;
                dataInicialReserva = DateTime.Today;

                amigo.RegistrarReserva(this);
                revista.RegistrarReserva(this);
            }
        }

        public void Fechar()
        {
            if (estaAberta)
                estaAberta = false;
        }

        public bool EstaExpirada()
        {
            bool ultrapassouDataReserva = dataInicialReserva.AddDays(2) > DateTime.Today;

            if (ultrapassouDataReserva)
                estaAberta = false;

            return ultrapassouDataReserva;
        }
        
        public override string ToString()
        {
            return "Número: " + numero + Environment.NewLine +
                "Revista emprestada: " + revista.Colecao + Environment.NewLine +
                "Nome do Amigo: " + amigo.Nome + Environment.NewLine +
                "Data da reserva: " + dataInicialReserva.ToShortDateString() + Environment.NewLine +
                "Status da reserva: " + Status + Environment.NewLine +
                "Data de expiração da Reserva: " + dataInicialReserva.AddDays(2).ToShortDateString() + Environment.NewLine;
            ;
        }

        public override ResultadoValidacao Validar()
        {
            throw new NotImplementedException();
        }
    }
}
