using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public class RepositorioBase<T> where T : EntidadeBase
    {
        protected readonly List<T> registros;

        protected int contadorNumero;

        public RepositorioBase()
        {
            registros = new List<T>();
        }

        public virtual string Inserir(T entidade)
        {
            ResultadoValidacao validacao = entidade.Validar();

            if (validacao.Status == StatusValidacao.Erro)
                return validacao.ToString();

            entidade.numero = ++contadorNumero;

            registros.Add(entidade);

            return "REGISTRO_VALIDO";
        }

        public bool Editar(int numeroSelecionado, T novaEntidade)
        {
            return Editar(x => x.numero == numeroSelecionado, novaEntidade);
        }

        public bool Editar(Predicate<T> condicao, T novaEntidade)
        {
            foreach (T entidade in registros)
            {
                if (condicao(entidade))
                {
                    novaEntidade.numero = entidade.numero;

                    int posicaoParaEditar = registros.IndexOf(entidade);
                    registros[posicaoParaEditar] = novaEntidade;

                    return true;
                }
            }

            return false;
        }

        public bool Excluir(int numeroSelecionado)
        {
            return Excluir(x => x.numero == numeroSelecionado);
        }

        public bool Excluir(Predicate<T> condicao)
        {
            foreach (T entidade in registros)
            {
                if (condicao(entidade))
                {
                    registros.Remove(entidade);
                    return true;
                }
            }
            return false;
        }

        public T SelecionarRegistro(int numeroSelecionado)
        {
            return SelecionarRegistro(x => x.numero == numeroSelecionado);
        }

        public T SelecionarRegistro(Predicate<T> condicao)
        {
            foreach (T entidade in registros)
            {
                if (condicao(entidade))
                    return entidade;
            }

            return null;
        }

        public List<T> SelecionarTodos()
        {
            return registros;
        }

        public List<T> Filtrar(Predicate<T> condicao)
        {
            List<T> registrosFiltrados = new List<T>();

            foreach (T registro in registros)
                if (condicao(registro))
                    registrosFiltrados.Add(registro);

            return registrosFiltrados;
        }

        public bool ExisteRegistro(int numeroSelecionado)
        {
            return ExisteRegistro(x => x.numero == numeroSelecionado);
        }

        public bool ExisteRegistro(Predicate<T> condicao)
        {
            foreach (T entidade in registros)
                if (condicao(entidade))
                    return true;
                
            return false;
        }
    }
}
