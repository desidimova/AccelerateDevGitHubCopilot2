using System.ComponentModel;

namespace Library.ApplicationCore.Enums;

public enum LoanExtensionStatus
{
    Success,
    LoanNotFound,
    LoanExpired,
    MembershipExpired,
    LoanReturned,
    Error
}