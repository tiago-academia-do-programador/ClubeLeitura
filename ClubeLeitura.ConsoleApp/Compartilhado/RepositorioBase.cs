using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public abstract class RepositorioBase<T> where T : EntidadeBase
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

        public bool Editar(int numeroSelecionado, T entidade)
        {
            return registros.Editar(x => x.numero == numeroSelecionado, entidade);
        }

        public bool Excluir(int numeroSelecionado)
        {
            return registros.Remover(x => x.numero == numeroSelecionado);
        }

        public T SelecionarRegistro(int numeroRegistro)
        {
            return registros.Selecionar(x => x.numero == numeroRegistro);
        }

        public List<T> SelecionarTodos()
        {
            return registros;
        }

        public bool ExisteRegistro(int numeroRegistro)
        {
            return registros.Verificar(x => x.numero == numeroRegistro);
        }
    }
}
