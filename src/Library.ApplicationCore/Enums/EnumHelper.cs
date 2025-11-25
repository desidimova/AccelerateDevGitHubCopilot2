using Library.ApplicationCore.Enums;

namespace Library.ApplicationCore.Enums;

public static class EnumHelper
{
    private static readonly Dictionary<LoanExtensionStatus, string> _loanExtensionDescriptions = new()
    {
        { LoanExtensionStatus.Success, "Book loan extension was successful." },
        { LoanExtensionStatus.LoanNotFound, "Loan not found." },
        { LoanExtensionStatus.LoanExpired, "Cannot extend book loan as it already has expired. Return the book instead." },
        { LoanExtensionStatus.MembershipExpired, "Cannot extend book loan due to expired patron's membership." },
        { LoanExtensionStatus.LoanReturned, "Cannot extend book loan as the book is already returned." },
        { LoanExtensionStatus.Error, "Cannot extend book loan due to an error." }
    };

    private static readonly Dictionary<LoanReturnStatus, string> _loanReturnDescriptions = new()
    {
        { LoanReturnStatus.Success, "Book was successfully returned." },
        { LoanReturnStatus.LoanNotFound, "Loan not found." },
        { LoanReturnStatus.AlreadyReturned, "Cannot return book as the book is already returned." },
        { LoanReturnStatus.Error, "Cannot return book due to an error." }
    };

    private static readonly Dictionary<MembershipRenewalStatus, string> _membershipRenewalDescriptions = new()
    {
        { MembershipRenewalStatus.Success, "Membership renewal was successful." },
        { MembershipRenewalStatus.PatronNotFound, "Patron not found." },
        { MembershipRenewalStatus.TooEarlyToRenew, "It is too early to renew the membership." },
        { MembershipRenewalStatus.LoanNotReturned, "Cannot renew membership due to an outstanding loan." },
        { MembershipRenewalStatus.Error, "Cannot renew membership due to an error." }
    };

    // Generic method (maintains backward compatibility)
    public static string GetDescription(Enum value)
    {
        if (value == null)
            return string.Empty;
    
        if (value is LoanExtensionStatus loanExtensionStatus)
            return GetDescription(loanExtensionStatus);
        
        if (value is LoanReturnStatus loanReturnStatus)
            return GetDescription(loanReturnStatus);
        
        if (value is MembershipRenewalStatus membershipRenewalStatus)
            return GetDescription(membershipRenewalStatus);
        
        return value.ToString();
    }
    
    // Strongly-typed overloads
    public static string GetDescription(LoanExtensionStatus status)
    {
        if (_loanExtensionDescriptions.TryGetValue(status, out var description))
            return description;
        
        return status.ToString();
    }
    
    public static string GetDescription(LoanReturnStatus status)
    {
        if (_loanReturnDescriptions.TryGetValue(status, out var description))
            return description;
        
        return status.ToString();
    }
    
    public static string GetDescription(MembershipRenewalStatus status)
    {
        if (_membershipRenewalDescriptions.TryGetValue(status, out var description))
            return description;
        
        return status.ToString();
    }
}