using Sample.Application.Abstractions.Messaging;

namespace Sample.Application.Members.Queries.GetMemberById;

public sealed record GetMemberByIdQuery(Guid Id) : IQuery<MemberResponse>;