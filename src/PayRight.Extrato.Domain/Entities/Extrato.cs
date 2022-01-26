using PayRight.Extrato.Domain.Enums;
using PayRight.Extrato.Domain.ValueObjects;
using PayRight.Shared.Entities;

namespace PayRight.Extrato.Domain.Entities;

public abstract class Extrato : Entity
{
    public Guid UsuarioId { get; }

    private readonly IList<Atividade> _atividades;
    
    public IReadOnlyCollection<Atividade> Atividades => _atividades.ToArray();

    public PeriodoExtrato PeriodoExtrato { get; }

    public decimal Total { get; private set; }

    public decimal TotalEstimado { get; private set; }

    protected Extrato(Guid usuarioId, int mes, int ano)
    {
        UsuarioId = usuarioId;
        _atividades = new List<Atividade>();
        PeriodoExtrato = new PeriodoExtrato(mes, ano);
        Total = 0;
        TotalEstimado = 0;
    }

    public void AdicionarAtividade(Atividade atividade)
    {
        _atividades.Add(atividade);
        atividade.AdicionarExtrato(this);
        RealizaCalculoExtratoEstimado(atividade);
        AddNotifications(atividade);
    }

    public void RemoveAtividade(Atividade atividade)
    {
        var resultado = _atividades.Remove(atividade);
        if (!resultado)
        {
            AddNotification("Atividade", "Atividade informada não existe no extrato");
            return;
        }
        
        RemoveValorDaAtividadeNoExtratoEstimado(atividade);
        if (atividade.Pago)
            RealizaCalculoExtratoRetornoPagamento(atividade);
    }

    public void RealizaCalculoExtratoEstimado(Atividade atividade)
    {
        if (atividade.TipoAtividade == TipoAtividade.Receita)
            SomarTotalEstimado(atividade.Valor);
        else
            SubtrairTotalEstimado(atividade.Valor);
    }
    
    public void RealizaCalculoExtratoAtividadePaga(Atividade atividade)
    {
        if (atividade.TipoAtividade == TipoAtividade.Receita)
            SomarTotal(atividade.Valor);
        else
            SubtrairTotal(atividade.Valor);
    }

    public void RealizaCalculoExtratoRetornoPagamento(Atividade atividade)
    {
        if (atividade.TipoAtividade == TipoAtividade.Receita)
            SubtrairTotal(atividade.Valor);
        else
            SomarTotal(atividade.Valor);
    }

    public void RemoveValorDaAtividadeNoExtratoEstimado(Atividade atividade)
    {
        if (atividade.TipoAtividade == TipoAtividade.Receita)
            SubtrairTotalEstimado(atividade.Valor);
        else
            SomarTotalEstimado(atividade.Valor);
    }

    private void SomarTotal(decimal valor)
    {
        if (valor >= 0)
            Total += valor;
    }

    private void SubtrairTotal(decimal valor)
    {
        if (valor >= 0)
            Total -= valor;
    }

    private void SomarTotalEstimado(decimal valor)
    {
        if (valor >= 0)
            TotalEstimado += valor;
    }

    private void SubtrairTotalEstimado(decimal valor)
    {
        if (valor >= 0)
            TotalEstimado -= valor;
    }
}