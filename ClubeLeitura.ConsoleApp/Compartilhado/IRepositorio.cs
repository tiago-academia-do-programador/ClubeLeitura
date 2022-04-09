using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public interface IRepositorio<T> where T: EntidadeBase
    {
        string Inserir(T entidade);
        bool Editar(int id, T entidade);
        bool Excluir(int id);
        bool ExisteRegistro(int id);
        List<T> SelecionarTodos();
        T SelecionarRegistro(int id);
    }
}