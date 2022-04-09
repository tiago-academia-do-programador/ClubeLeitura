namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public interface ITransacao
    {
        void Abrir();
        void Fechar();
    }
}