using System;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public class RepositorioBase
    {
        protected readonly EntidadeBase[] registros;
        protected int contadorNumero;

        public RepositorioBase(int qtdRegistros)
        {
            registros = new EntidadeBase[qtdRegistros];
        }

        public virtual void Inserir(EntidadeBase entidade)
        {
            entidade.numero = ++contadorNumero;

            int posicaoVazia = ObterPosicaoVazia();

            registros[posicaoVazia] = entidade;
        }

        public void Editar(int numeroSelecioando, EntidadeBase entidade)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i].numero == numeroSelecioando)
                {
                    entidade.numero = numeroSelecioando;
                    registros[i] = entidade;

                    break;
                }
            }
        }

        public void Excluir(int numeroSelecionado)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i].numero == numeroSelecionado)
                {
                    registros[i] = null;
                    break;
                }
            }
        }

        public EntidadeBase SelecionarRegistro(int numeroRegistro)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] != null && numeroRegistro == registros[i].numero)
                    return registros[i];
            }

            return null;
        }

        public EntidadeBase[] SelecionarTodos()
        {
            int quantidadeRegistros = ObterQtdRegistros();

            EntidadeBase[] registrosInseridos = new EntidadeBase[quantidadeRegistros];

            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] != null)
                    registrosInseridos[i] = registros[i];
            }

            return registrosInseridos;
        }

        public bool VerificarNumeroRegistroExiste(int numeroRegistro)
        {
            bool numeroRegistroExiste = false;

            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] != null && registros[i].numero == numeroRegistro)
                {
                    numeroRegistroExiste = true;
                    break;
                }
            }

            return numeroRegistroExiste;
        }

        protected int ObterQtdRegistros()
        {
            int numeroRegistros = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] != null)
                    numeroRegistros++;
            }

            return numeroRegistros;
        }

        protected int ObterPosicaoVazia()
        {
            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                    return i;
            }

            return -1;
        }
    }
}
