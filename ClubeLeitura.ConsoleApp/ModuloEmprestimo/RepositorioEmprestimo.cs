using ClubeLeitura.ConsoleApp.Compartilhado;

namespace ClubeLeitura.ConsoleApp.ModuloEmprestimo
{
    public class RepositorioEmprestimo : RepositorioBase<Emprestimo>, IRepositorio<Emprestimo>
    {
        public override string Inserir(Emprestimo emprestimo)
        {
            emprestimo.numero = ++contadorNumero;

            emprestimo.Abrir();

            registros.Add(emprestimo);

            return "REGISTRO_VALIDO";
        }

        public bool RegistrarDevolucao(Emprestimo emprestimo)
        {
            emprestimo.Fechar();

            return true;
        }
    }
}
