namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public interface ITransacao
    {
        string Status { get; }
        void Abrir();
        void Fechar();
    }
}