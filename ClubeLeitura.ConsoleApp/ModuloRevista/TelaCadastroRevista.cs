using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloCaixa;
using System;

namespace ClubeLeitura.ConsoleApp.ModuloRevista
{
    public class TelaCadastroRevista
    {
        public TelaCadastroCaixa telaCadastroCaixa;
        public RepositorioCaixa repositorioCaixa;
        public RepositorioRevista repositorioRevista;
        public Notificador notificador;

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("Cadastro de Revistas");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void InserirNovaRevista()
        {
            MostrarTitulo("Inserindo nova revista");

            // vamos inserir uma revista em uma caixa...
            // ...então precisamos da lista de caixas disponíveis
            Caixa caixaSelecionada = ObtemCaixa();

            Revista novaRevista = ObterRevista();

            novaRevista.caixa = caixaSelecionada;

            // após a escolha da caixa, então podemos cadastrar a revista
            // ...
            string statusValidacao = repositorioRevista.Inserir(novaRevista);

            if (statusValidacao != "REGISTRO_VALIDO")
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Revista inserida com sucesso", TipoMensagem.Sucesso);
        }


        public void EditarRevista()
        {
            MostrarTitulo("Editando Revista");

            bool temRevistasCadastradas = VisualizarRevistas("Pesquisando");

            if (temRevistasCadastradas == false)
            {
                notificador.ApresentarMensagem("Nenhuma revista cadastrada para poder editar", TipoMensagem.Atencao);
                return;
            }

            int numeroRevista = ObterNumeroRevista();

            Console.WriteLine();
            
            Caixa caixaSelecionada = ObtemCaixa();

            Revista revistaAtualizada = ObterRevista();

            revistaAtualizada.caixa = caixaSelecionada;

            repositorioRevista.Editar(numeroRevista, revistaAtualizada);

            notificador.ApresentarMensagem("Revista editada com sucesso", TipoMensagem.Sucesso);
        }

        public void ExcluirRevista()
        {
            MostrarTitulo("Excluindo Revista");

            bool temRevistasCadastradas = VisualizarRevistas("Pesquisando");

            if (temRevistasCadastradas == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhuma revista cadastrada para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int numeroRevista = ObterNumeroRevista();

            repositorioRevista.Excluir(numeroRevista);

            notificador.ApresentarMensagem("Revista excluída com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRevistas(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Revistas");

            Revista[] revistas = repositorioRevista.SelecionarTodos();

            if (revistas.Length == 0)
                return false;

            for (int i = 0; i < revistas.Length; i++)
            {
                Revista revista = revistas[i];

                Console.WriteLine("Número: " + revista.numero);
                Console.WriteLine("Coleção: " + revista.colecao);
                Console.WriteLine("Edição: " + revista.edicao);
                Console.WriteLine("Ano: " + revista.ano);
                Console.WriteLine("Caixa que está guardada: " + revista.caixa.cor);

                Console.WriteLine();
            }

            return true;
        }

        public Revista ObterRevista()
        {
            Console.Write("Digite a coleção da revista: ");
            string colecao = Console.ReadLine();

            Console.Write("Digite a edição da revista: ");
            int edicao = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("Digite o ano da revista: ");
            int ano = Convert.ToInt32(Console.ReadLine());

            Revista novaRevista = new Revista();

            novaRevista.colecao = colecao;
            novaRevista.edicao = edicao;
            novaRevista.ano = ano;

            return novaRevista;
        }

        public Caixa ObtemCaixa()
        {
            bool temCaixasDisponiveis = telaCadastroCaixa.VisualizarCaixas("");

            if (!temCaixasDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhuma caixa disponível para cadastrar revistas", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número da caixa que irá inserir: ");
            int numCaixaSelecionada = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Caixa caixaSelecionada = repositorioCaixa.SelecionarCaixa(numCaixaSelecionada);

            return caixaSelecionada;
        }

        public int ObterNumeroRevista()
        {
            int numeroRevista;
            bool numeroRevistaEncontrado;

            do
            {
                Console.Write("Digite o número da revista que deseja selecionar: ");
                numeroRevista = Convert.ToInt32(Console.ReadLine());

                numeroRevistaEncontrado = repositorioRevista.VerificarNumeroRevistaExiste(numeroRevista);

                if (numeroRevistaEncontrado == false)
                    notificador.ApresentarMensagem("Número de revista não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRevistaEncontrado == false);

            return numeroRevista;
        }

        public void MostrarTitulo(string titulo)
        {
            Console.Clear();

            Console.WriteLine(titulo);

            Console.WriteLine();
        }
    }
}
