using Sample.Application.Abstractions.Messaging;
using Sample.Domain.Entities;
using Sample.Domain.Errors;
using Sample.Domain.Repositories;
using Sample.Domain.Shared;

namespace Sample.Application.Members.Queries.GetMemberById;

internal sealed class GetMemberByIdQueryHandler : IQueryHandler<GetMemberByIdQuery, MemberResponse>
{
    private readonly IMemberRepository _memberRepository;

    public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Result<MemberResponse>> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        Member? member = await _memberRepository.GetByIdAsync(request.Id, cancellationToken);
        if (member is null)
        {
            return Result.Failure<MemberResponse>(DomainErrors.Member.NotFound(request.Id));
        }

        return new MemberResponse(member.Id, member.Email.Value, member.FirstName.Value, member.LastName.Value);
    }
}