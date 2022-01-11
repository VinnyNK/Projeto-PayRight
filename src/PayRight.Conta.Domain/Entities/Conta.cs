using PayRight.Conta.Domain.ValueObjects;
using PayRight.Shared.Entities;

namespace PayRight.Conta.Domain.Entities;

public abstract class Conta : Entity
{
    public Guid UsuarioId { get; }
    
    public NomeConta NomeConta { get; private set; }

    public DateTime UltimaAtualizacaoEm { get; private set; }

    public bool Ativo { get; private set; }
    
    protected Conta(Guid usuarioId, string nome, string? apelido)
    {
        UsuarioId = usuarioId;
        NomeConta = new NomeConta(nome, apelido);
        UltimaAtualizacaoEm = DateTime.Now;
        Ativo = true;
    }

    public void AlterarNomeConta(NomeConta nomeConta)
    {
        NomeConta = nomeConta;
        Validar();
    }

    public void DesativarConta()
    {
        Ativo = false;
    }
    
    public void AtivarConta()
    {
        Ativo = true;
    }
}