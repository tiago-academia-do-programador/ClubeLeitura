using ClubeLeitura.ConsoleApp.Compartilhado;

namespace ClubeLeitura.ConsoleApp.ModuloReserva
{
    public class RepositorioReserva : RepositorioBase
    {
        public RepositorioReserva(int qtdReservas) : base(qtdReservas)
        {
        }

        public override string Inserir(EntidadeBase reserva)
        {
            Reserva r = (Reserva)reserva;

            r.numero = ++contadorNumero;

            r.Abrir();

            registros[ObterPosicaoVazia()] = reserva;

            return "REGISTRO_VALIDO";
        }

        public Reserva[] SelecionarReservasEmAberto()
        {
            Reserva[] reservasInseridas = new Reserva[ObterQtdReservasEmAberto()];

            int j = 0;

            for (int i = 0; i < reservasInseridas.Length; i++)
            {
                Reserva r = (Reserva)registros[i];

                if (r != null && r.estaAberta)
                {
                    reservasInseridas[j] = r;
                    j++;
                }
            }

            return reservasInseridas;
        }

        public int ObterQtdReservasEmAberto()
        {
            int numeroReservas = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                Reserva r = (Reserva)registros[i];

                if (r != null && r.estaAberta)
                    numeroReservas++;
            }

            return numeroReservas;
        }
    }
}
