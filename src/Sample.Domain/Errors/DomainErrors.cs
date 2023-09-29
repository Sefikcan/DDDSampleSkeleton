using Sample.Domain.Shared;

namespace Sample.Domain.Errors;

public static class DomainErrors
{
    public static class Member
    {
        public static readonly Error EmailAlreadyInUse =
            new Error("Member.EmailAlreadyInUse", "The specified email is already in use");

        public static readonly Func<Guid, Error> NotFound = id => new Error(
            "Member.NotFound",
            $"The member with the identifier {id} was not found."
        );

        public static readonly Error NotExist = new Error(
            "Members.NotExist",
            "There is no members"
        );
    }
    
    public static class Email
    {
        public static readonly Error Empty = new Error("Email.Empty", "Email is empty");

        public static readonly Error InvalidFormat = new Error("Email.InvalidFormat", "Email format is invalid.");
    }
    
    public static class FirstName
    {
        public static readonly Error Empty = new Error("FirstName.Empty", "First name is empty");

        public static readonly Error TooLong = new Error("FirstName.TooLong", "First name is too long.");
    }
    
    public static class LastName
    {
        public static readonly Error Empty = new Error("LastName.Empty", "Last name is empty");
        
        public static readonly Error TooLong = new Error("LastName.TooLong", "Last name is too long.");
    }
    
    public static class Gathering
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            "Gathering.NotFound",
            $"The gathering with the identifier {id} was not found."
        );
    }
}