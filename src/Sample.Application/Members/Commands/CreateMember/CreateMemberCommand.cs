using Sample.Application.Abstractions.Messaging;

namespace Sample.Application.Members.Commands.CreateMember;

public sealed record CreateMemberCommand(string Email, string FirstName, string LastName) : ICommand<Guid>;