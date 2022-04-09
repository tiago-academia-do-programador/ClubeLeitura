using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public interface ITransacaoRepositorio<T> where T : ITransacao
    {
        List<T> SelecionarTransacoesEmAberto();
    }
}
