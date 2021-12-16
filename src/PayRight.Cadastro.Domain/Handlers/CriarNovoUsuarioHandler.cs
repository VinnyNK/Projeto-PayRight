using Flunt.Notifications;
using PayRight.Cadastro.Domain.Commands;
using PayRight.Cadastro.Domain.Entities;
using PayRight.Cadastro.Domain.Enums;
using PayRight.Cadastro.Domain.Repositories;
using PayRight.Cadastro.Domain.ValueObjects;
using PayRight.Shared.Commands;
using PayRight.Shared.Handlers;

namespace PayRight.Cadastro.Domain.Handlers;

public class CriarNovoUsuarioHandler : Notifiable<Notification>, IHandler<CriarNovoUsuarioCpfCommand>, IHandler<CriarNovoUsuarioCnpjCommand>
{
    private readonly IUsuarioRepository _usuarioRepository;

    public CriarNovoUsuarioHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<ICommandResult> Handle(CriarNovoUsuarioCpfCommand command)
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
        
        return retorno
            ? new CommandResult(true, "Usuario criado com sucesso")
            : new CommandResult(false, "Problemas para criar o usuario");
    }
    
    public async Task<ICommandResult> Handle(CriarNovoUsuarioCnpjCommand command)
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