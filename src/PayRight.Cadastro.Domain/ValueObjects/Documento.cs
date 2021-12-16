using Flunt.Validations;
using PayRight.Cadastro.Domain.Enums;
using PayRight.Shared.Utils.Validators;
using PayRight.Shared.ValueObjects;

namespace PayRight.Cadastro.Domain.ValueObjects;

public class Documento : ValueObject
{
    public string Numero { get; private set; }

    public TipoDocumento TipoDocumento { get; private set; }

    public Documento(string numero, TipoDocumento tipoDocumento)
    {
        Numero = numero;
        TipoDocumento = tipoDocumento;
        
        AddNotifications(
            new Contract<Documento>()
                .Requires()
                .IsNotNullOrEmpty(Numero, $"{nameof(Documento)}.{nameof(Numero)}", "Numero do Documento deve ser preenchido")
                .IsNotNull(TipoDocumento, $"{nameof(Documento)}.{nameof(TipoDocumento)}", "Tipo do Documento deve ser informado")
                .IsTrue(ValidadorCpfCnpj(), $"{nameof(Documento)}.{nameof(Numero)}", "Numero do Documento esta invalido"));
    }

    private bool ValidadorCpfCnpj()
    {
        return TipoDocumento == TipoDocumento.CPF 
            ? CpfCnpjValidator.IsCpf(Numero) 
            : CpfCnpjValidator.IsCnpj(Numero);
    }
}