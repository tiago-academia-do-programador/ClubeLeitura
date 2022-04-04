using ClubeLeitura.ConsoleApp.Compartilhado;
using System;

namespace ClubeLeitura.ConsoleApp.ModuloAmigo
{
    public class RepositorioAmigo : RepositorioBase
    {
        public RepositorioAmigo(int qtdAmigos) : base(qtdAmigos)
        {
        }

        public Amigo[] SelecionarAmigosComMulta()
        {
            Amigo[] amigosComMulta = new Amigo[ObterQtdAmigosComMulta()];

            int j = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                Amigo a = (Amigo)registros[i];
                if (registros[i] != null && a.TemMultaEmAberto())
                {
                    amigosComMulta[j] = a;
                    j++;
                }
            }

            return amigosComMulta;
        }

        #region Métodos privados
        private int ObterQtdAmigosComMulta()
        {
            int numeroAmigos = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                Amigo a = (Amigo)registros[i];

                if (registros[i] != null && a.TemMultaEmAberto())
                    numeroAmigos++;
            }

            return numeroAmigos;
        }
        #endregion
    }
}
