using Flunt.Notifications;
using PayRight.Cadastro.Domain.Commands;
using PayRight.Cadastro.Domain.Entities;
using PayRight.Cadastro.Domain.Enums;
using PayRight.Cadastro.Domain.EventNotifications;
using PayRight.Cadastro.Domain.Repositories;
using PayRight.Cadastro.Domain.ValueObjects;
using PayRight.Shared.Commands;
using PayRight.Shared.Handlers;
using PayRight.Shared.Mediator;

namespace PayRight.Cadastro.Domain.Handlers;

public class CriarNovoUsuarioHandler : Notifiable<Notification>, IHandler<CriarNovoUsuarioCpfCommand>, IHandler<CriarNovoUsuarioCnpjCommand>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMediatorHandler _mediator;

    public CriarNovoUsuarioHandler(IUsuarioRepository usuarioRepository, IMediatorHandler mediator)
    {
        _usuarioRepository = usuarioRepository;
        _mediator = mediator;
    }

    public async Task<ICommandResult> Handle(CriarNovoUsuarioCpfCommand command, CancellationToken cancellationToken)
    {
        command.Validar();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Problemas nos dados informados para criar o usuario");
        }

        if (await _usuarioRepository.EmailExiste(command.EnderecoEmail))
            AddNotification("Email", "Endereco de e-mail informado ja esta em uso");
        
        if (await _usuarioRepository.DocumentoExiste(command.NumeroDocumento))
            AddNotification("Documento", "Documento informado ja esta em uso");

        var nomeCompleto = new NomeCompleto(command.PrimeiroNome, command.Sobrenome);
        var documento = new Documento(command.NumeroDocumento, TipoDocumento.CPF);
        var email = new Email(command.EnderecoEmail);

        var usuario = new Usuario(nomeCompleto, email, documento, command.Senha, command.ConfirmacaoSenha);
        
        AddNotifications(usuario);

        if (!IsValid)
            return new CommandResult(false, "Problemas nos dados informados para criar o usuario");
        
        await _usuarioRepository.CriarNovoUsuario(usuario);

        var retorno = await _usuarioRepository.Commit();

        if (!retorno) return new CommandResult(false, "Problemas para criar o usuario");
        
        await _mediator.PublicarEvento(new UsuarioCriadoNotification(usuario.Id, usuario.NomeCompleto.PrimeiroNome,
            usuario.NomeUsuario.Endereco));
        return new CommandResult(true, "Usuario criado com sucesso");

    }
    
    public async Task<ICommandResult> Handle(CriarNovoUsuarioCnpjCommand command, CancellationToken cancellationToken)
    {
        command.Validar();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Problemas nos dados informados para criar o usuario");
        }

        if (await _usuarioRepository.EmailExiste(command.EnderecoEmail))
            AddNotification("Email", "Endereco de e-mail informado ja esta em uso");
        
        if (await _usuarioRepository.DocumentoExiste(command.NumeroDocumento))
            AddNotification("Documento", "Documento informado ja esta em uso");

        var nomeCompleto = new NomeCompleto(command.PrimeiroNome, command.Sobrenome);
        var documento = new Documento(command.NumeroDocumento, TipoDocumento.CNPJ);
        var email = new Email(command.EnderecoEmail);

        var usuario = new Usuario(nomeCompleto, email, documento, command.Senha, command.ConfirmacaoSenha);
        
        AddNotifications(usuario);

        if (!IsValid)
            return new CommandResult(false, "Problemas nos dados informados para criar o usuario");
        
        await _usuarioRepository.CriarNovoUsuario(usuario);

        var retorno = await _usuarioRepository.Commit();
        
        return retorno
            ? new CommandResult(true, "Usuario criado com sucesso")
            : new CommandResult(false, "Problemas para criar o usuario");
    }
}