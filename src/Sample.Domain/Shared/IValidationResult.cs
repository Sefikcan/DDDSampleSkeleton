namespace Sample.Domain.Shared;

public interface IValidationResult
{
    Error[] Errors { get; }

    public static readonly Error ValidationError = new Error("ValidationError", "A validation problem occured.");
}