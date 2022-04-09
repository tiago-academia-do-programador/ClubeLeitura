using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public class ResultadoValidacao
    {
        private readonly List<string> _erros;
        
        public StatusValidacao Status
        {
            get
            {
                return _erros.Count == 0 ? StatusValidacao.Ok : StatusValidacao.Erro;
            }
        }

        public ResultadoValidacao(List<string> erros)
        {
            _erros = erros;
        }

        public override string ToString()
        {
            string strErros = "";

            foreach (string erro in _erros)
            {
                if (!string.IsNullOrEmpty(erro))
                    strErros += erro + Environment.NewLine;
            }

            return strErros;
        }
    }
}
