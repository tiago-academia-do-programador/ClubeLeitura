using ClubeLeitura.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloCaixa
{
    public class TelaCadastroCaixa : TelaBase, ITelaCadastravel
    {
        private readonly Notificador notificador;
        private readonly IRepositorio<Caixa> repositorioCaixa;

        public TelaCadastroCaixa(IRepositorio<Caixa> repositorioCaixa, Notificador notificador)
            : base("Cadastro de Caixas")
        {
            this.repositorioCaixa = repositorioCaixa;
            this.notificador = notificador;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo nova Caixa");

            Caixa novaCaixa = ObterCaixa();
           
            string statusValidacao = repositorioCaixa.Inserir(novaCaixa);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Caixa cadastrada com sucesso!", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Caixa");

            bool temFuncionariosCadastrados = VisualizarRegistros("Pesquisando");

            if (temFuncionariosCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhum funcionario cadastrado para poder editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroCaixa = ObterNumeroCaixa();

            Caixa caixaAtualizada = ObterCaixa();

            bool conseguiuEditar = repositorioCaixa.Editar(x => x.numero == numeroCaixa, caixaAtualizada);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Caixa editada com sucesso", TipoMensagem.Sucesso);
        }

        public int ObterNumeroCaixa()
        {
            int numeroCaixa;
            bool numeroCaixaEncontrado;

            do
            {
                Console.Write("Digite o número da caixa que deseja editar: ");
                numeroCaixa = Convert.ToInt32(Console.ReadLine());

                numeroCaixaEncontrado = repositorioCaixa.ExisteRegistro(x => x.numero == numeroCaixa);

                if (numeroCaixaEncontrado == false)
                    notificador.ApresentarMensagem("Número de caixa não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroCaixaEncontrado == false);
            return numeroCaixa;
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Caixa");

            bool temCaixasCadastradas = VisualizarRegistros("Pesquisando");

            if (temCaixasCadastradas == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhuma caixa cadastrada para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int numeroCaixa = ObterNumeroCaixa();

            repositorioCaixa.Excluir(x => x.numero == numeroCaixa);

            notificador.ApresentarMensagem("Caixa excluída com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Caixas");

            List<Caixa> caixas = repositorioCaixa.SelecionarTodos();

            if (caixas.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma caixa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Caixa caixa in caixas)
                Console.WriteLine(caixa.ToString());

            Console.ReadLine();

            return true;
        }
        
        public Caixa ObterCaixa()
        {
            Console.Write("Digite a cor: ");
            string cor = Console.ReadLine();

            Console.Write("Digite a etiqueta: ");
            string etiqueta = Console.ReadLine();

            bool etiquetaJaUtilizada;

            do
            {
                etiquetaJaUtilizada = repositorioCaixa.ExisteRegistro(x => x.Etiqueta == etiqueta);

                if (etiquetaJaUtilizada)
                {
                    notificador.ApresentarMensagem("Etiqueta já utilizada, por gentileza informe outra", TipoMensagem.Erro);

                    Console.Write("Digite a etiqueta: ");
                    etiqueta = Console.ReadLine();
                }

            } while (etiquetaJaUtilizada);

            Caixa caixa = new Caixa(cor, etiqueta);

            return caixa;
        }

    }
}