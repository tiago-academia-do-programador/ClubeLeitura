using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeLeitura.ConsoleApp.ModuloReserva;

namespace ClubeLeitura.ConsoleApp
{
    internal class Program
    {
        static Notificador notificador = new Notificador();
        static TelaMenuPrincipal menuPrincipal = new TelaMenuPrincipal(notificador);

        static void Main(string[] args)
        {
            while (true)
            {
                TelaBase telaSelecionada = menuPrincipal.ObterTela();

                if (telaSelecionada is null)
                    return;

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is ITelaCadastravel)
                    GerenciarCadastroBasico(telaSelecionada, opcaoSelecionada);

                else if (telaSelecionada is TelaCadastroEmprestimo)
                    GerenciarCadastroEmprestimos(telaSelecionada, opcaoSelecionada);

                else if (telaSelecionada is TelaCadastroReserva)
                    GerenciarCadastroReservas(telaSelecionada, opcaoSelecionada);
            }
        }

        private static void GerenciarCadastroReservas(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastroReserva telaCadastroReserva = telaSelecionada as TelaCadastroReserva;

            if (telaCadastroReserva is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroReserva.RegistrarNovaReserva();

            else if (opcaoSelecionada == "2")
                telaCadastroReserva.VisualizarReservas("Tela");

            else if (opcaoSelecionada == "3")
                telaCadastroReserva.VisualizarReservasEmAberto("Tela");

            else if (opcaoSelecionada == "4")
                telaCadastroReserva.RegistrarNovoEmprestimo();
        }

        private static void GerenciarCadastroEmprestimos(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastroEmprestimo telaCadastroEmprestimo = telaSelecionada as TelaCadastroEmprestimo;

            if (telaCadastroEmprestimo is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroEmprestimo.RegistrarEmprestimo();

            else if (opcaoSelecionada == "2")
                telaCadastroEmprestimo.EditarEmprestimo();

            else if (opcaoSelecionada == "3")
                telaCadastroEmprestimo.ExcluirEmprestimo();

            else if (opcaoSelecionada == "4")
                telaCadastroEmprestimo.VisualizarEmprestimos("Tela");

            else if (opcaoSelecionada == "5")
                telaCadastroEmprestimo.VisualizarEmprestimosEmAberto("Tela");

            else if (opcaoSelecionada == "6")
                telaCadastroEmprestimo.RegistrarDevolucao();
        }

        public static void GerenciarCadastroBasico(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            ITelaCadastravel telaCadastroBasico = telaSelecionada as ITelaCadastravel;

            if (telaCadastroBasico is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroBasico.InserirRegistro();

            else if (opcaoSelecionada == "2")
                telaCadastroBasico.EditarRegistro();

            else if (opcaoSelecionada == "3")
                telaCadastroBasico.ExcluirRegistro();

            else if (opcaoSelecionada == "4")
                telaCadastroBasico.VisualizarRegistros("Tela");

            TelaCadastroAmigo telaCadastroAmigo = telaCadastroBasico as TelaCadastroAmigo;

            if (telaCadastroAmigo is null)
                return;

            if (opcaoSelecionada == "5")
                telaCadastroAmigo.VisualizarAmigosComMulta("Tela");

            else if (opcaoSelecionada == "6")
                telaCadastroAmigo.PagarMulta();
        }
    }
}
