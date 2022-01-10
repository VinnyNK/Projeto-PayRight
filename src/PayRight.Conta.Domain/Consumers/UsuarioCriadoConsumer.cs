using Confluent.Kafka;
using Flunt.Notifications;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PayRight.Conta.Domain.Messages;
using PayRight.Conta.Domain.Services;
using PayRight.Shared.Consumers;

namespace PayRight.Conta.Domain.Consumers;

public class UsuarioCriadoConsumer : Consumer
{
    private readonly ICarteiraService _carteiraService;
    
    public UsuarioCriadoConsumer(KafkaConfiguration kafkaConfiguration, ILogger logger, ICarteiraService carteiraService) : base(kafkaConfiguration,  logger)
    {
        _carteiraService = carteiraService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(Config).Build();
            consumer.Subscribe(_kafkaConfiguration.Topics);

            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(stoppingToken);
                var message = consumeResult.Message.Value;
                var usuario = JsonConvert.DeserializeObject<UsuarioMessage>(message);
                
                usuario.Validar();

                if (usuario.IsValid)
                {
                    var resultado = await _carteiraService.CriarCarteiraNovoUsuarioCriado(usuario);

                    if (resultado)
                        _logger.LogInformation($"{message}");
                    else
                        _logger.LogWarning($"{message}");                    
                }
                else
                {
                    _logger.LogWarning($"Mensagem com problemas. Notificações: {ListarNotificacoes(usuario.Notifications)}");
                }
            }

            consumer.Close();
        }
        catch (OperationCanceledException e)
        {
            _logger.LogInformation("Operação cancelada");
        }
        catch (Exception e)
        {
            _logger.LogError($"Exceção: {e.GetType().FullName} Mensagem: {e.Message}");
        }
    }

    private string ListarNotificacoes(IEnumerable<Notification>? notifications)
    {
        return notifications!.Aggregate("", (current, notification) => current + $"{notification.Key}: {notification.Message} \n");
    }
}