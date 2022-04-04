
using ClubeLeitura.ConsoleApp.Compartilhado;

namespace ClubeLeitura.ConsoleApp.ModuloCaixa
{
    public class RepositorioCaixa : RepositorioBase
    {
        public RepositorioCaixa(int qtdCaixas) : base(qtdCaixas)
        {
        }

        public bool EtiquetaJaUtilizada(string etiquetaInformada)
        {
            bool etiquetaJaUtilizada = false;

            for (int i = 0; i < registros.Length; i++)
            {
                Caixa c = (Caixa)registros[i];

                if (registros[i] != null && c.Etiqueta == etiquetaInformada)
                {
                    etiquetaJaUtilizada = true;
                    break;
                }
            }

            return etiquetaJaUtilizada;
        }
    }
}
