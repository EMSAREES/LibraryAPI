using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Events.Books;

public sealed class BookCategoryCreatedEvent : BaseDomainEvent
{
    public BookCategory BookCategory { get; }

    public BookCategoryCreatedEvent(BookCategory bookCategory)
    {
        BookCategory = bookCategory;
    }
}
