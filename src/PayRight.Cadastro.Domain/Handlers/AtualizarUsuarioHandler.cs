using Flunt.Notifications;
using MediatR;
using PayRight.Cadastro.Domain.Commands;
using PayRight.Cadastro.Domain.Entities;
using PayRight.Cadastro.Domain.Repositories;
using PayRight.Cadastro.Domain.ValueObjects;
using PayRight.Shared.Commands;
using PayRight.Shared.Handlers;

namespace PayRight.Cadastro.Domain.Handlers;

public class AtualizarUsuarioHandler : Notifiable<Notification>, IHandler<AtualizarUsuarioCommand>
{
    private readonly IUsuarioRepository _usuarioRepository;

    public AtualizarUsuarioHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<ICommandResult> Handle(AtualizarUsuarioCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Problemas na validacao dos campos");
        }

        var usuario = await _usuarioRepository.BuscaUsuario(command.Id);
        if (usuario == null)
        {
            AddNotification("Usuario.Id", "Usuario informado nao existe");
            return new CommandResult(false, "Usuario nao existe");
        }
        
        AlteraNomeCompletoUsuario(usuario, command);

        await AlterarEmailUsuario(usuario, command);
        
        AddNotifications(usuario);
        
        if (!IsValid)
            return new CommandResult(false, "Houve problemas na validacao ");

        await _usuarioRepository.AtualizarUsuario(usuario);
        
        var retorno = await _usuarioRepository.Commit();
        
        return retorno
            ? new CommandResult(true, "Usuario atualizado com sucesso")
            : new CommandResult(false, "Problemas para atualizar o usuario");
    }

    private void AlteraNomeCompletoUsuario(Usuario usuario, AtualizarUsuarioCommand command)
    {
        if (string.IsNullOrEmpty(command.PrimeiroNome) && string.IsNullOrEmpty(command.Sobrenome)) return;
        var primeiroNome = command.PrimeiroNome ?? usuario.NomeCompleto.PrimeiroNome;
        var sobrenome = command.Sobrenome ?? usuario.NomeCompleto.Sobrenome;
        usuario.AlterarNomeCompleto(new NomeCompleto(primeiroNome, sobrenome));
    }

    private async Task AlterarEmailUsuario(Usuario usuario, AtualizarUsuarioCommand command)
    {
        if (string.IsNullOrEmpty(command.EnderecoEmail) || usuario.NomeUsuario.Endereco == command.EnderecoEmail) return;

        if (await _usuarioRepository.EmailExiste(command.EnderecoEmail))
            AddNotification("Email", "Email informado ja existe");
        else
            usuario.AlterarEmail(new Email(command.EnderecoEmail));
    }
}