using LibraryAPI.Domain.Exceptions.Base;
using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.Exceptions.Books;

/// <summary>
/// Se lanza cuando se intenta prestar un libro que no tiene copias
/// disponibles en la sucursal solicitada.
/// HTTP → 409 Conflict.
/// </summary>
public sealed class BookNotAvailableException : DomainException
{
    public Guid BookId { get; }
    public Guid BranchId { get; }

    public BookNotAvailableException(Guid bookId, Guid branchId)
        : base("BOOK_NOT_AVAILABLE", DomainErrors.Loan.BookNotAvailable)
    {
        BookId = bookId;
        BranchId = branchId;
    }
}