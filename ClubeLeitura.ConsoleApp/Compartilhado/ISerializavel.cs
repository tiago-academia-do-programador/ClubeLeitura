﻿using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public interface ISerializavel<T> where T : EntidadeBase
    {
        void Serializar(T entidade);
        void SerializarLista(List<T> entidade);
        List<T> Deserializar();
    }
}