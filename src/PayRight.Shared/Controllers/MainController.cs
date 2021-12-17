using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace PayRight.Shared.Controllers;

[ApiController]
[Produces("application/json")]
public abstract class MainController : ControllerBase
{
    protected MainController(IMapper mapper)
    {
        
    }
    
    // Todo: Finalizar MainController 
    
}