namespace Sample.Presentation.Contracts.Member;

public sealed record RegisterMemberRequest(string Email, string FirstName, string LastName);