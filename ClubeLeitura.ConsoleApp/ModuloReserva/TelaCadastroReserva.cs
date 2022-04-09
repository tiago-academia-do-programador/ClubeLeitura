﻿using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeLeitura.ConsoleApp.ModuloRevista;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloReserva
{
    public class TelaCadastroReserva : TelaBase
    {
        private readonly Notificador notificador;
        private readonly IRepositorio<Reserva> repositorioReserva;
        private readonly IRepositorio<Amigo> repositorioAmigo;
        private readonly IRepositorio<Revista> repositorioRevista;
        private readonly TelaCadastroAmigo telaCadastroAmigo;
        private readonly TelaCadastroRevista telaCadastroRevista;
        private readonly IRepositorio<Emprestimo> repositorioEmprestimo;

        public TelaCadastroReserva(
            Notificador notificador,
            RepositorioReserva repositorioReserva,
            IRepositorio<Amigo> repositorioAmigo,
            IRepositorio<Revista> repositorioRevista,
            TelaCadastroAmigo telaCadastroAmigo,
            TelaCadastroRevista telaCadastroRevista,
            IRepositorio<Emprestimo> repositorioEmprestimo) : base("Cadastro de Reservas")
        {
            this.notificador = notificador;
            this.repositorioReserva = repositorioReserva;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
            this.telaCadastroAmigo = telaCadastroAmigo;
            this.telaCadastroRevista = telaCadastroRevista;
            this.repositorioEmprestimo = repositorioEmprestimo;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Registrar Reserva");
            Console.WriteLine("Digite 2 para Visualizar");
            Console.WriteLine("Digite 3 para Visualizar Reservas em Aberto");
            Console.WriteLine("Digite 4 para Cadastrar um Empréstimo à partir de Reserva");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void RegistrarNovaReserva()
        {
            MostrarTitulo("Inserindo nova Reserva");

            // Validação do Amigo
            Amigo amigoSelecionado = ObtemAmigo();

            if (amigoSelecionado == null)
            {
                notificador.ApresentarMensagem("Nenhum amigo selecionado", TipoMensagem.Erro);
                return;
            }

            if (amigoSelecionado.TemMultaEmAberto())
            {
                notificador.ApresentarMensagem("Este amigo tem uma multa em aberto e não pode reservar.", TipoMensagem.Erro);
                return;
            }

            if (amigoSelecionado.TemReservaEmAberto())
            {
                notificador.ApresentarMensagem("Este amigo já possui uma reserva em aberto..", TipoMensagem.Erro);
                return;
            }

            if (amigoSelecionado.TemEmprestimoEmAberto())
            {
                notificador.ApresentarMensagem("Este amigo já possui uma reserva em aberto e não pode reservar.", TipoMensagem.Erro);
                return;
            }

            // Validação da Revista
            Revista revistaSelecionada = ObtemRevista();

            if (revistaSelecionada.EstaReservada())
            {
                notificador.ApresentarMensagem("A revista selecionada já está reservada!", TipoMensagem.Erro);
                return;
            }

            if (revistaSelecionada.EstaEmprestada())
            {
                notificador.ApresentarMensagem("A revista selecionada já foi emprestada.", TipoMensagem.Erro);
                return;
            }

            Reserva novaReserva = ObtemReserva(amigoSelecionado, revistaSelecionada);

            string statusValidacao = repositorioReserva.Inserir(novaReserva);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Reserva cadastrada com sucesso!", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        public void RegistrarNovoEmprestimo()
        {
            MostrarTitulo("Registrando novo Empréstimo");

            Reserva reservaParaEmprestimo = ObtemReservaParaEmprestimo();

            reservaParaEmprestimo.Fechar();

            Emprestimo novoEmprestimo 
                = new Emprestimo(reservaParaEmprestimo.amigo, reservaParaEmprestimo.revista);

            string statusValidacao = repositorioEmprestimo.Inserir(novoEmprestimo);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Empréstimo cadastrado com sucesso", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        public bool VisualizarReservas(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Reservas");

            List<Reserva> reservas = repositorioReserva.SelecionarTodos();

            if (reservas.Count == 0)
                return false;

            foreach (Reserva reserva in reservas)
                Console.WriteLine(reserva.ToString());

            return true;
        }

        public bool VisualizarReservasEmAberto(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Reservas em Aberto");

            List<Reserva> reservas = repositorioReserva.Filtrar(x => x.estaAberta);

            if (reservas.Count == 0)
                return false;

            foreach (Reserva reserva in reservas)
                Console.WriteLine(reserva.ToString());

            return true;
        }

        private Reserva ObtemReserva(Amigo amigoSelecionado, Revista revistaSelecionada)
        {
            Reserva novaReserva = new Reserva(amigoSelecionado, revistaSelecionada);

            return novaReserva;
        }

        public Reserva ObtemReservaParaEmprestimo()
        {
            bool temReservasEmAberto = VisualizarReservasEmAberto("Pesquisando");

            if (!temReservasEmAberto)
            {
                notificador.ApresentarMensagem("Não há nenhuma reserva aberta disponível para cadastro.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número da reserva que irá emprestar: ");
            int numeroReserva = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Reserva reservaSelecionada = repositorioReserva.SelecionarRegistro(x => x.numero == numeroReserva);

            return reservaSelecionada;
        }

        public Amigo ObtemAmigo()
        {
            bool temAmigosDisponiveis = telaCadastroAmigo.VisualizarRegistros("Pesquisando");

            if (!temAmigosDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhum amigo disponível para cadastrar reservas.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do amigo que irá fazer a reserva: ");
            int numeroAmigoEmprestimo = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Amigo amigoSelecionado = repositorioAmigo.SelecionarRegistro(x => x.numero == numeroAmigoEmprestimo);

            return amigoSelecionado;
        }

        public Revista ObtemRevista()
        {
            bool temRevistasDisponiveis = telaCadastroRevista.VisualizarRegistros("Pesquisando");

            if (!temRevistasDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhuma revista disponível para cadastrar reservas.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número da revista que será reservada: ");
            int numeroRevistaEmprestimo = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Revista revistaSelecionada = repositorioRevista.SelecionarRegistro(x => x.numero == numeroRevistaEmprestimo);

            return revistaSelecionada;
        }
    }
}
