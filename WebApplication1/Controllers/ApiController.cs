using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        private ISender? _mediator;

        protected ISender Mediator => _mediator ??=
            HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
