using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public interface IMultavelRepository<T> where T : IMultavel
    {
        List<T> SelecionarRegistrosComMulta();
    }
}
