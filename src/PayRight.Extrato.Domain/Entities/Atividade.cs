﻿using Flunt.Validations;
using PayRight.Shared.Utils.Extentions;
using PayRight.Extrato.Domain.Enums;
using PayRight.Extrato.Domain.ValueObjects;
using PayRight.Shared.Entities;

namespace PayRight.Extrato.Domain.Entities;

public class Atividade : Entity
{
    public Extrato? Extrato { get; private set; }
    
    public NomeAtividade NomeAtividade { get; private set; }

    public decimal Valor { get; private set; }

    public TipoAtividade TipoAtividade { get; }

    public bool Pago { get; private set; }

    public DateOnly? DataPagamento { get; private set; }

    public DateOnly EstimativaPagamento { get; private set; }

    public Atividade(string nome, string? descricao, decimal valor, TipoAtividade tipoAtividade, DateOnly estimativaPagamento)
    {
        NomeAtividade = new NomeAtividade(nome, descricao);
        Valor = valor;
        TipoAtividade = tipoAtividade;
        Pago = false;
        EstimativaPagamento = estimativaPagamento;

        AddNotifications(NomeAtividade);
        Validar();
    }
    
    protected Atividade() {}

    public void AdicionarExtrato(Extrato extrato)
    {
        Extrato = extrato;
    }

    public void AlterarNomeAtividade(string nome, string? descricao)
    {
        NomeAtividade = new NomeAtividade(nome, descricao);
    }

    public void PagarAtividade()
    {
        if (Pago || Extrato == null) return;
        Pago = true;
        DataPagamento = DateOnly.FromDateTime(DateTime.Today);
        Extrato.RealizaCalculoExtratoAtividadePaga(this);
    }

    public void RetornarPagamento()
    {
        if (Pago == false || Extrato == null) return;
        Pago = false;
        DataPagamento = null;
        Extrato.RealizaCalculoExtratoRetornoPagamento(this);
    }

    public void AlterarValorAtividade(decimal valor)
    {
        if (Extrato == null) return;
        Extrato.RemoveValorDaAtividadeNoExtratoEstimado(this);
        if (Pago)
            Extrato.RealizaCalculoExtratoRetornoPagamento(this);
        Valor = valor;
        Extrato.RealizaCalculoExtratoEstimado(this);
        if (Pago)
            Extrato.RealizaCalculoExtratoAtividadePaga(this);
        Validar();
    }

    public sealed override void Validar()
    {
        var hj = DateOnly.FromDateTime(DateTime.Today);
        AddNotifications(
            new Contract<Atividade>()
                .Requires()
                .IsGreaterOrEqualsThan(Valor, 0, "Valor", "Valor informado deve ser maior que zero")
                .IsGreaterOrEqualsThan(EstimativaPagamento, DateOnly.FromDateTime(DateTime.Today), "EstimativaPagamento", "Data da estimativa deve ser maior que o dia de hoje")
        );
    }
}