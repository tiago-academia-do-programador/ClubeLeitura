using ClubeLeitura.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloCategoria
{
    public class TelaCadastroCategoria : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Categoria> repositorioCategoria;
        private readonly Notificador notificador;

        public TelaCadastroCategoria(IRepositorio<Categoria> repositorioCategoria, Notificador notificador)
            : base ("Cadastro de Categorias de Revista")
        {
            this.repositorioCategoria = repositorioCategoria;
            this.notificador = notificador;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo nova categoria de revista");

            Categoria novaCategoria = ObterCategoria();

            string statusValidacao = repositorioCategoria.Inserir(novaCategoria);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Categoria de Revista cadastrada com sucesso!", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Categoria");

            bool temCategoriasCadastradas = VisualizarRegistros("Pesquisando");

            if (temCategoriasCadastradas == false)
            {
                notificador.ApresentarMensagem("Nenhuma categoria cadastrada para poder editar", TipoMensagem.Atencao);
                return;
            }

            int numeroCategoria = ObterNumeroCategoria();

            Categoria categoriaAtualizada = ObterCategoria();

            bool conseguiuEditar = repositorioCategoria.Editar(x => x.numero == numeroCategoria, categoriaAtualizada);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Categoria editada com sucesso", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Categoria");

            bool temCategoriasCadastradas = VisualizarRegistros("Pesquisando");

            if (temCategoriasCadastradas == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhuma categoria   cadastrada para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int numeroCategoria = ObterNumeroCategoria();

            bool conseguiuExcluir = repositorioCategoria.Excluir(x => x.numero == numeroCategoria);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem("Categoria excluída com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Categorias");

            List<Categoria> categorias = repositorioCategoria.SelecionarTodos();

            if (categorias.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma categoria disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Categoria categoria in categorias)
                Console.WriteLine(categoria.ToString());

            return true;
        }

        #region Métodos privados
        private int ObterNumeroCategoria()
        {
            int numeroCategoria;
            bool numeroCadastroEncontrado;

            do
            {
                Console.Write("Digite o número da categoria que deseja selecionar: ");
                numeroCategoria = Convert.ToInt32(Console.ReadLine());

                numeroCadastroEncontrado = repositorioCategoria.ExisteRegistro(x => x.numero == numeroCategoria);

                if (numeroCadastroEncontrado == false)
                    notificador.ApresentarMensagem("Número de cadastro não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroCadastroEncontrado == false);

            return numeroCategoria;
        }

        private Categoria ObterCategoria()
        {
            Console.Write("Digite o nome da categoria: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o limite de dias de empréstimo das revistas: ");
            int diasEmprestimo = Convert.ToInt32(Console.ReadLine());

            Categoria novaCategoria = new Categoria(nome, diasEmprestimo);

            return novaCategoria;
        }
        #endregion
    }
}