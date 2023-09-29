using FluentValidation;
using Sample.Domain.ValueObjects.Member;

namespace Sample.Application.Members.Commands.UpdateMember;

internal sealed class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
{
    public UpdateMemberCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(FirstName.MaxLength);

        RuleFor(x => x.LastName).NotEmpty().MaximumLength(LastName.MaxLength);
    }
}