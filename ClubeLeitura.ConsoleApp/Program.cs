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
using ClubeLeitura.ConsoleApp.ModuloRevista;
using System;

namespace ClubeLeitura.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TelaMenuPrincipal menuPrincipal = new TelaMenuPrincipal();
            Notificador notificador = new Notificador();

            // Instanciação de Caixas
            RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
            repositorioCaixa.caixas = new Caixa[10];

            TelaCadastroCaixa telaCadastroCaixa = new TelaCadastroCaixa();
            telaCadastroCaixa.repositorioCaixa = repositorioCaixa; 
            telaCadastroCaixa.notificador = notificador;

            // Instanciação de Revistas
            RepositorioRevista repositorioRevista = new RepositorioRevista();
            repositorioRevista.revistas = new Revista[10];

            TelaCadastroRevista telaCadastroRevista = new TelaCadastroRevista();
            telaCadastroRevista.notificador = notificador;
            telaCadastroRevista.telaCadastroCaixa = telaCadastroCaixa;
            telaCadastroRevista.repositorioCaixa = repositorioCaixa;
            telaCadastroRevista.repositorioRevista = repositorioRevista;

            // Instanciação de Amigos
            RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
            repositorioAmigo.amigos = new Amigo[10];

            TelaCadastroAmigo telaCadastroAmigo = new TelaCadastroAmigo();
            telaCadastroAmigo.notificador = notificador;
            telaCadastroAmigo.repositorioAmigo = repositorioAmigo;

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
                else if (opcaoMenuPrincipal == "2") // Cadastro de Revistas
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

                else if (opcaoMenuPrincipal == "3") // Cadastro de Amigos
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
                }
            }
        }
    }
}
