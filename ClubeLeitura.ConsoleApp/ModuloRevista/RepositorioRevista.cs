namespace ClubeLeitura.ConsoleApp.ModuloRevista
{
    public class RepositorioRevista
    {
        public Revista[] revistas; // contém elementos null
        public int numeroRevista;

        public string Inserir(Revista revista)
        {
            string validacao = revista.Validar();

            if (validacao != "REGISTRO_VALIDO")
                return validacao;

            revista.numero = ++numeroRevista;

            int posicaoVazia = ObterPosicaoVazia();

            revistas[posicaoVazia] = revista;

            return validacao;
        }

        public void Editar(int numeroSelecionado, Revista revista)
        {
            for (int i = 0; i < revistas.Length; i++)
            {
                if (revistas[i].numero == numeroSelecionado)
                {
                    revista.numero = numeroSelecionado;
                    revistas[i] = revista;

                    break;
                }
            }
        }

        public void Excluir(int numeroSelecionado)
        {
            for (int i = 0; i < revistas.Length; i++)
            {
                if (revistas[i].numero == numeroSelecionado)
                {
                    revistas[i] = null;
                    break;
                }
            }
        }

        public Revista[] SelecionarTodos()
        {
            int quantidadeRevistas = ObterQtdRevistas();

            Revista[] revistasInseridas = new Revista[quantidadeRevistas];

            int j = 0;

            for (int i = 0; i < revistas.Length; i++)
            {
                if (revistas[i] != null)
                {
                    revistasInseridas[j] = revistas[i];
                    j++;
                }
            }

            return revistasInseridas;
        }

        public int ObterPosicaoVazia()
        {
            for (int i = 0; i < revistas.Length; i++)
            {
                if (revistas[i] == null)
                    return i;
            }

            return -1;
        }

        public bool VerificarNumeroRevistaExiste(int numeroRevista)
        {
            bool numeroRevistaEncontrado = false;

            for (int i = 0; i < revistas.Length; i++)
            {
                if (revistas[i] != null && revistas[i].numero == numeroRevista)
                {
                    numeroRevistaEncontrado = true;
                    break;
                }
            }

            return numeroRevistaEncontrado;
        }

        public int ObterQtdRevistas()
        {
            int numeroRevistas = 0;

            for (int i = 0; i < revistas.Length; i++)
            {
                if (revistas[i] != null)
                    numeroRevistas++;
            }

            return numeroRevistas;
        }
    }
}