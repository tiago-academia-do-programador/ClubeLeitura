using ClubeLeitura.ConsoleApp.Compartilhado;

namespace ClubeLeitura.ConsoleApp.ModuloEmprestimo
{
    public class RepositorioEmprestimo : RepositorioBase
    {
        public RepositorioEmprestimo(int qtdEmprestimos) : base(qtdEmprestimos)
        {
        }

        public override string Inserir(EntidadeBase emprestimo)
        {
            Emprestimo e = (Emprestimo)emprestimo;
            e.numero = ++contadorNumero;

            e.Abrir();

            e.revista.RegistrarEmprestimo(e);
            e.amigo.RegistrarEmprestimo(e);

            registros[ObterPosicaoVazia()] = e;

            return "REGISTRO_VALIDO";
        }

        public bool RegistrarDevolucao(Emprestimo emprestimo)
        {
            emprestimo.Fechar();

            return true;
        }

        public Emprestimo[] SelecionarEmprestimosAbertos()
        {
            Emprestimo[] emprestimosAbertos = new Emprestimo[ObterQtdEmprestimosAbertos()];

            int j = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                Emprestimo e = (Emprestimo)registros[i];

                if (e != null && e.estaAberto)
                {
                    emprestimosAbertos[j] = e;
                    j++;
                }
            }

            return emprestimosAbertos;
        }

        public int ObterQtdEmprestimosAbertos()
        {
            int numeroEmprestimos = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                Emprestimo e = (Emprestimo)registros[i];

                if (e != null && e.estaAberto)
                    numeroEmprestimos++;
            }

            return numeroEmprestimos;
        }
    }
}
