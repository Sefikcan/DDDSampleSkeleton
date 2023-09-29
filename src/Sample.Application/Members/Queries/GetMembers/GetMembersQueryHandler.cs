using Sample.Application.Abstractions.Messaging;
using Sample.Application.Members.Queries.GetMemberById;
using Sample.Domain.Entities;
using Sample.Domain.Errors;
using Sample.Domain.Repositories;
using Sample.Domain.Shared;

namespace Sample.Application.Members.Queries.GetMembers;

internal sealed class GetMembersQueryHandler : IQueryHandler<GetMembersQuery, List<MemberResponse>>
{
    private readonly IMemberRepository _memberRepository;

    public GetMembersQueryHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Result<List<MemberResponse>>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
    {
        List<Member> members = await _memberRepository.GetMembersAsync(cancellationToken);

        if (members.Count == 0)
        {
            return Result.Failure<List<MemberResponse>>(DomainErrors.Member.NotExist);
        }

        return members.Select(x => new MemberResponse(
            x.Id,
            x.Email.Value,
            x.FirstName.Value,
            x.LastName.Value
            )).ToList();
    }
}