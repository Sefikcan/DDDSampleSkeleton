using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sample.Application.Members.Commands.CreateMember;
using Sample.Application.Members.Commands.UpdateMember;
using Sample.Application.Members.Queries.GetMemberById;
using Sample.Application.Members.Queries.GetMembers;
using Sample.Domain.Shared;
using Sample.Presentation.Abstractions;
using Sample.Presentation.Contracts.Member;

namespace Sample.Presentation.Controllers;

[Route("api/v1/members")]
public sealed class MembersController : ApiController
{
    public MembersController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetMembers(CancellationToken cancellationToken)
    {
        Result<List<MemberResponse>> response = await Sender.Send(new GetMembersQuery(), cancellationToken);

        return response.IsSuccess ? Ok(response.Value()) : NotFound(response.Error);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMemberById(Guid id, CancellationToken cancellationToken)
    {
        Result<MemberResponse> response = await Sender.Send(new GetMemberByIdQuery(id), cancellationToken);

        return response.IsSuccess ? Ok(response.Value()) : NotFound(response.Error);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateMember([FromRoute] Guid id, [FromBody] UpdateMemberRequest request,
        CancellationToken cancellationToken)
    {
        Result result = await Sender.Send(new UpdateMemberCommand(id, request.FirstName, request.LastName), cancellationToken);
        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterMember([FromBody] RegisterMemberRequest request, CancellationToken cancellationToken)
    {
        Result<Guid> result =
            await Sender.Send(new CreateMemberCommand(request.Email, request.FirstName, request.LastName),
                cancellationToken);
        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return CreatedAtAction(nameof(GetMemberById), new { id = result.Value() }, result.Value());
    }
}