using ClubeLeitura.ConsoleApp.Compartilhado;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloEmprestimo
{
    public class RepositorioEmprestimo : RepositorioBase<Emprestimo>, IRepositorio<Emprestimo>, ITransacaoRepositorio<Emprestimo>
    {
        public RepositorioEmprestimo()
        {
        }

        public override string Inserir(Emprestimo emprestimo)
        {
            emprestimo.numero = ++contadorNumero;

            emprestimo.Abrir();

            emprestimo.revista.RegistrarEmprestimo(emprestimo);
            emprestimo.amigo.RegistrarEmprestimo(emprestimo);

            registros.Add(emprestimo);

            return "REGISTRO_VALIDO";
        }

        public bool RegistrarDevolucao(Emprestimo emprestimo)
        {
            emprestimo.Fechar();

            return true;
        }

        public List<Emprestimo> SelecionarTransacoesEmAberto()
        {
            return registros.Procurar(x => x.EstaAberto);
        }
    }
}
