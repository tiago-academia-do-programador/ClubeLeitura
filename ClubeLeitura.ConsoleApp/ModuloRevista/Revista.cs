using ClubeLeitura.ConsoleApp.ModuloCaixa;
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
    }
}