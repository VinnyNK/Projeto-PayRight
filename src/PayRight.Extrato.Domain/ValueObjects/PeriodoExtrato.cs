using Flunt.Validations;
using PayRight.Shared.ValueObjects;

namespace PayRight.Extrato.Domain.ValueObjects;

public class PeriodoExtrato : ValueObject
{
    public int Mes { get; }

    public int Ano { get; }

    public PeriodoExtrato(int mes, int ano)
    {
        Mes = mes;
        Ano = ano;
        
        Validar();
    }

    public sealed override void Validar()
    {
        AddNotifications(
            new Contract<PeriodoExtrato>()
                .Requires()
                .IsBetween(Mes, 1, 12, "PeriodoExtrato.Mes", "Mes deve ser entre 1 e 12")
                .IsTrue(MesInformadoValido(), "PeriodoExtrato.Mes", "Mes deve ser o mes atual ou no futuro")
                .IsGreaterOrEqualsThan(Ano, AnoAtual(), "PeriodoExtrato.Ano", "Ano não pode ser menor que o atual")
        );
    }

    private static int AnoAtual()
    {
        return DateTime.Now.Year;
    }

    private bool MesInformadoValido()
    {
        var mesAtual = DateTime.Now.Month;

        var anoAtual = AnoAtual();

        if (Ano > anoAtual)
            return true;

        return Mes >= mesAtual;
    }
}