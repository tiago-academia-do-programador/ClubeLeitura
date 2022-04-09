using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public static class ListExtensions
    {
        /// <summary>
        ///     Sumário:
        ///         Edita um elemento da lista especificado pelos argumento numeroRegistro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="registros"></param>
        /// <param name="numeroRegistro">Número do registro que será editado</param>
        /// <param name="registroEditado">Novo registro, com as informações que serão inseridas.</param>
        /// <returns>Booleano indicando se a edição foi bem-sucedida</returns>
        public static bool Editar<T>(this List<T> registros, int numeroRegistro, T registroEditado) where T : EntidadeBase
        {
            bool estaEditado = false;

            foreach (T item in registros)
            {
                if (numeroRegistro == item.numero)
                {
                    registroEditado.numero = numeroRegistro;
                    
                    int posicaoRegistro = registros.IndexOf(item);

                    registros[posicaoRegistro] = registroEditado;
                    
                    estaEditado = true;
                    break;
                }
            }

            return estaEditado;
        }

        /// <summary>
        ///     Sumário:
        ///         O método irá editar um elemento da lista especificado pelos argumento numeroRegistro
        ///         <br></br>
        ///         <br></br>
        ///         Utiliza um predicate para indicar qual elemento será editado à partir de uma condição
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="registros"></param>
        /// <param name="condicao"></param>
        /// <param name="novaEntidade"></param>
        /// <returns>Booleano indicando se a edição foi bem-sucedida</returns>
        public static bool Editar<T>(this List<T> registros, Predicate<T> condicao, T novaEntidade) where T : EntidadeBase
        {
            foreach (T entidade in registros)
            {
                if (condicao(entidade))
                {
                    novaEntidade.numero = entidade.numero;

                    int posicaoRegistro = registros.IndexOf(entidade);
                    registros[posicaoRegistro] = novaEntidade;

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Sumário:
        ///         Remove um elemento da lista especificado pelo numeroSelecionado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="registros"></param>
        /// <param name="numeroSelecionado">Nùmero do registro a ser excluído</param>
        /// <returns>Booleano indicando se a remoção foi bem-sucedida</returns>
        public static bool Remover<T>(this List<T> registros, int numeroSelecionado) where T: EntidadeBase
        {
            foreach (T entidade in registros)
            {
                if (numeroSelecionado == entidade.numero)
                {
                    registros.Remove(entidade);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Sumário:
        ///         Remove um elemento da lista especificado pelo predicate
        ///         <br></br>
        ///         <br></br>
        ///         Utiliza um predicate para indicar qual elemento será excluído à partir de uma condição
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="registros"></param>
        /// <param name="condicao"></param>
        /// <returns>Booleano indicando se a remoção foi bem-sucedida</returns>
        public static bool Remover<T>(this List<T> registros, Predicate<T> condicao)
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

        /// <summary>
        ///     Sumário:
        ///         Seleciona um elemento da lista especificado pelo numeroSelecionado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="registros"></param>
        /// <param name="numeroSelecionado"></param>
        /// <returns>Entidade selecionada</returns>
        public static T Selecionar<T>(this List<T> registros, int numeroSelecionado) where T : EntidadeBase
        {
            foreach (T entidade in registros)
            {
                if (numeroSelecionado == entidade.numero)
                    return entidade;
            }

            return default(T);
        }

        /// <summary>
        ///     Sumário:
        ///         Seleciona um elemento da lista especificado pelo predicate
        ///         <br></br>
        ///         <br></br>
        ///         Utiliza um predicate para indicar qual elemento será selecionado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="registros"></param>
        /// <param name="condicao"></param>
        /// <returns></returns>
        public static T Selecionar<T>(this List<T> registros, Predicate<T> condicao)
        {
            foreach (T entidade in registros)
            {
                if (condicao(entidade))
                    return entidade;
            }

            return default(T);
        }
        
        public static List<T> Procurar<T>(this List<T> registros, Predicate<T> condicao)
        {
            List<T> listaFiltrada = new List<T>();

            foreach (T entidade in registros)
                if (condicao(entidade))
                    listaFiltrada.Add(entidade);

            return listaFiltrada;
        }

        public static bool Verificar<T>(this List<T> registros, Predicate<T> condicao)
        {
            foreach (T entidade in registros)
                return condicao(entidade);

            return false;
        }
    }
}
