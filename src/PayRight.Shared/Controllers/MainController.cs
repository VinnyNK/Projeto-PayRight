using AutoMapper;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using PayRight.Shared.Mediator;
using PayRight.Shared.Utils.Filters;

namespace PayRight.Shared.Controllers;

[ApiController]
[Produces("application/json")]
public abstract class MainController : ControllerBase
{
    protected readonly IMapper? Mapper;
    protected readonly IMediatorHandler? MediatorHandler;
    
    protected MainController(IMapper mapper, IMediatorHandler mediatorHandler)
    {
        Mapper = mapper;
        MediatorHandler = mediatorHandler;
    }
    
    protected MainController(IMapper mapper)
    {
        Mapper = mapper;
    }
    
    protected MainController()
    { }

    protected IActionResult RetornaErro(IReadOnlyCollection<Notification> notifications)
    {
        foreach (var notification in notifications)
        {
            ModelState.AddModelError(notification.Key, notification.Message);
        }

        return ValidationFilter.ControllerBadRequestResponse(ModelState);
    }
    
    // Todo: Finalizar MainController 
    
}