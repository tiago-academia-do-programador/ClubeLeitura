using ClubeLeitura.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloCaixa
{
    public class Caixa : EntidadeBase
    {
        private readonly string cor;
        private readonly string etiqueta;

        public string Cor => cor;

        public string Etiqueta => etiqueta;

        public Caixa(string cor, string etiqueta)
        {
            this.cor = cor;
            this.etiqueta = etiqueta;
        }

        public override string ToString()
        {
            return "Número: " + numero + Environment.NewLine +
            "Cor: " + Cor + Environment.NewLine +
            "Etiqueta: " + Etiqueta + Environment.NewLine;
        }

        public override ResultadoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(cor))
                erros.Add("É necessário inserir uma cor para as caixas!");

            if (string.IsNullOrEmpty(etiqueta))
                erros.Add("Por favor insira uma etiqueta válida!");

            return new ResultadoValidacao(erros);
        }
    }
}
