using System.Security.Claims;
using AutoMapper;
using Flunt.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayRight.Shared.Mediator;
using PayRight.Shared.Utils.Filters;

namespace PayRight.Shared.Controllers;

[ApiController]
[Authorize]
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

    protected Guid? BuscaIdDoUsuarioAutenticado()
    {
        return Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier)?.Value, out var id) ? id : null;
    }

    protected string? BuscaEmailDoUsuarioAutenticado()
    {
        return HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    }
    
}