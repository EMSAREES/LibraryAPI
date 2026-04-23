using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Books;

public sealed class BookAuthorCreatedEvent : BaseDomainEvent
{
    public BookAuthor BookAuthor { get; }

    public BookAuthorCreatedEvent(BookAuthor bookAuthor)
    {
        BookAuthor = bookAuthor;
    }
}
