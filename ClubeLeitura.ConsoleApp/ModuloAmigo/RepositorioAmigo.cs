using ClubeLeitura.ConsoleApp.Compartilhado;
using System.Collections.Generic;
using System.Linq;

namespace ClubeLeitura.ConsoleApp.ModuloAmigo
{
    public class RepositorioAmigo : RepositorioBase<Amigo>, IRepositorio<Amigo>, IMultavelRepository<Amigo>
    {
        public List<Amigo> SelecionarRegistrosComMulta()
        {
            return registros.FindAll(x => x.TemMultaEmAberto()).ToList();
        }
    }
}
