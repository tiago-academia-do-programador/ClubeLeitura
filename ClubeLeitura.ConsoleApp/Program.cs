/**
 * O sistema deve permirtir o usuário escolher qual opção ele deseja
 *  -Para acessar o cadastro de caixas, ele deve digitar "1"
 *  -Para acessar o cadastro de revistas, ele deve digitar "2"
 *  -Para acessar o cadastro de amigquinhos, ele deve digitar "3"
 *  
 *  -Para gerenciar emprestimos, ele deve digitar "4"
 *  
 *  -Para sair, usuário deve digitar "s"
**/
using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloCaixa;
using ClubeLeitura.ConsoleApp.ModuloCategoria;
using ClubeLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeLeitura.ConsoleApp.ModuloReserva;
using ClubeLeitura.ConsoleApp.ModuloRevista;
using System;

namespace ClubeLeitura.ConsoleApp
{
    internal class Program
    {
        private const int QUANTIDADE_REGISTROS = 10;

        static void Main(string[] args)
        {
            TelaMenuPrincipal menuPrincipal = new TelaMenuPrincipal();
            Notificador notificador = new Notificador();

            // Instanciação de Caixas
            RepositorioCaixa repositorioCaixa = new RepositorioCaixa(QUANTIDADE_REGISTROS);

            TelaCadastroCaixa telaCadastroCaixa = new TelaCadastroCaixa(repositorioCaixa, notificador);

            // Instanciação de Categorias
            RepositorioCategoria repositorioCategoria = new RepositorioCategoria(QUANTIDADE_REGISTROS);

            TelaCadastroCategoria telaCadastroCategoria = new TelaCadastroCategoria(repositorioCategoria, notificador);

            // Instanciação de Revistas
            RepositorioRevista repositorioRevista = new RepositorioRevista(QUANTIDADE_REGISTROS);

            TelaCadastroRevista telaCadastroRevista = new TelaCadastroRevista(
                telaCadastroCategoria,
                repositorioCategoria,
                telaCadastroCaixa,
                repositorioCaixa,
                repositorioRevista,
                notificador
            );

            // Instanciação de Amigos
            RepositorioAmigo repositorioAmigo = new RepositorioAmigo(QUANTIDADE_REGISTROS);

            TelaCadastroAmigo telaCadastroAmigo = new TelaCadastroAmigo(repositorioAmigo, notificador);

            // Instanciação de Empréstimos
            RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo(QUANTIDADE_REGISTROS);

            TelaCadastroEmprestimo telaCadastroEmprestimo = new TelaCadastroEmprestimo(
                    notificador,
                    repositorioEmprestimo,
                    repositorioRevista,
                    repositorioAmigo,
                    telaCadastroRevista,
                    telaCadastroAmigo
                );

            // Instanciação de Reservas
            RepositorioReserva repositorioReserva = new RepositorioReserva(QUANTIDADE_REGISTROS);

            TelaCadastroReserva telaCadastroReserva = new TelaCadastroReserva(
                notificador,
                repositorioReserva,
                repositorioAmigo,
                repositorioRevista,
                telaCadastroAmigo,
                telaCadastroRevista,
                repositorioEmprestimo
            );

            while (true)
            {
                string opcaoMenuPrincipal = menuPrincipal.MostrarOpcoes();

                if (opcaoMenuPrincipal == "1") // Cadastro de Caixas
                {
                    string opcao = telaCadastroCaixa.MostrarOpcoes();

                    if (opcao == "1")
                    {
                        telaCadastroCaixa.InserirNovaCaixa();
                    }
                    else if (opcao == "2")
                    {
                        telaCadastroCaixa.EditarCaixa();
                    }
                    else if (opcao == "3")
                    {
                        telaCadastroCaixa.ExcluirCaixa();
                    }
                    else if (opcao == "4")
                    {
                        bool temCaixaCadastrada = telaCadastroCaixa.VisualizarCaixas("Tela");

                        if (temCaixaCadastrada == false)
                            notificador.ApresentarMensagem("Nenhuma caixa cadastrada", TipoMensagem.Atencao);

                        Console.ReadLine();
                    }
                }
                else if (opcaoMenuPrincipal == "2") // Cadastro de Categorias
                {
                    string opcao = telaCadastroCategoria.MostrarOpcoes();

                    if (opcao == "1")
                    {
                        telaCadastroCategoria.InserirNovaCategoria();
                    }
                    else if (opcao == "2")
                    {
                        telaCadastroCategoria.EditarCategoria();
                    }
                    else if (opcao == "3")
                    {
                        telaCadastroCategoria.ExcluirCategoria();
                    }
                    else if (opcao == "4")
                    {
                        bool temCategoriasCadastradas = telaCadastroCategoria.VisualizarCategorias("Tela");

                        if (!temCategoriasCadastradas)
                            notificador.ApresentarMensagem("Nenhuma categoria cadastrada.", TipoMensagem.Atencao);

                        Console.ReadLine();
                    }
                }
                else if (opcaoMenuPrincipal == "3") // Cadastro de Revistas
                {
                    string opcao = telaCadastroRevista.MostrarOpcoes();

                    if (opcao == "1")
                    {
                        telaCadastroRevista.InserirNovaRevista();
                    }
                    else if (opcao == "2")
                    {
                        telaCadastroRevista.EditarRevista();
                    }
                    else if (opcao == "3")
                    {
                        telaCadastroRevista.ExcluirRevista();
                    }
                    else if (opcao == "4")
                    {
                        bool temRevistaCadastrada = telaCadastroRevista.VisualizarRevistas("Tela");

                        if (!temRevistaCadastrada)
                            notificador.ApresentarMensagem("Nenhuma revista cadastrada", TipoMensagem.Atencao);

                        Console.ReadLine();
                    }
                }
                else if (opcaoMenuPrincipal == "4") // Cadastro de Amigos
                {
                    string opcao = telaCadastroAmigo.MostrarOpcoes();

                    if (opcao == "1")
                    {
                        telaCadastroAmigo.InserirNovoAmigo();
                    }
                    else if (opcao == "2")
                    {
                        telaCadastroAmigo.EditarAmigo();
                    }
                    else if (opcao == "3")
                    {
                        telaCadastroAmigo.ExcluirAmigo();
                    }
                    else if (opcao == "4")
                    {
                        bool temAmigoCadastrado = telaCadastroAmigo.VisualizarAmigos("Tela");

                        if (!temAmigoCadastrado)
                            notificador.ApresentarMensagem("Nenhum amigo cadastrado.", TipoMensagem.Atencao);

                        Console.ReadLine();
                    }
                    else if (opcao == "5")
                    {
                        bool temAmigoComMulta = telaCadastroAmigo.VisualizarAmigosComMulta("Tela");

                        if (!temAmigoComMulta)
                            notificador.ApresentarMensagem("Nenhum amigo com multa em aberto.", TipoMensagem.Atencao);

                        Console.ReadLine();
                    }
                    else if (opcao == "6")
                    {
                        telaCadastroAmigo.PagarMulta();
                    }
                }
                else if (opcaoMenuPrincipal == "5") // Cadastro de Empréstimos
                {
                    string opcao = telaCadastroEmprestimo.MostrarOpcoes();

                    if (opcao == "1")
                    {
                        telaCadastroEmprestimo.InserirNovoEmprestimo();
                    }
                    else if (opcao == "2")
                    {
                        telaCadastroEmprestimo.EditarEmprestimo();
                    }
                    else if (opcao == "3")
                    {
                        telaCadastroEmprestimo.ExcluirEmprestimo();
                    }
                    else if (opcao == "4")
                    {
                        bool temEmprestimoCadastrado = telaCadastroEmprestimo.VisualizarEmprestimos("Tela");

                        if (!temEmprestimoCadastrado)
                            notificador.ApresentarMensagem("Nenhum empréstimo cadastrado.", TipoMensagem.Atencao);

                        Console.ReadLine();
                    }
                    else if (opcao == "5")
                    {
                        bool temEmprestimoCadastrado = telaCadastroEmprestimo.VisualizarEmprestimosEmAberto("Tela");

                        if (!temEmprestimoCadastrado)
                            notificador.ApresentarMensagem("Nenhum empréstimo em aberto.", TipoMensagem.Atencao);

                        Console.ReadLine();
                    }
                    else if (opcao == "6")
                    {
                        telaCadastroEmprestimo.RegistrarDevolucao();
                    }
                }
                else if (opcaoMenuPrincipal == "6") // Cadastro de Reservas
                {
                    string opcao = telaCadastroReserva.MostrarOpcoes();

                    if (opcao == "1")
                    {
                        telaCadastroReserva.InserirNovaReserva();
                    }
                    else if (opcao == "2")
                    {
                        bool temReservaCadastrada = telaCadastroReserva.VisualizarReservas("Tela");

                        if (!temReservaCadastrada)
                            notificador.ApresentarMensagem("Nenhuma reserva cadastrada.", TipoMensagem.Atencao);

                        Console.ReadLine();
                    }
                    else if (opcao == "3")
                    {
                        bool temReservaCadastrada = telaCadastroReserva.VisualizarReservasEmAberto("Tela");

                        if (!temReservaCadastrada)
                            notificador.ApresentarMensagem("Nenhuma reserva em aberto disponível.", TipoMensagem.Atencao);

                        Console.ReadLine();
                    }
                    else if (opcao == "4")
                    {
                        telaCadastroReserva.RegistrarNovoEmprestimo();
                    }
                }
            }
        }
    }
}
