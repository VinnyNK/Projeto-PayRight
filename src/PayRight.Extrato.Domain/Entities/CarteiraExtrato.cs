namespace PayRight.Extrato.Domain.Entities;

public class CarteiraExtrato : Extrato
{
    public CarteiraExtrato(Guid usuarioId, int mes, int ano) : base(usuarioId, mes, ano)
    {
    }

    public override void Validar()
    {
        throw new NotImplementedException();
    }
}