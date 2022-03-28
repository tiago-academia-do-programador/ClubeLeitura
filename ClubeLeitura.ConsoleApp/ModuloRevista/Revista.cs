using ClubeLeitura.ConsoleApp.ModuloCaixa;
using ClubeLeitura.ConsoleApp.ModuloEmprestimo;
using System;

namespace ClubeLeitura.ConsoleApp.ModuloRevista
{
    public class Revista
    {
        public int numero; // número que iremos usar como identificador das revistas
        public string colecao;
        public int edicao;
        public int ano;
        public Caixa caixa;

        public Emprestimo[] historicoEmprestimos = new Emprestimo[10];

        public void RegistrarEmprestimo(Emprestimo emprestimo)
        {
            historicoEmprestimos[ObtemPosicaoVazia()] = emprestimo;
        }

        public bool EstaEmprestada()
        {
            bool temEmprestimoEmAberto = false;

            foreach (Emprestimo emprestimo in historicoEmprestimos)
            {
                if (emprestimo != null && emprestimo.estaAberto)
                {
                    temEmprestimoEmAberto = true;
                    break;
                }
            }
            return temEmprestimoEmAberto;
        }

        public string Validar()
        {
            string validacao = "";

            if (string.IsNullOrEmpty(colecao))
                validacao += "É necessário incluir uma coleção!\n";

            if (edicao < 0)
                validacao += "A edição de uma revista não pode ser menor que zero!\n";

            if (ano < 0 || ano > DateTime.Now.Year)
                validacao += "O ano da revista precisa ser válido!\n";

            if (string.IsNullOrEmpty(validacao))
                return "REGISTRO_VALIDO";

            return validacao;
        }

        public int ObtemPosicaoVazia()
        {
            for (int i = 0; i < historicoEmprestimos.Length; i++)
            {
                if (historicoEmprestimos[i] == null)
                    return i;
            }

            return -1;
        }
    }
}