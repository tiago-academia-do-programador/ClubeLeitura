using ClubeLeitura.ConsoleApp.Compartilhado;

namespace ClubeLeitura.ConsoleApp.ModuloEmprestimo
{
    public class RepositorioEmprestimo : RepositorioBase<Emprestimo>, IRepositorio<Emprestimo>
    {
        public bool RegistrarDevolucao(Emprestimo emprestimo)
        {
            emprestimo.Fechar();

            return true;
        }
    }
}
