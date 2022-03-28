﻿using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloRevista;
using System;

namespace ClubeLeitura.ConsoleApp.ModuloEmprestimo
{
    public class Emprestimo
    {
        public int numero;

        public Amigo amigo;
        public Revista revista;
        public DateTime dataEmprestimo;
        public DateTime dataDevolucao;

        public bool estaAberto;

        public void Abrir()
        {
            if (!estaAberto)
            {
                estaAberto = true;
                dataEmprestimo = DateTime.Now;
            }
        }

        public void Fechar()
        {
            if (estaAberto)
            {
                estaAberto = false;
                dataDevolucao = DateTime.Now;
            }
        }

    }
}