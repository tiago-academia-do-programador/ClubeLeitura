﻿using System;

namespace ClubeLeitura.ConsoleApp.ModuloAmigo
{
    public class RepositorioAmigo
    {
        public Amigo[] amigos;

        public int numeroAmigo;
        public void Inserir(Amigo amigo)
        {
            amigo.numero = ++numeroAmigo;

            amigos[ObterPosicaoVazia()] = amigo;
        }

        public void Editar(int numeroSelecionado, Amigo amigo)
        {
            for (int i = 0; i < amigos.Length; i++)
            {
                if (amigos[i].numero == numeroSelecionado)
                {
                    amigo.numero = numeroSelecionado;
                    amigos[i] = amigo;

                    break;
                }
            }
        }

        public void Excluir(int numeroSelecionado)
        {
            for (int i = 0; i < amigos.Length; i++)
            {
                if (amigos[i].numero == numeroSelecionado)
                {
                    amigos[i] = null;
                    break;
                }
            }
        }

        public Amigo[] SelecionarTodos()
        {
            Amigo[] amigosInseridos = new Amigo[ObterQtdAmigos()];

            int j = 0;

            for (int i = 0; i < amigos.Length; i++)
            {
                if (amigos[i] != null)
                {
                    amigosInseridos[j] = amigos[i];
                    j++;
                }
            }

            return amigosInseridos;
        }

        public Amigo[] SelecionarAmigosComMulta()
        {
            Amigo[] amigosComMulta = new Amigo[ObterQtdAmigosComMulta()];

            int j = 0;

            for (int i = 0; i < amigos.Length; i++)
            {
                if (amigos[i] != null && amigos[i].TemMultaEmAberto())
                {
                    amigosComMulta[j] = amigos[i];
                    j++;
                }
            }

            return amigosComMulta;
        }

        public Amigo SelecionarAmigo(int numeroAmigo)
        {
            for (int i = 0; i < amigos.Length; i++)
            {
                if (amigos[i] != null && numeroAmigo == amigos[i].numero)
                    return amigos[i];
            }

            return null;
        }

        public int ObterPosicaoVazia()
        {
            for (int i = 0; i < amigos.Length; i++)
            {
                if (amigos[i] == null)
                    return i;
            }

            return -1;
        }

        public int ObterQtdAmigos()
        {
            int numeroAmigos = 0;

            for (int i = 0; i < amigos.Length; i++)
            {
                if (amigos[i] != null)
                    numeroAmigos++;
            }

            return numeroAmigos;
        }

        public int ObterQtdAmigosComMulta()
        {
            int numeroAmigos = 0;

            for (int i = 0; i < amigos.Length; i++)
            {
                if (amigos[i] != null && amigos[i].TemMultaEmAberto())
                    numeroAmigos++;
            }

            return numeroAmigos;
        }

        public bool VerificarNumeroAmigoExiste(int numeroAmigo)
        {
            bool numeroAmigoExiste = false;

            for (int i = 0; i < amigos.Length; i++)
            {
                if (amigos[i] != null && amigos[i].numero == numeroAmigo)
                {
                    numeroAmigoExiste = true;
                    break;
                }
            }

            return numeroAmigoExiste;
        }
    }
}
