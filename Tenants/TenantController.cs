using LearnMediatr.Tenants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnMediatr.Controllers;

[ApiController]
[Route("[controller]s")]
public class TenantController : ControllerBase
{
    private readonly IMediator _mediator;

    public TenantController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost()]
    public async Task<IActionResult> Create([FromBody] CreateTenantCommand request)
    {
        var res = await _mediator.Send(request);
        return res.Match<IActionResult>(t => Ok(t), e => BadRequest(e));
    }
}
