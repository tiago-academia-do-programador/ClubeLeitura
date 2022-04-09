
using ClubeLeitura.ConsoleApp.Compartilhado;

namespace ClubeLeitura.ConsoleApp.ModuloCaixa
{
    public class RepositorioCaixa : RepositorioBase<Caixa>, IRepositorio<Caixa>, IEtiquetavel
    {
        public bool EtiquetaIndisponivel(string etiquetaInformada)
        {
            foreach (Caixa caixa in registros)
                if (caixa.Etiqueta == etiquetaInformada)
                    return true;

            return false;
        }
    }
}
