using Sample.Application.Abstractions.Messaging;

namespace Sample.Application.Members.Commands.UpdateMember;

public sealed record UpdateMemberCommand(Guid Id, string FirstName, string LastName) : ICommand;