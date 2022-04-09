using ClubeLeitura.ConsoleApp.ModuloAmigo;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public interface IMultavel
    {
        Multa Multa { get; set; }
        
        void RegistrarMulta(decimal valor);
        void PagarMulta();
        bool TemMultaEmAberto();
    }
}
