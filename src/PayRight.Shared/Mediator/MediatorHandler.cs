﻿using MediatR;
using PayRight.Shared.Commands;
using PayRight.Shared.EventNotifications;

namespace PayRight.Shared.Mediator;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ICommandResult> EnviarComando<T>(T comando) where T : ICommand
    {
        return await _mediator.Send(comando);
    }

    public async Task PublicarEvento<T>(T evento) where T : ICustomNotification
    {
        await _mediator.Publish(evento);
    }
}