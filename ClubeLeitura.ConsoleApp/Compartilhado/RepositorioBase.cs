using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public class RepositorioBase
    {
        protected readonly List<EntidadeBase> registros;
        protected int contadorNumero;

        public RepositorioBase()
        {
            registros = new List<EntidadeBase>();
        }

        public virtual string Inserir(EntidadeBase entidade)
        {
            //string statusValidacao = entidade.Validar();

            //if (statusValidacao != "REGISTRO_VALIDO")
            //    return statusValidacao;

            entidade.numero = ++contadorNumero;

            registros.Add(entidade);

            return "REGISTRO_VALIDO";
        }

        public bool Editar(int numeroSelecionado, EntidadeBase entidadeEditada)
        {
            EntidadeBase entidadeSelecionada = SelecionarRegistro(numeroSelecionado);

            if (entidadeSelecionada is null)
                return false;

            entidadeEditada.numero = numeroSelecionado;

            entidadeSelecionada = entidadeEditada;

            return true;
        }

        public bool Excluir(int numeroSelecionado)
        {
            EntidadeBase entidadeSelecionada = SelecionarRegistro(numeroSelecionado);

            if (entidadeSelecionada is null)
                return false;

            registros.Remove(entidadeSelecionada);

            return true;
        }

        public EntidadeBase SelecionarRegistro(int numeroRegistro)
        {
            foreach (EntidadeBase registro in registros)
            {
                if (registro.numero == numeroRegistro)
                    return registro;
            }

            return null;
        }

        public List<EntidadeBase> SelecionarTodos()
        {
            return registros;
        }

        public bool ExisteRegistro(int numeroRegistro)
        {
            bool existeRegistro = false;

            EntidadeBase registroSelecionado = SelecionarRegistro(numeroRegistro);

            if (registroSelecionado is null)
                return existeRegistro;

            foreach (EntidadeBase registro in registros)
            {
                if (registro.numero == numeroRegistro)
                {
                    existeRegistro = true;
                    break;
                }
            }

            return existeRegistro;
        }
    }
}
