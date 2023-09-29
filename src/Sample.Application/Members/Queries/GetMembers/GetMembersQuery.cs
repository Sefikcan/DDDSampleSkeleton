using Sample.Application.Abstractions.Messaging;
using Sample.Application.Members.Queries.GetMemberById;

namespace Sample.Application.Members.Queries.GetMembers;

public sealed record GetMembersQuery : IQuery<List<MemberResponse>>;