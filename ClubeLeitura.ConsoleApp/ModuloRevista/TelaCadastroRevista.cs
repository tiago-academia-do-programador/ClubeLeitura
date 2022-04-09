using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloCaixa;
using ClubeLeitura.ConsoleApp.ModuloCategoria;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloRevista
{
    public class TelaCadastroRevista : TelaBase, ITelaCadastravel
    {
        private readonly TelaCadastroCaixa telaCadastroCaixa;
        private readonly TelaCadastroCategoria telaCadastroCategoria;
        private readonly IRepositorio<Categoria> repositorioCategoria;
        private readonly IRepositorio<Caixa> repositorioCaixa;
        private readonly IRepositorio<Revista> repositorioRevista;
        private readonly Notificador notificador;

        public TelaCadastroRevista(
            TelaCadastroCategoria telaCadastroCategoria,
            IRepositorio<Categoria> repositorioCategoria,
            TelaCadastroCaixa telaCadastroCaixa,
            IRepositorio<Caixa> repositorioCaixa,
            IRepositorio<Revista> repositorioRevista,
            Notificador notificador) : base("Cadastro de Revistas")
        {
            this.telaCadastroCategoria = telaCadastroCategoria;
            this.repositorioCategoria = repositorioCategoria;
            this.telaCadastroCaixa = telaCadastroCaixa;
            this.repositorioCaixa = repositorioCaixa;
            this.repositorioRevista = repositorioRevista;
            this.notificador = notificador;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo nova revista");

            Caixa caixaSelecionada = ObtemCaixa();

            Categoria categoriaSelecionada = ObtemCategoria();

            if (caixaSelecionada == null || categoriaSelecionada == null)
            {
                notificador
                    .ApresentarMensagem("Cadastre uma caixa e uma categoria antes de cadastrar revistas!", TipoMensagem.Atencao);
                return;
            }

            Revista novaRevista = ObterRevista(caixaSelecionada, categoriaSelecionada);

            string statusValidacao = repositorioRevista.Inserir(novaRevista);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Revista inserida com sucesso", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Revista");

            bool temRevistasCadastradas = VisualizarRegistros("Pesquisando");

            if (temRevistasCadastradas == false)
            {
                notificador.ApresentarMensagem("Nenhuma revista cadastrada para poder editar", TipoMensagem.Atencao);
                return;
            }

            int numeroRevista = ObterNumeroRevista();

            Console.WriteLine();
            
            Caixa caixaSelecionada = ObtemCaixa();

            Categoria categoriaSelecionada = ObtemCategoria();

            Revista revistaAtualizada = ObterRevista(caixaSelecionada, categoriaSelecionada);

            bool conseguiuEditar = repositorioRevista.Editar(x => x.numero == numeroRevista, revistaAtualizada);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem("Revista editada com sucesso", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Revista");

            bool temRevistasCadastradas = VisualizarRegistros("Pesquisando");

            if (temRevistasCadastradas == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhuma revista cadastrada para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int numeroRevista = ObterNumeroRevista();

            bool conseguiuExcluir = repositorioRevista.Excluir(x => x.numero == numeroRevista);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem("Revista excluída com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Revistas");

            List<Revista> revistas = repositorioRevista.SelecionarTodos();

            if (revistas.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma revista disponível", TipoMensagem.Atencao);
                return false;
            }

            foreach (Revista revista in revistas)
                Console.WriteLine(revista.ToString());

            return true;
        }

        #region Métodos privados
        private Revista ObterRevista(Caixa caixaSelecionada, Categoria categoriaSelecionada)
        {
            Console.Write("Digite a coleção da revista: ");
            string colecao = Console.ReadLine();

            Console.Write("Digite a edição da revista: ");
            int edicao = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("Digite o ano da revista: ");
            int ano = Convert.ToInt32(Console.ReadLine());

            Revista novaRevista = new Revista(colecao, edicao, ano, caixaSelecionada, categoriaSelecionada);

            return novaRevista;
        }

        private Categoria ObtemCategoria()
        {
            bool temCategoriasDisponiveis = telaCadastroCategoria.VisualizarRegistros("");

            if (!temCategoriasDisponiveis)
            {
                notificador.ApresentarMensagem("Você precisa cadastrar uma categoria antes de uma revista!", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número da categoria da revista: ");
            int numCategoriaSelecionada = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Categoria categoriaSelecionada = repositorioCategoria.SelecionarRegistro(x => x.numero == numCategoriaSelecionada);

            return categoriaSelecionada;
        }

        private Caixa ObtemCaixa()
        {
            bool temCaixasDisponiveis = telaCadastroCaixa.VisualizarRegistros("");

            if (!temCaixasDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhuma caixa disponível para cadastrar revistas", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número da caixa que irá inserir: ");
            int numCaixaSelecionada = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Caixa caixaSelecionada = repositorioCaixa.SelecionarRegistro(x => x.numero == numCaixaSelecionada);

            return caixaSelecionada;
        }

        private int ObterNumeroRevista()
        {
            int numeroRevista;
            bool numeroRevistaEncontrado;

            do
            {
                Console.Write("Digite o número da revista que deseja selecionar: ");
                numeroRevista = Convert.ToInt32(Console.ReadLine());

                numeroRevistaEncontrado = repositorioRevista.ExisteRegistro(x => x.numero == numeroRevista);

                if (numeroRevistaEncontrado == false)
                    notificador.ApresentarMensagem("Número de revista não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRevistaEncontrado == false);

            return numeroRevista;
        }
        #endregion
    }
}
