using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sample.Application.Gathering.Queries.GetGatheringById;
using Sample.Domain.Shared;
using Sample.Presentation.Abstractions;

namespace Sample.Presentation.Controllers;

[Route("api/v1/gatherings")]
public sealed class GatheringsController : ApiController
{
    public GatheringsController(ISender sender) : base(sender)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        Result<GatheringResponse> response = await Sender.Send(new GetGatheringByIdQuery(id), cancellationToken);

        return response.IsSuccess ? Ok(response.Value()) : NotFound(response.Error);
    }
}