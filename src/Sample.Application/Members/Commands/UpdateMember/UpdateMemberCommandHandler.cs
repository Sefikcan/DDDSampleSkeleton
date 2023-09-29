using Sample.Application.Abstractions.Messaging;
using Sample.Domain.Entities;
using Sample.Domain.Errors;
using Sample.Domain.Repositories;
using Sample.Domain.Shared;
using Sample.Domain.ValueObjects.Member;

namespace Sample.Application.Members.Commands.UpdateMember;

public sealed class UpdateMemberCommandHandler : ICommandHandler<UpdateMemberCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMemberCommandHandler(
        IUnitOfWork unitOfWork, 
        IMemberRepository memberRepository)
    {
        _unitOfWork = unitOfWork;
        _memberRepository = memberRepository;
    }

    public async Task<Result> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        Member? member = await _memberRepository.GetByIdAsync(request.Id, cancellationToken);
        if (member is null)
        {
            return Result.Failure(DomainErrors.Member.NotFound(request.Id));
        }

        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
        Result<LastName> lastNameResult = LastName.Create(request.LastName);
        
        member.ChangeName(firstNameResult.Value(), lastNameResult.Value());
        
        _memberRepository.Update(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}