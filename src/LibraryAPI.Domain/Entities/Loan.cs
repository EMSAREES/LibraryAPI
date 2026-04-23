using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Enums;
using LibraryAPI.Domain.Events.Loans;
using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Exceptions.Loans;

namespace LibraryAPI.Domain.Entities;

public sealed class Loan : BaseEntity
{
    public Guid UserId { get; private set; }
    public Guid BookId { get; private set; }
    public Guid BranchId { get; private set; }
    public Guid LoanedByUserId { get; private set; }
    public DateTime LoanDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? ReturnDate { get; private set; }
    public LoanStatus Status { get; private set; }
    public string? Notes { get; private set; }

    private Loan() { }

    private Loan(
        Guid userId,
        Guid bookId,
        Guid branchId,
        Guid loanedByUserId,
        DateTime loanDate,
        DateTime dueDate,
        string? notes)
    {
        UserId = userId;
        BookId = bookId;
        BranchId = branchId;
        LoanedByUserId = loanedByUserId;
        LoanDate = loanDate;
        DueDate = dueDate;
        Notes = notes;
        Status = LoanStatus.Active;

        CreatedByUserId = loanedByUserId;
    }

    public static Loan Create(
        Guid userId,
        Guid bookId,
        Guid branchId,
        Guid loanedByUserId,
        DateTime loanDate,
        DateTime dueDate,
        string? notes = null)
    {
        if (userId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.General.InvalidGuid)
            {
                FieldName = nameof(userId)
            };

        if (bookId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.General.InvalidGuid)
            {
                FieldName = nameof(bookId)
            };

        if (branchId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.General.InvalidGuid)
            {
                FieldName = nameof(branchId)
            };

        if (loanedByUserId == Guid.Empty)
            throw new DomainValidationException(DomainErrors.General.InvalidGuid)
            {
                FieldName = nameof(loanedByUserId)
            };

        if (dueDate < loanDate)
            throw new DomainValidationException(DomainErrors.General.InvalidDateRange)
            {
                FieldName = nameof(dueDate)
            };

        var loan = new Loan(userId, bookId, branchId, loanedByUserId, loanDate, dueDate, notes);
        loan.AddDomainEvent(new LoanCreatedEvent(loan));
        return loan;
    }

    public void Return(DateTime? returnedAt = null)
    {
        if (Status == LoanStatus.Returned)
            throw new LoanAlreadyReturnedException();

        if (Status == LoanStatus.Cancelled)
            throw new LoanCannotBeReturnedException();

        ReturnDate = returnedAt ?? DateTime.UtcNow;
        Status = LoanStatus.Returned;

        MarkAsUpdated();
        AddDomainEvent(new LoanReturnedEvent(this, ReturnDate.Value));
    }

    public void Cancel()
    {
        if (Status is LoanStatus.Returned or LoanStatus.Cancelled)
            throw new LoanCannotBeCancelledException();

        Status = LoanStatus.Cancelled;

        MarkAsUpdated();
        AddDomainEvent(new LoanCancelledEvent(this));
    }

    public void MarkAsOverdue(int overdueDays)
    {
        if (overdueDays <= 0)
            throw new DomainValidationException(DomainErrors.General.InvalidValue)
            {
                FieldName = nameof(overdueDays)
            };

        if (Status is LoanStatus.Returned or LoanStatus.Cancelled)
            return;

        Status = LoanStatus.Overdue;

        MarkAsUpdated();
        AddDomainEvent(new LoanOverdueEvent(this, overdueDays));
    }
}