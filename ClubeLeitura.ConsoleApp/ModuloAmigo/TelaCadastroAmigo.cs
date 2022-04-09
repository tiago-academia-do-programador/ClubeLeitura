using ClubeLeitura.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloAmigo
{
    public class TelaCadastroAmigo : TelaBase, ITelaCadastravel
    {
        private readonly Notificador notificador;
        private readonly IRepositorio<Amigo> repositorioAmigo;

        public TelaCadastroAmigo(IRepositorio<Amigo> repositorioAmigo, Notificador notificador)
            : base ("Cadastro de Amigos")
        {
            this.repositorioAmigo = repositorioAmigo;
            this.notificador = notificador;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 5 para Visualizar Amigos com Multa");
            Console.WriteLine("Digite 6 para Pagar Multas");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void PagarMulta()
        {
            MostrarTitulo("Pagamento de Multas");

            bool temAmigosComMulta = VisualizarAmigosComMulta("Pesquisando");

            if (!temAmigosComMulta)
            {
                notificador.ApresentarMensagem("Não há nenhum amigo com multas em aberto", TipoMensagem.Atencao);
                return;
            }

            int numeroAmigoComMulta = ObterNumeroAmigo();

            Amigo amigoComMulta = repositorioAmigo
                    .SelecionarRegistro(x => x.TemMultaEmAberto() && x.numero == numeroAmigoComMulta);

            amigoComMulta.PagarMulta();
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo novo amigo");

            Amigo novoAmigo = ObterAmigo();

            string statusValidacao = repositorioAmigo.Inserir(novoAmigo);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Amigo cadastrado com sucesso!", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Amigo");

            bool temAmigosCadastrados = VisualizarRegistros("Pesquisando");

            if (temAmigosCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhum amigo cadastrado para poder editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroAmigo = ObterNumeroAmigo();

            Amigo amigoAtualizado = ObterAmigo();

            bool conseguiuEditar = repositorioAmigo.Editar(x => x.numero == numeroAmigo, amigoAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Amigo editado com sucesso", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Amigo");

            bool temAmigosCadastrados = VisualizarRegistros("Pesquisando");

            if (temAmigosCadastrados == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhum amigo cadastrado para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int numeroAmigo = ObterNumeroAmigo();

            bool conseguiuExcluir = repositorioAmigo.Excluir(x => x.numero == numeroAmigo);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem("Amigo excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Amigos");

            List<Amigo> amigos = repositorioAmigo.SelecionarTodos();

            if (amigos.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhum amigo disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Amigo amigo in amigos)
                Console.WriteLine(amigo.ToString());

            return true;
        }

        public bool VisualizarAmigosComMulta(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Amigos com Multa");

            List<Amigo> amigosComMulta = repositorioAmigo.Filtrar(x => x.TemMultaEmAberto());

            if (amigosComMulta.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhum amigo com multa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Amigo amigo in amigosComMulta)
                Console.WriteLine(amigo.ToString());

            return true;
        }

        #region Métodos privados
        private int ObterNumeroAmigo()
        {
            int numeroAmigo;
            bool numeroAmigoEncontrado;

            do
            {
                Console.Write("Digite o número do amigo que deseja selecionar: ");
                numeroAmigo = Convert.ToInt32(Console.ReadLine());

                numeroAmigoEncontrado = repositorioAmigo.ExisteRegistro(x => x.numero == numeroAmigo);

                if (numeroAmigoEncontrado == false)
                    notificador.ApresentarMensagem("Número do amigo não encontrado, digite novamente.", TipoMensagem.Atencao);

            } while (numeroAmigoEncontrado == false);
            return numeroAmigo;
        }

        private Amigo ObterAmigo()
        {
            Console.Write("Digite o nome do amigo: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o nome do responsável: ");
            string nomeResponsavel = Console.ReadLine();

            Console.Write("Digite o número do telefone: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite onde o amigo mora: ");
            string endereco = Console.ReadLine();

            Amigo amigo = new Amigo(nome, nomeResponsavel, telefone, endereco);

            return amigo;
        }
        #endregion
    }
}
